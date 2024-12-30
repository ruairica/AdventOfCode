namespace Aoc2024;

public class Day21 : Day
{
    public override void Part1()
    {
        var lines = Text().Lines();

        var ng = new Grid<char>((char[][])[
            ['7','8','9'],
            ['4','5','6'],
            ['1','2','3'],
            ['.','0','A'],
        ]);

        var ag = new Grid<char>((char[][])[
            ['.', '^', 'A'],
            ['<', 'v', '>'],
        ]);

        var sum = 0L;
        foreach (var line in lines)
        {
            line.Dump();
            var ind = new Coord(3, 2);

            var mins = new List<List<char>>();
            foreach (var (c, index) in line.Index())
            {
                var numWays = new List<(char, List<(Coord, char?)>)>();
                var q = new Queue<(Coord c, List<(Coord, char?)> path)>();
                q.Enqueue((ind, [(ind, null)]));

                while (q.TryDequeue(out var e))
                {
                    if (ng[e.c] == c)
                    {
                        numWays.Add((c, e.path));
                    }

                    var ns = e.c.GetValidAdjacentNoDiagWithDir(ng.Width, ng.Height)
                        .Where(
                            x => ng[x.coord] != '.' && !e.path.Select(nc => nc.Item1)
                                .Contains(x.coord));

                    foreach (var n in ns)
                    {
                        q.Enqueue((n.coord, [..e.path, (n.coord, DirToChar(n.dir))]));
                    }
                }

                ind = ng.FirstOrDefault(x => x == c);

                var pathsToFind = numWays.Select(
                        x => x.Item2.Select(x => x.Item2)
                            .Where(x => x.HasValue)
                            .Select(x => x.Value)
                            .ToList())
                    .ToList();

                pathsToFind = pathsToFind.GroupBy(x => x.Count)
                    .MinBy(x => x.Key)
                    .ToList();

                var start = new Coord(0, 2);
                var r1s = pathsToFind.Select(x => x.Concat(['A']).ToList()).ToList();
                List<List<List<char>>> r2s = [];
                foreach (var p in r1s)
                {
                    var ways = Ways(p, start, ag);
                    r2s.Add(ways);
                }

                List<List<List<char>>> r3s = [];
                foreach (var p in r2s.SelectMany(x => x).GroupBy(x => x.Count).MinBy(x => x.Count()))
                {
                    var ways = Ways(p, start, ag);
                    r3s.Add(ways);
                }

                mins = mins.Count == 0
                    ? r3s.SelectMany(x => x).ToList()
                    : r3s.SelectMany(x => x).SelectMany(x => mins.Select(m => m.Concat(x).ToList()))
                        .ToList();
            }

            $"{mins.Min(x => x.Count)} * {line.Nums()[0]}".Dump();
            sum += ((long)mins.Min(x => x.Count) * line.Nums()[0]).Dump();
        }

        sum.Dump();
    }

    public override void Part2()
    {
        throw new NotImplementedException();
    }

    private List<List<char>> Ways(List<char> pathNeeded, Coord start, Grid<char> grid)
    {
        var q = new Queue<(Coord c, List<char> path, int target, int currentLength)>();
        q.Enqueue((start, [], 0, 0));
        var ways = new List<List<char>>();
        var ttb = int.MaxValue;

        while (q.TryDequeue(out var e))
        {
            if (grid[e.c] == pathNeeded[e.target])
            {
                if (e.target == pathNeeded.Count - 1 )
                {
                    if (ttb == int.MaxValue)
                    {
                        ttb = e.path.Count;
                    }
                    else if (e.path.Count > ttb)
                    {
                        break;
                    }

                    List<char> chars = [..e.path, 'A'];
                    ways.Add(chars);
                }
                else
                {
                    List<char> chars = [..e.path, 'A'];
                    q.Enqueue((e.c, chars, e.target + 1, 0));
                }
            }
            else
            {
                if (e.currentLength > 4)
                {
                    continue;
                }
                var ns = e.c.GetValidAdjacentNoDiagWithDir(grid.Width, grid.Height)
                    .Where(x => grid[x.coord] != '.');

                foreach (var n in ns)
                {
                    q.Enqueue((n.coord, [..e.path, DirToChar(n.dir)], e.target, e.currentLength + 1));
                }
            }
        }

        return ways;
    }

    private char DirToChar(Dir dir) =>
        dir switch
        {
            Dir.Up => '^',
            Dir.Down => 'v',
            Dir.Left => '<',
            Dir.Right => '>',
            _ => throw new Exception($"{dir} out of bounds")
        };
}
