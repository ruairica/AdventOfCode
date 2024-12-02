using Dumpify;
using Utils;

namespace Aoc2023;

public class Day1 : Day
{
    public override void Part1()
    {
        var input = FP.ReadLines(FilePath);

        input.Select(x =>
            {
                var nums = x.Where(char.IsDigit).ToList();
                return $"{nums[0]}{nums[^1]}";
            })

            .Select(int.Parse).Sum().Dump();

    }

    public override void Part2()
    {
        List<string> numStrings =
        [
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        ];

        var dict = new Dictionary<string, int>();
        numStrings.Index().ToList().ForEach(x => dict.Add(x.val, x.index + 1));

        var collection = Enumerable.Range(1, 9).Select(x => x.ToString()).ToList();
        collection.ForEach(x => dict.Add(x, int.Parse(x)));

        numStrings.AddRange(collection);

        var lines = FP.ReadFile(FilePath).Split("\n");

        lines.Select(
                line => numStrings.SelectMany(
                    x => line.AllIndexOf(x)
                        .Where(e => e != -1)
                        .Select(y => (index: y, str: x))))
            .Sum(
                digits => int.Parse(
                    $"{dict[digits.MinBy(x => x.index).str]}{dict[digits.MaxBy(x => x.index).str]}"))
            .Dump();
    }
}
