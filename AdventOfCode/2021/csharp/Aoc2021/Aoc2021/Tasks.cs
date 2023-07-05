using Dumpify;
using NUnit.Framework;

namespace Aoc2021;

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
    public async Task day5_1_2()
    {
        var textLines = File.ReadAllText(inputPath + "/day5.txt")
             .Trim()
             .Split("\n")
             .ToList();


        var lines = textLines.Select(l =>
        {
            var (start, end)= l.Trim().Split(" -> ");
            var (sx, sy) = start.Split(',').Select(int.Parse).ToList();
            var (ex, ey) = end.Split(',').Select(int.Parse).ToList();
            return new CoordPair(sx, sy, ex, ey);
        })//.Where(c => c.startX == c.endX || c.startY == c.endY) // uncomment for part 1
        .ToList();

        var pointCounter = new Dictionary<(int, int), int>();

        foreach(var line in lines)
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
                
                foreach(var (x,y) in diagPoints)
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
            for (var f =0; f< timers.Count; f++)
            {
                timers[f] -= 1;
                if (timers[f] <0 )
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

        var timers = Enumerable.Range(0,9).ToDictionary(x => x, x => (long)0);
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
            .Aggregate(new List<long>(new long[newFishResetDay]), (list, kv) => { list[kv.Key] = kv.Count();
                return list; });

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
        var positions = File.ReadAllText(inputPath + "/day7.txt")
                         .Trim()
                         .Split(",")
                         .Select(int.Parse)
                         .ToList();

        var minFuelcost = int.MaxValue;
        foreach(var pos in positions)
        {
            var fuel = positions.Select(p => Math.Abs(p - pos)).Sum();
            minFuelcost = Math.Min(minFuelcost, fuel);
        }

        minFuelcost.Dump();
    }

    [Test]
    public void day7_2()
    {
        var positions = File.ReadAllText(inputPath + "/day7.txt")
                         .Trim()
                         .Split(",")
                         .Select(int.Parse)
                         .ToList();

        var minFuelcost = long.MaxValue;
        foreach (var pos in Enumerable.Range(0, positions.Max()))
        {
            var currentFuel = positions.Sum(p => Enumerable.Range(0, Math.Abs(p - pos)+1).Sum(x => 1*x));
            minFuelcost = Math.Min(minFuelcost, currentFuel);
        }

        minFuelcost.Dump();
    }

    [Test]
    public void day7_2_TwoLiner()
    {
        var allPositions = File.ReadAllText(inputPath + "/day7.txt")
                         .Trim()
                         .Split(",")
                         .Select(int.Parse)
                         .ToList();
        Enumerable.Range(0, allPositions.Max())
            .Select(pos => allPositions.Sum(p => Enumerable.Range(0, Math.Abs(p - pos) + 1).Sum(x => 1 * x)))
            .Min()
            .Dump();
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
