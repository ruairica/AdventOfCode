using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Text.RegularExpressions;
using Aoc.Utils;
using Aoc.Utils.Grids;
using Dumpify;
using FluentAssertions;
using NUnit.Framework;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aoc;

[TestFixture]
public class Tasks2023
{
    //run single test with eg dotnet watch test --filter day1_1_2023
    private const string basePath = "./inputs/2023";

    [Test]
    public void day1_1_2023()
    {
        var lines = FP.ReadFile($"{basePath}/day1.txt").Split("\n");

        lines.Select(line => line.Where(char.IsDigit).ToList())
            .Sum(digits => int.Parse($"{digits[0]}{digits[^1]}"))
            .Dump();
    }

    [Test]
    public void day1_2_2023()
    {
        var numStrings = new List<string>
        {
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        };

        var dict = new Dictionary<string, int>();
        numStrings.Enumerate().ToList().ForEach(x => dict.Add(x.val, x.index + 1));

        var collection = Enumerable.Range(1, 9).Select(x => x.ToString()).ToList();
        collection.ForEach(x => dict.Add(x, int.Parse(x)));

        numStrings.AddRange(collection);

        var lines = FP.ReadFile($"{basePath}/day1.txt").Split("\n");

        lines.Select(
                line => numStrings.SelectMany(
                    x => line.AllIndexOf(x).Where(e => e != -1).Select(y => (y, x))))
            .Sum(
                digits => int.Parse(
                    $"{dict[digits.MinBy(x => x.Item1).Item2]}{dict[digits.MaxBy(x => x.Item1).Item2]}"))
            .Dump();
    }

    [Test]
    public void day2_1_2023()
    {
        var lines = FP.ReadFile($"{basePath}/day2.txt").Split("\n");

        var redMax = 12;
        var greenMax = 13;
        var blueMax = 14;

        var invalidGameIds = new HashSet<int>();
        var allGameIds = new HashSet<int>();

        foreach (var line in lines)
        {
            var gameId = Regex.Matches(line, @"Game (\d+)")
                .Select(x => int.Parse(x.Value.Split(" ")[1]))
                .FirstOrDefault();

            allGameIds.Add(gameId);

            var sets = line.Split(";");

            // looping through sets is redundant here could have just got all the matches for the line and checked them all, see day2_1_2023_Cleaner
            foreach (var set in sets)
            {
                var blue = Regex.Matches(set, @"(\d+) blue")
                    .Select(x => int.Parse(x.Value.Split(" ")[0]))
                    .FirstOrDefault();

                var red = Regex.Matches(set, @"(\d+) red")
                    .Select(x => int.Parse(x.Value.Split(" ")[0]))
                    .FirstOrDefault();

                var green = Regex.Matches(set, @"(\d+) green")
                    .Select(x => int.Parse(x.Value.Split(" ")[0]))
                    .FirstOrDefault();

                if (blue > blueMax || red > redMax || green > greenMax)
                {
                    invalidGameIds.Add(gameId);
                }
            }
        }

        allGameIds.Except(invalidGameIds).Sum().Dump();
    }

    [Test]
    public void day2_1_2023_Cleaner()
    {
        var lines = FP.ReadFile($"{basePath}/day2.txt").Split("\n");

        lines.Where(
                line =>
                {
                    var blues = Regex.Matches(line, @"(\d+) blue")
                        .Select(x => int.Parse(x.Value.Split(" ")[0]));

                    var greens = Regex.Matches(line, @"(\d+) green")
                        .Select(x => int.Parse(x.Value.Split(" ")[0]));

                    var reds = Regex.Matches(line, @"(\d+) red")
                        .Select(x => int.Parse(x.Value.Split(" ")[0]));

                    return blues.All(x => x <= 14) && greens.All(x => x <= 13) &&
                           reds.All(x => x <= 12);
                })
            .Select(
                line => Regex.Matches(line, @"Game (\d+)")
                    .Select(x => int.Parse(x.Value.Split(" ")[1]))
                    .FirstOrDefault())
            .Sum()
            .Dump();
    }

