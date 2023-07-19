using Aoc2021.utils.Grids;

public class AStarAlgorithm_2
{
    public Grid Grid { get; set; }

    public HashSet<Coord> Closed { get; set; } = new HashSet<Coord>();

    static int ManhattanDistance(AStarNode current, AStarNode target)
    {
        return Math.Abs(current.Row - target.Row) + Math.Abs(current.Column - target.Column);
    }

    /*
    private List<AStarNode> GetNeighbors(AStarNode current, List<List<int>> grid)
    {
        int[] dr = { -1, 0, 1, 0 };
        int[] dc = { 0, 1, 0, -1 };

        int rows = grid.Count;
        int columns = grid[0].Count;

        List<AStarNode> neighbors = new List<AStarNode>();

        for (int i = 0; i < 4; i++)
        {
            int newRow = current.Row + dr[i];
            int newColumn = current.Column + dc[i];

            if (newRow >= 0 && newRow < rows && newColumn >= 0 && newColumn < columns && !this.Closed.Contains(new Coord(newRow, newColumn)))
            {
                neighbors.Add(new AStarNode(newRow, newColumn));
            }
        }

        return neighbors;
    }*/

    public int AStar(Grid grid, AStarNode start, AStarNode target)
    {
        // List<AStarNode> openList = new List<AStarNode>();
        var openList = new List<AStarNode>();
        // TODO
        // can I turn this into a proper prio Q, need to be able to acces elements tho
        // Node should have computed F, DONE
        // have coord instead of row/column in AStarNode
        start.G = 0;
        start.H = ManhattanDistance(start, target);
        start.Parent = null;
        openList.Add(start);

        while (openList.Count > 0)
        {
            AStarNode current = openList[0];

            int currentIndex = 0;
            // prio queue could possibly remove this
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].F < current.F || (openList[i].F == current.F && openList[i].H < current.H))
                {
                    current = openList[i];
                    currentIndex = i;
                }
            }

            openList.RemoveAt(currentIndex);

            Closed.Add(new Coord(current.Row, current.Column));

            if (current.Row == target.Row && current.Column == target.Column)
            {
                return current.G;
            }

            var neighbors = grid.GetValidAdjacentNoDiag(new Coord(current.Row, current.Column))
                .Where(c => !this.Closed.Contains(c))
                .Select(c => new AStarNode(c.x, c.y));

            foreach (AStarNode neighbor in neighbors)
            {
                var gScore = current.G + grid.grid[neighbor.Row][neighbor.Column];

                if (!openList.Contains(neighbor))
                {
                    neighbor.G = gScore;
                    neighbor.H = ManhattanDistance(neighbor, target);
                    neighbor.Parent = current;
                    openList.Add(neighbor);
                }
                else if (gScore < neighbor.G) // should be something here to adjust it's position in the q.. not sure that's possible
                {
                    // I think this is wrong, doesn't seem to ever get hit
                    neighbor.G = gScore;
                    neighbor.Parent = current;
                }
            }
        }

        // No path found
        return -1;
    }
}
