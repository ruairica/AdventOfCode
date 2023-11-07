namespace Aoc;
using System.Text.RegularExpressions;
using Aoc.Utils;
using Aoc.Utils.Grids;
using Dumpify;
using NUnit.Framework;

[TestFixture]
public class Tasks2022
{
    [Test]
    public void day1_1_2022()
    {
        var elves = File.ReadAllText("./inputs/2022/day1.txt")
            .Trim()
            .Replace("\r\n", "\n")
            .Split("\n\n")
            .Select(x => x.Split("\n").Select(int.Parse).Sum())
            .OrderByDescending(x => x)
            .ToList();

        elves.First().Dump();
    }

    [Test]
    public void day1_2_2022()
    {
        var elves = File.ReadAllText("./inputs/2022/day1.txt")
            .Trim()
            .Replace("\r\n", "\n")
            .Split("\n\n")
            .Select(x => x.Split("\n").Select(int.Parse).Sum())
            .OrderByDescending(x => x)
            .ToList();

        elves.Take(3).Sum().Dump();
    }

    [Test]
    public void day2_1_2022()
    {
        // A for Rock, B for Paper, and C for Scissors
        //X for Rock, Y for Paper, and Z for Scissors
        // shape you selected(1 for Rock, 2 for Paper, and 3 for Scissors) plus the score for the outcome of the round(0 if you lost, 3 if the round was a draw, and 6 if you won).
        var lines = File.ReadAllText("./inputs/2022/day2.txt")
            .Trim()
            .Replace("\r\n", "\n")
            .Split("\n")
            .ToList();
        var lossScore = 0;
        var drawScore = 3;
        var winScore = 6;

        var rockScore = 1;
        var paperScore = 2;
        var scissorsScore = 3;

        var result = 0;
        foreach (var line in lines)
        {
            var (myGuess, elfGuess) = line.Split(" ");

            result += (myGuess, elfGuess) switch
            {
                ("A", "X") => rockScore + drawScore,
                ("A", "Y") => paperScore + winScore,
                ("A", "Z") => scissorsScore + lossScore,

                ("B", "X") => rockScore + lossScore,
                ("B", "Y") => paperScore + drawScore,
                ("B", "Z") => scissorsScore + winScore,

                ("C", "X") => rockScore + winScore,
                ("C", "Y") => paperScore + lossScore,
                ("C", "Z") => scissorsScore + drawScore,
                _ => throw new NotSupportedException(),
            };
        }

        result.Dump();
    }

    [Test]
    public void day2_2_2022()
    {
        // A for Rock, B for Paper, and C for Scissors
        //X for Rock, Y for Paper, and Z for Scissors
        // shape you selected(1 for Rock, 2 for Paper, and 3 for Scissors) plus the score for the outcome of the round(0 if you lost, 3 if the round was a draw, and 6 if you won).
        var lines = File.ReadAllText("./inputs/2022/day2.txt")
            .Trim()
            .Replace("\r\n", "\n")
            .Split("\n")
            .ToList();
        var lossScore = 0;
        var drawScore = 3;
        var winScore = 6;

        var rockScore = 1;
        var paperScore = 2;
        var scissorsScore = 3;

        var result = 0;
        foreach (var line in lines)
        {
            var (myGuess, elfGuess) = line.Split(" ");

            result += (myGuess, elfGuess) switch
            {
                ("A", "X") => scissorsScore + lossScore,
                ("A", "Y") => rockScore + drawScore,
                ("A", "Z") => paperScore + winScore,

                ("B", "X") => rockScore + lossScore,
                ("B", "Y") => paperScore + drawScore,
                ("B", "Z") => scissorsScore + winScore,

                ("C", "X") => paperScore + lossScore,
                ("C", "Y") => scissorsScore + drawScore,
                ("C", "Z") => rockScore + winScore,
                _ => throw new NotSupportedException(),
            };
        }

        result.Dump();
    }

    [Test]
    public void day3_1_2022()
    {
        var lines = File.ReadAllText("./inputs/2022/day3.txt")
            .Trim()
            .Replace("\r\n", "\n")
            .Split("\n")
            .ToList();

        var result = 0;
        foreach (var line in lines)
        {
            var first = line[..(line.Length / 2)];
            var second = line[(line.Length / 2)..];

            var i = first.ToCharArray().Intersect(second.ToCharArray()).Single();

            if (char.IsLower(i))
            {
                result += (int)i - 96;
            }
            else
            {
                result += (int)i.ToString().ToLower().First() - 96 + 26;
            }
        }

        result.Dump();
    }

