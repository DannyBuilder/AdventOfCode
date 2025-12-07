namespace AdventOfCode
{
    public class Day2
    {
        private readonly string[]? _input;

        public Day2()
        {
            var line = Console.ReadLine();
            _input = line?.Split(',');
        }

        public long Part1()
        {
            long answ = 0;
            if (_input == null) return answ;
            foreach (var t in _input)
            {
                for (var j = long.Parse(t.Split('-')[0]); j <= long.Parse(t.Split('-')[1]); j++)
                {
                    var s = j.ToString();
                    if (s[..(s.Length / 2)].Equals(s[(s.Length / 2)..]))
                    {
                        answ += j;
                    }
                }
            }

            return answ;
        }

        public long Part2()
        {
            
            var ranges = new List<(long Start, long End)>();
            long globalMax = 0;

            if (_input != null)
                foreach (var line in _input)
                {
                    var parts = line.Split('-');
                    var s = long.Parse(parts[0]);
                    var e = long.Parse(parts[1]);
                    ranges.Add((s, e));
                    if (e > globalMax) globalMax = e;
                }

            var foundIds = new HashSet<long>();
            var maxDigits = globalMax.ToString().Length;
            
            for (var L = 2; L <= maxDigits; L++)
            {
                for (var l = 1; l <= L / 2; l++)
                {
                    if (L % l != 0) continue;
                    var k = L / l;
                    long multiplier = 0;
                    long blockValue = 1;
                    var shift = (long)Math.Pow(10, l);

                    for (var i = 0; i < k; i++)
                    {
                        multiplier += blockValue;
                        if (i < k - 1) blockValue *= shift;
                    }
                        
                    var minSeedLimit = (long)Math.Pow(10, l - 1);
                    var maxSeedLimit = (long)Math.Pow(10, l) - 1;

                    foreach (var range in ranges)
                    {
                        var minSeedCalc = (long)Math.Ceiling((double)range.Start / multiplier);
                        var maxSeedCalc = (long)Math.Floor((double)range.End / multiplier);
                        var startSeed = Math.Max(minSeedLimit, minSeedCalc);
                        var endSeed = Math.Min(maxSeedLimit, maxSeedCalc);

                        if (startSeed > endSeed) continue;
                        for (var seed = startSeed; seed <= endSeed; seed++)
                        {
                            foundIds.Add(seed * multiplier);
                        }
                    }
                }
            }

            return foundIds.Sum();
            
        }
    }
}