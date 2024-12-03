
using System.Text.RegularExpressions;

namespace Aoc2024;

public class Day3 : Day
{
    public override void Part1()
    {
        Regex.Matches(Text(), @"mul\(\d+,\d+\)")
            .Select(x => x.Value)
            .Select(x => x[4..].TrimEnd(')'))
            .Sum(x =>
            {
                var splits = x.Split(',');
                return int.Parse(splits[0]) * int.Parse(splits[1]);
            })
            .Dump();
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
    }
}
