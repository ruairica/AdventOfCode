using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Windows.Markup;

namespace Aoc2024;

public class Day5 : Day
{
    public override void Part1()
    {
        var text = Text().Split("\n\n");
        var first = text[0];
        var second = text[1];

        var dict = new Dictionary<string, List<string>>();
        foreach (var thing in first.Lines().Select(x =>
                 {
                     var (before, after) = x.Split("|");
                     return (before, after);
                 }))
        {
            dict[thing.after] = dict.GetValueOrDefault(thing.after, []).Concat([thing.before]).Distinct().ToList();
        }

        var sum = 0;
        foreach (var line in second.Lines())
        {
            var illegal = new HashSet<string>();
            var splits = line.Split(",");

            var valid = true;
            foreach (var split in splits)
            {
                if (illegal.Contains(split))
                {
                    valid = false;
                    break;
                }

                if (dict.TryGetValue(split, out var before))
                {
                    before.ForEach(x => illegal.Add(x));
                }
            }

            if (valid)
            {
                sum += int.Parse(splits[splits.Length / 2]);
            }
        }

        sum.Dump();
    }

    public override void Part2()
    {
        var text = Text().Split("\n\n");
        var first = text[0];
        var second = text[1];

        var dict = new Dictionary<string, List<string>>();
        foreach (var thing in first.Lines().Select(x =>
                 {
                     var (before, after) = x.Split("|");
                     return (before, after);
                 }))
        {
            dict[thing.after] = dict.GetValueOrDefault(thing.after, []).Concat([thing.before]).Distinct().ToList();
        }

        var invalids = new List<string>(second.Lines().Where(line => !IsValid(line,dict)));


        var nowvalids = new List<List<string>>();
        foreach (var invalidLine in invalids)
        {
            var els = invalidLine.Split(",");
            var makeValid = new List<string>();

            var firstel = els.First(x => !dict.TryGetValue(x, out var val) || !val.Any(e => els.Contains(e)));
            makeValid.Add(firstel);

            while (makeValid.Count != els.Length)
            {
                makeValid.Add(els.First(x => !makeValid.Contains(x) && !dict.GetValueOrDefault(x, []).Any(cb => els.Where(el => !makeValid.Contains(el) && x != el).Contains(cb))));
            }

            nowvalids.Add(makeValid);
        }

        nowvalids.Sum(v => int.Parse(v[v.Count / 2])).Dump();
    }

    private static bool IsValid(string line, Dictionary<string, List<string>> dict)
    {
        HashSet<string> illegal = new();

        foreach (var split in line.Split(","))
        {
            if (illegal.Contains(split))
            {
                return false;
            }

            if (dict.TryGetValue(split, out var before))
            {
                before.ForEach(x => illegal.Add(x));
            }
        }

        return true;
    }
}
