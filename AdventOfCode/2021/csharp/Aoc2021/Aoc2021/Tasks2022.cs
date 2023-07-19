using NUnit.Framework;

namespace Aoc2021;

using Aoc2021.utils;
using Dumpify;

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

            if (Char.IsLower(i))
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

            if (Char.IsLower(i))
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
}