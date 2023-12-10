namespace Aoc.Utils.Grids;
public class Grid<T>
{
    public T this[Coord coord]
    {
        get => grid[coord.r][coord.c];
        set => grid[coord.r][coord.c] = value;
    }

    public bool Complete { get; set; }

    public int Number { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public List<List<T>> grid;

    public Grid(List<List<T>> grid)
    {
        this.grid = grid;
        Width = grid[0].Count;
        Height = grid.Count;
    }

    public void ForEachWithCoord(Action<T, Coord> action)
    {
        for (var x = 0; x < Height; x++)
        {
            for (int y = 0; y < Width; y++)
            {
                action(grid[x][y], new(x, y));
            }
        }
    }

    public Coord? FirstOrDefault(Func<T, bool> func)
    {
        for (int x = 0; x < this.Height; x++)
        {
            for (int y = 0; y < this.Width; y++)
            {
                if (func(grid[x][y]))
                {
                    return new Coord(x, y);
                }
            }
        }

        return null;
    }

    public IEnumerable<ValCoord<T>> WhereWithCoord(Func<T, Coord, bool> func)
    {
        for (var x = 0; x < this.Height; x++)
        {
            for (int y = 0; y < this.Width; y++)
            {
                if (func(grid[x][y], new(x, y)))
                {
                    yield return new ValCoord<T>(grid[x][y], new(x, y));
                }
            }
        }
    }

    public bool AllValues(Func<T, bool> func)
    {
        return grid.SelectMany(x => x).All(func);
    }


    public List<List<T>> GetAllRows()
    {
        return grid.ConvertAll(x => x);
    }


    public List<List<T>> GetAllColumns()
    {
        return Enumerable.Range(0, Width)
            .Select(column => Enumerable.Range(0, Height).Select(row => grid[row][column]).ToList())
            .ToList();
    }

    public List<T> GetAllValuesRightOfCoord(Coord coord)
    {
        return Enumerable.Range(coord.c + 1, this.Width - 1 - coord.c)
            .Select(y => this.grid[coord.r][y])
            .ToList();
    }

    public List<T> GetAllValuesLeftOfCoord(Coord coord)
    {
        var row = Enumerable.Range(0, coord.c)
            .Select(y => this.grid[coord.r][y])
            .ToList();

        row.Reverse();
        return row;
    }

    public List<T> GetAllValuesDownFromCoord(Coord coord)
    {
        return Enumerable.Range(coord.r + 1, this.Height - 1 - coord.r)
            .Select(x => this.grid[x][coord.c])
            .ToList();
    }

    public List<T> GetAllValuesUpFromCoord(Coord coord)
    {
        var col = Enumerable.Range(0, coord.r)
            .Select(x => this.grid[x][coord.c])
            .ToList();

        col.Reverse();
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

    public Grid<T> SetValues(Func<T, T> func)
    {
        var resultGrid = CopyGrid(this);

        for (var x = 0; x < this.Height; x++)
        {
            for (int y = 0; y < this.grid[x].Count; y++)
            {
                resultGrid.grid[x][y] = func(this.grid[x][y]);
            }
        }

        return resultGrid;
    }

    private static Grid<T> CopyGrid(Grid<T> grid)
    {
        var resultGrid = new Grid<T>(
            Enumerable.Range(0, grid.Height)
                .Select(x =>
                    Enumerable.Range(0, grid.Width)
                        .Select(y => grid.grid[x][y])
                        .ToList())
                .ToList());
        return resultGrid;
    }
}
