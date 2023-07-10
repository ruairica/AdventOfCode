namespace Aoc2021;
public class Grid
{
    // static methods
    // get directions including diagonals
    // get directions not including diagonals

    // ctor takes in list<list<int>>
    // each value should have an int, + a bool maybe, accompanying number?

    // methods
    // get all rows
    // get all columns
    // get row
    // get column

    // extensions
    // allValues
    // allValuesForeach
    // get CoordsWhere

    // TODO
    // grid with just numbers could be an abstraction over GridWithOtherItem
    // unify both grid classes so they have all the same methods/extensions
    // should probably have a combination of each, ones that return a new grid and one
    // Print
    //    public void ForEachWithCoord(Action<int, Coord> action)
    //    public List<Coord> GetValidAdjacentIncludingDiag(Coord coord)
    //    public List<Coord> GetValidAdjacentNoDiag(Coord coord)


    public int this[Coord coord]
    {
        get => this.grid[coord.x][coord.y];
        set { this.grid[coord.x][coord.y] = value; }
    }

    public int Width { get; set; }

    public int Height { get; set; }

    public List<List<int>> grid;

    public static readonly List<Coord> dirsWithDiags = new() { new(0, 1), new(0, -1), new(1, 0), new(-1, 0), new(1, 1), new(1, -1), new(-1, 1), new(-1, -1) };

    public static readonly List<Coord> dirsNoDiags = new() { new(0, 1), new(0, -1), new(1, 0), new(-1, 0) };


    public Grid(List<List<int>> grid)
    {
        this.grid = grid;
        this.Width = grid[0].Count;
        this.Height = grid.Count;
    }

    public void ForEachWithCoord(Action<int, Coord> action)
    {
        for (var x = 0; x < this.Width; x++)
        {
            for (int y = 0; y < this.Height; y++)
            {
                action(this.grid[x][y], new(x, y));
            }
        }
    }

    public List<Coord> GetValidAdjacentIncludingDiag(Coord coord)
    {
        return dirsWithDiags
            .Where(e => e.x + coord.x >= 0 &&
                        e.x + coord.x < this.Height &&
                        e.y + coord.y >= 0 &&
                        e.y + coord.y < this.Width)
            .Select(e => new Coord(coord.x + e.x, coord.y + e.y))
            .ToList();
    }

    public List<Coord> GetValidAdjacentNoDiag(Coord coord)
    {
        return dirsNoDiags
            .Where(e => e.x + coord.x >= 0 &&
                        e.x + coord.x < this.Height &&
                        e.y + coord.y >= 0 &&
                        e.y + coord.y < this.Width)
            .Select(e => new Coord(coord.x + e.x, coord.y + e.y))
            .ToList();
    }
}

public record GridItem<T>(int val, T otherVal);

public record GridItemCoord<T>(GridItem<T> gridItem, Coord Coord);

public class GridWithOtherVal<T>
{
    public GridItem<T> this[Coord coord]
    {
        get => this.grid[coord.x][coord.y];
        set { this.grid[coord.x][coord.y] = value; }
    }

    public Type OtherVal { get; set; }

