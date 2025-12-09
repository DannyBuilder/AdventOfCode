namespace AdventOfCode;

public class Day4
{
    private readonly List<string> _allLines = [];
    private int[,] _dp;
    
    public Day4()
    {
        string? line;
        while (!string.IsNullOrEmpty(line = Console.ReadLine()))
        {
            _allLines.Add(line);
        }

        _dp = new int[_allLines.Count, _allLines[0].Length];
        for(var i =0; i < _allLines.Count; i++){
            for (var j = 0; j < _allLines[i].Length; j++)
            {
                if(_allLines[i][j] == '.')
                {
                    _dp[i, j] = 0;
                }
                else
                {
                    _dp[i, j] = 1;
                }
            }
        }
    }

    public int Part1()
    {
        var answ = 0;
        for(var i =0; i < _dp.GetLength(0); i++){
            for (var j = 0; j < _dp.GetLength(1); j++)
            {
                if (_dp[i, j] == 1)
                {
                    if (Check(i, j))
                    {
                        answ++;
                    }
                }
            }
        }

        return answ;
    }
    
    private bool Check(int r, int c)
    {
        var neighborCount = 0;

        int[] dRow = [-1, -1, -1,  0, 0,  1, 1, 1];
        int[] dCol = [-1,  0,  1, -1, 1, -1, 0, 1];

        for (var k = 0; k < 8; k++)
        {
            var newRow = r + dRow[k];
            var newCol = c + dCol[k];
            
            if (newRow >= 0 && newRow < _dp.GetLength(0) && 
                newCol >= 0 && newCol < _dp.GetLength(1))
            {
                if (_dp[newRow, newCol] == 1)
                {
                    neighborCount++;
                }
            }
        }
        return neighborCount < 4;
    }

    public int Part2()
    {
        var totalRemoved = 0;
        bool anyRemoved;

        do 
        {
            anyRemoved = false;
            var toRemove = new List<(int r, int c)>();

            for (var i = 0; i < _dp.GetLength(0); i++)
            {
                for (var j = 0; j < _dp.GetLength(1); j++)
                {
                    if (_dp[i, j] == 1 && Check(i, j))
                    {
                        toRemove.Add((i, j));
                    }
                }
            }
            if (toRemove.Count > 0)
            {
                totalRemoved += toRemove.Count;
                anyRemoved = true;
                foreach (var (r, c) in toRemove)
                {
                    _dp[r, c] = 0;
                }
            }

        } while (anyRemoved);

        return totalRemoved;
    }
}