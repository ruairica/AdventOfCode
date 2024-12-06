using System.Reflection.Metadata.Ecma335;

namespace Aoc2024;

public class Day4 : Day
{
    public override void Part1()
    {
        var text = Text();
        var count = 0;
        var g = new Grid<char>(text.CharGrid());

        count += g.GetAllRows().Sum(Count);

        // vert
        count += g.GetAllColumns().Sum(Count);

        // go across the top row
        foreach (var c in Range(0, g.Width))
        {
            count += diagcount(new Coord(0, c), g);
        }

        // go down LHS and RHS  columns, skipping first element to avoid overlap with above
        foreach (var c in Range(1, g.Height - 1))
        {
            foreach (var cc in new List<Coord> { new(c, 0), new(c, g.Width - 1) })
            {
                count += diagcount(cc, g);
            }
        }

        count.Dump();

        int Count(List<char> x)
        {
            var s = string.Join("", x);
            return s.AllIndexOf("XMAS").Count + s.AllIndexOf("SAMX").Count;
        }

        Part1Redo().Dump();
    }

    private int Part1Redo()
    {
        var g = new Grid<char>(Text().CharGrid());
        var count = 0;
        g.ForEachWithCoord((c, coord) =>
        {
            if (c != 'X')
            {
                return;
            }

            count += GridExtensions.dirsWithDiags
                .Select(dir =>
                    Range(1, 3).Select(x => new Coord(coord.r + x * dir.r, coord.c + x * dir.c)).ToList())
                .Where(path => path.All(x => g.InBounds(x)))
                .Count(path => g[path[0]] == 'M' && g[path[1]] == 'A' && g[path[2]] == 'S');
        });
        return count;
    }

    public override void Part2()
    {
        var text = Text();
        var g = new Grid<char>(text.CharGrid());
        var points = new List<Coord>();

        foreach (var c in Range(0, g.Width))
        {
            points.AddRange(findCentresOfX(new Coord(0, c), g));
        }

        foreach (var c in Range(1, g.Height - 1))
        {
            foreach (var cc in new List<Coord> { new(c, 0), new(c, g.Width - 1) })
            {
                points.AddRange(findCentresOfX(cc, g));
            }
        }

        var dict = points.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

        var total = 0;
        total += dict.Count(x => x.Value == 2);
        total += dict.Count(x => x.Value == 4) * 2;
        total.Dump();
    }

    private static List<Coord> findCentresOfX(Coord cc, Grid<char> g)
    {
        List<string> dirs = ["downright", "downleft"];
        var ind = new List<Coord>();

        foreach (var dir in dirs)
        {
            var ncc = cc;
            var l = new List<char>();
            var l2 = new List<Coord>();
            l.Add(g[ncc]);
            l2.Add(ncc);
            var valid = true;
            while (valid)
            {
                var (mv, mc) = MoveDiag(g, ncc, dir);

                if (mv)
                {
                    l.Add(g[mc]);
                    l2.Add(mc);
                    ncc = mc;
                }
                else
                {
                    var s = string.Join("", l);
                    ind.AddRange(s.AllIndexOf("MAS").Concat(s.AllIndexOf("SAM")).Select(x => l2[x + 1]));
                    valid = false;
                }
            }
        }

        return ind;
    }

    private static int diagcount(Coord cc, Grid<char> g)
    {
        List<string> dirs = ["downright", "downleft"];

        var count = 0;
        foreach (var dir in dirs)
        {
            var ncc = cc;
            var l = new List<char> { g[ncc] };
            var valid = true;
            while (valid)
            {
                var (mv, mc) = MoveDiag(g, ncc, dir);

                if (mv)
                {
                    l.Add(g[mc]);
                    ncc = mc;
                }
                else
                {
                    var s = string.Join("", l);
                    var i = s.AllIndexOf("XMAS").Count + s.AllIndexOf("SAMX").Count;
                    count += i;
                    valid = false;
                }
            }
        }

        return count;
    }

    public static (bool valid, Coord coord) MoveDiag(Grid<char> g, Coord coord, string dir)
    {
        var newCoord = dir switch
        {
            "downright" => new Coord(coord.r + 1, coord.c + 1),
            "downleft" => new Coord(coord.r + 1, coord.c - 1),
            _ => throw new NotSupportedException()
        };

        return (
            !(newCoord.r < 0 || newCoord.r >= g.Height || newCoord.c < 0 ||
              newCoord.c >= g.Width), newCoord);
    }
}