    public bool Complete { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public List<List<GridItem<T>>> grid;

    public static readonly List<Coord> dirsWithDiags = new() { new(0, 1), new(0, -1), new(1, 0), new(-1, 0), new(1, 1), new(1, -1), new(-1, 1), new(-1, -1) };

    public static readonly List<Coord> dirsNoDiags = new() { new(0, 1), new(0, -1), new(1, 0), new(-1, 0) };


    public GridWithOtherVal(List<List<int>> grid, T defaultOtherVal)
    {
        // Could I refactor GridItem to be generic
        this.grid = grid.Select(x => x.Select(x => new GridItem<T>(x, defaultOtherVal)).ToList()).ToList();
        this.Width = grid[0].Count;
        this.Height = grid.Count;
    }

    public void SetOtherVal(Coord coord, T otherVal)
    {
        for (int x = 0; x < this.Width; x++)
        {
            for (int y = 0; y < this.Height; y++)
            {
                if (coord == new Coord(x, y))
                {
                    this.grid[x][y] = new GridItem<T>(this[coord].val, otherVal);
                }
            }
        }
    }

    public void ForEachWithCoord(Action<GridItem<T>, Coord> action)
    {
        for (var x = 0; x < this.Width; x++)
        {
            for (int y = 0; y < this.Height; y++)
            {
                action(this.grid[x][y], new(x, y));
            }
        }
    }

    public List<List<GridItem<T>>> GetAllRows()
    {
        return this.grid.Select(x => x).ToList();
    }


    public List<List<GridItem<T>>> GetAllColumns()
    {
        return Enumerable.Range(0, this.Width)
            .Select(column => Enumerable.Range(0, this.Height).Select(row => this.grid[row][column]).ToList())
            .ToList();
    }

    public List<Coord> GetValidAdjacentIncludingDiag(Coord coord)
    {
        return dirsWithDiags
            .Where(e => e.x + coord.x >= 0 &&
                        e.x + coord.x < this.Height &&
                        e.y + coord.y >= 0 &&
                        e.y + coord.y < this.Width)
            .Select(e => new Coord(coord.x + e.x, coord.y + e.y))
            .ToList();
    }

    public List<Coord> GetValidAdjacentNoDiag(Coord coord)
    {
        return dirsNoDiags
            .Where(e => e.x + coord.x >= 0 &&
                        e.x + coord.x < this.Height &&
                        e.y + coord.y >= 0 &&
                        e.y + coord.y < this.Width)
            .Select(e => new Coord(coord.x + e.x, coord.y + e.y))
            .ToList();
    }
}


public record ValCoord(int Val, Coord Coord);

public record Coord(int x, int y);

public static class GridWithOtherValExtensions
{
    public static Coord? FirstOrDefault<T>(this GridWithOtherVal<T> grid, Func<GridItem<T>, bool> func)
    {
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                if (func(grid.grid[x][y]))
                {
                    return new Coord(x, y);
                }
            }
        }

        return null;
    }

    public static IEnumerable<GridItemCoord<T>> WhereWithCoord<T>(this GridWithOtherVal<T> grid, Func<GridItem<T>, Coord, bool> func)
    {
        for (var x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                if (func(grid.grid[x][y], new(x, y)))
                {
                    yield return new GridItemCoord<T>(new GridItem<T>(grid.grid[x][y].val, grid.grid[x][y].otherVal), new(x, y));
                }
            }
        }
    }

    public static bool AllValues<T>(this GridWithOtherVal<T> grid, Func<GridItem<T>, bool> func)
    {
        return grid.grid.SelectMany(x => x).All(func);
    }
}

public static class GridExtensions
{
    public static Grid SetValues(this Grid grid, Func<int, int> func)
    {
        var resultGrid = CopyGrid(grid);

        for (var x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.grid[x].Count; y++)
            {
                resultGrid.grid[x][y] = func(grid.grid[x][y]);
            }
        }

        return resultGrid;
    }

    public static bool AllValues(this Grid grid, Func<int, bool> func)
    {
        return grid.grid.SelectMany(x => x).All(func);
    }

    public static IEnumerable<ValCoord> WhereWithCoord(this Grid grid, Func<int, Coord, bool> func)
    {
        for (var x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                if (func(grid.grid[x][y], new(x, y)))
                {
                    yield return new ValCoord(grid.grid[x][y], new(x, y));
                }
            }
        }
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


    public static void Print(this Grid g)
    {
        for (int x = 0; x < g.grid.Count; x++)
        {
            Console.Write("[");
            for (int y = 0; y < g.grid[x].Count; y++)
            {
                if (g.grid[x][y] == 0)
                {
                    Console.Write($"{g.grid[x][y]}, ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write($"{g.grid[x][y]}, ");
                }
            }
            Console.Write($"]{Environment.NewLine}");
        }

        Console.WriteLine();
    }
}