using System.Text.RegularExpressions;

namespace AdventOfCode;

public partial class Day10
{
    private readonly List<string> _inputLines = [];
    
        public Day10()
        {
            string? line;
            while ((line = Console.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) break;
                _inputLines.Add(line);
            }
        }
        
        public long Part1()
        {
            return _inputLines.Select(SolveHelper).Where(minPresses => minPresses != -1).Aggregate<int, long>(0, (current, minPresses) => current + minPresses);
        }

        private int SolveHelper(string line)
        {

            var diagramMatch = MyRegex().Match(line);
            if (!diagramMatch.Success) return -1;
            var pattern = diagramMatch.Groups[1].Value;
            var rows = pattern.Length;

            var buttonMatches = Regex.Matches(line, @"\(([\d,]+)\)");
            var cols = buttonMatches.Count;
            var matrix = new int[rows, cols + 1];
            
            for (var c = 0; c < cols; c++)
            {
                var indices = buttonMatches[c].Groups[1].Value
                    .Split(',')
                    .Select(int.Parse);

                foreach (var idx in indices)
                {
                    if (idx < rows) matrix[idx, c] = 1;
                }
            }
            
            for (var r = 0; r < rows; r++)
            {
                matrix[r, cols] = pattern[r] == '#' ? 1 : 0;
            }

            // Gaussian Elimination (GF2)
            var pivotRow = 0;
            var pivotCols = new List<int>();

            for (var c = 0; c < cols && pivotRow < rows; c++)
            {
                var sel = -1;
                for (var r = pivotRow; r < rows; r++)
                {
                    if (matrix[r, c] != 1) continue;
                    sel = r;
                    break;
                }
                
                if (sel == -1) continue;
                
                for (var k = c; k <= cols; k++)
                {
                    (matrix[pivotRow, k], matrix[sel, k]) = (matrix[sel, k], matrix[pivotRow, k]);
                }
                for (var r = 0; r < rows; r++)
                {
                    if (r == pivotRow || matrix[r, c] != 1) continue;
                    for (var k = c; k <= cols; k++)
                    {
                        matrix[r, k] ^= matrix[pivotRow, k];
                    }
                }

                pivotCols.Add(c);
                pivotRow++;
            }
            
            for (var r = pivotRow; r < rows; r++)
            {
                if (matrix[r, cols] == 1) return -1;
            }
            
            var freeVars = Enumerable.Range(0, cols).Except(pivotCols).ToList();

            var minWeight = int.MaxValue;
            var combinations = 1L << freeVars.Count;

            for (long i = 0; i < combinations; i++)
            {
                var solution = new int[cols];
                var currentWeight = 0;

                for (var bit = 0; bit < freeVars.Count; bit++)
                {
                    if (((i >> bit) & 1) != 1) continue;
                    solution[freeVars[bit]] = 1;
                    currentWeight++;
                }


                for (var pIdx = 0; pIdx < pivotCols.Count; pIdx++)
                {
                    var r = pIdx;
                    var c = pivotCols[pIdx];

                    var val = matrix[r, cols];

                    val = freeVars.Where(fv => matrix[r, fv] == 1 && solution[fv] == 1).Aggregate(val, (current, fv) => current ^ 1);

                    solution[c] = val;
                    if (val == 1) currentWeight++;
                }

                if (currentWeight < minWeight)
                {
                    minWeight = currentWeight;
                }
            }

            return minWeight;
        }

    [GeneratedRegex(@"\[([.#]+)\]")]
    private static partial Regex MyRegex();
}