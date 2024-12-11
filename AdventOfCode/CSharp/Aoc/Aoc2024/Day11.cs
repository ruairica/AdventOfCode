namespace Aoc2024;

public class Day11 : Day
{
    public override void Part1() => RunForIterations(25);


    public override void Part2() => RunForIterations(75);

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
}
