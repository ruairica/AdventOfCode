public class Node
{
    public int Row { get; set; }
    public int Column { get; set; }
    public int F { get; set; } // Estimated total cost
    public int G { get; set; } // Cost from start node
    public int H { get; set; } // Heuristic estimate to target node
    public Node Parent { get; set; }

    public Node(int row, int column)
    {
        Row = row;
        Column = column;
        F = 0;
        G = 0;
        H = 0;
        Parent = null;
    }
}

public class AStarAlgorithm
{
    static int ManhattanDistance(Node current, Node target)
    {
        return Math.Abs(current.Row - target.Row) + Math.Abs(current.Column - target.Column);
    }

    static List<Node> GetNeighbors(Node current, List<List<int>> grid, List<List<bool>> closed)
    {
        int[] dr = { -1, 0, 1, 0 };
        int[] dc = { 0, 1, 0, -1 };

        int rows = grid.Count;
        int columns = grid[0].Count;

        List<Node> neighbors = new List<Node>();

        for (int i = 0; i < 4; i++)
        {
            int newRow = current.Row + dr[i];
            int newColumn = current.Column + dc[i];

            if (newRow >= 0 && newRow < rows && newColumn >= 0 && newColumn < columns && !closed[newRow][newColumn])
            {
                neighbors.Add(new Node(newRow, newColumn));
            }
        }

        return neighbors;
    }

    public static int AStar(List<List<int>> grid, Node start, Node target)
    {
        int rows = grid.Count;
        int columns = grid[0].Count;

        List<Node> openList = new List<Node>();
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
        start.F = start.G + start.H;
        start.Parent = null;
        openList.Add(start);

        while (openList.Count > 0)
        {
            Node current = openList[0];
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
                Node node = current;
                while (node != null)
                {
                    totalCost += grid[node.Row][node.Column];
                    node = node.Parent;
                }
                return totalCost - grid[0][0]; // first cost isn't counted
            }

            List<Node> neighbors = GetNeighbors(current, grid, closedList);

            foreach (Node neighbor in neighbors)
            {
                int gScore = current.G + grid[neighbor.Row][neighbor.Column];
                bool isBetterPath = false;

                if (!openList.Contains(neighbor))
                {
                    neighbor.G = gScore;
                    neighbor.H = ManhattanDistance(neighbor, target);
                    neighbor.F = neighbor.G + neighbor.H;
                    neighbor.Parent = current;
                    openList.Add(neighbor);
                    isBetterPath = true;
                }
                else if (gScore < neighbor.G)
                {
                    neighbor.G = gScore;
                    neighbor.F = neighbor.G + neighbor.H;
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
