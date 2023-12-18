using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO.MemoryMappedFiles;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;
using Aoc.Utils;
using Aoc.Utils.Grids;
using Dumpify;
using FluentAssertions;
using FluentAssertions.Equivalency.Steps;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Spectre.Console;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Schema;
using System.Net.Http.Headers;
using FluentAssertions.Formatting;
using System.Reflection;

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

        var currentRanges = new List<(long, long)>();

        for (int x = 0; x < seeds.Count; x += 2)
        {
            currentRanges.Add((seeds[x], seeds[x] + seeds[x + 1] - 1));
        }

        var newRanges = new List<(long, long)>();

        foreach (var block in text.Split("\n\n").Skip(1))
        {
            foreach (var line in block.Split("\n").Skip(1))
            {
                var (destinationR, sourceR, range) = Regex.Matches(line, @"(\d+)")
                    .Select(x => long.Parse(x.Value))
                    .ToList();

                var nr = new List<(long, long)>();

                var leftover =
                    new List<(long, long)>(); // ranges that were left over from partial overlap

                var used =
                    new List<(long, long)>(); // record which of the original ranges were used

                var (start, end) = (sourceR, sourceR + range - 1);

                foreach (var (rangeS, rangeE) in currentRanges)
                {
                    if (rangeS >= start && rangeE <= end) // fully contained
                    {
                        used.Add((rangeS, rangeE));

                        nr.Add(
                            (destinationR + rangeS - start,
                                destinationR - start + rangeE));
                    }
                    else if ((rangeS >= start && rangeS <= end) &&
                             rangeE > end) // partial overlap
                    {
                        used.Add((rangeS, rangeE));
                        leftover.Add((end + 1, rangeE));

                        nr.Add(
                            (destinationR + rangeS - start, destinationR - start + end));
                    }
                    else if (rangeS < start &&
                             (rangeE <= end && rangeE >= start)) // partial overlap
                    {
                        used.Add((rangeS, rangeE));
                        leftover.Add((rangeS, start - 1));
                        nr.Add((destinationR, destinationR + (rangeE - start)));
                    }
                    else if (rangeS < start && rangeE > end) // fully overlapping
                    {
                        used.Add((rangeS, rangeE));
                        leftover.Add((rangeS, start - 1));
                        leftover.Add((end + 1, rangeE));
                        nr.Add((destinationR, destinationR + range - 1));
                    }
                    else if (rangeE < start || rangeS > end) // no overlap
                    {
                        leftover.Add((rangeS, rangeE));
                    }
                }

                newRanges.AddRange(nr); // newly mapped ranges from the line

                // unused ranges and leftover ranges from partial overlaps move on to the next line
                currentRanges = currentRanges.Except(used).ToList();
                currentRanges.AddRange(leftover);
            }

            currentRanges = newRanges.Select(x => x).Concat(currentRanges).ToList();

            newRanges = new List<(long, long)>();
        }

        currentRanges.Select(x => x.Item1).Min().Dump();
    }

    [Test]
    public void day6_1_2023()
    {
        var lines = FP.ReadFile($"{basePath}/day6.txt").Split("\n");

        var times = lines[0].GetNums();

        var recordDistances = lines[1].GetNums();

        var results = new List<int>();

        for (int race = 0; race < times.Count; race++)
        {
            var rd = recordDistances[race];
            var time = times[race];
            results.Add(0);

            for (var t = 1; t < time; t++)
            {
                var d = (time - t) * t;

                if (d > rd)
                {
                    results[race] += 1;
                }
            }
        }

        results.Aggregate((a, b) => a * b).Dump();
    }

    [Test]
    public void day6_2_2023()
    {
        var lines = FP.ReadFile($"{basePath}/day6.txt").Split("\n");

        var tt = long.Parse(lines[0].Replace(" ", string.Empty)[5..]);
        var dd = long.Parse(lines[1].Replace(" ", string.Empty)[9..]);
        var times = new List<long> { tt };
        var recordDistances = new List<long> { dd };

        var results = 0;

        for (int race = 0; race < times.Count; race++)
        {
            var rd = recordDistances[race];
            var time = times[race];

            for (var t = 1; t < time; t++)
            {
                var d = (time - t) * t;

                if (d > rd)
                {
                    results += 1;
                }
            }
        }

        results.Dump();
    }

    [Test]
    public void day7_1_2023()
    {
        var lines = FP.ReadFile($"{basePath}/day7.txt").Split("\n");

        lines.Select(
                l =>
                {
                    var (cards, bid) = l.Split(" ");

                    return (new Hand(cards, part2: false), int.Parse(bid));
                })
            .ToList()
            .OrderBy(x => x.Item1)
            .Reverse()
            .Select((d, index) => d.Item2 * (index + 1))
            .Sum()
            .Dump();
    }

    [Test]
    public void day7_2_2023()
    {
        var lines = FP.ReadFile($"{basePath}/day7.txt").Split("\n");

        lines.Select(
                l =>
                {
                    var (cards, bid) = l.Split(" ");

                    return (new Hand(cards, part2: true), int.Parse(bid));
                })
            .OrderBy(x => x.Item1)
            .Reverse()
            .Select((d, index) => d.Item2 * (index + 1))
            .Sum()
            .Dump();
    }

    [Test]
    public void day8_1_2023()
    {
        var (steps, lines) = FP.ReadFile($"{basePath}/day8.txt").Split("\n\n");

        var dict = lines.Split("\n")
            .ToDictionary(
                x => x.Split(" ")[0],
                x =>
                {
                    var lr = x.Split(" = ")[1].TrimEnd(')').TrimStart('(');

                    var (left, right) = lr.Split(", ");

                    return (left, right);
                });

        var count = 0;
        var current = "AAA";
        var goal = "ZZZ";

        while (true)
        {
            foreach (var step in steps)
            {
                if (current == goal)
                {
                    count.Dump();

                    return;
                }

                current = step switch
                {
                    'L' => dict[current].left,
                    'R' => dict[current].right,
                    _ => throw new Exception("invalid step"),
                };

                count += 1;
            }
        }
    }

    [Test]
    public void day8_2_2023()
    {
        var (steps, lines) = FP.ReadFile($"{basePath}/day8.txt").Split("\n\n");

        var dict = lines.Split("\n")
            .ToDictionary(
                x => x.Split(" ")[0],
                x =>
                {
                    var lr = x.Split(" = ")[1].TrimEnd(')').TrimStart('(');

                    var (left, right) = lr.Split(", ");

                    return (left, right);
                });

        var nodes = dict.Keys.Where(c => c.EndsWith("A")).ToList();

        var cycleLengths = new List<long>();

        foreach (var node in nodes)
        {
            // once it hits a z, find the number of times it takes to hit that z again
            long cycleLength = 0;

            var current = node;
            var endNode = string.Empty;
            var endFound = false;

            foreach (var step in Enumerable.Repeat(steps, 10_000).SelectMany(x => x))
            {
                var justFound = false;

                if (current.EndsWith('Z') && !endFound)
                {
                    endNode = current;
                    endFound = true;
                    justFound = true;
                }

                if (current == endNode && endFound && !justFound)
                {
                    cycleLengths.Add(cycleLength);

                    break;
                }

                current = step switch
                {
                    'L' => dict[current].left,
                    'R' => dict[current].right,
                    _ => throw new Exception("invalid step"),
                };

                if (endFound)
                {
                    cycleLength += 1;
                }
            }
        }

        cycleLengths.FindLCM().Dump();
    }

    [Test]
    public void day9_1_2023()
    {
        var lines = FP.ReadFile($"{basePath}/day9.txt").Split("\n");

        var result = new List<int>();

        foreach (var line in lines)
        {
            var seqs = new List<List<int>> { line.GetNums() };

            while (!(seqs.Last().Distinct().Count() == 1 && seqs.Last().Last() == 0))
            {
                seqs.Add(seqs.Last().GetDifferences().ToList());
            }

            seqs.Reverse();
            var finals = new List<int> { 0 };

            for (var i = 1; i < seqs.Count; i++)
            {
                finals.Add(seqs[i].Last() + finals.Last());
            }

            result.Add(finals.Last());
        }

        result.Sum().Dump();
    }

    [Test]
    public void day9_2_2023()
    {
        var lines = FP.ReadFile($"{basePath}/day9.txt").Split("\n");

        var result = new List<int>();

        foreach (var line in lines)
        {
            var seqs = new List<List<int>> { line.GetNums() };

            while (!(seqs.Last().Distinct().Count() == 1 && seqs.Last().Last() == 0))
            {
                seqs.Add(getDifferences(seqs.Last()).ToList());
            }

            seqs.Reverse();
            var firsts = new List<int> { 0 };

            for (var i = 1; i < seqs.Count; i++)
            {
                firsts.Add(seqs[i].First() - firsts.Last());
            }

            result.Add(firsts.Last());
        }

        result.Sum().Dump();

        IEnumerable<int> getDifferences(IReadOnlyList<int> nums)
        {
            for (var i = 1; i < nums.Count; i++)
            {
                yield return nums[i] - nums[i - 1];
            }
        }
    }

    [Test]
    public void day10_1_2023()
    {
        var G = FP.ReadAsCharGrid($"{basePath}/day10.txt");
        var g = new Grid<char>(G);
        Coord source = new(0, 0);

        g.ForEachWithCoord(
            (c, coord) =>
            {
                if (c == 'S')
                {
                    source = coord;
                }
            });

        var path = new List<Coord> { source };

        var lastCoord = new Coord(-1, -1);

        while (!(path.Last() == source && path.Count > 1))
        {
            if (path.Count > 1)
            {
                lastCoord = path[^2];
            }

            var cc = path.Last();
            var cv = g[cc];
            var surrounding = g.GetValidAdjacentNoDiag(cc);

            foreach (var nc in surrounding)
            {
                var nv = g[nc];

                // down
                if (nc.r > cc.r && nc.c == cc.c && nc != lastCoord &&
                    cv is 'S' or '|' or 'F' or '7' && nv is 'S' or '|' or 'J' or 'L')
                {
                    path.Add(nc);

                    break;
                }

                // up
                if (nc.r < cc.r && nc.c == cc.c && nc != lastCoord &&
                    (cv is 'S' or '|' or 'L' or 'J') && (nv is 'S' or '|' or 'F' or '7'))
                {
                    path.Add(nc);

                    break;
                }

                // left
                if (nc.r == cc.r && nc.c < cc.c && nc != lastCoord &&
                    (cv is 'S' or '-' or '7' or 'J') && (nv is 'S' or '-' or 'F' or 'L'))
                {
                    path.Add(nc);

                    break;
                }

                // right
                if (nc.r == cc.r && nc.c > cc.c && nc != lastCoord &&
                    (cv is 'S' or '-' or 'F' or 'L') && (nv is 'S' or '-' or '7' or 'J'))
                {
                    path.Add(nc);

                    break;
                }
            }
        }

        (path.Count / 2).Dump();
    }

    [Test]
    public void day10_2_2023()
    {
        var G = FP.ReadAsCharGrid($"{basePath}/day10.txt");

        var g = new Grid<char>(G);
        // find S
        Coord source = new(0, 0);

        g.ForEachWithCoord(
            (c, coord) =>
            {
                if (c == 'S')
                {
                    source = coord;
                }
            });

        var path = new List<Coord> { source };

        var lastCoord = new Coord(-1, -1);

        while (!(path.Last() == source && path.Count > 1))
        {
            if (path.Count > 1)
            {
                lastCoord = path[^2];
            }

            var cc = path.Last();
            var cv = g[cc];
            var surrounding = g.GetValidAdjacentNoDiag(cc);

            foreach (var nc in surrounding)
            {
                var nv = g[nc];

                // down
                if (nc.r > cc.r && nc.c == cc.c && nc != lastCoord &&
                    cv is 'S' or '|' or 'F' or '7' && nv is 'S' or '|' or 'J' or 'L')
                {
                    path.Add(nc);

                    break;
                }

                // up
                if (nc.r < cc.r && nc.c == cc.c && nc != lastCoord &&
                    (cv is 'S' or '|' or 'L' or 'J') && (nv is 'S' or '|' or 'F' or '7'))
                {
                    path.Add(nc);

                    break;
                }

                // left
                if (nc.r == cc.r && nc.c < cc.c && nc != lastCoord &&
                    (cv is 'S' or '-' or '7' or 'J') && (nv is 'S' or '-' or 'F' or 'L'))
                {
                    path.Add(nc);

                    break;
                }

                // right
                if (nc.r == cc.r && nc.c > cc.c && nc != lastCoord &&
                    (cv is 'S' or '-' or 'F' or 'L') && (nv is 'S' or '-' or '7' or 'J'))
                {
                    path.Add(nc);

                    break;
                }
            }
        }

        // for each point go right and see how many times it intersects with a pipe on chars "| F 7"
        // for a given point if it intersects with the shape an odd number of times, it's inside it
        // just manually checked my source to be J so it is not important

        var hash = path.ToHashSet();
        var area = 0;

        g.ForEachWithCoord(
            (_, coord) =>
            {
                if (!hash.Contains(coord))
                {
                    var pointsToRight = Enumerable
                        .Range(coord.c + 1, g.Width - 1 - coord.c)
                        .Select(y => coord with { c = y });

                    var intersections = pointsToRight.Where(x => hash.Contains(x))
                        .Count(x => g[x] is '|' or 'F' or '7');

                    if (intersections % 2 != 0)
                    {
                        area += 1;
                    }
                }
            });

        area.Dump();
    }

    [Test]
    public void day10_2_2023_Shoelace()
    {
        var G = FP.ReadAsCharGrid($"{basePath}/day10.txt");

        var g = new Grid<char>(G);
        // find S
        Coord source = new(0, 0);

        g.ForEachWithCoord(
            (c, coord) =>
            {
                if (c == 'S')
                {
                    source = coord;
                }
            });

        var path = new List<Coord> { source };

        var lastCoord = new Coord(-1, -1);

        while (!(path.Last() == source && path.Count > 1))
        {
            if (path.Count > 1)
            {
                lastCoord = path[^2];
            }

            var cc = path.Last();
            var cv = g[cc];
            var surrounding = g.GetValidAdjacentNoDiag(cc);

            foreach (var nc in surrounding)
            {
                var nv = g[nc];

                // down
                if (nc.r > cc.r && nc.c == cc.c && nc != lastCoord &&
                    cv is 'S' or '|' or 'F' or '7' && nv is 'S' or '|' or 'J' or 'L')
                {
                    path.Add(nc);

                    break;
                }

                // up
                if (nc.r < cc.r && nc.c == cc.c && nc != lastCoord &&
                    (cv is 'S' or '|' or 'L' or 'J') && (nv is 'S' or '|' or 'F' or '7'))
                {
                    path.Add(nc);

                    break;
                }

                // left
                if (nc.r == cc.r && nc.c < cc.c && nc != lastCoord &&
                    (cv is 'S' or '-' or '7' or 'J') && (nv is 'S' or '-' or 'F' or 'L'))
                {
                    path.Add(nc);

                    break;
                }

                // right
                if (nc.r == cc.r && nc.c > cc.c && nc != lastCoord &&
                    (cv is 'S' or '-' or 'F' or 'L') && (nv is 'S' or '-' or '7' or 'J'))
                {
                    path.Add(nc);

                    break;
                }
            }
        }

        path.Pop();

        var corners = path.Where(x => g[x] is 'S' or 'F' or 'J' or '7' or 'L');

        // https://en.wikipedia.org/wiki/Pick%27s_theorem relates total area to number of points inside
        //A = I + B/2 - 1 where I is internal points and B is boundary points
        // I = A - B/2 + 1
        var i = corners.ToList().CalculateShoelaceArea() - ((double)path.Count / 2) + 1;
        i.Dump();
    }

    [TestCase(2)] // part1 
    [TestCase(1000000)] // part 2
    public void day11_2023(int scale)
    {
        var g1 = new Grid<char>(FP.ReadAsCharGrid($"{basePath}/day11.txt"));

        var indexesOfRowsToScale = g1.GetAllRows()
            .Enumerate()
            .Where(x => x.val.All(x => x == '.'))
            .Select(r => r.index)
            .ToHashSet();

        var indexesOfColsToScale = g1.GetAllColumns()
            .Enumerate()
            .Where(x => x.val.All(x => x == '.'))
            .Select(r => r.index)
            .ToHashSet();

        var galaxies = g1.WhereWithCoord((c, _) => c == '#')
            .Select(x => x.Coord)
            .ToList();

        var combinations = galaxies.SelectMany(
            (x, i) => galaxies.Skip(i + 1),
            Tuple.Create);

        long total = 0;

        foreach (var (c1, c2) in combinations)
        {
            var dist = AStarAlgorithm.ManhattanDistance(c1, c2);

            var rowsInWay = indexesOfRowsToScale.Count(
                s => c1.r > c2.r ? s < c1.r && s > c2.r : s > c1.r && s < c2.r);

            var colsInWay = indexesOfColsToScale.Count(
                s => c1.c > c2.c ? s < c1.c && s > c2.c : s > c1.c && s < c2.c);

            total += dist + (scale - 1) * (rowsInWay + colsInWay);
        }

        total.Dump();
    }

    [Test]
    public void day12_1_2023()
    {
        var total = 0; 
        var lines= FP.ReadFile($"{basePath}/day12.txt").Split("\n");

        const char operational = '.';
        const char damaged = '#';
        foreach (var line in lines)
        {
            var (pattern, seq) = line.Split(" ");
            var brokens = seq.GetNums();

            var result = generatePermutations(pattern);

            var variations = result.Count(x => MatchesBrokens(x, brokens));
            total += variations;
        }

        total.Dump();

        static bool MatchesBrokens(string line, List<int> b)
        {
            var bCopy = b.ConvertAll(x => x);

            line += "."; // add padding so it always ends with an operational

            var toMatch = bCopy.Pop(0);

            var inMatch = false;
            var matchCount = 0;
            var finishedMatching = false;

            var matchesFound = 0;
            foreach (var (letter, index) in line.Enumerate())
            {
                if (letter == damaged && !inMatch)
                {
                    inMatch = true;
                    matchCount += 1;

                }
                else if (letter == damaged && inMatch)
                {
                    matchCount += 1;
                }
                else if (letter == operational && inMatch)
                {
                    inMatch = false;

                    if (matchCount != toMatch)
                    {
                        return false;
                    }

                    matchesFound += 1;
                    matchCount = 0;

                    if (bCopy.Count == 0 && line.IndexOf(damaged, index + 1) != -1)
                    {
                        return false;
                    }

                    if (bCopy.Count == 0 && line.IndexOf(damaged, index + 1) == -1)
                    {

                        return true;
                    }
                    if (bCopy.Count > 0)
                    {
                        toMatch = bCopy.Pop(0);
                    }

                }
            }

            if (matchesFound == b.Count && !inMatch)
            {
                return true;
            }

            return false;
        }
        

        static List<string> generatePermutations(string input)
        {
            List<string> result = new List<string>();
            generatePermutationsHelper(input.ToCharArray(), 0, result);
            return result;
        }

        static void generatePermutationsHelper(char[] chars, int index, List<string> result)
        {
            if (index == chars.Length)
            {
                result.Add(new string(chars));
                return;
            }

            if (chars[index] == '?')
            {
                chars[index] = '.';
                generatePermutationsHelper(chars, index + 1, result);

                chars[index] = '#';
                generatePermutationsHelper(chars, index + 1, result);

                // Reset to '?' for backtracking
                chars[index] = '?';
            }
            else
            {
                generatePermutationsHelper(chars, index + 1, result);
            }
        }
    }

    [Test] // TODO
    public void day12_2_2023()
    {
        var total = 0;
        var lines = FP.ReadFile($"{basePath}/day12.txt").Split("\n");

        const char operational = '.';
        const char damaged = '#';
        var cache = new Dictionary<string, bool>();

        foreach (var line in lines.Skip(1))
        {
            var (pattern, seq) = line.Split(" ");

            var newPattern = string.Join('?', Enumerable.Repeat(0, 5).Select(_ => pattern));

            var brokensString = string.Join(',', Enumerable.Repeat(0, 5).Select(_ => seq));
            var brokens = brokensString.GetNums();
            var permutations = generatePermutations(newPattern);
            // foreach line in result, check if it matches the brokens

            var variations = 0;

            foreach (var p in permutations)
            {
                if (cache.TryGetValue(p + brokensString, out var result))
                {
                    if (result)
                    {
                        variations += 1;
                    }
                }
                else
                { 
                    var matches = MatchesBrokens(p, brokens);
                    cache.Add(p + brokensString, matches);

                    if (matches)
                    {
                        variations += 1;
                    }
                
                }
            }

            total += variations;
            break;
        }

        total.Dump();

        static bool MatchesBrokens(string line, List<int> b)
        {
            var bCopy = b.ConvertAll(x => x);

            line += "."; // add padding so it always ends with an operational

            var toMatch = bCopy.Pop(0);

            var inMatch = false;
            var matchCount = 0;
            var finishedMatching = false;

            var matchesFound = 0;
            foreach (var (letter, index) in line.Enumerate())
            {
                if (letter == damaged && !inMatch)
                {
                    inMatch = true;
                    matchCount += 1;

                }
                else if (letter == damaged && inMatch)
                {
                    matchCount += 1;
                }
                else if (letter == operational && inMatch)
                {
                    inMatch = false;

                    if (matchCount != toMatch)
                    {
                        return false;
                    }

                    matchesFound += 1;
                    matchCount = 0;

                    if (bCopy.Count == 0 && line.IndexOf(damaged, index + 1) != -1)
                    {
                        return false;
                    }

                    if (bCopy.Count == 0 && line.IndexOf(damaged, index + 1) == -1)
                    {

                        return true;
                    }
                    if (bCopy.Count > 0)
                    {
                        toMatch = bCopy.Pop(0);
                    }

                }
            }

            if (matchesFound == b.Count && !inMatch)
            {
                return true;
            }

            return false;
        }


        static List<string> generatePermutations(string input)
        {
            List<string> result = new List<string>();
            generatePermutationsHelper(input.ToCharArray(), 0, result);
            return result;
        }

        static void generatePermutationsHelper(char[] chars, int index, List<string> result)
        {
            if (index == chars.Length)
            {
                result.Add(new string(chars));
                return;
            }

            if (chars[index] == '?')
            {
                chars[index] = '.';
                generatePermutationsHelper(chars, index + 1, result);

                chars[index] = '#';
                generatePermutationsHelper(chars, index + 1, result);

                // Reset to '?' for backtracking
                chars[index] = '?';
            }
            else
            {
                generatePermutationsHelper(chars, index + 1, result);
            }
        }
    }

    [Test]
    public void day13_1_2023()
    {
        var sections = FP.ReadFile($"{basePath}/day13.txt").Split("\n\n");
        var total = 0;

        foreach (var (section, si) in sections.Enumerate())
        {
            var g = new Grid<char>(
                section.Split("\n").Select(x => x.Select(y => y).ToList()).ToList());

            var biggest = (planes: 0, score: 0);

            for (var i = 1; i < g.Height; i++)
            {
                if (g.grid[i].Enumerate().All(x => x.val == g.grid[i - 1][x.index]))
                {
                    var hitEdge = false;

                    var top = i - 2;
                    var bottom = i + 1;

                    var rowsOfSym = 2;

                    if (top == -1 || bottom == g.Height)
                    {
                        rowsOfSym += 2;
                        hitEdge = true;
                    }

                    while (top >= 0 && bottom < g.Height)
                    {
                        var symmetrical = g.grid[top]
                            .Enumerate()
                            .All(x => x.val == g.grid[bottom][x.index]);

                        if (symmetrical)
                        {
                            rowsOfSym += 2;
                            top -= 1;
                            bottom += 1;

                            if (top == -1 || bottom == g.Height)
                            {
                                hitEdge = true;
                                rowsOfSym += 2;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (rowsOfSym > biggest.planes && hitEdge)
                    {
                        biggest = (rowsOfSym, i * 100);
                    }
                }
            }

            // cols
            var cols = g.GetAllColumns();

            for (var i = 1; i < cols.Count; i++)
            {
                if (cols[i].Enumerate().All(x => x.val == cols[i - 1][x.index]))
                {
                    var left = i - 2;
                    var right = i + 1;

                    var hitEdge = false;
                    var colsOfSym = 2;

                    if (left == -1 || right == g.Width)
                    {
                        colsOfSym += 2;
                        hitEdge = true;
                    }

                    while (left >= 0 && right < g.Width)
                    {
                        var symmetrical = cols[left]
                            .Enumerate()
                            .All(x => x.val == cols[right][x.index]);

                        if (symmetrical)
                        {
                            colsOfSym += 2;
                            left -= 1;
                            right += 1;

                            if (left == -1 || right == g.Width)
                            {
                                hitEdge = true;
                                ;
                                colsOfSym += 2;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (colsOfSym > biggest.planes && hitEdge)
                    {
                        biggest = (colsOfSym, i);
                    }
                }
            }

            total += biggest.score;
        }

        total.Dump();
    }

    [Test]
    public void day13_2_2023()
    {
        var sections = FP.ReadFile($"{basePath}/day13.txt").Split("\n\n");
        var total = 0;

        foreach (var (section, si) in sections.Enumerate())
        {
            var g = new Grid<char>(
                section.Split("\n").Select(x => x.Select(y => y).ToList()).ToList());

            var biggest = (planes: 0, score: 0);

            // rows
            for (var i = 1; i < g.Height; i++)
            {
                var usedSmudge = false;

                var matches = g.grid[i]
                    .Enumerate()
                    .Count(x => x.val == g.grid[i - 1][x.index]);

                if (matches == g.Width - 1)
                {
                    usedSmudge = true;
                }

                if (matches == g.Width || matches == g.Width - 1)
                {
                    var hitEdge = false;

                    var top = i - 2;
                    var bottom = i + 1;

                    var rowsOfSym = 2;

                    if (top == -1 || bottom == g.Height)
                    {
                        rowsOfSym += 2;
                        hitEdge = true;
                    }

                    while (top >= 0 && bottom < g.Height)
                    {
                        var symmetricalHits = g.grid[top]
                            .Enumerate()
                            .Count(x => x.val == g.grid[bottom][x.index]);

                        if (symmetricalHits == g.Width)
                        {
                            rowsOfSym += 2;
                            top -= 1;
                            bottom += 1;

                            if (top == -1 || bottom == g.Height)
                            {
                                hitEdge = true;
                                rowsOfSym += 2;
                            }
                        }
                        else if (symmetricalHits == g.Width - 1 && !usedSmudge)
                        {
                            usedSmudge = true;
                            rowsOfSym += 2;
                            top -= 1;
                            bottom += 1;

                            if (top == -1 || bottom == g.Height)
                            {
                                hitEdge = true;
                                rowsOfSym += 2;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (rowsOfSym > biggest.planes && hitEdge && usedSmudge)
                    {
                        biggest = (rowsOfSym, i * 100);
                    }
                }
            }

            var cols = g.GetAllColumns();

            for (var i = 1; i < cols.Count; i++)
            {
                var usedSmudge = false;

                var matches =
                    cols[i].Enumerate().Count(x => x.val == cols[i - 1][x.index]);

                if (matches == g.Height - 1)
                {
                    usedSmudge = true;
                }

                if (matches == g.Height || matches == g.Height - 1)
                {
                    var left = i - 2;
                    var right = i + 1;

                    var hitEdge = false;
                    var colsOfSym = 2;

                    if (left == -1 || right == g.Width)
                    {
                        colsOfSym += 2;
                        hitEdge = true;
                    }

                    while (left >= 0 && right < g.Width)
                    {
                        var symmetricalHits = cols[left]
                            .Enumerate()
                            .Count(x => x.val == cols[right][x.index]);

                        if (symmetricalHits == g.Height)
                        {
                            colsOfSym += 2;
                            left -= 1;
                            right += 1;

                            if (left == -1 || right == g.Width)
                            {
                                hitEdge = true;
                                ;
                                colsOfSym += 2;
                            }
                        }
                        else if (symmetricalHits == g.Height - 1 && !usedSmudge)
                        {
                            usedSmudge = true;
                            colsOfSym += 2;
                            left -= 1;
                            right += 1;

                            if (left == -1 || right == g.Width)
                            {
                                hitEdge = true;
                                ;
                                colsOfSym += 2;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (colsOfSym > biggest.planes && hitEdge && usedSmudge)
                    {
                        biggest = (colsOfSym, i);
                    }
                }
            }

            if (biggest.planes == 0)
            {
                $"no planes found for {si}".Dump();
            }

            total += biggest.score;
        }

        total.Dump();
    }

    [Test]
    public void day14_1_2023()
    {
        var grid = new Grid<char>(FP.ReadAsCharGrid($"{basePath}/day14.txt"));
        grid.Print();

        for (int r = 0; r < grid.Height; r++)
        {
            for (var c = 0; c < grid.Width; c++)
            {
                var cc = new Coord(r, c);
                var letter = grid[cc];

                if (letter == 'O')
                {
                    var range = Enumerable.Range(0, r)
                        .Select(x => new Coord(x, c))
                        .Reverse()
                        .ToList();

                    var place = range.FindIndex(x => grid[x] == '#' || grid[x] == 'O');
                    if (place == -1)
                    {
                        if (r == 0)
                        {
                            continue;
                        }

                        grid[new Coord(0, c)] = 'O';
                        grid[cc] = '.';
                    }
                    else if (place > 0)
                    {
                        grid[range[place - 1]] = 'O';
                        grid[cc] = '.';
                    }
                }
            }
        }
        
        grid.WhereWithCoord((ch, _) => ch == 'O')
            .Select(x => grid.Height - x.Coord.r)
            .Sum()
            .Dump();
    }

    [Test] //TODO
    public void day14_2_2023()
    {

        int cycleCount = 0;

        var grid = new Grid<char>(FP.ReadAsCharGrid($"{basePath}/day14.txt"));

        var loadOnNorth = new List<(int load, int cycleNumber)>();

        var seen = new Dictionary<string, int>();
        while (true)
        {
            cycleCount += 1;

            foreach (var dir in new List<Dir> { Dir.Up, Dir.Left, Dir.Down, Dir.Right })
            {
                if (cycleCount == 1 && dir == Dir.Up)
                {
                    // don't rotate
                    "didn't transpose".Dump();
                }
                else
                {
                    "transposed".Dump();
                    grid.Rotate90ClockWise();
                }
                for (int r = 0; r < grid.Height; r++)
                {
                    for (var c = 0; c < grid.Width; c++)
                    {
                        var cc = new Coord(r, c);
                        var letter = grid[cc];

                        if (letter == 'O')
                        {
                            var range = Enumerable.Range(0, r)
                                .Select(x => new Coord(x, c))
                                .Reverse()
                                .ToList();

                            var place = range.FindIndex(x => grid[x] == '#' || grid[x] == 'O');
                            if (place == -1)
                            {
                                if (r == 0)
                                {
                                    continue;
                                }

                                grid[new Coord(0, c)] = 'O';
                                grid[cc] = '.';
                            }
                            else if (place > 0)
                            {
                                grid[range[place - 1]] = 'O';
                                grid[cc] = '.';
                            }
                        }
                    }
                }

                loadOnNorth.Add(
                    (grid.WhereWithCoord((ch, _) => ch == 'O')
                        .Select(x => grid.Height - x.Coord.r)
                        .Sum(), cycleCount));
            }

            var serialized = JsonSerializer.Serialize(grid);

            var found = seen.GetValueOrDefault(serialized);

            if (found != default && cycleCount > 1)
            {
                $"FOUND at {found} during {cycleCount}".Dump();
                break;
            }

            seen.Add(serialized, cycleCount);
            if (cycleCount == 50)
            {
                break;
            }
            break;
        }
        
        grid.Print();
    }

    [Test]
    public void day15_1_2023()
    {
        FP.ReadFile($"{basePath}/day15.txt")
            .Split(',')
            .Select(
                p => p.Select(x => (int)x)
                    .Aggregate(0, (acc, prev) => ((acc + prev) * 17) % 256))
            .Sum()
            .Dump();
    }

    [Test]
    public void day15_2_2023()
    {
        var parts = FP.ReadFile($"{basePath}/day15.txt").Split(',');

        var boxes = Enumerable.Range(0, 256)
            .Select(x => new List<(string, int)>())
            .ToList();

        foreach (var p in parts)
        {
            Func<int, int, int> hash = (acc, prev) => ((acc + prev) * 17) % 256;

            if (p.EndsWith('-'))
            {
                var label = p[..^1];

                var boxNum = label.Select(x => (int)x).Aggregate(0, hash);

                if (boxes[boxNum].Any(x => x.Item1 == label))
                {
                    boxes[boxNum] = boxes[boxNum].Where(x => x.Item1 != label).ToList();
                }
            }
            else
            {
                var (label, focal) = p.Split('=');
                var f = int.Parse(focal);

                var boxNum = label.Select(x => (int)x).Aggregate(0, hash);

                var current = boxes[boxNum].FindIndex(x => x.Item1 == label);

                if (current == -1)
                {
                    boxes[boxNum].Add((label, f));
                }
                else
                {
                    boxes[boxNum][current] = (label, f);
                }
            }
        }

        boxes.Select((b, bi) => b.Select((l, li) => (bi + 1) * (li + 1) * l.Item2).Sum())
            .Sum()
            .Dump();
    }

    [Test]
    public void day16_1_2023()
    {
        var g = new Grid<char>(FP.ReadAsCharGrid($"{basePath}/day16.txt"));

        var visited = new HashSet<(Coord, Dir)>();

        var stack = new Stack<(Coord, Dir)>();
        stack.Push((new Coord(0, 0), Dir.Right));

        while (stack.Any())
        {
            var (c, d) = stack.Pop();

            if (visited.Contains((c, d)))
            {
                continue;
            }

            visited.Add((c, d));

            List<Dir> toProcess = (g[c], d) switch
            {
                ('.', _) => new List<Dir> { d },
                ('/', Dir.Up) => new List<Dir> { Dir.Right },
                ('/', Dir.Down) => new List<Dir> { Dir.Left },
                ('/', Dir.Left) => new List<Dir> { Dir.Down },
                ('/', Dir.Right) => new List<Dir> { Dir.Up },
                ('\\', Dir.Up) => new List<Dir> { Dir.Left },
                ('\\', Dir.Down) => new List<Dir> { Dir.Right },
                ('\\', Dir.Left) => new List<Dir> { Dir.Up },
                ('\\', Dir.Right) => new List<Dir> { Dir.Down },
                ('|', Dir.Up) => new List<Dir> { Dir.Up },
                ('|', Dir.Down) => new List<Dir> { Dir.Down },
                ('|', _) => new List<Dir> { Dir.Up, Dir.Down },
                ('-', Dir.Left) => new List<Dir> { Dir.Left },
                ('-', Dir.Right) => new List<Dir> { Dir.Right },
                ('-', _) => new List<Dir> { Dir.Left, Dir.Right },
                (_, _) => throw new Exception("invalid")
            };

            toProcess.ForEach(
                x =>
                {
                    var (valid, nec) = g.Move(c, x);

                    if (valid)
                    {
                        stack.Push((nec, x));
                    }
                });
        }

        visited.Select(x => x.Item1).ToHashSet().Count.Dump();
    }

    [Test]
    public void day16_2_2023()
    {
        var g = new Grid<char>(FP.ReadAsCharGrid($"{basePath}/day16.txt"));

        var rows = g.GetAllRowsCoords();
        var cols = g.GetAllColCoords();

        var starting = rows[0]
            .ConvertAll(r => (r, Dir.Down))
            .Concat(rows[^1].ConvertAll(r => (r, Dir.Up)))
            .Concat(cols[0].ConvertAll(c => (c, Dir.Right)))
            .Concat(cols[^1].ConvertAll(c => (c, Dir.Left)));

        long highScore = 0;

        foreach (var (sc, sd) in starting)
        {
            var visited = new HashSet<(Coord, Dir)>();

            var stack = new Stack<(Coord, Dir)>();
            stack.Push((sc, sd));

            while (stack.Any())
            {
                var (c, d) = stack.Pop();

                if (visited.Contains((c, d)))
                {
                    continue;
                }

                visited.Add((c, d));

                List<Dir> toProcess = (g[c], d) switch
                {
                    ('.', _) => new List<Dir> { d },
                    ('/', Dir.Up) => new List<Dir> { Dir.Right },
                    ('/', Dir.Down) => new List<Dir> { Dir.Left },
                    ('/', Dir.Left) => new List<Dir> { Dir.Down },
                    ('/', Dir.Right) => new List<Dir> { Dir.Up },
                    ('\\', Dir.Up) => new List<Dir> { Dir.Left },
                    ('\\', Dir.Down) => new List<Dir> { Dir.Right },
                    ('\\', Dir.Left) => new List<Dir> { Dir.Up },
                    ('\\', Dir.Right) => new List<Dir> { Dir.Down },
                    ('|', Dir.Up) => new List<Dir> { Dir.Up },
                    ('|', Dir.Down) => new List<Dir> { Dir.Down },
                    ('|', _) => new List<Dir> { Dir.Up, Dir.Down },
                    ('-', Dir.Left) => new List<Dir> { Dir.Left },
                    ('-', Dir.Right) => new List<Dir> { Dir.Right },
                    ('-', _) => new List<Dir> { Dir.Left, Dir.Right },
                    (_, _) => throw new Exception("invalid")
                };

                toProcess.ForEach(
                    x =>
                    {
                        var (valid, nec) = g.Move(c, x);

                        if (valid)
                        {
                            stack.Push((nec, x));
                        }
                    });
            }

            highScore = Math.Max(
                highScore,
                visited.Select(x => x.Item1).ToHashSet().Count);
        }

        highScore.Dump();
    }

    [Test]
    public void day17_1_2023()
    {
        var grid = new Grid<int>(FP.ReadAsGrid($"{basePath}/day17.txt"));

        var starting = new Coord(0, 0);
        var end = new Coord(grid.Height - 1, grid.Width - 1);
        grid.Print();
        grid.Height.Dump();
        grid.Width.Dump();


        // var result = AStarAlgorithm2.AStar(grid, starting, end, Dir.Down);
        var result2 = AStarAlgorithm2.AStar(grid, starting, end, Dir.Right);


        //result.Dump();
        result2.Dump();
    }

    [Test]
    public void day18_1_2023()
    {
        var lines = FP.ReadFile($"{basePath}/day18.txt").Split("\n");
        var path = new List<Coord> { new(0, 0) };

        var corners = new List<Coord>();
        foreach (var line in lines)
        {
            var parts = line.Split(" ");
            var dir = parts[0];
            var dist = int.Parse(parts[1]);
            var last = path.Last();

            var rowMultiplier = dir switch
            {
                "U" => -1,
                "D" => 1,
                _ => 0
            };

            var colMultiplier = dir switch
            {
                "L" => -1,
                "R" => 1,
                _ => 0
            };

            var newCoords = Enumerable.Range(1, dist)
                .Select(
                    x => new Coord(
                        last.r + (rowMultiplier * x),
                        last.c + (colMultiplier * x)))
                .ToList();

            corners.Add(newCoords.Last());

            path.AddRange(newCoords);
        }

        path = path.Distinct().ToList();
        var internalPoints = corners.ToList().CalculateShoelaceArea() - ((double)path.Count / 2) + 1;
        (internalPoints + path.Count).Dump();
    }

    [Test]
    public void day18_2_2023()
    {
        var lines = FP.ReadFile($"{basePath}/day18.txt").Split("\n");

        var corners = new List<CoordL>() { new (0,0)};
        long pathCount = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(" ");
            var last = corners.Last();
            var hexNumber = parts[2][2..^1];
            long dist = long.Parse(hexNumber[..^1], System.Globalization.NumberStyles.HexNumber);
            var direction = hexNumber[^1].ToString();
            

            var rowMultiplier = direction switch
            {
                "3" => -1,
                "1" => 1,
                _ => 0
            };

            var colMultiplier = direction switch
            {
                "2" => -1,
                "0" => 1,
                _ => 0
            };

            corners.Add(
                new CoordL(
                    last.r + rowMultiplier * dist,
                    last.c + colMultiplier * dist));

            pathCount += dist;
        }

        var internalPoints = corners.ToList().CalculateShoelaceArea() - ((double)pathCount / 2) + 1;
        (internalPoints + pathCount).Dump();
    }
}