namespace AdventOfCode;

public class Day9
{
    private readonly  List<(int x, int y)> _input = [];
    public Day9()
    {
        string? line;
        while (!string.IsNullOrEmpty(line = Console.ReadLine()))
        {
            var tokens = line.Split(',');
            _input.Add((int.Parse(tokens[0]), int.Parse(tokens[1])));
        }
    }

    public long Part1()
    {
        long answ = 0;

        for (var i = 0; i < _input.Count; i++)
        {
            for (var j = i + 1; j < _input.Count; j++)
            {
                long width = Math.Abs(_input[i].Item1 - _input[j].Item1) + 1;
                long height = Math.Abs(_input[i].Item2 - _input[j].Item2) + 1;

                answ = Math.Max(answ, width * height);
            }
        }

        return answ;
    }
    
    public long Part2()
    {
        long maxArea = 0;
        int n = _input.Count;

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                var p1 = _input[i];
                var p2 = _input[j];

                long width = Math.Abs(p1.x - p2.x) + 1;
                long height = Math.Abs(p1.y - p2.y) + 1;
                long area = width * height;

                if (area <= maxArea) continue;

                if (IsValidRectangle(p1, p2))
                {
                    maxArea = area;
                }
            }
        }

        return maxArea;
    }

    private bool IsValidRectangle((int x, int y) p1, (int x, int y) p2)
    {
        var rMinX = Math.Min(p1.x, p2.x);
        var rMaxX = Math.Max(p1.x, p2.x);
        var rMinY = Math.Min(p1.y, p2.y);
        var rMaxY = Math.Max(p1.y, p2.y);

        var n = _input.Count;
        var insideRayCrossings = 0;

        for (var k = 0; k < n; k++)
        {
            var e1 = _input[k];
            var e2 = _input[(k + 1) % n];

            var isVertical = e1.x == e2.x;

            if (isVertical)
            {
                if (e1.x > rMinX && e1.x < rMaxX)
                {
                    var edgeYMin = Math.Min(e1.y, e2.y);
                    var edgeYMax = Math.Max(e1.y, e2.y);

                    if (Math.Max(edgeYMin, rMinY + 1) <= Math.Min(edgeYMax, rMaxY - 1))
                        return false; 
                }

                if (e1.x <= rMinX) continue;
                var spansY = (e1.y > rMinY) != (e2.y > rMinY);
                if (spansY)
                {
                    insideRayCrossings++;
                }
            }
            else
            {
                if (e1.y <= rMinY || e1.y >= rMaxY) continue;
                var edgeXMin = Math.Min(e1.x, e2.x);
                var edgeXMax = Math.Max(e1.x, e2.x);

                if (Math.Max(edgeXMin, rMinX + 1) <= Math.Min(edgeXMax, rMaxX - 1))
                    return false;
            }
        }

        return (insideRayCrossings % 2 != 0);
    }
}