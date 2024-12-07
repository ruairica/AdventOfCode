namespace Aoc2024;

public class Day6 :Day
{
    public override void Part1()
    {
        var grid = new Grid<char>(Text().CharGrid());

        var position = grid.FirstOrDefault(x => x == '^');
        var dir = Dir.Up;

        var visited = new HashSet<Coord> { position };
        while (true)
        {
            var (vm, m) = grid.Move(position, dir);

            if (!vm)
            {
                break;
            }

            if (grid[m] == '#')
            {
                dir = TurnRight(dir);
            }
            else
            {
                position = m;
                visited.Add(position);
            }

        }

        visited.Count.Dump();
    }

    private Dir TurnRight(Dir dir) =>
        dir switch
        {
            Dir.Up => Dir.Right,
            Dir.Down => Dir.Left,
            Dir.Left => Dir.Up,
            Dir.Right => Dir.Down,
        };

    public override void Part2()
    {
        var g = new Grid<char>(Text().CharGrid());
        var position = g.FirstOrDefault(x => x == '^');
        var start = position;

        var total = 0;

        // could imrpove this : only needed to check the squares that are in the original (part 1) path of the guard
        for (int r = 0; r < g.Height; r++)
        {
            for (int c = 0; c < g.Width; c++)
            {
                var currentPos = new Coord(r, c);
                if (currentPos != start && g[currentPos] != '#')
                {
                    g[currentPos] = '#';
                    total += CreatesInfiniteLoop(start, g) ? 1 : 0;
                    g[currentPos] = '.';
                }
            }
        }

        total.Dump();
    }

    private bool CreatesInfiniteLoop(Coord start, Grid<char> grid)
    {
        var position = start;
        var dir = Dir.Up;

        var visited = new HashSet<(Coord, Dir)> { (position, dir) };
        while (true)
        {
            var (vm, m) = grid.Move(position, dir);

            if (!vm)
            {
                return false;
            }

            if (grid[m] == '#')
            {
                dir = TurnRight(dir);
            }
            else
            {
                position = m;
                if (!visited.Add((position, dir)))
                {
                    return true;
                }
            }
        }
    }
}
