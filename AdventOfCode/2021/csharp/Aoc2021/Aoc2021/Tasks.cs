using Dumpify;
using NUnit.Framework;

namespace Aoc2021;
// Note: can do dotnet watch test --filter day8_1 to run specific test in terminal with hot reloading
[TestFixture]
public class Tasks
{
    private const string inputPath = @"C:\repos\AdventOfCode\AdventOfCode\2021\csharp\Aoc2021\Aoc2021\inputs\";

    [Test]
    public void day3_1()
    {
        var i = File.ReadAllText(inputPath + "/day3.txt")
        .Trim()
        .Split("\n")
        .ToList();

        var maxs = new List<int>();
        var mins = new List<int>();
        for (int x = 0; x < i[0].Length; x++)
        {
            var counter0 = 0;
            var counter1 = 0;


            for (int y = 0; y < i.Count; y++)
            {
                if (int.Parse(i[y][x].ToString()) == 1)
                {
                    counter1 += 1;
                }
                else
                {
                    counter0 += 1;
                }
            }
            if (counter1 > counter0)
            {
                maxs.Add(1);
                mins.Add(0);
            }
            else
            {
                mins.Add(1);
                maxs.Add(0);
            }
        }

        var mind = 0;
        for (int m = 0; m < mins.Count; m++)
        {
            mind += mins[m] * Helpers.ConvertToDecimal(mins); ;
        }

        var maxd = 0;
        for (int m = 0; m < maxs.Count; m++)
        {
            maxd += maxs[m] * Helpers.ConvertToDecimal(maxs);
        }

        Console.WriteLine(mind * maxd);
    }

    [Test]
    public void day3_2()
    {
        var numbers = File.ReadAllText(inputPath + "/day3.txt")
            .Trim()
            .Split("\n")
            .ToList();


        // CO2 scrubber rating
        var filteredMins = numbers.Select(x => x).ToList();

        // oxygen generator rating
        var filteredMaxes = numbers.Select(x => x).ToList();

        var finalMax = "";
        for (int i = 0; i < numbers[0].Length; i++)
        {
            var maxesDict = filteredMaxes
            .GroupBy(x => x[i])
            .ToDictionary(x => x.Key, x => x.ToList());

            if (maxesDict.First().Value.Count == maxesDict.Last().Value.Count)
            {
                filteredMaxes = maxesDict['1'];
            }
            else
            {
                filteredMaxes = maxesDict
                .MaxBy(x => x.Value.Count)
                .Value;
            }

            if (filteredMaxes.Count == 1)
            {
                finalMax = filteredMaxes[0];
                break;
            }
        }

        var finalMin = "";
        for (int i = 0; i < numbers[0].Length; i++)
        {
            var minsDict = filteredMins
            .GroupBy(x => x[i])
            .ToDictionary(x => x.Key, x => x.ToList());

            if (minsDict.First().Value.Count == minsDict.Last().Value.Count)
            {
                filteredMins = minsDict['0'];
            }
            else
            {
                filteredMins = minsDict
                .MinBy(x => x.Value.Count)
                .Value;
            }

            if (filteredMins.Count == 1)
            {
                finalMin = filteredMins[0];
                break;
            }
        }
        Console.WriteLine(Helpers.ConvertToDecimal(finalMax.Select(x => int.Parse(x.ToString())).ToList()) * Helpers.ConvertToDecimal(finalMin.Select(x => int.Parse(x.ToString())).ToList()));
    }

    [Test]
    public void day4_1_2()
    {
        var text = File.ReadAllText(inputPath + "/day4.txt").Trim();

        var splits = text.Replace("\r\n", "\n").Split("\n\n");
        var bingoCalls = splits[0].Trim();
        var bingoCardSplits = splits[1..]
            .Select(x => x.Replace("  ", " ").Trim());

        var bingoCards = bingoCardSplits
            .Select(x => x.Split('\n')
                .Select(x => x.Trim().Split(' ')
                    .Select(x => new ValueTuple<int, bool>(int.Parse(x), false))
                    .ToList())
                .ToList())
            .ToList()
            .Select(x => new BingoCard(x)).ToList();

        BingoCard lastCardRemaining = null;
        var firstOnefound = false;
        foreach (var call in bingoCalls.Split(',').Select(int.Parse))
        {
            foreach (var bc in bingoCards)
            {
                var hasWons = bingoCards.Where(x => x.HasWon);
                if (hasWons.Count() == bingoCards.Count - 1)
                {
                    var remaining = bingoCards.Except(hasWons);
                    lastCardRemaining = remaining.Single();
                }

                var marked = bc.MarkItem(call);

                if (!marked)
                {
                    continue;
                }

                var bingo = bc.CheckForWin();
                if (bingo && !firstOnefound)
                {
                    firstOnefound = true;
                    Console.WriteLine($"PART1: {bc.SumOfUnmarkedNumbers() * call}");
                }

                if (bingo && bc == lastCardRemaining)
                {
                    Console.WriteLine($"PART2: {bc.SumOfUnmarkedNumbers() * call}");
                    return;
                }
            }
        }
    }

