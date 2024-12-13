namespace Aoc2024;

public class Day11 : Day
{
    public override void Part1() => RunForIterations(25);


    public override void Part2()
    {
        RunForIterations(75);
        AlternativeSolution();
    }

    private void RunForIterations(int iterations)
    {
        var dict = Text().Longs().ToDictionary(x => x, _ => (long)1);

        foreach (var _ in Range(1, iterations))
        {
            var newDict = new Dictionary<long, long>();
            foreach (var (k, kcount) in dict)
            {
                if (k == 0)
                {
                    newDict.AddOrIncrement(1, kcount);
                }
                else
                {
                    var s = k.ToString();
                    if (s.Length % 2 == 0)
                    {
                        var first = s[..(s.Length / 2)];
                        var second = s[(s.Length / 2)..];

                        newDict.AddOrIncrement(long.Parse(second), kcount);
                        newDict.AddOrIncrement(long.Parse(first), kcount);
                    }
                    else
                    {
                        newDict.AddOrIncrement(k * 2024, kcount);
                    }
                }
            }

            dict = newDict;
        }

        dict.Sum(x => x.Value).Dump();

    }


    private void AlternativeSolution()
    {
        Dictionary<(long, int), long> cache = new();

        Text().Longs().Sum(x => Count(x, 2)).Dump();
        // if we put stone though step, how long is the resulting list
        long Count(long stone, int step)
        {
            long res;
            if (step == 0)
            {
                return 1; // nothing to do, we just get element back so that length is 1
            }

            if (cache.TryGetValue((stone, step), out var v))
            {
                return v;
            }

            var s = stone.ToString();
            if (stone == 0)
            {
                res = Count(1, step - 1);
            }
            else if (s.Length % 2 == 0)
            {
                var first = s[..(s.Length / 2)];
                var second = s[(s.Length / 2)..];

                res = Count(long.Parse(second), step - 1) + Count(long.Parse(first), step - 1);
            }
            else
            {
                res = Count(stone * 2024, step - 1);
            }

            cache.Add((stone, step), res);
            return res;
        }
    }
}
