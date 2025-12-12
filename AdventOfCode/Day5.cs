namespace AdventOfCode;

public class Day5
{
    private readonly List<long> _input = [];
    private readonly List<(long Start, long End)> _ranges = [];
    
    public Day5()
    {
        string? line;
        while (!string.IsNullOrEmpty(line = Console.ReadLine()))
        {
            var parts = line.Split('-');
            var start = long.Parse(parts[0]);
            var end = long.Parse(parts[1]);
            _ranges.Add((start, end));
        }
        string? line1;
        while (!string.IsNullOrEmpty(line1 = Console.ReadLine()))
        {
            _input.Add(long.Parse(line1));
        }
        _ranges.Sort((a, b) => a.Start.CompareTo(b.Start));
    }

    public int Part1()
    {
        var answer = 0;
        foreach (var i in _input)
        {
            foreach (var j in _ranges)
            {
                if (j.Start > i)
                    break;
                if (i >= j.Start && i <= j.End)
                {
                    answer++;
                    break;
                }
            }
        }
        return answer;
        /*
         * This can be simplified by a one liner that the IDE suggests:  return input.Count(i => ranges.TakeWhile(j => j.Start <= i).Any(j => i >= j.Start && i <= j.End));
         */
    }

    public long Part2()
    {
        long answer = 0;
        var currentStart = _ranges[0].Start;
        var currentEnd = _ranges[0].End;
        for (var i = 1; i < _ranges.Count; i++)
        {
            var nextRange = _ranges[i];
            if (nextRange.Start <= currentEnd + 1) 
            {
                currentEnd = Math.Max(currentEnd, nextRange.End);
            }
            else
            {
                answer += (currentEnd - currentStart + 1);
                currentStart = nextRange.Start;
                currentEnd = nextRange.End;
            }
        }
        answer += (currentEnd - currentStart + 1);
        return answer;
    }


}