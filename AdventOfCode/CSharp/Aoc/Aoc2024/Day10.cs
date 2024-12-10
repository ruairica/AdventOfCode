namespace Aoc2024;

public class Day10 : Day
{
    public override void Part1()
    {
        // sum of trail scores
        // amount of 9s that can be reached by going one step, no diags
        // 9 must be unique within trail
        var g = new Grid<int>(Text().Grid());
        g.WhereWithCoord((v, _) => v == 0)
            .Select(thead =>
            {
                var summits = new HashSet<Coord>();
                var visited = new HashSet<Coord>();

                var q = new Queue<Coord>();
                q.Enqueue(thead.Coord);
                while (q.TryDequeue(out var cc))
                {
                    var cv = g[cc];
                    if (cv == 9)
                    {
                        summits.Add(cc);
                        continue;
                    }

                    if (!visited.Add(cc))
                    {
                        continue;
                    }

                    foreach (var n in g.GetValidAdjacentNoDiag(cc).Where(e => g[e] == cv + 1))
                    {
                        q.Enqueue(n);
                    }
                }

                return summits.Count;
            })
            .Sum()
            .Dump();
    }

    public override void Part2()
    {
        // ratings is the number of distinct paths to a 9 from a 0, add them all up
        var g = new Grid<int>(Text().Grid());
        g.WhereWithCoord((v, _) => v == 0)
            .Select(thead =>
            {
                var summits = new List<Coord>();
                var q = new Queue<Coord>();
                q.Enqueue(thead.Coord);
                while (q.TryDequeue(out var cc))
                {
                    var cv = g[cc];
                    if (cv == 9)
                    {
                        summits.Add(cc);
                        continue;
                    }

                    foreach (var n in g.GetValidAdjacentNoDiag(cc).Where(e => g[e] == cv + 1))
                    {
                        q.Enqueue(n);
                    }
                }

                return summits.Count;
            })
            .Sum()
            .Dump();
    }
}
