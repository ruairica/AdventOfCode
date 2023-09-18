namespace Aoc.Utils.Grids;

public static class GridExtensions
{
    public static readonly List<Coord> dirsWithDiags = new() { new(0, 1), new(0, -1), new(1, 0), new(-1, 0), new(1, 1), new(1, -1), new(-1, 1), new(-1, -1) };

    public static readonly List<Coord> dirsNoDiags = new() { new(0, 1), new(0, -1), new(1, 0), new(-1, 0) };

    public static List<Coord> GetValidAdjacentIncludingDiag(this Coord coord, int Width, int Height)
    {
        return dirsWithDiags
            .Where(e => e.x + coord.x >= 0 &&
                        e.x + coord.x < Height &&
                        e.y + coord.y >= 0 &&
                        e.y + coord.y < Width)
            .Select(e => new Coord(coord.x + e.x, coord.y + e.y))
            .ToList();
    }

    public static List<Coord> GetValidAdjacentNoDiag(this Coord coord, int Width, int Height)
    {
        return dirsNoDiags
            .Where(e => e.x + coord.x >= 0 &&
                        e.x + coord.x < Height &&
                        e.y + coord.y >= 0 &&
                        e.y + coord.y < Width)
            .Select(e => new Coord(coord.x + e.x, coord.y + e.y))
            .ToList();
    }
}