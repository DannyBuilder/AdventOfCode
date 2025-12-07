namespace AdventOfCode
{
    public class Day1
    {
        private readonly List<string> _allLines = [];

        public Day1()
        {
            string? line;
            while (!string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                _allLines.Add(line);
            }
        }

        public int Part1()
        {
            var currentPos = 50;
            var answer = 0;

            foreach (var line in _allLines)
            {
                var direction = line[0];
                var amount = int.Parse(line.Substring(1));

                if (direction == 'R')
                {
                    currentPos += amount;
                }
                else
                {
                    currentPos -= amount;
                }

                currentPos = CleanModulo(currentPos, 100);

                if (currentPos == 0)
                {
                    answer++;
                }
            }

            return answer;
        }

        public int Part2()
        {
            var currentPos = 50;
            var answer = 0;

            foreach (var line in _allLines)
            {
                var direction = line[0];
                var amount = int.Parse(line[1..]);

                if (direction == 'R')
                {
                    answer += (currentPos + amount) / 100;
                    currentPos = (currentPos + amount) % 100;
                }
                else
                {
                    var distToZero = (currentPos == 0) ? 100 : currentPos;

                    if (amount >= distToZero)
                    {
                        answer += 1 + (amount - distToZero) / 100;
                    }

                    currentPos = (currentPos - amount) % 100;
                    
                    if (currentPos < 0) currentPos += 100;
                }
            }

            return answer;
        }

        private static int CleanModulo(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}