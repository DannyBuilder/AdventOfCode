namespace AdventOfCode;

public class Day7
{
    private readonly List<int[]> _input = [];
    public Day7()
    {
        string? line;
        while (!string.IsNullOrEmpty(line = Console.ReadLine()))
        {
            var digits = line.Select(c => c switch
            {
                'S' => 1,
                '^' => 2,
                _   => 0
            }).ToArray();

            _input.Add(digits);
        }
    }

    public long Part1()
    {
        var answ = 0;
        for (var i = 1; i < _input.Count; i++)
        {
            for (var j = 0; j < _input[i].Length; j++)
            {
                if(_input[i-1][j] == 0 || _input[i-1][j] == 2) continue;
                if (_input[i][j] == 2)
                {
                    _input[i][j + 1] = 1;
                    _input[i][j - 1] = 1;
                    answ++;
                }
                else if (_input[i][j] == 0)
                {
                    _input[i][j] = 1;
                }
            }
        }
        
        return answ;
    }
    
    public long Part2()
    {
        var counts = _input.Select(row => new long[row.Length]).ToList();
        for (var j = 0; j < _input[0].Length; j++)
        {
            if (_input[0][j] == 1) 
            {
                counts[0][j] = 1;
            }
        }
        
        for (var i = 1; i < _input.Count; i++)
        {
            for (var j = 0; j < _input[i].Length; j++)
            {
                var incomingTimelines = counts[i - 1][j];

                if (incomingTimelines == 0) continue;
                
                if (_input[i][j] == 2)
                {
                    if (j - 1 >= 0)
                        counts[i][j - 1] += incomingTimelines;
                
                    if (j + 1 < _input[i].Length)
                        counts[i][j + 1] += incomingTimelines;
                }
                else
                {
                    counts[i][j] += incomingTimelines;
                }
            }
        }
        return counts[^1].Sum();
    }
}