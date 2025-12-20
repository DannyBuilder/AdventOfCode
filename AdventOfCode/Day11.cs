namespace AdventOfCode;

public class Day11
{
    private static readonly Dictionary<string, List<string>> _graph = new Dictionary<string, List<string>>();
    private static readonly Dictionary<string, long> _memo = new Dictionary<string, long>();

    public Day11()
    {
        string? line;
        while (!string.IsNullOrEmpty(line = Console.ReadLine()))
        {
            var parts = line.Split(':');
            var from = parts[0].Trim();
            var targets = parts[1].Trim().Split(' ').ToList();
            
            _graph[from] = targets;
        }
    }

    public long Part1(string current)
    {
        if (current == "out")
            return 1;
        if (_memo.TryGetValue(current, out var part1))
        {
            return part1;
        }
        if (!_graph.TryGetValue(current, out List<string>? value))
        {
            return 0;
        }
        var pathCount = value.Sum(Part1);

        _memo[current] = pathCount;
        return pathCount;
    }
}