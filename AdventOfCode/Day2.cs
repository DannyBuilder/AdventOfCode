namespace AdventOfCode
{
    public class Day2
    {
        private string[] input;

        public Day2()
        {
            string line = Console.ReadLine();
            input = line.Split(',');
        }

        public long Part1()
        {
            long answ = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (long j = long.Parse(input[i].Split('-')[0]); j <= long.Parse(input[i].Split('-')[1]); j++)
                {
                    string s = j.ToString();
                    if (s.Substring(0, (s.Length / 2)).Equals(s.Substring(s.Length / 2)))
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

            foreach (var line in input)
            {
                var parts = line.Split('-');
                long s = long.Parse(parts[0]);
                long e = long.Parse(parts[1]);
                ranges.Add((s, e));
                if (e > globalMax) globalMax = e;
            }
            
            var foundIds = new HashSet<long>();
            int maxDigits = globalMax.ToString().Length;
            
            for (int L = 2; L <= maxDigits; L++)
            {
                for (int l = 1; l <= L / 2; l++)
                {
                    if (L % l == 0)
                    {
                        int k = L / l;
                        long multiplier = 0;
                        long blockValue = 1;
                        long shift = (long)Math.Pow(10, l);

                        for (int i = 0; i < k; i++)
                        {
                            multiplier += blockValue;
                            if (i < k - 1) blockValue *= shift;
                        }
                        
                        long minSeedLimit = (long)Math.Pow(10, l - 1);
                        long maxSeedLimit = (long)Math.Pow(10, l) - 1;

                        foreach (var range in ranges)
                        {
                            long minSeedCalc = (long)Math.Ceiling((double)range.Start / multiplier);
                            long maxSeedCalc = (long)Math.Floor((double)range.End / multiplier);
                            long startSeed = Math.Max(minSeedLimit, minSeedCalc);
                            long endSeed = Math.Min(maxSeedLimit, maxSeedCalc);

                            if (startSeed <= endSeed)
                            {
                                for (long seed = startSeed; seed <= endSeed; seed++)
                                {
                                    foundIds.Add(seed * multiplier);
                                }
                            }
                        }
                    }
                }
            }

            return foundIds.Sum();
            
        }
    }
}