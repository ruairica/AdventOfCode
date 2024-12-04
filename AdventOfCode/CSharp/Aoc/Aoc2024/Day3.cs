
using System.Text.RegularExpressions;

namespace Aoc2024;

public class Day3 : Day
{
    public override void Part1()
    {

        Part1RegexBetter(Text().Dump());
        Regex.Matches(Text(), @"mul\(\d+,\d+\)")
            .Select(x => x.Value)
            .Select(x => x[4..].TrimEnd(')')) // could use ^1 instead of trimend
            .Sum(x =>
            {
                var splits = x.Split(',');
                return int.Parse(splits[0]) * int.Parse(splits[1]);
            })
            .Dump();
    }

    private int Part1RegexBetter(string text)
    {
        //mul, \( bracket. \d+ some positive digit. put inside bracket for capture group
        // stuff inside brackets is a capture group
        // always use @"" other wise backslashes are  amess
        return Regex.Matches(text, @"mul\((\d+),(\d+)\)")
            .Sum(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value)).Dump();
    }

    public override void Part2()
    {
        var text = Text();

        var matches = Regex.Matches(text, @"mul\(\d+,\d+\)")
            .Select(x =>
        {
            var t = x.Value[4..].TrimEnd(')');
            var splits = t.Split(',');
            return (x.Index, int.Parse(splits[0]) * int.Parse(splits[1]));
        }).ToDictionary(x => x.Index, x => x.Item2);

        var dos = text.AllIndexOf("do()").Select(x => (x, true)).ToList();
        var dont = text.AllIndexOf("don't()").Select(x => (x, false));

        var conditions = dos.Concat(dont)
            .ToDictionary(x => x.x, x => x.Item2);

        var on = true;
        var sum = 0;
        for (var i = 0; i < text.Length; i++)
        {
            if (on && matches.TryGetValue(i, out var ts))
            {
                sum += ts;
            }

            if (conditions.TryGetValue(i, out var c))
            {
                on = c;
            }
        }

        sum.Dump();

        Part2Regex(text).Dump();
    }

    private int Part2Regex(string text)
    {
        // you can do named capture groups if you add ?<> to it, a capture group is defined with brackest
        var matchCollection = Regex.Matches(text, @"(?<instruction>mul\((?<n1>\d+),(?<n2>\d+)\)|do\(\)|don't\(\))");

        var enabled = true;
        var sum = 0;

        foreach (Match match in matchCollection)
        {
            var inst = match.Groups["instruction"];

            switch (inst.Value)
            {
                case "do()":
                    enabled = true;
                    break;
                case "don't()":
                    enabled = false;
                    break;
                default:
                    if (enabled)
                    {
                        sum += int.Parse(match.Groups["n1"].Value) * int.Parse(match.Groups["n2"].Value);
                    }

                    break;
            }
        }

        return sum;
    }
}
