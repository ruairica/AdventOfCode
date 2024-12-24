namespace Aoc2024;

public class Day22 : Day
{
    public override void Part1()
    {
        Text()
            .Longs()
            .Select(n => Range(0, 2000)
                .Aggregate(n, (acc, _) =>
                {
                    var a = acc * 64;
                    acc ^= a;
                    acc %= 16777216;

                    var b = acc / 32;
                    acc ^= b;
                    acc %= 16777216;

                    var c = acc * 2048;
                    acc ^= c;
                    acc %= 16777216;

                    return acc;
                })
            )
            .Sum()
            .Dump();
    }

    public override void Part2()
    {
        var listOfResults = Text()
            .Longs()
            .Select(n => Range(0, 2000)
                .Aggregate(new List<long> { n }, (list, _) =>
                {
                    var acc = list[^1];
                    var a = acc * 64;
                    acc ^= a;
                    acc %= 16777216;

                    var b = acc / 32;
                    acc ^= b;
                    acc %= 16777216;

                    var c = acc * 2048;
                    acc ^= c;
                    acc %= 16777216;
                    list.Add(acc);
                    return list;
                })
                .Select(x => x % 10)
                .ToList()
            );

        List<Dictionary<(long, long, long, long), long>> map = [];
        foreach (var l in listOfResults)
        {
            var dict = new Dictionary<(long, long, long, long), long>();

            var diffs = l.Skip(1).Select((val, index) => val - l[index]).ToList();
            foreach (var d in diffs.Index().Skip(3))
            {
                var tup = (diffs[d.index - 3], diffs[d.index - 2], diffs[d.index - 1], d.val);
                if (!dict.ContainsKey(tup))
                {
                    dict.Add(tup, l[d.index + 1]);
                }
            }

            map.Add(dict);
        }

        map.SelectMany(x => x.Select(x => (x.Key, x.Value)))
            .GroupBy(x => x.Key, (a, b) => b.Sum(x => x.Value))
            .Max()
            .Dump();
    }
}
