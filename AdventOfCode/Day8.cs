namespace AdventOfCode;

public class Point(int x, int y, int z)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public int Z { get; set; } = z;
}

public class Day8
{
    private readonly List<Point> _points = [];
    public Day8()
    {
        string? line;
        while (!string.IsNullOrEmpty(line = Console.ReadLine()))
        {
            _points.Add(new Point(int.Parse(line.Split(",")[0]),  int.Parse(line.Split(",")[1]),  int.Parse(line.Split(",")[2])));
        }
    }

    private long Distance(Point p1, Point p2)
    {
        long dx = p1.X - p2.X;
        long dy = p1.Y - p2.Y;
        long dz = p1.Z - p2.Z;
        return (dx * dx) + (dy * dy) + (dz * dz);
    }
    

    public long Part1(){
        var pairs = new List<(long DistSq, int IdA, int IdB)>();
        var n = _points.Count;

        for (var i = 0; i < n; i++)
        {
            for (var j = i + 1; j < n; j++)
            {
                pairs.Add((Distance(_points[i], _points[j]), i, j));
            }
        }
        
        pairs.Sort((a, b) => a.DistSq.CompareTo(b.DistSq));
        
        var parent = Enumerable.Range(0, n).ToArray();
        var size = Enumerable.Repeat(1, n).ToArray();


        for (var k = 0; k < 1000; k++)
        {
            Union(pairs[k].IdA, pairs[k].IdB);
        }
        
        var finalSizes = new List<long>();
        var seenRoots = new HashSet<int>();

        for (var i = 0; i < n; i++)
        {
            var root = Find(i);
            if (seenRoots.Add(root))
            {
                finalSizes.Add(size[root]);
            }
        }
        
        var top3 = finalSizes.OrderByDescending(x => x).Take(3).ToList();
        

        var result = top3[0] * top3[1] * top3[2];
        return result;

        int Find(int i)
        {
            if (parent[i] != i)
                parent[i] = Find(parent[i]);
            return parent[i];
        }

        void Union(int i, int j)
        {
            var rootA = Find(i);
            var rootB = Find(j);

            if (rootA == rootB) return;
            if (size[rootA] < size[rootB])
            {
                parent[rootA] = rootB;
                size[rootB] += size[rootA];
            }
            else
            {
                parent[rootB] = rootA;
                size[rootA] += size[rootB];
            }
        }
    }
}