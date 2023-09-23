namespace Aoc;

using System.Text.RegularExpressions;
using Aoc.Utils;
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
        var chars = File.ReadAllText("./inputs/2022/day6.txt")
            .Replace("\r\n", "\n")
            .Trim();

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
}