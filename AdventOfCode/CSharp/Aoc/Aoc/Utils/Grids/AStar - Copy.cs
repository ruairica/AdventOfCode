using System.Linq;
using System.Runtime.CompilerServices;
using Dumpify;

namespace Aoc.Utils.Grids;

public static class AStarAlgorithm2
{
    public static int ManhattanDistance(Coord current, Coord target)
    {
        return Math.Abs(current.r - target.r) + Math.Abs(current.c - target.c);
    }

    public static int AStar(Grid<int> grid, Coord start, Coord target, Dir dir)
    {
        HashSet<(Coord, Dir?)> closed = new();
        var finalG = new List<int>();
        var openQ = new List<AStarNode2>
        {
            new AStarNode2(
                start.r,
                start.c,
                dir,
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
                    (openQ[i].F == current.F && openQ[i].H < current.H))
                {
                    current = openQ[i];
                    currentIndex = i;
                }
            }

            openQ.RemoveAt(currentIndex);

            closed.Add((new Coord(current.Row, current.Column), current.Dir));
            $"processing {current.Row}, {current.Column}, {current.Dir}".Dump();

            if (current.Row == target.r && current.Column == target.c)
            {
                var printing = current;

                var strings = new List<string>();

                while (true)
                {
                    strings.Add(
                        $"r:{printing.Row}, c:{printing.Column}, d:{printing.Dir}, g:{printing.G}");

                    printing = printing.Parent;

                    if (printing is null)
                    {
                        break;
                    }
                }

                strings.Reverse();
                strings.ForEach(x => x.Dump());

                return current.G;
            }

            // var neighbourCoords = grid
            //     .GetValidAdjacentNoDiagWithDir(new Coord(current.Row, current.Column))
            //     .Where(c => !closed.Contains(c));

            var neighbourCoords = new Coord(current.Row, current.Column)
                .GetValidAdjacentNoDiagWithDir(grid.Width, grid.Height)
                .ToList();

            if (current.Dir == current.Parent?.Dir &&
                current.Parent?.Dir == current.Parent?.Parent?.Dir)
            {
                //$"max line lenght===================================== {current.Dir}, {current.Parent?.Dir}, {current.Parent?.Parent?.Dir}".Dump();
                if (current.Dir is Dir.Down or Dir.Up)
                {
                                        neighbourCoords = neighbourCoords.Where(x => x.dir is Dir.Left or Dir.Right)
                        .ToList();
                }
                else
                {
                    neighbourCoords = neighbourCoords.Where(x => x.dir is Dir.Down or Dir.Up)
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
                neighbourCoords = neighbourCoords.Where(x => x.dir != opposite).ToList();
            }

            // can't go backwards, can only got at most 3 forward
            // get neighbor coords,
            // check parent for direction by looking at parent,
            // check straight count, to see if you can go forwards,
            //$"--current: {current.Row}, {current.Column}. {current.Dir}".Dump();
            //foreach (var item in neighbourCoords)
            {
               // $"---  valid neighbour : {item.coord.r}, {item.coord.c}. {item.dir}".Dump();
            }

            foreach (var (nc, nd) in neighbourCoords)
            {
                var gScore = current.G + grid[nc];

                var neighbour =
                    openQ.FirstOrDefault(x => x.Row == nc.r && x.Column == nc.c);

                if (neighbour is null)
                {
                    if (current.Dir == nd && current.Parent?.Dir == nd &&
                        current.Parent?.Parent?.Dir == nd)
                    {
                        //$"{nc.r}, {nc.c} : had to continue past loop {current.Dir}, {current.Parent?.Dir}, {current.Parent?.Parent?.Dir}"
                           // .Dump();
                        continue;
                    }

                    openQ.Add(
                        new AStarNode2(
                            nc.r,
                            nc.c,
                            nd,
                            gScore,
                            ManhattanDistance(nc, target),
                            current));
                }
                else if (gScore < neighbour.G)
                {
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

                    //$"---------------------updated {nc.r}, {nc.c}".Dump();
                    neighbour.Dir = updatedDir;
                    neighbour.G = gScore;
                    neighbour.Parent = current;
                }
            }
        }

        // No path found
        finalG.Dump();
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

    public AStarNode2(
        int row,
        int column,
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
    }
}