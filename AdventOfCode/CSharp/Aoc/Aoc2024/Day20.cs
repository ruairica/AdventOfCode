namespace Aoc2024;

public class Day20 : Day
{
    public override void Part1()
    {
        var g = new Grid<char>(Text().CharGrid());

        var start = g.FirstOrDefault(x => x == 'S');
        var end = g.FirstOrDefault(x => x == 'E');
        g[start] = '.';
        g[end] = '.';

        var seen = new HashSet<Coord>();
        var q = new Queue<(Coord c, int seconds)>();
        q.Enqueue((start, 0));
        var ttb = int.MaxValue;
        var dict = new Dictionary<Coord, int>();

        while (q.TryDequeue(out var e))
        {
            if (e.c == end)
            {
                ttb = e.seconds;
                dict.Add(e.c, e.seconds);
                break;
            }

            if (!seen.Add(e.c))
            {
                continue;
            }

            dict.Add(e.c, e.seconds);

            foreach (var n in g.GetValidAdjacentNoDiag(e.c))
            {
                if (g[n] != '#')
                {
                    q.Enqueue((n, e.seconds + 1));
                }
            }
        }

        var cheatScores = new List<int>();
        foreach (var (c, s) in dict)
        {
            var ns = c.GetValidAdjacentNoDiagWithDir(g.Width, g.Height);

            foreach (var (n, d) in ns)
            {
                if (g[n] == '#' && dict.TryGetValue(g.Move(n, d).coord, out var score2))
                {
                    var newScore = s + (ttb - score2) + 2;
                    cheatScores.Add(newScore);
                }
            }
        }

        cheatScores.Count(x => x < ttb - 99).Dump();
    }

    public override void Part2()
    {
        var g = new Grid<char>(Text().CharGrid());

        var start = g.FirstOrDefault(x => x == 'S');
        var end = g.FirstOrDefault(x => x == 'E');
        g[start] = '.';
        g[end] = '.';

        var seen = new HashSet<Coord>();
        var q = new Queue<(Coord c, int seconds)>();
        q.Enqueue((start, 0));
        var ttb = int.MaxValue;
        var dict = new Dictionary<Coord, int>();
        while (q.TryDequeue(out var e))
        {
            if (e.c == end)
            {
                ttb = e.seconds;
                dict.Add(e.c, e.seconds);
                break;
            }

            if (!seen.Add(e.c))
            {
                continue;
            }

            dict.Add(e.c, e.seconds);

            foreach (var n in g.GetValidAdjacentNoDiag(e.c).Where(n => g[n] != '#'))
            {
                q.Enqueue((n, e.seconds + 1));
            }
        }

        var cheatScores = new List<int>();
        foreach (var (c, s) in dict)
        {
            var ns = dict
                .Where(x => c.ManhattanDistance(x.Key) <= 20 && g[x.Key] == '.')
                .Select(x => (c: x.Key, s: x.Value, d: c.ManhattanDistance(x.Key)));

            foreach (var n in ns)
            {
                var newScore = s + (ttb - n.s) + n.d;
                cheatScores.Add(newScore);
            }
        }

        cheatScores
            .Select(x => ttb - x)
            .Count(x => x >= 100)
            .Dump();
    }
}