    [Test]
    public void day4_1_grid()
    {
        var text = File.ReadAllText("./inputs/day4.txt").Trim();

        var splits = text.Replace("\r\n", "\n").Split("\n\n");
        var bingoCalls = splits[0].Trim();
        var bingoCardSplits = splits[1..]
            .Select(x => x.Replace("  ", " ").Trim());

        var cards = bingoCardSplits
            .Select(x => x.Split('\n')
                .Select(x => x.Trim().Split(' ')
                    .Select(int.Parse)
                    .ToList())
                .ToList())
            .ToList()
            .Select(x => new GridWithOtherVal<bool>(x, false)).ToList();

        foreach (var call in bingoCalls.Split(',').Select(int.Parse))
        {
            foreach (var card in cards)
            {
                var coord = card.FirstOrDefault(x => x.val == call);
                if (coord != null)
                {
                    card[coord] = new GridItem<bool>(card[coord].val, true);
                    card.SetOtherVal(coord, true);
                }

                var bingo = card.GetAllColumns()
                    .Concat(card.GetAllRows())
                    .Any(x => x.All(c => c.otherVal));

                if (bingo)
                {
                    (call * card
                        .WhereWithCoord((gi, coord) => !gi.otherVal)
                        .Select(x => x.gridItem.val)
                        .Sum())
                        .Dump();
                }
            }
        }
    }


    [Test]
    public void day4_2_grid()
    {
        var text = File.ReadAllText("./inputs/day4.txt").Trim();

        var splits = text.Replace("\r\n", "\n").Split("\n\n");
        var bingoCalls = splits[0].Trim();
        var bingoCardSplits = splits[1..]
            .Select(x => x.Replace("  ", " ").Trim());

        var cards = bingoCardSplits
            .Select(x => x.Split('\n')
                .Select(x => x.Trim().Split(' ')
                    .Select(int.Parse)
                    .ToList())
                .ToList())
            .ToList()
            .Select(x => new GridWithOtherVal<bool>(x, false)).ToList();

        foreach (var call in bingoCalls.Split(',').Select(int.Parse))
        {
            foreach (var card in cards)
            {
                if (card.Complete)
                {
                    continue;
                }

                var coord = card.FirstOrDefault(x => x.val == call);
                if (coord != null)
                {
                    card.SetOtherVal(coord, true);
                }

                var bingo = card.GetAllColumns()
                    .Concat(card.GetAllRows())
                    .Any(x => x.All(c => c.otherVal));

                if (bingo)
                {
                    card.Complete = true;
                    (call * card
                            .WhereWithCoord((gi, coord) => !gi.otherVal)
                            .Select(x => x.gridItem.val)
                            .Sum())
                        .Dump();
                }
            }
        }
    }

    [Test]
    public async Task day5_1_2()
    {
        var textLines = File.ReadAllText(inputPath + "/day5.txt")
             .Trim()
             .Split("\n")
             .ToList();


        var lines = textLines.Select(l =>
        {
            var (start, end) = l.Trim().Split(" -> ");
            var (sx, sy) = start.Split(',').Select(int.Parse).ToList();
            var (ex, ey) = end.Split(',').Select(int.Parse).ToList();
            return new CoordPair(sx, sy, ex, ey);
        })//.Where(c => c.startX == c.endX || c.startY == c.endY) // uncomment for part 1
        .ToList();

        var pointCounter = new Dictionary<(int, int), int>();

        foreach (var line in lines)
        {
            // vert
            if (line.endY != line.startY && line.startX == line.endX)
            {
                var start = Math.Min(line.startY, line.endY);
                var end = Math.Max(line.startY, line.endY);

                for (var y = start; y <= end; y++)
                {
                    var point = new ValueTuple<int, int>(line.startX, y);
                    pointCounter[point] = pointCounter.GetValueOrDefault(point, 0) + 1;
                }
            }

            // horizontal
            if (line.startX != line.endX && line.startY == line.endY)
            {
                var start = Math.Min(line.startX, line.endX);
                var end = Math.Max(line.startX, line.endX);

                for (var x = start; x <= end; x++)
                {
                    var point = new ValueTuple<int, int>(x, line.startY);
                    pointCounter[point] = pointCounter.GetValueOrDefault(point, 0) + 1;
                }
            }

            // edge case 0 lenght line
            if (line.startY == line.endY && line.startX == line.endX)
            {
                var point = new ValueTuple<int, int>(line.startX, line.startY);
                pointCounter[point] = pointCounter.GetValueOrDefault(point, 0) + 1;
            }

            // for part 2 diags
            if (line.startX != line.endX && line.startY != line.endY)
            {
                var xDiff = Math.Abs(line.endX - line.startX) + 1;

                var (x1, x2, y1, y2) = (line.startX, line.endX, line.startY, line.endY);
                var yPos = y1 < y2;
                var xPos = x1 < x2;

                var diagPoints = Enumerable.Range(x1, xDiff).Select(_ => (xPos ? x1++ : x1--, yPos ? y1++ : y1--));

                foreach (var (x, y) in diagPoints)
                {
                    var point = new ValueTuple<int, int>(x, y);
                    pointCounter[point] = pointCounter.GetValueOrDefault(point, 0) + 1;
                }
            }
        }

        var result = pointCounter.Count(p => p.Value >= 2);
        result.Dump();
    }