    [Test]
    public void day3_2_2022()
    {
        var chunks = File.ReadAllText("./inputs/2022/day3.txt")
            .Trim()
            .Replace("\r\n", "\n")
            .Split("\n")
            .Chunk(3)
            .ToList();

        var result = 0;
        foreach (var c in chunks)
        {
            var chunk = c.Select(x => x.ToCharArray()).ToList();
            var i = chunk[0].Intersect(chunk[1]).Intersect(chunk[2]).Single();

            if (char.IsLower(i))
            {
                result += (int)i - 96;
            }
            else
            {
                result += (int)i.ToString().ToLower().First() - 96 + 26;
            }
        }

        result.Dump();
    }

    [Test]
    // https://adventofcode.com/2022/day/4
    public void day4_1_2022()
    {
        File.ReadAllText("./inputs/2022/day4.txt")
            .Trim()
            .Replace("\r\n", "\n")
            .Split("\n")
            .Select(x =>
                {
                    var (e1, e2) = x.Split(",");
                    var (l1, u1) = e1.Split("-");
                    var (l2, u2) = e2.Split("-");
                    return (int.Parse(l1), int.Parse(u1), int.Parse(l2), int.Parse(u2));
                }).Count(x =>
                {
                    var (l1, u1, l2, u2) = x;
                    return l1 >= l2 && u1 <= u2 || l2 >= l1 && u2 <= u1;
                }).Dump();
    }

    [Test]
    public void day4_2_2022()
    {
        File.ReadAllText("./inputs/2022/day4.txt")
            .Trim()
            .Replace("\r\n", "\n")
            .Split("\n")
            .Select(x =>
            {
                var (e1, e2) = x.Split(",");
                var (l1, u1) = e1.Split("-");
                var (l2, u2) = e2.Split("-");
                return (int.Parse(l1), int.Parse(u1), int.Parse(l2), int.Parse(u2));
            }).Count(x =>
            {
                var (l1, u1, l2, u2) = x;
                return u1 >= l2 && l1 <= l2 || l1 <= u2 && u1 >= u2 || u2 >= l1 && l2 <= l1 || l2 <= u1 && u2 >= u1;
            }).Dump();
    }

    [Test]
    public void day5_1_2022()
    {
        var (start, steps) = File.ReadAllText("./inputs/2022/day5.txt")
            .Replace("\r\n", "\n")
            .Split("\n\n");

        var lines = start.Split("\n");

        var stacks = Enumerable.Range(1, 9).Select(_ => new Stack<string>()).ToList();

        foreach (var line in lines[..^1].Reverse())
        {
            for (int i = 1; i < lines[0].Length; i += 4)
            {
                if (char.IsAsciiLetterOrDigit(line[i]))
                {
                    stacks[i / 4].Push(line[i].ToString());
                }
            }
        }

        foreach (var step in steps.Split("\n"))
        {
            var matches = Regex.Match(step, @"move (\d+) from (\d+) to (\d+)");
            var n = int.Parse(matches.Groups[1].Value);
            var from = int.Parse(matches.Groups[2].Value);
            var to = int.Parse(matches.Groups[3].Value);

            foreach (var _ in Enumerable.Range(1, n))
            {
                stacks[to - 1].Push(stacks[from - 1].Pop());
            }
        }

        string.Join("", stacks.Select(x => x.Peek())).Dump();
    }

    [Test]
    public void day5_2_2022()
    {
        var (start, steps) = File.ReadAllText("./inputs/2022/day5.txt")
            .Replace("\r\n", "\n")
            .Split("\n\n");

        var lines = start.Split("\n");

        var stacks = Enumerable.Range(1, 9).Select(_ => new Stack<string>()).ToList();

        foreach (var line in lines[..^1].Reverse())
        {
            for (int i = 1; i < lines[0].Length; i += 4)
            {
                if (char.IsAsciiLetterOrDigit(line[i]))
                {
                    stacks[i / 4].Push(line[i].ToString());
                }
            }
        }

        foreach (var step in steps.Split("\n"))
        {
            var matches = Regex.Match(step, @"move (\d+) from (\d+) to (\d+)");
            var n = int.Parse(matches.Groups[1].Value);
            var from = int.Parse(matches.Groups[2].Value);
            var to = int.Parse(matches.Groups[3].Value);

            var items = Enumerable.Range(1, n).Select(_ => stacks[from - 1].Pop());
            items = items.Reverse();
            items.ToList().ForEach(x => stacks[to - 1].Push(x));
        }

        string.Join("", stacks.Select(x => x.Peek())).Dump();
    }

