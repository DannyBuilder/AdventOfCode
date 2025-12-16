namespace AdventOfCode;

public class Day9
{
    private readonly  List<(int, int)> _input = [];
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
}