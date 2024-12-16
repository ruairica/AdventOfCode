namespace Aoc2024;

public class Day16 : Day
{
    public override void Part1()
    {
        var g = new Grid<char>(Text().CharGrid());

        var start = g.FirstOrDefault(x => x == 'S');
        var end = g.FirstOrDefault(x => x == 'E');

        var visited = new HashSet<(Coord coord, Dir dir)>();
        var q = new PriorityQueue<(Coord coord, Dir dir), int>();
        q.Enqueue((start, Dir.Right), 0);

        while (q.TryDequeue(out var e, out var cost))
        {
            if (e.coord == end)
            {
                cost.Dump();
                break;
            }

            if (!visited.Add((e.coord, e.dir)))
            {
                continue;
            }

            if (g[e.coord] == '#')
            {
                continue;
            }

            q.Enqueue((g.Move(e.coord, e.dir).coord, e.dir), cost + 1);
            q.Enqueue((e.coord, TurnRight(e.dir)), cost + 1000);
            q.Enqueue((e.coord, TurnLeft(e.dir)), cost + 1000);
        }
    }

    public override void Part2()
    {
        var g = new Grid<char>(Text().CharGrid());

        var start = g.FirstOrDefault(x => x == 'S');
        var end = g.FirstOrDefault(x => x == 'E');

        var visited = new Dictionary<(Coord coord, Dir dir), int>();
        var q = new PriorityQueue<(Coord coord, Dir dir, List<Coord> path), int>();
        q.Enqueue((start, Dir.Right, [start]), 0);

        var finalPaths = new HashSet<Coord>();
        var minCost = int.MaxValue;

        while (q.TryDequeue(out var e, out var cost))
        {
            if (e.coord == end)
            {
                if (minCost == int.MaxValue || cost == minCost)
                {
                    minCost = cost;
                    e.path.ForEach(p => finalPaths.Add(p));
                }
                else
                {
                    break;
                }
            }

            if (visited.TryGetValue((e.coord, e.dir), out var mc) && cost > mc)
            {
                continue;
            }

            visited[(e.coord, e.dir)] = cost;

            if (g[e.coord] == '#')
            {
                continue;
            }

            var moveForward = g.Move(e.coord, e.dir).coord;
            q.Enqueue((moveForward, e.dir, [.. e.path, moveForward]), cost + 1);
            q.Enqueue((e.coord, TurnRight(e.dir),e.path), cost + 1000);
            q.Enqueue((e.coord, TurnLeft(e.dir), e.path), cost + 1000);
        }

        finalPaths.Count.Dump();
    }
}
