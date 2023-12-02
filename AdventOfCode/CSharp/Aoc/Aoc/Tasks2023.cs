using System.Text.RegularExpressions;
using Aoc.Utils;
using Dumpify;
using NUnit.Framework;

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
        var lines = FP.ReadFile($"{basePath}/day2.txt").Split("\n");
        lines.Dump();
    }
}