    [Test]
    public void day6_1()
    {
        var timers = File.ReadAllText(inputPath + "/day6.txt")
                         .Trim()
                         .Split(",")
                         .Select(int.Parse)
                         .ToList();

        var resetDay = 6;
        var newFishResetDay = 8;
        var days = 80;

        for (var i = 0; i < days; i++)
        {
            var newFishCount = 0;
            for (var f = 0; f < timers.Count; f++)
            {
                timers[f] -= 1;
                if (timers[f] < 0)
                {
                    timers[f] = resetDay;
                    newFishCount++;
                }
            }
            timers.AddRange(Enumerable.Repeat(newFishResetDay, newFishCount));
        }

        timers.Count.Dump();
    }

    [Test]
    public void day6_2()
    {
        var initialTimers = File.ReadAllText(inputPath + "/day6.txt")
                         .Trim()
                         .Split(",")
                         .Select(int.Parse)
                         .ToList();

        var resetDay = 6;
        var newFishResetDay = 8;
        var days = 256;

        var timers = Enumerable.Range(0, 9).ToDictionary(x => x, x => (long)0);
        initialTimers.ForEach(x => timers[x] += 1);

        for (var i = 0; i < days; i++)
        {
            var zeroCount = timers[0];
            for (var f = 0; f < 8; f++)
            {
                timers[f] = timers[f + 1];
            }
            timers[resetDay] += zeroCount;
            timers[newFishResetDay] = zeroCount;
        }

        timers.Select(x => x.Value).Sum().Dump();
    }

    [Test]
    public void day6_2_ListInsteadOfDict()
    {
        var resetDay = 6;
        var newFishResetDay = 9;
        var days = 256;
        var fish = File.ReadAllLines(inputPath + "/day6.txt")
            .SelectMany(input =>
                input.Split(',')
                .Select(int.Parse))
            .GroupBy(f => f)
            .Aggregate(new List<long>(new long[newFishResetDay]), (list, kv) =>
            {
                list[kv.Key] = kv.Count();
                return list;
            });

        for (var i = 0; i < days; i++)
        {
            var zeroCount = fish.First();
            fish.RemoveAt(0);
            fish[resetDay] += zeroCount;
            fish.Add(zeroCount);
        }

        fish.Sum().Dump();
    }

    [Test]
    public void day7_1()
    {
        var positions = File.ReadAllText("./inputs/day7.txt")
                         .Trim()
                         .Split(",")
                         .Select(int.Parse)
                         .ToList();

        var minFuelcost = int.MaxValue;
        foreach (var pos in positions)
        {
            var fuel = positions.Select(p => Math.Abs(p - pos)).Sum();
            minFuelcost = Math.Min(minFuelcost, fuel);
        }

        minFuelcost.Dump();
    }

    [Test]
    public void day7_2()
    {
        var positions = File.ReadAllText("./inputs/day7.txt")
                         .Trim()
                         .Split(",")
                         .Select(int.Parse)
                         .ToList();

        var minFuelcost = long.MaxValue;
        foreach (var pos in Enumerable.Range(0, positions.Max()))
        {
            var currentFuel = positions.Sum(p => Enumerable.Range(0, Math.Abs(p - pos) + 1).Sum(x => 1 * x));
            minFuelcost = Math.Min(minFuelcost, currentFuel);
        }

        minFuelcost.Dump();
    }

    [Test]
    public void day7_2_TwoLiner()
    {
        var allPositions = File.ReadAllText("./inputs/day7.txt")
                         .Trim()
                         .Split(",")
                         .Select(int.Parse)
                         .ToList();
        Enumerable.Range(0, allPositions.Max())
            .Select(pos => allPositions.Sum(p => Enumerable.Range(0, Math.Abs(p - pos) + 1).Sum(x => 1 * x)))
            .Min()
            .Dump();
    }

    [Test]
    public void day7_2_TwoLiner2()
    {

        var positions = File.ReadAllText("./inputs/day7.txt")
            .Trim()
            .Split(",")
            .Select(int.Parse)
            .ToList();


        Enumerable.Range(0, positions.Max())
            .Select(pos => positions
                .Sum(p => Enumerable.Range(0, Math.Abs(p - pos) + 1)
                    .Sum(x => 1 * x)))
            .Aggregate(long.MaxValue, (winner, current) => Math.Min(winner, current))
            .Dump();
    }


