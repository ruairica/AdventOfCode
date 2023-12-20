using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using Dumpify;

namespace Aoc.Utils.Grids;

public static class AStarAlgorithm2
{
    public static int ManhattanDistance(Coord current, Coord target)
    {
        return 1;// Math.Abs(current.r - target.r) + Math.Abs(current.c - target.c);
    }

    public static int AStar(Grid<int> grid, Coord start, Coord target, Dir dir)
    {
        HashSet<(Coord, Dir?, int)> closed = new();
        var finalG = new List<int>();
        var openQ = new List<AStarNode2>
        {
            new AStarNode2(
                start.r,
                start.c,
                0,
                0,
                ManhattanDistance(start, target),
                parent: null)
        };

        while (openQ.Count > 0)
        {
            var current = openQ[0];

            var currentIndex = 0;

            // find next node by priority
            // prio queue could remove this but c# prio q doesn't support changing priority mid Q.
            for (var i = 1; i < openQ.Count; i++)
            {
                if (openQ[i].F < current.F ||
                    (openQ[i].F == current.F && openQ[i].LineLength < current.LineLength))
                {
                    current = openQ[i];
                    currentIndex = i;
                }
            }

            openQ.RemoveAt(currentIndex);

            if (closed.Contains(
                    (new Coord(current.Row, current.Column), current.Dir,
                        current.LineLength)))
            {
                continue;
            }

            closed.Add((new Coord(current.Row, current.Column), current.Dir, current.LineLength));
            $"processing {current.Row}, {current.Column}, {current.Dir}, {current.LineLength}, {current.G}".Dump();

            if (current.Row == target.r && current.Column == target.c)
            {
                var printing = current;

                var strings = new List<string>();

                var t = new List<(int, int)>();
                while (true)
                {
                    strings.Add(
                        $"r:{printing.Row}, c:{printing.Column}, d:{printing.Dir}, g:{printing.G}");

                    t.Add((printing.Row, printing.Column));
                    printing = printing.Parent;

                    if (printing is null)
                    {
                        break;
                    }
                }

                strings.Reverse();
                strings.ForEach(x => x.Dump());
                finalG.Add(current.G);

                foreach (var a in t)
                {
                    grid.grid[a.Item1][a.Item2] = 0;
                }
                break;

                //grid.Print();

                openQ.ForEach(x => $"{x.Row}, {x.Column}".Dump());

                continue;
            }

            // var neighbourCoords = grid
            //     .GetValidAdjacentNoDiagWithDir(new Coord(current.Row, current.Column))
            //     .Where(c => !closed.Contains(c));

            var neighbourCoords = new Coord(current.Row, current.Column)
                .GetValidAdjacentNoDiagWithDir(grid.Width, grid.Height)
                .ToList();


            var nl = new List<(Coord, Dir, int)>();
            if (current.LineLength == 2)
            {
                $"max line lenght===================================== {current.Dir}, {current.Parent?.Dir}, {current.Parent?.Parent?.Dir}".Dump();
                if (current.Dir is Dir.Down or Dir.Up)
                {
                    nl = neighbourCoords
                        .Where(x => x.dir is Dir.Left or Dir.Right)
                        .Select(x => (x.coord,x.dir, 0))
                        .ToList();
                }
                else
                {
                    nl = neighbourCoords.Where(x => x.dir is Dir.Down or Dir.Up)
                        .Select(x => (x.coord, x.dir, 0))

                        .ToList();
                }
            }
            else
            {
                var opposite = current.Dir switch
                {
                    Dir.Up => Dir.Down,
                    Dir.Down => Dir.Up,
                    Dir.Left => Dir.Right,
                    Dir.Right => Dir.Left,
                    _ => throw new Exception("invalid dir")
                };
                "normal".Dump();
                nl = neighbourCoords.Where(x => x.dir != opposite)
                    .Select(x =>
                    {
                        var cl = current.LineLength;
                        var y = (x.coord, x.dir,
                                x.dir == current.Dir ? (cl + 1) : 0);

                        $"{current.Dir}, {y.dir}, {y.Item3}".Dump();
                        return y;
                    }).ToList();
            }

            // can't go backwards, can only got at most 3 forward
            // get neighbor coords,
            // check parent for direction by looking at parent,
            // check straight count, to see if you can go forwards,
            //$"--current: {current.Row}, {current.Column}. {current.Dir}".Dump();
            foreach (var (coord, d, l) in nl)
            {
               $"---{current.LineLength}  valid neighbour : {coord.r}, {coord.c}, {d}, {l}".Dump();
            }

            foreach (var (nc, nd, ll) in nl)
            {
                var gScore = current.G + grid[nc];

                var neighbour =
                    openQ.FirstOrDefault(x => x.Row == nc.r && x.Column == nc.c && x.LineLength == ll);

                if (neighbour is null)
                {
                    if (current.Dir == nd && current.Parent?.Dir == nd &&
                        current.Parent?.Parent?.Dir == nd)
                    {
                        //$"{nc.r}, {nc.c} : had to continue past loop {current.Dir}, {current.Parent?.Dir}, {current.Parent?.Parent?.Dir}"
                           // .Dump();
                        //continue;
                    }

                    openQ.Add(
                        new AStarNode2(
                            nc.r,
                            nc.c,
                            ll,
                            nd,
                            gScore,
                            ManhattanDistance(nc, target),
                            current));
                }
                else if (gScore < neighbour.G)
                {
                    throw new Exception("shouldn't happen");    
                    var updatedDir = (neighbour.Row - current.Row,
                            neighbour.Column - current.Column) switch
                        {
                            (0, 1) => Dir.Right,
                            (0, -1) => Dir.Left,
                            (1, 0) => Dir.Down,
                            (-1, 0) => Dir.Up,
                            _ => throw new Exception("invalid dir") 
                        };

                    if (updatedDir == current.Dir && current.Dir == current.Parent?.Dir && current.Parent?.Dir == current.Parent?.Parent?.Dir)
                    {
                        continue;
                    }

                    $"---------------------updated {nc.r}, {nc.c}".Dump();
                    neighbour.Dir = updatedDir;
                    neighbour.G = gScore;
                    neighbour.Parent = current;
                }
                else
                {
                    $"current g, {current.G}, existing g: {neighbour.G}".Dump();
                }
            }
        }

        // No path found
        finalG.Dump();

        foreach (var (coord, _, _) in closed)
        {
            //grid.grid[coord.r][coord.c] = 0;
        }

        grid.Print();
        return -1;
    }

    public static bool ContainsCoord(AStarNode2 node, Coord coord)
    {
        var parent = node.Parent;

        while (parent is not null)
        {
            if (parent.Row == coord.r && parent.Column == coord.c)
            {
                return true;
            }

            parent = parent.Parent;
        }
        return false;
    }
}

public class AStarNode2
{
    public int Row { get; set; }
    public int Column { get; set; }
    public int F => G + H; // Estimated total cost
    public int G { get; set; } // Cost from start node
    public int H { get; set; } // Heuristic estimate to target node
    public Dir? Dir { get; set; } // Number of straight moves
    public AStarNode2? Parent { get; set; }
    public int LineLength { get; set; }

    public AStarNode2(
        int row,
        int column,
        int ll,
        Dir? dir,
        int g = 0,
        int h = 0,
        AStarNode2? parent = null)
    {
        Row = row;
        Column = column;
        G = g;
        H = h;
        Parent = parent;
        Dir = dir;
        LineLength = ll;
    }
}