namespace Aoc.Utils.Grids;

public class GridWithOtherVal<T>
{
    public GridItem<T> this[Coord coord]
    {
        get => grid[coord.r][coord.c];
        set { grid[coord.r][coord.c] = value; }
    }

    public Type OtherVal { get; set; }

    public bool Complete { get; set; }

    public int Number { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public List<List<GridItem<T>>> grid;

    public GridWithOtherVal(List<List<int>> grid, T defaultOtherVal)
    {
        this.grid = grid.Select(x => x.Select(x => new GridItem<T>(x, defaultOtherVal)).ToList()).ToList();
        Width = grid[0].Count;
        Height = grid.Count;
    }

    public void SetOtherVal(Coord coord, T otherVal)
    {
        this[coord] = new GridItem<T>(this[coord].val, otherVal);
    }

    public void ForEachWithCoord(Action<GridItem<T>, Coord> action)
    {
        for (var x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                action(grid[x][y], new(x, y));
            }
        }
    }

    public Coord? FirstOrDefault(Func<GridItem<T>, bool> func)
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

    public IEnumerable<GridItemCoord<T>> WhereWithCoord(Func<GridItem<T>, Coord, bool> func)
    {
        for (var x = 0; x < this.Width; x++)
        {
            for (int y = 0; y < this.Height; y++)
            {
                if (func(grid[x][y], new(x, y)))
                {
                    yield return new GridItemCoord<T>(new GridItem<T>(grid[x][y].val, grid[x][y].otherVal), new(x, y));
                }
            }
        }
    }

    public void Print()
    {
        Console.Write("[");
        for (int x = 0; x < this.Width; x++)
        {
            Console.Write($"{Environment.NewLine}[");
            Console.Write(string.Join(", ", this.grid[x].Select(e => $"({e.val}, {e.otherVal})")));
            Console.Write($"]");
        }
        Console.Write($"{Environment.NewLine}]{Environment.NewLine}");
    }

    public bool AllValues(Func<GridItem<T>, bool> func)
    {
        return grid.SelectMany(x => x).All(func);
    }

    public List<List<GridItem<T>>> GetAllRows()
    {
        return grid.Select(x => x).ToList();
    }

    public List<List<GridItem<T>>> GetAllColumns()
    {
        return Enumerable.Range(0, Width)
            .Select(column =>
                Enumerable.Range(0, Height)
                    .Select(row => grid[row][column])
                    .ToList())
            .ToList();
    }

    public List<Coord> GetValidAdjacentIncludingDiag(Coord coord) => coord.GetValidAdjacentIncludingDiag(this.Width, this.Height);

    public List<Coord> GetValidAdjacentNoDiag(Coord coord) => coord.GetValidAdjacentNoDiag(this.Width, this.Height);
}
