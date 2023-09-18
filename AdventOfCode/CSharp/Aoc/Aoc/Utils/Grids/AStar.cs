namespace Aoc.Utils.Grids;

public static class AStarAlgorithm
{
    static int ManhattanDistance(Coord current, Coord target)
    {
        return Math.Abs(current.x - target.x) + Math.Abs(current.y - target.y);
    }

    public static int AStar(Grid grid, Coord start, Coord target)
    {
        HashSet<Coord> closed = new();
        var openQ = new List<AStarNode>
        {
            new AStarNode(start.x, start.y, 0, ManhattanDistance(start, target), null)
        };

        while (openQ.Count > 0)
        {
            var current = openQ[0];

            var currentIndex = 0;
            // find next node by priority
            // prio queue could remove this but c# prio q doesn't support changing priority mid Q.
            for (var i = 1; i < openQ.Count; i++)
            {
                if (openQ[i].F < current.F || (openQ[i].F == current.F && openQ[i].H < current.H))
                {
                    current = openQ[i];
                    currentIndex = i;
                }
            }

            openQ.RemoveAt(currentIndex);

            closed.Add(new Coord(current.Row, current.Column));

            if (current.Row == target.x && current.Column == target.y)
            {
                return current.G;
            }

            var neighbourCoords = grid.GetValidAdjacentNoDiag(new Coord(current.Row, current.Column))
                .Where(c => !closed.Contains(c));

            foreach (var nc in neighbourCoords)
            {
                var gScore = current.G + grid.grid[nc.x][nc.y];
                var neighbour = openQ.FirstOrDefault(x => x.Row == nc.x && x.Column == nc.y);

                if (neighbour is null)
                {
                    openQ.Add(new AStarNode(nc.x, nc.y, gScore, ManhattanDistance(nc, target), current));
                }
                else if (gScore < neighbour.G)
                {
                    neighbour.G = gScore;
                    neighbour.Parent = current;
                }
            }
        }

        // No path found
        return -1;
    }
}
public class AStarNode
{
    public int Row { get; set; }
    public int Column { get; set; }
    public int F => G + H; // Estimated total cost
    public int G { get; set; } // Cost from start node
    public int H { get; set; } // Heuristic estimate to target node
    public AStarNode? Parent { get; set; }

    public AStarNode(int row, int column, int g = 0, int h = 0, AStarNode? parent = null)
    {
        Row = row;
        Column = column;
        G = g;
        H = h;
        Parent = parent;
    }
}