    [Test]
    public void day2_2_2023()
    {
        var lines = FP.ReadFile($"{basePath}/day2.txt").Split("\n");

        var powers = new List<int>();

        foreach (var line in lines)
        {
            var sets = line.Split(";");

            var redMax = 0;
            var greenMax = 0;
            var blueMax = 0;

            foreach (var set in sets)
            {
                blueMax = Math.Max(
                    blueMax,
                    Regex.Matches(set, @"(\d+) blue")
                        .Select(x => int.Parse(x.Value.Split(" ")[0]))
                        .FirstOrDefault());

                redMax = Math.Max(
                    redMax,
                    Regex.Matches(set, @"(\d+) red")
                        .Select(x => int.Parse(x.Value.Split(" ")[0]))
                        .FirstOrDefault());

                greenMax = Math.Max(
                    greenMax,
                    Regex.Matches(set, @"(\d+) green")
                        .Select(x => int.Parse(x.Value.Split(" ")[0]))
                        .FirstOrDefault());
            }

            powers.Add(blueMax * redMax * greenMax);
        }

        powers.Sum().Dump();
    }

    [Test]
    public void day2_2_2023_Cleaner()
    {
        var lines = FP.ReadFile($"{basePath}/day2.txt").Split("\n");

        lines.Select(
                line =>
                {
                    var blue = Regex.Matches(line, @"(\d+) blue")
                        .Select(x => int.Parse(x.Value.Split(" ")[0]))
                        .Max();

                    var red = Regex.Matches(line, @"(\d+) red")
                        .Select(x => int.Parse(x.Value.Split(" ")[0]))
                        .Max();

                    var green = Regex.Matches(line, @"(\d+) green")
                        .Select(x => int.Parse(x.Value.Split(" ")[0]))
                        .Max();

                    return blue * red * green;
                })
            .Sum()
            .Dump();
    }

    [Test]
    public void day3_1_2023()
    {
        var grid = FP.ReadAsCharGrid($"{basePath}/day3.txt");
        var result = 0;
        var counted = new HashSet<(int, int)>();
        var width = grid[0].Count;
        var height = grid.Count;

        for (int r = 0; r < width; r++)
        {
            for (var c = 0; c < height; c++)
            {
                if (char.IsDigit(grid[r][c]) && !counted.Contains((r, c)))
                {
                    List<(int r, int c)> coords = new() { new(r, c) };
                    string number = grid[r][c].ToString();
                    counted.Add((r, c));

                    // walk forward until there is no more numbers
                    var walker = c + 1;

                    while (walker < width && char.IsDigit(grid[r][walker]))
                    {
                        coords.Add((r, walker));
                        counted.Add((r, walker));
                        number += grid[r][walker].ToString();
                        walker++;
                    }

                    // check for any adjacent symbols
                    if (coords.Select(x => new Coord(x.r, x.c))
                        .Any(
                            x => x.GetValidAdjacentIncludingDiag(width, height)
                                .Select(e => grid[e.r][e.c])
                                .Any(x => !char.IsDigit(x) && x != '.')))
                    {
                        result += int.Parse(number);
                    }
                }
            }
        }

        result.Dump();
    }

    [Test]
    public void day3_1_2023_Grid()
    {
        var g = FP.ReadAsCharGrid($"{basePath}/day3.txt");
        var grid = new Grid<char>(g);

        var result = 0;
        var counted = new HashSet<Coord>();

        grid.ForEachWithCoord(
            (c, coord) =>
            {
                if (char.IsDigit(c) && !counted.Contains(coord))
                {
                    List<Coord> coords = new() { coord };
                    string number = c.ToString();
                    counted.Add(coord);

                    // walk forward until there is no more numbers
                    var walker = coord.c + 1;

                    var currentCoord = new Coord(coord.r, walker);

                    while (walker < grid.Width && char.IsDigit(grid[currentCoord]))
                    {
                        coords.Add(currentCoord);
                        counted.Add(currentCoord);
                        number += grid[currentCoord].ToString();
                        walker++;
                    }

                    // check for any adjacent symbols
                    if (coords.Any(
                            e => grid.GetValidAdjacentIncludingDiag(e)
                                .Select(e => grid[e])
                                .Any(x => !char.IsDigit(x) && x != '.')))
                    {
                        result += int.Parse(number);
                    }
                }
            });

        result.Dump();
    }

