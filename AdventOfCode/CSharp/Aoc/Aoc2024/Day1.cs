namespace Aoc2024;

public class Day1 : Day
{
    public override void Part1()
    {
        var input = Text()
            .Lines()
            .Select(line => line.Nums())
            .Select(x => (x[0], x[1]))
            .ToList();

        var line1 = input.Select(x => x.Item1).OrderBy(x => x);
        var line2 = input.Select(x => x.Item2).OrderBy(x => x);

        line1.Zip(line2, (i1, i2) => Math.Abs(i1 - i2))
            .Sum()
            .Dump();
    }

    public override void Part2()
    {
        var input = Text()
            .Lines()
            .Select(line => line.Nums())
            .Select(x => (x[0], x[1]))
            .ToList();

        var line1 = input.Select(x => x.Item1);
        var line2 = input
            .Select(x => x.Item2)
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());

        line1.Select(x => x * line2.GetValueOrDefault(x, 0)).Sum().Dump();
    }
}
