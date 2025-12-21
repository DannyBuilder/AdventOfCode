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
    
    public long Part2()
    {
        return _inputLines.Sum(SolveMachine);
    }

        private long SolveMachine(string line)
        {
            var buttonMatches = MyRegex1().Matches(line);
            var buttons = buttonMatches.Cast<Match>()
                .Select(m => m.Groups[1].Value.Split(',').Select(int.Parse).ToList())
                .ToList();

            var joltageMatch = Regex.Match(line, @"\{([\d,]+)\}");
            var targetJoltages = joltageMatch.Groups[1].Value.Split(',').Select(int.Parse).ToList();

            var numCounters = targetJoltages.Count;
            
            var parityDict = new Dictionary<string, List<int[]>>();
            var valueDict = new Dictionary<int[], List<int>>();

            var numButtons = buttons.Count;
            var totalCombos = 1 << numButtons; // 2^numButtons

            for (var i = 0; i < totalCombos; i++)
            {
                var combo = new int[numButtons];
                var joltageIncreases = Enumerable.Repeat(0, numCounters).ToList();

                for (var b = 0; b < numButtons; b++)
                {
                    if (((i >> b) & 1) != 1) continue;
                    combo[b] = 1;
                    foreach (var counterIdx in buttons[b].Where(counterIdx => counterIdx < numCounters))
                    {
                        joltageIncreases[counterIdx]++;
                    }
                }

                var parityKey = string.Join(",", joltageIncreases.Select(x => x % 2));
                if (!parityDict.ContainsKey(parityKey)) parityDict[parityKey] = new List<int[]>();
                
                parityDict[parityKey].Add(combo);
                valueDict[combo] = joltageIncreases;
            }
            
            var memo = new Dictionary<string, long>();
            return MinPresses(targetJoltages, parityDict, valueDict, memo);
        }

        private long MinPresses(List<int> currentJoltages, 
                               Dictionary<string, List<int[]>> parityDict, 
                               Dictionary<int[], List<int>> valueDict,
                               Dictionary<string, long> memo)
        {
            if (currentJoltages.All(x => x == 0)) return 0;
            if (currentJoltages.Any(x => x < 0)) return int.MaxValue;

            var memoKey = string.Join(",", currentJoltages);
            if (memo.TryGetValue(memoKey, out var presses)) return presses;

            long minResult = int.MaxValue;
            var currentParity = string.Join(",", currentJoltages.Select(x => x % 2));

            if (parityDict.TryGetValue(currentParity, out var validCombos))
            {
                minResult = (from combo in validCombos let joltageIncreases = valueDict[combo] let nextJoltages = currentJoltages.Select((t, i) => (t - joltageIncreases[i]) / 2).ToList() let pressesInThisStep = combo.Sum() select pressesInThisStep + 2 * MinPresses(nextJoltages, parityDict, valueDict, memo)).Prepend(minResult).Min();
            }

            memo[memoKey] = minResult;
            return minResult;
        }

    [GeneratedRegex(@"\(([\d,]+)\)")]
    private static partial Regex MyRegex1();
}
