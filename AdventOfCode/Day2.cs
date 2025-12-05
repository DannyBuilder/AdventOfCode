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

        public long part1()
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
    }
}