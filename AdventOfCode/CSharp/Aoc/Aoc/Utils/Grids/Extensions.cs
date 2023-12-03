namespace Aoc.Utils.Grids;

public static class GridExtensions
{
    public static readonly List<Coord> dirsWithDiags = new() { new(0, 1), new(0, -1), new(1, 0), new(-1, 0), new(1, 1), new(1, -1), new(-1, 1), new(-1, -1) };

    public static readonly List<Coord> dirsNoDiags = new() { new(0, 1), new(0, -1), new(1, 0), new(-1, 0) };

    public static List<Coord> GetValidAdjacentIncludingDiag(this Coord coord, int Width, int Height)
    {
        return dirsWithDiags
            .Where(e => e.r + coord.r >= 0 &&
                        e.r + coord.r < Height &&
                        e.c + coord.c >= 0 &&
                        e.c + coord.c < Width)
            .Select(e => new Coord(coord.r + e.r, coord.c + e.c))
            .ToList();
    }

    public static List<Coord> GetValidAdjacentNoDiag(this Coord coord, int Width, int Height)
    {
        return dirsNoDiags
            .Where(e => e.r + coord.r >= 0 &&
                        e.r + coord.r < Height &&
                        e.c + coord.c >= 0 &&
                        e.c + coord.c < Width)
            .Select(e => new Coord(coord.r + e.r, coord.c + e.c))
            .ToList();
    }
}