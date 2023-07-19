public class AStarNode
{
    public int Row { get; set; }
    public int Column { get; set; }
    public int F => G + H; // Estimated total cost
    public int G { get; set; } // Cost from start node
    public int H { get; set; } // Heuristic estimate to target node
    public AStarNode Parent { get; set; }

    public AStarNode(int row, int column)
    {
        Row = row;
        Column = column;
        G = 0;
        H = 0;
        Parent = null;
    }
}

public class AStarAlgorithm
{
    static int ManhattanDistance(AStarNode current, AStarNode target)
    {
        return Math.Abs(current.Row - target.Row) + Math.Abs(current.Column - target.Column);
    }

    static List<AStarNode> GetNeighbors(AStarNode current, List<List<int>> grid, List<List<bool>> closed)
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

            if (newRow >= 0 && newRow < rows && newColumn >= 0 && newColumn < columns && !closed[newRow][newColumn])
            {
                neighbors.Add(new AStarNode(newRow, newColumn));
            }
        }

        return neighbors;
    }

    public static int AStar(List<List<int>> grid, AStarNode start, AStarNode target)
    {
        int rows = grid.Count;
        int columns = grid[0].Count;

        List<AStarNode> openList = new List<AStarNode>();
        List<List<bool>> closedList = new List<List<bool>>();

        for (int i = 0; i < rows; i++)
        {
            closedList.Add(new List<bool>());
            for (int j = 0; j < columns; j++)
            {
                closedList[i].Add(false);
            }
        }

        start.G = 0;
        start.H = ManhattanDistance(start, target);
        start.Parent = null;
        openList.Add(start);

        while (openList.Count > 0)
        {
            AStarNode current = openList[0];
            int currentIndex = 0;

            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].F < current.F || (openList[i].F == current.F && openList[i].H < current.H))
                {
                    current = openList[i];
                    currentIndex = i;
                }
            }

            openList.RemoveAt(currentIndex);
            closedList[current.Row][current.Column] = true;

            if (current.Row == target.Row && current.Column == target.Column)
            {
                // Reached the target, return the total cost (sum of grid values)
                int totalCost = 0;
                AStarNode aStarNode = current;
                while (aStarNode != null)
                {
                    totalCost += grid[aStarNode.Row][aStarNode.Column];
                    aStarNode = aStarNode.Parent;
                }

                return current.G;
            }

            List<AStarNode> neighbors = GetNeighbors(current, grid, closedList);

            foreach (AStarNode neighbor in neighbors)
            {
                int gScore = current.G + grid[neighbor.Row][neighbor.Column];
                bool isBetterPath = false;

                if (!openList.Contains(neighbor))
                {
                    neighbor.G = gScore;
                    neighbor.H = ManhattanDistance(neighbor, target);
                    neighbor.Parent = current;
                    openList.Add(neighbor);
                    isBetterPath = true;
                }
                else if (gScore < neighbor.G)
                {
                    neighbor.G = gScore;
                    neighbor.Parent = current;
                    isBetterPath = true;
                }

                if (isBetterPath)
                {
                    neighbor.Row = neighbor.Row;
                    neighbor.Column = neighbor.Column;
                }
            }
        }

        // No path found
        return -1;
    }
}