    [Test]
    public void day3_2_2023()
    {
        var grid = FP.ReadAsCharGrid($"{basePath}/day3.txt");
        var counted = new HashSet<(int, int)>();
        var width = grid[0].Count;
        var height = grid.Count;

        var gears = new Dictionary<(int, int), List<int>>();

        for (int r = 0; r < width; r++)
        {
            for (var c = 0; c < height; c++)
            {
                if (char.IsDigit(grid[r][c]) && !counted.Contains((r, c)))
                {
                    List<(int r, int c)> coords = new() { new(r, c) };
                    string number = grid[r][c].ToString();
                    counted.Add((r, c));

                    // walk forward until there is no more numbers
                    var walker = c + 1;

                    while (walker < width && char.IsDigit(grid[r][walker]))
                    {
                        coords.Add((r, walker));
                        counted.Add((r, walker));
                        number += grid[r][walker].ToString();
                        walker++;
                    }

                    // check for any adjacent *
                    var gearFound = coords.Select(x => new Coord(x.r, x.c))
                        .SelectMany(
                            c => c.GetValidAdjacentIncludingDiag(width, height)
                                .Where(e => grid[e.r][e.c] == '*')
                                .Select(e => (e.r, e.c)))
                        .ToList();

                    if (gearFound.Any())
                    {
                        var list = gears.GetValueOrDefault(
                            gearFound.First(),
                            new List<int>());

                        list.Add(int.Parse(number));
                        gears[gearFound.First()] = list;
                    }
                }
            }
        }

        gears.Where(x => x.Value.Count == 2)
            .Select(x => x.Value.Aggregate((a, b) => a * b))
            .Sum()
            .Dump();
    }

    [Test]
    public void day4_1_2023()
    {
        var lines = FP.ReadFile($"{basePath}/day4.txt").Split("\n").ToList();

        var result = 0;

        foreach (var line in lines)
        {
            var allNums = line.Split(":")[1];

            var (winners, myNums) = allNums.Split("|");

            var wNum = Regex.Matches(winners, @"(\d+)").Select(x => int.Parse(x.Value));
            var aNum = Regex.Matches(myNums, @"(\d+)").Select(x => int.Parse(x.Value));

            var count = wNum.Intersect(aNum).Count();

            if (count == 0)
            {
                continue;
            }

            var total = 1;

            for (var i = 0; i < count - 1; i++)
            {
                total *= 2;
            }

            result += total;
        }

        result.Dump();
    }

    [Test]
    public void day4_2_2023()
    {
        var lines = FP.ReadFile($"{basePath}/day4.txt").Split("\n").ToList();

        var copies = new Dictionary<int, int>();

        foreach (var (line, index) in lines.Enumerate())
        {
            var cardNumber = index + 1;
            var allNums = line.Split(":")[1];

            var (winners, myNums) = allNums.Split("|");

            var wNum = Regex.Matches(winners, @"(\d+)").Select(x => int.Parse(x.Value));
            var aNum = Regex.Matches(myNums, @"(\d+)").Select(x => int.Parse(x.Value));

            var count = wNum.Intersect(aNum).Count();

            // process copies for this line
            var numOfCopies = copies.GetValueOrDefault(cardNumber, 0);

            foreach (var num in Enumerable.Range(cardNumber + 1, count))
            {
                copies.AddOrUpdate(num, 1 + numOfCopies);
            }
        }

        (copies.Values.Sum() + lines.Count).Dump();
    }

    [Test]
    public void day5_1_2023()
    {
        var text = FP.ReadFile($"{basePath}/day5.txt");

        var c = Regex.Matches(text.Split("\n")[0], @"(\d+)")
            .Select(x => long.Parse(x.Value))
            .ToList();

        var maps = c.Select(x => new List<long> { x }).ToList();

        maps.Dump();

        foreach (var (block, blockIndex) in text.Split("\n\n").Skip(1).Enumerate())
        {
            foreach (var line in block.Split("\n").Skip(1))
            {
                var (destinationR, sourceR, range) = Regex
                    .Matches(line.Split("\n")[0], @"(\d+)")
                    .Select(x => long.Parse(x.Value))
                    .ToList();

                foreach (var (map, i) in maps.Enumerate()
                             .Where(x => x.val.Count == blockIndex + 1))
                {
                    var current = map.Last();

                    if (current >= sourceR && current <= sourceR + range - 1)
                    {
                        var mappedValue = current - sourceR + destinationR;
                        map.Add(mappedValue);
                    }
                }
            }

            var unmapped = maps.Where(x => x.Count < blockIndex + 2).ToList();
            unmapped.ForEach(x => x.Add(x.Last()));

            if (maps.Select(x => x.Count).Distinct().Count() != 1)
            {
                "PROBLEM".Dump();
            }
        }

        maps.Select(x => x.Last()).Min().Dump();
    }

