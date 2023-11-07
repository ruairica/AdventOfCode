namespace Aoc.Utils.Grids;

using Dumpify;

public class Grid
{
    public int this[Coord coord]
    {
        get => grid[coord.x][coord.y];
        set => grid[coord.x][coord.y] = value;
    }

    public bool Complete { get; set; }

    public int Number { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public List<List<int>> grid;

    public Grid(List<List<int>> grid)
    {
        this.grid = grid;
        Width = grid[0].Count;
        Height = grid.Count;
    }

    public void ForEachWithCoord(Action<int, Coord> action)
    {
        for (var x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                action(grid[x][y], new(x, y));
            }
        }
    }

    public Coord? FirstOrDefault(Func<int, bool> func)
    {
        for (int x = 0; x < this.Width; x++)
        {
            for (int y = 0; y < this.Height; y++)
            {
                if (func(grid[x][y]))
                {
                    return new Coord(x, y);
                }
            }
        }

        return null;
    }

    public IEnumerable<ValCoord> WhereWithCoord(Func<int, Coord, bool> func)
    {
        for (var x = 0; x < this.Width; x++)
        {
            for (int y = 0; y < this.Height; y++)
            {
                if (func(grid[x][y], new(x, y)))
                {
                    yield return new ValCoord(grid[x][y], new(x, y));
                }
            }
        }
    }

    public bool AllValues(Func<int, bool> func)
    {
        return grid.SelectMany(x => x).All(func);
    }


    public List<List<int>> GetAllRows()
    {
        return grid.Select(x => x).ToList();
    }


    public List<List<int>> GetAllColumns()
    {
        return Enumerable.Range(0, Width)
            .Select(column => Enumerable.Range(0, Height).Select(row => grid[row][column]).ToList())
            .ToList();
    }

    public List<int> GetAllValuesRightOfCoord(Coord coord)
    {
        return Enumerable.Range(coord.x + 1, this.grid[coord.x].Count - coord.x - 1)
            .Select(x => this.grid[x][coord.y])
            .ToList();
    }

    public List<int> GetAllValuesLeftOfCoord(Coord coord)
    {
        var row = Enumerable.Range(0, coord.x)
            .Select(x => this.grid[x][coord.y])
            .ToList();

        row.Reverse();
        return row;
    }

    public List<int> GetAllValuesDownFromCoord(Coord coord)
    {
        return Enumerable.Range(coord.y + 1, this.grid.Count - coord.y - 1)
            .Select(y => this.grid[coord.x][y])
            .ToList();
    }

    public List<int> GetAllValuesUpFromCoord(Coord coord)
    {
        var col = Enumerable.Range(0, coord.y)
            .Select(y => this.grid[coord.x][y])
            .ToList();

        col.Reverse();
        col.Dump();
        return col;
    }

    public void Print()
    {
        Console.Write("[");
        for (int x = 0; x < this.Height; x++)
        {
            Console.Write($"{Environment.NewLine}[");
            Console.Write(string.Join(", ", this.grid[x]));
            Console.Write($"]");
        }
        Console.Write($"{Environment.NewLine}]{Environment.NewLine}");
    }

    public List<Coord> GetValidAdjacentIncludingDiag(Coord coord) => coord.GetValidAdjacentIncludingDiag(this.Width, this.Height);

    public List<Coord> GetValidAdjacentNoDiag(Coord coord) => coord.GetValidAdjacentNoDiag(this.Width, this.Height);

    public Grid SetValues(Func<int, int> func)
    {
        var resultGrid = CopyGrid(this);

        for (var x = 0; x < this.Width; x++)
        {
            for (int y = 0; y < this.grid[x].Count; y++)
            {
                resultGrid.grid[x][y] = func(this.grid[x][y]);
            }
        }

        return resultGrid;
    }

    private static Grid CopyGrid(Grid grid)
    {
        var resultGrid = new Grid(
            Enumerable.Range(0, grid.Height)
                .Select(x =>
                    Enumerable.Range(0, grid.Width)
                        .Select(y => grid.grid[x][y])
                        .ToList())
                .ToList());
        return resultGrid;
    }
}
