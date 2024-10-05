
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

    public T[][] grid;

    public Grid(T[][] grid)
    {
        this.grid = grid;
        Width = grid[0].Length;
        Height = grid.Length;
    }

    public Grid(List<List<T>> grid)
    {
        this.grid = grid.Select(x => x.ToArray()).ToArray();
        Width = grid[0].Count;
        Height = grid.Count;
    }

    public void ForEachWithCoord(Action<T, Coord> action)
    {
        for (var r = 0; r < Height; r++)
        {
            for (int c = 0; c < Width; c++)
            {
                action(grid[r][c], new(r, c));
            }
        }
    }

    public Coord? FirstOrDefault(Func<T, bool> func)
    {
        for (int r = 0; r < this.Height; r++)
        {
            for (int c = 0; c < this.Width; c++)
            {
                if (func(grid[r][c]))
                {
                    return new Coord(r, c);
                }
            }
        }

        return null;
    }

    public IEnumerable<ValCoord<T>> WhereWithCoord(Func<T, Coord, bool> func)
    {
        for (var r = 0; r < this.Height; r++)
        {
            for (int c = 0; c < this.Width; c++)
            {
                if (func(grid[r][c], new(r, c)))
                {
                    yield return new ValCoord<T>(grid[r][c], new(r, c));
                }
            }
        }
    }

    public bool AllValues(Func<T, bool> func)
    {
        return grid.SelectMany(x => x).All(func);
    }

    public List<List<Coord>> GetAllRowsCoords()
    {
        return Enumerable.Range(0, this.Height)
            .Select(
                row => Enumerable.Range(0, this.Width)
                    .Select(col => new Coord(row, col))
                    .ToList())
            .ToList();
    }

    public List<List<Coord>> GetAllColCoords()
    {
        return Enumerable.Range(0, this.Width)
            .Select(
                col => Enumerable.Range(0, this.Height)
                    .Select(row => new Coord(row, col))
                    .ToList())
            .ToList();
    }

    public T[][] GetAllRows()
    {
        return grid.Select(x => x).ToArray();
    }

    public List<List<T>> GetAllColumns()
    {
        return Enumerable.Range(0, Width)
            .Select(
                column => Enumerable.Range(0, Height)
                    .Select(row => grid[row][column])
                    .ToList())
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

    public List<Coord> GetValidAdjacentIncludingDiag(Coord coord) =>
        coord.GetValidAdjacentIncludingDiag(this.Width, this.Height);

    public List<Coord> GetValidAdjacentNoDiag(Coord coord) =>
        coord.GetValidAdjacentNoDiag(this.Width, this.Height);

    public (bool valid, Coord coord) Move(Coord coord, Dir dir)
    {
        var newCoord = dir switch
        {
            Dir.Up => coord with { r = coord.r - 1 },
            Dir.Down => coord with { r = coord.r + 1 },
            Dir.Left => coord with { c = coord.c - 1 },
            Dir.Right => coord with { c = coord.c + 1 },
        };

        return (
            !(newCoord.r < 0 || newCoord.r >= this.Height || newCoord.c < 0 ||
              newCoord.c >= this.Width), newCoord);
    }

    public Grid<T> SetValues(Func<T, T> func)
    {
        var resultGrid = CopyGrid(this);

        for (var x = 0; x < this.Height; x++)
        {
            for (int y = 0; y < this.grid[x].Length; y++)
            {
                resultGrid.grid[x][y] = func(this.grid[x][y]);
            }
        }

        return resultGrid;
    }

    public void Transpose()
    {
        var resultGrid = new Grid<T>(
            Enumerable.Range(0, this.Width)
                .Select(
                    x => Enumerable.Range(0, this.Height)
                        .Select(y => this.grid[y][x])
                        .ToArray())
                .ToArray());

        this.grid = resultGrid.grid;
        this.Width = resultGrid.Width;
        this.Height = resultGrid.Height;
    }

    // TODO check these still work
    public void Rotate90ClockWise()
    {
        this.Transpose();
        for (int i = 0; i < this.Height; i++)
        {
            this.grid[i] = this.grid[i].Reverse().ToArray();
        }
    }

    public void Rotate90Counterclockwise()
    {

        List<int> x = new();

        for (int i = 0; i < this.Height; i++)
        {
            this.grid[i] = this.grid[i].Reverse().ToArray();
        }
        this.Transpose();
    }

    public static Grid<T> CopyGrid(Grid<T> grid)
    {
        var resultGrid = new Grid<T>(
            Enumerable.Range(0, grid.Height)
                .Select(
                    x => Enumerable.Range(0, grid.Width)
                        .Select(y => grid.grid[x][y])
                        .ToArray())
                .ToArray());

        return resultGrid;
    }
}