    [Test]
    public void day5_2_2023()
    {
        var text = FP.ReadFile($"{basePath}/day5.txt");

        var seeds = Regex.Matches(text.Split("\n")[0], @"(\d+)")
            .Select(x => long.Parse(x.Value))
            .ToList();

        var originalRanges = new List<(long, long)>();

        for (int x = 0; x < seeds.Count; x += 2)
        {
            originalRanges.Add((seeds[x], seeds[x] + seeds[x + 1] - 1));
        }


        var currentRanges = originalRanges.Select(x => x).ToList();
        var newRanges = new List<(long, long)>();

        foreach (var block in text.Split("\n\n").Skip(1))
        {
            var usedRanges = new HashSet<(long, long)>();   

            foreach (var line in block.Split("\n").Skip(1))
            {
                var (destinationR, sourceR, range) = Regex
                    .Matches(line.Split("\n")[0], @"(\d+)")
                    .Select(x => long.Parse(x.Value))
                    .ToList();

                var (nr, leftover, used) = CheckRanges(
                    destinationR,
                    sourceR,
                    range,
                    currentRanges);

                used.ForEach(x => usedRanges.Add(x));
                newRanges.AddRange(nr);
                currentRanges = currentRanges.Except(used).ToList();
                currentRanges.AddRange(leftover);
                currentRanges = currentRanges.Distinct().ToList();
            }

            currentRanges =
                newRanges.Select(x => x)
                    .ToList()
                    .Concat(currentRanges)
                    .Concat(originalRanges)
                    .Except(usedRanges)
                    .ToList();
            originalRanges = currentRanges.Select(x => x).ToList();
            newRanges = new List<(long, long)>();
        }

        currentRanges.Select(x => x.Item1).Min().Dump();
    }

    private (List<(long, long)> newRanges, List<(long, long)> leftOvers, List<(long, long)>
        unused) CheckRanges(
            long destinationR,
            long sourceR,
            long range,
            List<(long, long)> ranges)
    {
        var newRanges = new List<(long, long)>();
        var newLeftovers = new List<(long, long)>();
        var used = new List<(long, long)>();

        var (start, end) = (sourceR, sourceR + range - 1);

        foreach (var (rangeS, rangeE) in ranges)
        {
            if (rangeS >= start && rangeE <= end)
            {
                used.Add((rangeS, rangeE));
                newRanges.Add((destinationR + rangeS - start,
                    destinationR + rangeS - start + (rangeE - rangeS)));
            }
            else if ((rangeS >= start && rangeS <= end) && rangeE > end)
            {
                used.Add((rangeS, rangeE));
                newLeftovers.Add((end + 1, rangeE));
                newRanges.Add((destinationR + (rangeS - start), destinationR + (rangeS - start) + (end - rangeS)));
            }
            else if (rangeS < start && (rangeE <= end && rangeE >= start))
            {
                used.Add((rangeS, rangeE));
                newLeftovers.Add((rangeS, start - 1));
                newRanges.Add((destinationR, destinationR + (rangeE - start)));
            }
            else if (rangeS < start && rangeE > end)
            {
                used.Add((rangeS, rangeE));

                newLeftovers.Add((rangeS, start - 1));
                newLeftovers.Add((end + 1, rangeE));
                newRanges.Add((destinationR, destinationR + range - 1));
            }
            else if (rangeE < start || rangeS > end)
            {
                newLeftovers.Add((rangeS, rangeE));
            }
        }

        return (newRanges, newLeftovers, used);
    }
}