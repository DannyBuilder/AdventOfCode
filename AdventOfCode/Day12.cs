using System.Text;

namespace AdventOfCode;

public class Day12
{
    public Day12()
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "input.txt");
        var input = ParseInput(path);

        var shapeInput = input.Take(30);
        var packInput = input.Skip(30);

        List<Shape> shapes = [];

        var shapeIndex = 0;
        while (shapeInput.Any())
        {
            shapes.Add(new Shape(shapeIndex++, shapeInput.Take(5)));
            shapeInput = shapeInput.Skip(5);
        }

        PackInstruction[] packInstructions =
        [
            .. packInput
                .Select(line => line.Split(' '))
                .Select(parts =>
                {
                    var areaParts = string.Join("", parts.First().SkipLast(1)).Split('x');
                    var requiredShapesPart = parts.Skip(1);

                    (int x, int y) size = (x: int.Parse(areaParts[0]), int.Parse(areaParts[1]));
                    int[] requiredShapes = [.. requiredShapesPart.Select(int.Parse)];
                    return new PackInstruction(size, requiredShapes);
                }),
        ];

        var areasThatFit = packInstructions.Where(instruction =>
        {
            var area = instruction.Size.X * instruction.Size.Y;
            int[] shapeAreas =
            [
                .. instruction.RequiredShapes.Keys.Select(i =>
                    shapes[i].Area * instruction.RequiredShapes[i]
                ),
            ];
            return shapeAreas.Sum() < area;
        });

        Console.WriteLine("Day 12 - " + areasThatFit.Count());
    }

    private class Shape(int index, IEnumerable<string> input)
    {
        public int Index { get; } = index;

        public int Area => _contents.SelectMany(row => row).Count(b => b);

        private readonly bool[][] _contents =
        [
            .. input
                .Skip(1)
                .TakeWhile(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Select(c => c == '#').ToArray()),
        ];

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var t in _contents)
            {
                for (var x = 0; x < _contents[0].Length; x++)
                {
                    sb.Append(t[x] ? '#' : '.');
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }

    private class PackInstruction((int X, int Y) size, int[] requiredShapes)
    {
        public (int X, int Y) Size { get; } = size;
        public Dictionary<int, int> RequiredShapes { get; } =
            requiredShapes
                .Select((n, i) => (n, i))
                .ToDictionary(nAndIndex => nAndIndex.i, nAndIndex => nAndIndex.n);
    }

    static List<string> ParseInput(string path) => [.. File.ReadLines(path)];
}