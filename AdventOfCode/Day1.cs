namespace AdventOfCode
{

    public class Day1
    {
        List<string> allLines = new List<string>();
        string line;

        public Day1()
        {
            while (!string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                allLines.Add(line);
            }
        }

        public int part1()
        {
            int curr = 50;
            int answ = 0;
            foreach (string s in allLines)
            {
                if (s[0] == 'R')
                {
                    curr += int.Parse(s.Substring(1));
                    curr = curr % 100;
                }
                else
                {
                    curr -= int.Parse(s.Substring(1));
                    curr = curr % 100;
                }

                if (curr == 0)
                    answ++;
            }

            return answ;
        }

        public int part2()
        {
            int curr = 50;
            int answ = 0;
            foreach (string s in allLines)
            {
                int amount = int.Parse(s.Substring(1));

                if (s[0] == 'R')
                {
                    answ += (curr + amount) / 100;
                    curr = (curr + amount) % 100;
                }
                else
                {
                    int distToZero = (curr == 0) ? 100 : curr;

                    if (amount >= distToZero)
                    {
                        answ += 1 + (amount - distToZero) / 100;
                    }

                    curr = (curr - amount) % 100;
                    if (curr < 0) curr += 100;
                }

            }

            return answ;

        }
    }
}