    [Test]
    public void day6_1_2022()
    {
        var chars = File.ReadAllText("./inputs/2022/day6.txt")
            .Replace("\r\n", "\n")
            .Trim();

        var result = 0;
        for (int i = 4; i < chars.Length; i++)
        {
            if (chars[(i - 4)..i].Distinct().Count() == 4)
            {
                result = i;
                break;
            }
        }

        result.Dump();
    }

    [Test]
    public void day6_2_2022()
    {
        var chars = FP.ReadFile("./inputs/2022/day6.txt");

        var result = 0;
        for (int i = 14; i < chars.Length; i++)
        {
            if (chars[(i - 14)..i].Distinct().Count() == 14)
            {
                result = i;
                break;
            }
        }

        result.Dump();
    }

    [Test]
    public void day7_1_2022()
    {
        var lines = FP.ReadLines("./inputs/2022/day7.txt");

        Dictionary<string, int> sizes = new();
        List<string> currentPath = new();
        foreach (var line in lines)
        {
            // change directory
            if (line.StartsWith("$ cd"))
            {
                var (_, _, dir) = line.Split(" ");
                if (dir == "..")
                {
                    currentPath.Pop();
                }
                else
                {
                    currentPath.Add(dir);
                }
            }

            else if (int.TryParse(line.Split(" ")[0], out var size))
            {
                // add size to all in path?
                var copy = currentPath.Select(x => x).ToList();
                while (copy.Count > 0)
                {
                    sizes.AddOrUpdate(CreateKey(copy), size);
                    copy.Pop();
                }
            }
        }

        var max = 100000;
        sizes.Where(x => x.Value <= max).Sum(x => x.Value).Dump();


        string CreateKey(List<string> path)
        {
            return string.Join(",", path);
        }
    }

    [Test]
    public void day7_2_2022()
    {
        var lines = FP.ReadLines("./inputs/2022/day7.txt");

        Dictionary<string, int> sizes = new();
        List<string> currentPath = new();
        foreach (var line in lines)
        {
            // change directory
            if (line.StartsWith("$ cd"))
            {
                var (_, _, dir) = line.Split(" ");
                if (dir == "..")
                {
                    currentPath.Pop();
                }
                else
                {
                    currentPath.Add(dir);
                }
            }

            else if (int.TryParse(line.Split(" ")[0], out var size))
            {
                // add size to all in path
                var copy = currentPath.Select(x => x).ToList();
                while (copy.Count > 0)
                {
                    sizes.AddOrUpdate(CreateKey(copy), size);
                    copy.Pop();
                }
            }
        }

        var totalAvailable = 70000000;
        var spaceNeeded = 30000000;

        var spaceUsedCurrently = sizes["/"];

        var currentUnusedSpace = totalAvailable - spaceUsedCurrently;
        var needsFreed = spaceNeeded - currentUnusedSpace;

        sizes
            .Select(x => x.Value)
            .OrderBy(x => x)
            .First(x => x >= needsFreed)
            .Dump();

        string CreateKey(List<string> path)
        {
            return string.Join(",", path);
        }
    }

    [Test]
    public void day8_1_2022()
    {
        var g = FP.ReadAsGrid("./inputs/2022/day8.txt");

        // all edges
        var visibleTrees = g.Count * 4 - 4;

        var grid = new Grid(g);

        grid.ForEachWithCoord((val, coord) =>
        {
            if (
                !(coord.x == 0 || coord.y == 0 || coord.x == grid.Width - 1 || coord.y == grid.Height - 1) &&
                (grid.GetAllValuesDownFromCoord(coord).All(x => x < val) ||
                grid.GetAllValuesUpFromCoord(coord).All(x => x < val) ||
                grid.GetAllValuesLeftOfCoord(coord).All(x => x < val) ||
                grid.GetAllValuesRightOfCoord(coord).All(x => x < val)))
            {
                visibleTrees += 1;
            }
        });

        visibleTrees.Dump();
    }


    [Test]
    public void day8_2_2022()
    {
        var g = FP.ReadAsGrid("./inputs/2022/day8.txt");

        var maxScore = 0;
        var grid = new Grid(g);

        grid.ForEachWithCoord((val, coord) =>
        {
            var currentScore = 1;
            var up = grid.GetAllValuesUpFromCoord(coord).Enumerate().FirstOrDefault(x => x.val >= val);
            currentScore *= (up == default ? coord.x : up.index + 1);

            var left = grid.GetAllValuesLeftOfCoord(coord).Enumerate().FirstOrDefault(x => x.val >= val);
            currentScore *= (left == default ? coord.y : left.index + 1);

            var right = grid.GetAllValuesRightOfCoord(coord).Enumerate().FirstOrDefault(x => x.val >= val);
            currentScore *= (right == default ? grid.Width - 1 - coord.y : right.index + 1);

            var down = grid.GetAllValuesDownFromCoord(coord).Enumerate().FirstOrDefault(x => x.val >= val);
            currentScore *= (down == default ? grid.Height - 1 - coord.x : down.index + 1);

            maxScore = Math.Max(maxScore, currentScore);
        });

        maxScore.Dump();
    }

