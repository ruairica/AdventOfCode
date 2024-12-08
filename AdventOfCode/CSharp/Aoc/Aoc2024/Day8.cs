namespace Aoc2024;

public class Day8 : Day
{
    public override void Part1()
    {
        var text = Text();
        var g = new Grid<char>(text.CharGrid());
        var antiNodes = new HashSet<Coord>();
        var antennaMap = new Dictionary<char, List<Coord>>();

        g.ForEachWithCoord((c, coord) =>
        {
            if (c != '.')
            {
                antennaMap[c] = antennaMap.GetValueOrDefault(c, []).Concat([coord]).ToList();
            }
        });


        foreach (var (k, v) in antennaMap)
        {
            var points = v;

            // combinations taken from day11_2023
            var combinations = points.SelectMany(
                (x, i) => points.Skip(i + 1),
                Tuple.Create);

            foreach (var (c1,c2) in combinations)
            {
                var rDiff1 = c1.r - c2.r;
                var cDiff1 = c1.c - c2.c;
                var anti1 = new Coord(c1.r + rDiff1, c1.c + cDiff1);

                if (g.InBounds(anti1))
                {
                    antiNodes.Add(anti1);
                }

                var rDiff2 = c2.r - c1.r;
                var cDiff2 = c2.c - c1.c;
                var anti2 = new Coord(c2.r + rDiff2, c2.c + cDiff2);

                if (g.InBounds(anti2))
                {
                    antiNodes.Add(anti2);

                }
            }
        }

        antiNodes.Count.Dump();
    }

    public override void Part2()
    {
        var text = Text();
        var g = new Grid<char>(text.CharGrid());
        var antiNodes = new HashSet<Coord>();
        var antennaMap = new Dictionary<char, List<Coord>>();

        g.ForEachWithCoord((c, coord) =>
        {
            if (c != '.')
            {
                antennaMap[c] = antennaMap.GetValueOrDefault(c, []).Concat([coord]).ToList();
            }
        });

        foreach (var (k, v) in antennaMap)
        {
            var points = v;

            // combinations taken from day11_2023
            var combinations = points.SelectMany(
                (x, i) => points.Skip(i + 1),
                Tuple.Create);

            foreach (var (c1, c2) in combinations)
            {
                var rDiff1 = c1.r - c2.r;
                var cDiff1 = c1.c - c2.c;
                var anti1 = new Coord(c1.r + rDiff1, c1.c + cDiff1);

                while (g.InBounds(anti1))
                {
                    antiNodes.Add(anti1);
                    anti1 = new Coord(anti1.r + rDiff1, anti1.c + cDiff1);
                }

                var rDiff2 = c2.r - c1.r;
                var cDiff2 = c2.c - c1.c;
                var anti2 = new Coord(c2.r + rDiff2, c2.c + cDiff2);
                while (g.InBounds(anti2))
                {
                    antiNodes.Add(anti2);
                    anti2 = new Coord(anti2.r + rDiff2, anti2.c + cDiff2);
                }
            }
        }

        antiNodes.Concat(
            antennaMap.SelectMany(x => x.Value)
        ).Distinct().Count().Dump();

    }
}
