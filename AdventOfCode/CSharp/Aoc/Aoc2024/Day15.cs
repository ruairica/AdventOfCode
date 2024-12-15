namespace Aoc2024;

public class Day15 : Day
{
    public override void Part1()
    {
        var (gridText, moves) = Text().Split("\n\n");
        var g = new Grid<char>(gridText.CharGrid());
        var robot = g.FirstOrDefault(x => x == '@');
        var validMoves = new[] { '<', '>', '^', 'v' };

        foreach (var move in moves.Where(x => validMoves.Contains(x)))
        {
            var md = MoveDir(move);
            var newCoord = g.Move(robot, md).coord;
            var newCoordChar = g[newCoord];

            if (newCoordChar == '#') continue;
            
            if (newCoordChar == '.')
            {
                var temp = robot;
                robot = newCoord;
                g[robot] = '@';
                g[temp] = '.';
                continue;
            }

            var currentRobot = newCoord;

            // find first either . or #
            while (newCoordChar == 'O')
            {
                newCoord = g.Move(newCoord, md).coord;
                newCoordChar = g[newCoord];
            }

            if (newCoordChar == '#') continue;
            
            g[newCoord] = 'O';
            var tr = robot;
            robot = currentRobot;
            g[robot] = '@';
            g[tr] = '.';
        }


        g.WhereWithCoord((c, _) => c == 'O')
            .Sum(x => x.Coord.r * 100 + x.Coord.c).Dump();
    }

    public override void Part2()
    {
        var (gridText, moves) = Text().Split("\n\n");
        var newCharGrid = gridText.Split('\n')
            .Select(x => x.Select(y => y switch
            {
                '#' => "##",
                'O' => "[]",
                '.' => "..",
                '@' => "@."
            }).SelectMany(x => x).ToList())
            .ToList();

        var g = new Grid<char>(newCharGrid);

        var robot = g.FirstOrDefault(x => x == '@');

        var validMoves = new[] { '<', '>', '^', 'v' };
        foreach (var move in moves.Where(x => validMoves.Contains(x)))
        {
            var md = MoveDir(move);
            var newCoord = g.Move(robot, md).coord;
            var newCoordChar = g[newCoord];

            if (newCoordChar == '.')
            {
                var temp = robot;
                robot = newCoord;
                g[robot] = '@';
                g[temp] = '.';
                continue;
            }

            if (newCoordChar == '#') continue;
            
            if (md is Dir.Left or Dir.Right)
            {
                // walk until first . or #
                List<Coord> toMove = [robot, newCoord];

                while (newCoordChar is '[' or ']')
                {
                    newCoord = g.Move(newCoord, md).coord;
                    newCoordChar = g[newCoord];
                    toMove.Add(newCoord);
                }

                if (newCoordChar == '#') continue;

                var reversedToMove = toMove.AsEnumerable().Reverse().ToList();
                foreach (var (last, sl) in reversedToMove.Zip(reversedToMove.Skip(1)))
                {
                    g[last] = g[sl];
                }

                g[robot] = '.';
                robot = g.Move(robot, md).coord;
                g[robot] = '@';
            }
            else // going up or down
            {
                List<Coord> toMove = [];
                var q = new Queue<Coord>();
                q.Enqueue(newCoord);
                q.Enqueue(newCoordChar switch
                {
                    '[' => new Coord(newCoord.r, newCoord.c + 1),
                    ']' => new Coord(newCoord.r, newCoord.c - 1)
                });

                var hitWall = false;
                var visited = new HashSet<Coord>();
                while (q.TryDequeue(out var pc))
                {
                    if (!visited.Add(pc))
                    {
                        continue;
                    }

                    if (g[pc] == '#')
                    {
                        hitWall = true;
                        break;
                    }

                    if (g[pc] == '.')
                    {
                        continue;
                    }

                    toMove.Add(pc);
                    var next = g.Move(pc, md).coord;

                    q.Enqueue(next);

                    if (g[next] is '[' or ']')
                    {
                        q.Enqueue(new Coord(next.r, g[next] == '[' ? pc.c + 1 : pc.c - 1));
                    }
                }

                if (!hitWall)
                {
                    foreach (var pair in toMove.AsEnumerable().Reverse())
                    {
                        g[g.Move(pair, md).coord] = g[pair];
                        g[pair] = '.';
                    }

                    var temp = robot;
                    robot = g.Move(robot, md).coord;

                    g[robot] = '@';
                    g[temp] = '.';
                }
            }
        }

        g.WhereWithCoord((c, _) => c == '[')
            .Sum(x => x.Coord.r * 100 + x.Coord.c)
            .Dump();
    }

    private Dir MoveDir(char x)
    {
        return x switch
        {
            '^' => Dir.Up,
            '<' => Dir.Left,
            '>' => Dir.Right,
            'v' => Dir.Down,
            _ => throw new NotImplementedException($"x was {x}")
        };
    }
}
