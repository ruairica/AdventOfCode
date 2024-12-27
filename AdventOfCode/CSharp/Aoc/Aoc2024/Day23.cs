namespace Aoc2024;

public class Day23 : Day
{
    public override void Part1()
    {
        var dict = new Dictionary<string, List<string>>();
        foreach (var line in Text().Lines())
        {
            var (f, l) = line.Split('-');
            dict.AddOrAppend(f, l);
            dict.AddOrAppend(l, f);
        }

        List<List<string>> sets = [];
        foreach (var (k, v) in dict)
        {
            var m1 = v.Where(x => x != k && dict[x].Contains(k)).ToList();
            foreach (var m in m1)
            {
                var m2 = dict[m].Where(x => x != m && dict[x].Contains(k)).ToList();
                foreach (var mm in m2)
                {
                    sets.Add([k, m, mm]);
                }
            }
        }

        var result = sets
            .Select(x => x.OrderBy(x => x).ToList()).Select(x => (x[0], x[1], x[2])).Distinct();

        result.Count(x => x.Item1.StartsWith('t') || x.Item2.StartsWith('t') || x.Item3.StartsWith('t')).Dump();
    }

    public override void Part2()
    {
        var text = Text();

        var dict = new Dictionary<string, List<string>>();
        foreach (var line in text.Lines())
        {
            var (f, l) = line.Split('-');
            dict.AddOrAppend(f, l);
            dict.AddOrAppend(l, f);
        }

        List<List<string>> sets = [];
        foreach (var k in dict.Keys)
        {
            sets.Add(F([], k));
        }

        string.Join(',', sets.MaxBy(x => x.Count).OrderBy(x => x)).Dump();
        return;

        List<string> F(List<string> existingList, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return existingList;
            }

            return F([..existingList, value], dict[value].FirstOrDefault(x => x != value && existingList.All(e => dict[x].Contains(e))));
        }
    }
}