    [Test]
    public void day9_1_2022()
    {
        var visted = new HashSet<Coord>();

        var headPos = new Coord(0, 0);
        var tailPos = new Coord(0, 0);
        var lines = FP.ReadLines("./inputs/2022/day9.txt");

        /*If the head is ever two steps directly up, down, left, or right from the tail, the tail must also move one step in that direction so it remains close enough:
         Otherwise, if the head and tail aren't touching and aren't in the same row or column, the tail always moves one step diagonally to keep up*/
        visted.Add(tailPos);
        foreach (var line in lines)
        {
            var (dir, dists) = line.Split(' ');
            var dist = int.Parse(dists);

            for (int i = 0; i < dist; i++)
            {
                headPos = dir switch
                {
                    "U" => headPos with { x = headPos.x - 1 },
                    "D" => headPos with { x = headPos.x + 1 },
                    "R" => headPos with { y = headPos.y + 1 },
                    "L" => headPos with { y = headPos.y - 1 },
                    _ => throw new InvalidOperationException(),
                };

                if (Math.Abs(headPos.x - tailPos.x) == 2 && headPos.y == tailPos.y) // vert
                {
                    tailPos = tailPos with { x = tailPos.x + (headPos.x > tailPos.x ? 1 : -1) };
                }
                else if (Math.Abs(headPos.y - tailPos.y) == 2 && headPos.x == tailPos.x) // horizontal
                {
                    tailPos = tailPos with { y = tailPos.y + (headPos.y > tailPos.y ? 1 : -1) };
                }
                else if (Math.Abs(headPos.x - tailPos.x) == 2 && headPos.y != tailPos.y || Math.Abs(headPos.y - tailPos.y) == 2 && headPos.x != tailPos.x) //diag 
                {
                    tailPos = new Coord(tailPos.x + (headPos.x > tailPos.x ? 1 : -1), headPos.y > tailPos.y ? tailPos.y + 1 : tailPos.y - 1);
                }

                visted.Add(tailPos);
            }
        }

        visted.Count.Dump();
    }

    [Test]
    public void day9_2_2022()
    {
        var visted = new HashSet<Coord>();

        var tail = Enumerable.Repeat(1, 10).Select(_ => new Coord(0, 0)).ToList();
        var lines = FP.ReadLines("./inputs/2022/day9.txt");

        visted.Add(tail[^1]);
        foreach (var line in lines)
        {
            var (dir, dists) = line.Split(' ');
            var dist = int.Parse(dists);

            for (int i = 0; i < dist; i++)
            {
                var newHead = dir switch
                {
                    "U" => tail[0] with { x = tail[0].x - 1 },
                    "D" => tail[0] with { x = tail[0].x + 1 },
                    "R" => tail[0] with { y = tail[0].y + 1 },
                    "L" => tail[0] with { y = tail[0].y - 1 },
                    _ => throw new InvalidOperationException(),
                };
                var newTail = new List<Coord>() { newHead };

                // go through the whole tail and apply changes, skip the head as it's already added
                foreach (var coord in tail.Skip(1))
                {
                    if (Math.Abs(newTail[^1].x - coord.x) == 2 && newTail[^1].y == coord.y) // vert
                    {
                        newTail.Add(coord with { x = coord.x + (newTail[^1].x > coord.x ? 1 : -1) });
                    }
                    else if (Math.Abs(newTail[^1].y - coord.y) == 2 && newTail[^1].x == coord.x) // horizontal
                    {
                        newTail.Add(coord with { y = coord.y + (newTail[^1].y > coord.y ? 1 : -1) });
                    }
                    else if (Math.Abs(newTail[^1].x - coord.x) == 2 && newTail[^1].y != coord.y || Math.Abs(newTail[^1].y - coord.y) == 2 && newTail[^1].x != coord.x) //diag 
                    {
                        newTail.Add(new Coord(coord.x + (newTail[^1].x > coord.x ? 1 : -1), newTail[^1].y > coord.y ? coord.y + 1 : coord.y - 1));
                    }
                    else
                    {
                        newTail.Add(coord);
                    }
                }

                visted.Add(newTail[^1]);
                tail = newTail;
            }
        }


        visted.Count.Dump();
    }
}