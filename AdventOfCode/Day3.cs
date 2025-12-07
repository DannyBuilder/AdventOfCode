namespace AdventOfCode;

public class Day3
{
    private readonly List<string> _allLines = [];

    public Day3()
    {
        string? line;
        while (!string.IsNullOrEmpty(line = Console.ReadLine()))
        {
            _allLines.Add(line);
        }
    }

    public long Part1()
    {
        var result = 0;
        foreach (var line in _allLines)
        {
            var first = 0;
            var second = 0;
            for (var i = 0; i < line.Length-1; i++)
            {
                if (line[i] - '0' > first)
                {
                    first = line[i] - '0';
                    second = 0;
                }
                else if (line[i] - '0' > second)
                {
                    second = line[i] - '0';
                }
            }

            if (line[^1] - '0' > second)
                second = line[^1] - '0';
            
            result += first * 10 + second;
        }
        return result;
    }

    public long Part2()
    {
        long total = 0;
        const int targetLength = 12;

        foreach (var line in _allLines)
        {
            var dropsAllowed = line.Length - targetLength;
            var stack = new List<char>();

            foreach (var digit in line)
            {
                while (stack.Count > 0 && 
                       digit > stack[^1] && 
                       dropsAllowed > 0)
                {
                    stack.RemoveAt(stack.Count - 1);
                    dropsAllowed--;
                }
                    
                stack.Add(digit);
            }
            
            var resultString = new string(stack.Take(targetLength).ToArray());
                
            total += long.Parse(resultString);
        }

        return total;
    }
}
    