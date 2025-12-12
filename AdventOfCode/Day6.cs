namespace AdventOfCode;

public class Day6
{
    private readonly List<string[]> _input = [];

    public Day6()
    {
        string? line;
        
        while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
        {
            _input?.Add(line.Split((char[])null!, StringSplitOptions.RemoveEmptyEntries));
        }
    }

    public long Part1()
    {
        var width = _input[0].Length;
        var result = 0L;

        for (var x = 0; x < width; x++)
        {
            var col = _input.Select(row => row[x]).ToArray();
            var op = col[^1];
            var nums = col[..^1].Select(long.Parse);

            result += op switch
            {
                "+" => nums.Sum(),
                "*" => nums.Aggregate((a, b) => a * b),
                _ => 0
            };
        }

        return result;
    }
}