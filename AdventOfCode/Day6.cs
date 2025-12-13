namespace AdventOfCode;

public class Day6
{
    private readonly List<string[]> _input = [];
    private readonly List<string> _rawLines = [];

    public Day6()
    {
        string? line;
        
        while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
        {
            _rawLines.Add(line);
            _input?.Add(line.Split((char[])null!, StringSplitOptions.RemoveEmptyEntries));
        }

        if (_rawLines.Count <= 0) return;
        var maxWidth = _rawLines.Max(x => x.Length);
        for (var i = 0; i < _rawLines.Count; i++)
        {
            _rawLines[i] = _rawLines[i].PadRight(maxWidth);
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

    public long Part2()
    {
        var width = _rawLines[0].Length;
        var grandTotal = 0L;
        
        List<string> currentBlock = [];

        for (var x = 0; x < width; x++)
        {
            var colChars = new char[_rawLines.Count];
            var isSeparator = true;

            for (var y = 0; y < _rawLines.Count; y++)
            {
                colChars[y] = _rawLines[y][x];
                if (colChars[y] != ' ') isSeparator = false;
            }
            
            if (isSeparator)
            {
                if (currentBlock.Count <= 0) continue;
                grandTotal += SolveVerticalBlock(currentBlock);
                currentBlock.Clear();
            }
            else
            {
                currentBlock.Add(new string(colChars));
            }
        }
        
        if (currentBlock.Count > 0)
        {
            grandTotal += SolveVerticalBlock(currentBlock);
        }

        return grandTotal;
    }
    private long SolveVerticalBlock(List<string> cols)
    {

        List<long> nums = [];
        var op = "+";

        foreach (var colStr in cols)
        {
            var bottomChar = colStr[^1];
            if (bottomChar is '+' or '*')
            {
                op = bottomChar.ToString();
            }
            
            var numberStr = new string(colStr[..^1].Where(char.IsDigit).ToArray());
            
            if (long.TryParse(numberStr, out var val))
            {
                nums.Add(val);
            }
        }

        return op switch
        {
            "+" => nums.Sum(),
            "*" => nums.Aggregate((a, b) => a * b),
            _ => 0
        };
    }
}