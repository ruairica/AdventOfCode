namespace Aoc2024;

public class Day18 : Day 
{
    public override void Part1()
    {
        var blocks= Text().Lines().Select(x =>
        {
            var splits = x.Split(',');
            return new Coord(int.Parse(splits[1]), int.Parse(splits[0]));
        }).Take(1024).ToHashSet();

        var start = new Coord(0, 0);
        var end = new Coord(70, 70);

        var q = new Queue<(Coord c, int d)>();
        q.Enqueue((start, 0));
        var visited = new HashSet<Coord>();
        while (q.TryDequeue(out var e))
        {
            if (e.c == end)
            {
                e.d.Dump();
                return;
            }

            if (!visited.Add(e.c))
            {
                continue;
            }

            foreach (var n in e.c.GetValidAdjacentNoDiag(71, 71)
                         .Where(x => !blocks.Contains(x)))
            {
                q.Enqueue((n, e.d + 1));
            }

        }

        "not found".Dump();
    }

    public override void Part2()
    {
        var allBlocks = Text().Lines().Select(x =>
        {
            var splits = x.Split(',');
            return new Coord(int.Parse(splits[1]), int.Parse(splits[0]));
        }).ToList();

        var blockingIndex = Range(0, allBlocks.Count)
            .First(x => x > 1024 && !HasPath(allBlocks.Take(x).ToHashSet()));

        $"{allBlocks[blockingIndex-1].c},{allBlocks[blockingIndex-1].r}".Dump();
    }

    private bool HasPath(HashSet<Coord> blocks)
    {
        var start = new Coord(0, 0);
        var end = new Coord(70, 70);

        var q = new Queue<(Coord c, int d)>();
        q.Enqueue((start, 0));
        var visited = new HashSet<Coord>();
        while (q.TryDequeue(out var e))
        {
            if (e.c == end)
            {
                return true;
            }

            if (!visited.Add(e.c))
            {
                continue;
            }

            foreach (var n in e.c.GetValidAdjacentNoDiag(71, 71)
                         .Where(x => !blocks.Contains(x)))
            {
                q.Enqueue((n, e.d + 1));
            }
        }

        return false;
    }
}