    [Test]
    public void day8_1()
    {
        File.ReadAllText(inputPath + "/day8.txt")
                         .Trim()
                         .Split("\n")
                         .Select(x => x.Split('|')[1].Trim().Split(' '))
                         .SelectMany(x => x)
                         .Count(x => new List<int> { 2, 4, 3, 7 }.Contains(x.Length))
                         .Dump();
    }

    [Test]
    public void day8_2()
    {
        File.ReadAllText(inputPath + "/day8.txt")
                 .Trim()
                 .Split("\n")
                 .Select(line =>
                 {
                     var (s, o) = line.Split('|');
                     var signals = s.Trim().Split(' ').Select(x => string.Join("", x.OrderBy(x => x)));
                     var lineOut = o.Trim().Split(' ').Select(x => string.Join("", x.OrderBy(x => x)));
                     var numberMatches = new List<string> { "", "", "", "", "", "", "", "", "", "" };
                     numberMatches[1] = signals.Single(x => x.Count() == 2);
                     numberMatches[4] = signals.Single(x => x.Count() == 4);
                     numberMatches[7] = signals.Single(x => x.Count() == 3);
                     numberMatches[8] = signals.Single(x => x.Count() == 7);

                     numberMatches[9] = signals.Single(x => x.Count() == 6 && x.Intersect(numberMatches[4]).Count() == 4);
                     numberMatches[9] = signals.Single(x => x.Count() == 6 && x.Intersect(numberMatches[4]).Count() == 4);
                     numberMatches[6] = signals.Single(x => x.Count() == 6 && x.Intersect(numberMatches[4]).Count() == 3 && x.Intersect(numberMatches[1]).Count() == 1);
                     numberMatches[0] = signals.Single(x => x.Count() == 6 && x.Intersect(numberMatches[4]).Count() == 3 && x.Intersect(numberMatches[1]).Count() == 2);

                     numberMatches[3] = signals.Single(x => x.Count() == 5 && x.Intersect(numberMatches[1]).Count() == 2);
                     numberMatches[5] = signals.Single(x => x.Count() == 5 && x.Intersect(numberMatches[4]).Count() == 3 && x.Intersect(numberMatches[1]).Count() != 2);
                     numberMatches[2] = signals.Single(x => x.Count() == 5 && x.Intersect(numberMatches[4]).Count() == 2);

                     return long.Parse(string.Join("", (lineOut.Select(x => numberMatches.IndexOf(x)))));
                 })
                 .Sum()
                 .Dump();
    }

    /*
 Note can do dotnet watch test --filter day8_1 to run specific test in terminal with hot reloading
 */

    [Test]
    public void day9_1()
    {
        var grid = File.ReadAllText("./inputs/day9.txt")
         .Trim()
         .Replace("\r\n", "\n")
         .Split("\n")
         .Select(x => x.Select(x => int.Parse(x.ToString())).ToList())
         .ToList();

        var total = 0;
        for (var x = 0; x < grid.Count; x++)
        {
            for (var y = 0; y < grid[0].Count; y++)
            {
                var val = grid[x][y];
                if (new List<(int, int)> { new(0, 1), new(0, -1), new(1, 0), new(-1, 0) }
                .Where(e => e.Item1 + x >= 0 && e.Item1 + x < grid.Count && e.Item2 + y >= 0 && e.Item2 + y < grid[0].Count)
                .All(e => val < grid[x + e.Item1][y + e.Item2]))
                {
                    total += (1 + val);
                }
            }
        }

        total.Dump();
    }

    [Test]
    public void day9_1_grid()
    {
        var g = File.ReadAllText("./inputs/day9.txt")
            .Trim()
            .Replace("\r\n", "\n")
            .Split("\n")
            .Select(x => x.Select(x => int.Parse(x.ToString())).ToList())
            .ToList();

        var grid = new Grid(g);
        var total = 0;
        grid.ForEachWithCoord((val, coord) =>
        {
            if (grid.GetValidAdjacentNoDiag(coord).All(a => grid[a] > val))
            {
                total += (1 + val);
            }
        });

        total.Dump();
    }


    [Test]
    public void day9_1_Linqier()
    {
        var grid = File.ReadAllText("./inputs/day9.txt")
         .Trim()
         .Replace("\r\n", "\n")
         .Split("\n")
         .Select(x => x.Select(x => int.Parse(x.ToString())).ToList())
         .ToList();

        Enumerable.Range(0, grid.Count)
            .SelectMany(x => Enumerable.Range(0, grid[x].Count)
                .Select(y => new List<(int, int)> { new(0, 1), new(0, -1), new(1, 0), new(-1, 0) }
                    .Where(e => e.Item1 + x >= 0 && e.Item1 + x < grid.Count && e.Item2 + y >= 0 && e.Item2 + y < grid[0].Count)
                    .All(e => grid[x][y] < grid[x + e.Item1][y + e.Item2]) ? 1 + grid[x][y] : 0))
            .Sum()
            .Dump();
    }

