
using System.Windows.Markup;

namespace Aoc2024;

public class Day12 : Day
{
    public override void Part1()
    {
        var g = new Grid<char>(Text().CharGrid());
        var totalCounted = new HashSet<Coord>();

        var totalPrice = 0;
        g.ForEachWithCoord((v, c) =>
        {
            if (!totalCounted.Contains(c))
            {
                var visited = new HashSet<Coord>();
                var q = new Queue<Coord>();
                q.Enqueue(c);
                while (q.TryDequeue(out var cc))
                {
                    if (!visited.Add(cc))
                    {
                        continue;
                    }

                    foreach (var n in g.GetValidAdjacentNoDiag(cc).Where(e => g[e] == g[cc]))
                    {
                        q.Enqueue(n);
                    }
                }

                var area = visited.Count;
                var perimCost = 0;
                foreach (var vc in visited)
                {
                    totalCounted.Add(vc);

                    var touching = vc
                        .GetValidAdjacentNoDiag(g.Width, g.Height).Count(x => visited.Contains(x));

                    // boundary points should be all points that touch 0,1,2,3 coordinates
                    perimCost += touching switch
                    {
                        0 => 4,
                        1 => 3,
                        2 => 2,
                        3 => 1,
                        4 => 0
                    };
                }

                totalPrice += (area * perimCost);

            }
        });

        totalPrice.Dump();
    }

    public override void Part2()
    {
        var g = new Grid<char>(Text().CharGrid());
        var totalCounted = new HashSet<Coord>();

        var totalPrice = 0;
        g.ForEachWithCoord((v, c) =>
        {
            if (!totalCounted.Contains(c))
            {
                var visited = new HashSet<Coord>();

                var q = new Queue<Coord>();
                q.Enqueue(c);
                while (q.TryDequeue(out var cc))
                {
                    if (!visited.Add(cc))
                    {
                        continue;
                    }

                    foreach (var n in g.GetValidAdjacentNoDiag(cc).Where(e => g[e] == g[cc]))
                    {
                        q.Enqueue(n);
                    }
                }

                var area = visited.Count;
                var corners = 0;
                foreach (var vc in visited)
                {
                    totalCounted.Add(vc);

                    var validAdjacentNoDiag = vc
                        .GetValidAdjacentNoDiag(g.Width, g.Height);
                    var touchingCoord = validAdjacentNoDiag.Where(x => visited.Contains(x)).ToList();

                    var touching = touchingCoord.Count;

                    if (touching == 0)
                    {
                        corners += 4;
                    }
                    else if (touching == 1)
                    {
                        corners += 2;
                    }
                    else if (touching == 2 && touchingCoord[0].r != touchingCoord[1].r && touchingCoord[0].c != touchingCoord[1].c)
                    {
                        // at a corner, could be 2 or 1
                        // get inside corner
                        var ic = new Coord(touchingCoord.Single(x => x.r != vc.r).r, touchingCoord.Single(x => x.c != vc.c).c);
                        var insideCorner = g[ic];

                        if (insideCorner != v)
                        {
                            corners += 2;
                        }
                        else
                        {
                            corners += 1;
                        }
                    }
                    else if (touching == 3)
                    {
                        // find odd one out of 3
                        var offcentre = touchingCoord
                            .Where((x, i) =>
                                (x.r != vc.r && touchingCoord.Where((_, ii) => i != ii).All(x => x.r == vc.r))
                                || (x.c != vc.c && touchingCoord.Where((_, ii) => i != ii).All(x => x.c == vc.c))).Single();


                        var notfillIns = touchingCoord
                            .Where(x => x != offcentre)
                            .Count(e =>
                            {
                                var ir = e.r == vc.r ? offcentre.r : e.r;
                                var ic = e.c == vc.c ? offcentre.c : e.c;
                                return !visited.Contains(new Coord(ir, ic));
                            });
                        
                        corners += notfillIns;
                    }
                    else if (touching == 4)
                    {
                        corners += new List<(int, int)> { (1, 1), (-1, -1), (1, -1), (-1, 1) }
                            .Select(x => new Coord(vc.r + x.Item1, vc.c + x.Item2)).Count(x => !visited.Contains(x));
                    }
                }

                totalPrice += (area * corners);

            }
        });

        totalPrice.Dump();
    }
}