    [Test]
    public void day9_2()
    {
        var grid = File.ReadAllText("./inputs/day9.txt")
         .Trim()
         .Replace("\r\n", "\n")
         .Split("\n")
         .Select(x => x.Select(x => int.Parse(x.ToString())).ToList())
         .ToList();

        // adapted part 1
        var basins = Enumerable.Range(0, grid.Count)
            .SelectMany(x => Enumerable.Range(0, grid[x].Count)
                .Select(y => new List<(int, int)> { new(0, 1), new(0, -1), new(1, 0), new(-1, 0) }
                    .Where(e => e.Item1 + x >= 0 && e.Item1 + x < grid.Count && e.Item2 + y >= 0 && e.Item2 + y < grid[0].Count)
                    .All(e => grid[x][y] < grid[x + e.Item1][y + e.Item2])
                        ? new ValueTuple<int, int>(x, y)
                        : new ValueTuple<int, int>(-100, -100)))
            .Where(x => x.Item1 != -100 && x.Item2 != -100)
            .ToList();

        var basinTotals = new List<int>();
        foreach (var (bx, by) in basins)
        {
            // BFS and just don't add 9s
            var visited = new HashSet<(int, int)>();
            var tail = new Queue<(int, int)>();
            tail.Enqueue(new(bx, by));

            while (tail.Count > 0)
            {
                var (cx, cy) = tail.Dequeue();
                if (visited.Contains(new(cx, cy)))
                {
                    continue;
                }
                visited.Add(new(cx, cy));

                new List<(int, int)> { new(0, 1), new(0, -1), new(1, 0), new(-1, 0) }
                .Where(e => e.Item1 + cx >= 0 && e.Item1 + cx < grid.Count && e.Item2 + cy >= 0 && e.Item2 + cy < grid[0].Count)
                .Where(e => grid[cx + e.Item1][cy + e.Item2] != 9)
                .ToList()
                .ForEach(e =>
                {
                    tail.Enqueue(new(cx + e.Item1, cy + e.Item2));
                });
            }
            basinTotals.Add(visited.Count);
        }

        basinTotals
            .OrderByDescending(x => x)
            .Take(3)
            .Aggregate(1, (total, cur) => total * cur)
            .Dump();
    }

    [Test]
    public void day9_2_grid()
    {
        var g = File.ReadAllText("./inputs/day9.txt")
            .Trim()
            .Replace("\r\n", "\n")
            .Split("\n")
            .Select(x => x.Select(x => int.Parse(x.ToString())).ToList())
            .ToList();

        var grid = new Grid(g);

        var basinCoords = grid.WhereWithCoord((v, coord) => grid.GetValidAdjacentNoDiag(coord).All(a => grid[a] > v))
            .Select(x => new Coord(x.Coord.x, x.Coord.y))
            .ToList();

        var basinTotals = new List<int>();
        foreach (var bc in basinCoords)
        {
            // BFS and just don't add 9s
            var visited = new HashSet<Coord>();
            var tail = new Queue<Coord>();
            tail.Enqueue(bc);

            while (tail.Count > 0)
            {
                var currentCoord = tail.Dequeue();
                if (visited.Contains(currentCoord))
                {
                    continue;
                }
                visited.Add(currentCoord);

                grid.GetValidAdjacentNoDiag(currentCoord)
                    .Where(adj => grid[adj] != 9)
                    .ToList()
                    .ForEach(tail.Enqueue);
            }
            basinTotals.Add(visited.Count);
        }

        basinTotals
            .OrderByDescending(x => x)
            .Take(3)
            .Aggregate(1, (total, cur) => total * cur)
            .Dump();
    }

    [Test]
    public void day10_1()
    {
        var lines = File.ReadAllText("./inputs/day10.txt")
            .Trim()
            .Split("\n")
            .Select(x => x.ToList())
            .ToList();

        var scores = new Dictionary<char, long>()
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 }
        };

        var matches = new Dictionary<char, char>()
        {
            { ')', '(' },
            { ']', '[' },
            { '}' , '{'},
            { '>' , '<'}
        };

        var invalidChars = new List<char>();
        foreach (var line in lines)
        {
            var stack = new Stack<char>();
            foreach (var letter in line)
            {
                //  first invalid character
                if (matches.Values.Contains(letter))
                {
                    stack.Push(letter);
                }
                else
                {
                    if (stack.Peek() == matches[letter])
                    {
                        stack.Pop();
                    }
                    else
                    {
                        invalidChars.Add(letter);
                        break;
                    }
                }
            }
        }

        invalidChars.Sum(x => scores[x]).Dump();
    }

    [Test]
    public void day10_1_Linqier()
    {
        var scores = new Dictionary<char, long>()
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 }
        };

        var matches = new Dictionary<char, char>()
        {
            { '(', ')' },
            { '[', ']' },
            { '{' , '}'},
            { '<' , '>'}
        };

        File.ReadAllText("./inputs/day10.txt")
            .Trim()
            .Split("\n")
            .Select(x => x.ToList())
            .Select(line =>
            {
                var stack = new Stack<char>();
                foreach (var letter in line)
                {
                    if (matches.Keys.Contains(letter))
                    {
                        stack.Push(letter);
                    }
                    else
                    {
                        if (matches[stack.Peek()] == letter)
                        {
                            stack.Pop();
                        }
                        else
                        {
                            return scores[letter];
                        }
                    }
                }

                return 0;
            })
            .Sum()
            .Dump();
    }

    [Test]
    public void day10_2()
    {
        var scores = new Dictionary<char, long>()
        {
            { ')', 1 },
            { ']', 2 },
            { '}', 3 },
            { '>', 4 }
        };

        var matches = new Dictionary<char, char>()
        {
            { '(', ')' },
            { '[', ']' },
            { '{' , '}'},
            { '<' , '>'}
        };

        File
           .ReadAllText(
                        "./inputs/day10.txt")
           .Trim()
           .Split("\n")
           .Select(x => x.ToList())
           .Where(line => !this.IsCorrupted(line))
           .Select(line =>
           {
               var stack = new Stack<char>();
               foreach (var letter in line)
               {
                   if (matches.ContainsKey(letter))
                   {
                       stack.Push(letter);
                   }
                   else
                   {
                       stack.Pop();
                   }
               }

               return stack.Aggregate((long)0, (score, letter) => score * 5 + scores[matches[letter]]);
           })
           .OrderBy(x => x)
           .Median()
           .Dump();
    }

    [Test]
    public void day11_1()
    {
        var grid = File
            .ReadAllText(
                "./inputs/day11.txt")
            .Trim()
            .Split("\n")
            .Select(x => x.ToCharArray().Select(x => int.Parse(x.ToString())).ToList())
            .ToList();

        var totalFlashes = 0;

        for (var step = 0; step < 100; step++)
        {
            var q = new Queue<(int, int)>();
            var visited = new HashSet<(int, int)>();
            // increase all by one, if it's >9 reset to 0

            // add now 0 coords to queue
            for (var x = 0; x < grid.Count; x++)
            {
                for (var y = 0; y < grid[0].Count; y++)
                {
                    grid[x][y] += 1;
                    if (grid[x][y] > 9)
                    {
                        grid[x][y] = 0;
                        q.Enqueue(new(x, y));

                    }
                }
            }

            while (q.Any())
            {
                var (cx, cy) = q.Dequeue();

                if (visited.Contains(new(cx, cy)))
                {
                    continue;
                }

                visited.Add(new(cx, cy));
                totalFlashes += 1;
                var directions = new List<(int, int)>
                {
                    new(0, 1), new(0, -1), new(1, 0), new(-1, 0),  new(1, 1), new(1, -1), new(-1, 1), new(-1, -1)
                };

                directions
                    .Where(e => e.Item1 + cx >= 0 && e.Item1 + cx < grid.Count && e.Item2 + cy >= 0 && e.Item2 + cy < grid[0].Count)
                    .ToList()
                    .ForEach(dir =>
                    {
                        if (grid[cx + dir.Item1][cy + dir.Item2] != 0)
                        {
                            grid[cx + dir.Item1][cy + dir.Item2] += 1;
                        }

                        if (grid[cx + dir.Item1][cy + dir.Item2] > 9)
                        {
                            grid[cx + dir.Item1][cy + dir.Item2] = 0;
                            q.Enqueue(new(cx + dir.Item1, cy + dir.Item2));
                        }
                    });
            }

            $"after {step + 1} steps:".Dump();
            grid.PrintGrid();
            totalFlashes.Dump();
        }

        totalFlashes.Dump();
    }

    [Test]
    public void day11_2()
    {
        var grid = File
            .ReadAllText(
                "./inputs/day11.txt")
            .Trim()
            .Replace("\r\n", "\n") // for working with example
            .Split("\n")
            .Select(x => x.ToCharArray().Select(x => int.Parse(x.ToString())).ToList())
            .ToList();

        for (var step = 0; step < 10000; step++)
        {
            var q = new Queue<(int, int)>();
            var visited = new HashSet<(int, int)>();
            // increase all by one, if it's >9 reset to 0

            // add now 0 coords to queue
            for (var x = 0; x < grid.Count; x++)
            {
                for (var y = 0; y < grid[0].Count; y++)
                {
                    grid[x][y] += 1;
                    if (grid[x][y] > 9)
                    {
                        grid[x][y] = 0;
                        q.Enqueue(new(x, y));

                    }
                }
            }

            while (q.Any())
            {
                var (cx, cy) = q.Dequeue();

                if (visited.Contains(new(cx, cy)))
                {
                    continue;
                }

                visited.Add(new(cx, cy));
                var directions = new List<(int, int)>
                {
                    new(0, 1), new(0, -1), new(1, 0), new(-1, 0),  new(1, 1), new(1, -1), new(-1, 1), new(-1, -1)
                };

                directions
                    .Where(e => e.Item1 + cx >= 0 && e.Item1 + cx < grid.Count && e.Item2 + cy >= 0 && e.Item2 + cy < grid[0].Count)
                    .ToList()
                    .ForEach(dir =>
                    {
                        if (grid[cx + dir.Item1][cy + dir.Item2] != 0)
                        {
                            grid[cx + dir.Item1][cy + dir.Item2] += 1;
                        }

                        if (grid[cx + dir.Item1][cy + dir.Item2] > 9)
                        {
                            grid[cx + dir.Item1][cy + dir.Item2] = 0;
                            q.Enqueue(new(cx + dir.Item1, cy + dir.Item2));
                        }
                    });
            }

            if (grid.SelectMany(x => x).All(x => x == 0))
            {
                $"Sync'd at step {step + 1}".Dump(); // offset because my steps start at 0
                break;
            }
        }
    }

    [Test]
    public void day11_1_grid()
    {
        var g = File
            .ReadAllText(
                "./inputs/day11.txt")
            .Trim()
            .Split("\n")
            .Select(x => x.ToCharArray().Select(x => int.Parse(x.ToString())).ToList())
            .ToList();

        var grid = new Grid(g);
        var totalFlashes = 0;
        for (int step = 1; step <= 100; step++)
        {
            var q = new Queue<Coord>();
            var visited = new HashSet<Coord>();
            // increase all by one, if it's >9 reset to 0
            grid.Print();
            grid = grid.SetValues(x => x += 1)
                .SetValues(x => x > 9 ? 0 : x);

            grid.ForEachWithCoord((val, coord) =>
            {
                if (val == 0)
                {
                    q.Enqueue(coord);
                }
            });


            while (q.Any())
            {
                var currentCoord = q.Dequeue();

                if (visited.Contains(currentCoord))
                {
                    continue;
                }

                visited.Add(currentCoord);
                totalFlashes += 1;

                grid.GetValidAdjacentIncludingDiag(currentCoord)
                    .ForEach(adjCoord =>
                    {

                        if (grid[adjCoord] != 0)
                        {
                            grid[adjCoord] += 1;
                        }

                        if (grid[adjCoord] > 9)
                        {
                            grid[adjCoord] = 0;
                            q.Enqueue(adjCoord);
                        }
                    });
            }

            totalFlashes.Dump();
        }
    }

    [Test]
    public void day11_2_grid()
    {
        var g = File
            .ReadAllText(
                "./inputs/day11.txt")
            .Trim()
            .Split("\n")
            .Select(x => x.ToCharArray().Select(x => int.Parse(x.ToString())).ToList())
            .ToList();

        var grid = new Grid(g);
        for (int step = 1; step <= 10000; step++)
        {
            var q = new Queue<Coord>();
            var visited = new HashSet<Coord>();
            // increase all by one, if it's >9 reset to 0
            grid.Print();
            grid = grid.SetValues(x => x += 1)
                .SetValues(x => x > 9 ? 0 : x);

            grid.ForEachWithCoord((val, coord) =>
            {
                if (val == 0)
                {
                    q.Enqueue(coord);
                }
            });


            while (q.Any())
            {
                var currentCoord = q.Dequeue();

                if (visited.Contains(currentCoord))
                {
                    continue;
                }

                visited.Add(currentCoord);

                grid.GetValidAdjacentIncludingDiag(currentCoord)
                    .ForEach(adjCoord =>
                    {

                        if (grid[adjCoord] != 0)
                        {
                            grid[adjCoord] += 1;
                        }

                        if (grid[adjCoord] > 9)
                        {
                            grid[adjCoord] = 0;
                            q.Enqueue(adjCoord);
                        }
                    });
            }

            if (grid.AllValues(x => x == 0))
            {
                $"Sync'd at {step}".Dump();
                break;
            }

        }
    }

    [Test]
    public void day12_1()
    {
        var links = new Dictionary<string, List<string>>();
        var smallCaves = new HashSet<string>();
        File
            .ReadAllText(
                "./inputs/day12.txt")
            .Trim()
            .Replace("\r\n", "\n")
            .Split("\n")
            .ToList()
            .ForEach(line =>
            {
                var (cave1, cave2) = line.Trim().Split("-");
                // add caves to eachother
                links[cave1] = links.GetValueOrDefault(cave1, new List<string>()).Concat(new List<string> { cave2 }).ToList();
                links[cave2] = links.GetValueOrDefault(cave2, new List<string>()).Concat(new List<string> { cave1 }).ToList();

                if (cave1 == cave1.ToLower())
                {
                    smallCaves.Add(cave1);
                }

                if (cave2 == cave2.ToLower())
                {
                    smallCaves.Add(cave2);
                }
            });

        links.Dump();

        var stack = new Stack<(string cave, List<string> pathSoFar)>();
        var startCave = "start";
        var endCave = "end";
        stack.Push(new(startCave, new List<string>()));
        var totalPaths = new HashSet<string>();
        while (stack.Any())
        {
            stack.Count.Dump();
            var currentCave = stack.Pop();

            // end of path reached
            if (currentCave.cave == endCave)
            {
                totalPaths.Add(string.Join(", ", currentCave.pathSoFar));
                continue;
            }

            // can only visit each small cave once so continue if visited
            if (smallCaves.Contains(currentCave.cave) && currentCave.pathSoFar.Contains(currentCave.cave))
            {
                continue;
            }

            // add all links
            var linkCaves = links[currentCave.cave].Select(x => x).ToList();


            linkCaves.ForEach(x => stack.Push(new(x, currentCave.pathSoFar.Concat(new List<string> { currentCave.cave }).ToList())));

        }

        totalPaths.Count.Dump();
    }

    [Test]
    public void day12_2()
    {
        var links = new Dictionary<string, List<string>>();
        var smallCaves = new HashSet<string>();
        File
            .ReadAllText(
                "./inputs/day12.txt")
            .Trim()
            .Replace("\r\n", "\n")
            .Split("\n")
            .ToList()
            .ForEach(line =>
            {
                var (cave1, cave2) = line.Trim().Split("-");
                // add caves to eachother
                links[cave1] = links.GetValueOrDefault(cave1, new List<string>()).Concat(new List<string> { cave2 }).ToList();
                links[cave2] = links.GetValueOrDefault(cave2, new List<string>()).Concat(new List<string> { cave1 }).ToList();

                if (cave1 == cave1.ToLower())
                {
                    smallCaves.Add(cave1);
                }

                if (cave2 == cave2.ToLower())
                {
                    smallCaves.Add(cave2);
                }
            });

        var stack = new Stack<(string cave, List<string> pathSoFar, bool hasUsedDuplicate)>();
        var startCave = "start";
        var endCave = "end";
        stack.Push(new(startCave, new List<string>(), false));
        var totalPaths = new HashSet<string>();
        while (stack.Any())
        {
            var currentCave = stack.Pop();

            // end of path reached
            if (currentCave.cave == endCave)
            {
                totalPaths.Add(string.Join(", ", currentCave.pathSoFar));
                continue;
            }

            // one small cave can be visited twice, all the rest once, start can never be visited twice
            if ((smallCaves.Contains(currentCave.cave) && currentCave.pathSoFar.Contains(currentCave.cave) && currentCave.hasUsedDuplicate) || (currentCave.cave == startCave && currentCave.pathSoFar.Contains(currentCave.cave)))
            {
                continue;
            }

            var shouldHaveDup = currentCave.hasUsedDuplicate || (smallCaves.Contains(currentCave.cave) &&
                                                             currentCave.pathSoFar.Contains(currentCave.cave) &&
                                                             !currentCave.hasUsedDuplicate);


            // add all links
            var linkCaves = links[currentCave.cave].Select(x => x).ToList();


            linkCaves.ForEach(x => stack.Push(
                new(x, new List<string>(currentCave.pathSoFar) { currentCave.cave }, shouldHaveDup)));
        }

        totalPaths.Count.Dump();
    }


    // copy from part 1 for part 2
    private bool IsCorrupted(IEnumerable<char> line)
    {

        var matches = new Dictionary<char, char>()
        {
            { ')', '(' },
            { ']', '[' },
            { '}' , '{'},
            { '>' , '<'}
        };

        var stack = new Stack<char>();
        foreach (var letter in line)
        {
            if (matches.Values.Contains(letter))
            {
                stack.Push(letter);
            }
            else
            {
                if (stack.Peek() == matches[letter])
                {
                    stack.Pop();
                }
                else
                {
                    return true;
                }
            }
        }

        return false;
    }
    public record CoordPair(int startX, int startY, int endX, int endY);

    public class BingoCard
    {
        public BingoCard(List<List<(int number, bool marked)>> numbers)
        {
            this.Numbers = numbers;
        }
        public List<List<(int number, bool marked)>> Numbers { get; set; }

        public bool HasWon { get; set; }

        public bool MarkItem(int bingoCall)
        {
            for (var row = 0; row < this.Numbers.Count; row++)
            {
                var items = this.Numbers[row];
                for (var column = 0; column < items.Count; column++)
                {
                    var (number, _) = items[column];
                    if (number == bingoCall)
                    {
                        this.Numbers[row][column] = new ValueTuple<int, bool>(number, true);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CheckForWin()
        {
            if (this.HasWon)
            {
                return true;
            }

            if (this.Numbers.Any(x => x.All(x => x.marked)))
            {
                this.HasWon = true;
                return true;
            }

            if (this.GetAllColumns().Any(x => x.All(x => x.marked)))
            {
                this.HasWon = true;
                return true;
            }

            return false;

        }

        public int SumOfUnmarkedNumbers()
        {
            return this.Numbers.SelectMany(x => x).Where(x => !x.marked).Select(x => x.number).Sum();
        }

        private List<List<(int number, bool marked)>> GetAllColumns()
        {
            return Enumerable.Range(0, this.Numbers[0].Count)
                .Select(column => Enumerable.Range(0, this.Numbers.Count).Select(row => this.Numbers[row][column]).ToList())
                .ToList();
        }
    }
}
