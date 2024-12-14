namespace Aoc2024;

public class Day14 : Day
{
    public override void Part1()
    {
        var WIDTH = 101 - 1;
        var HEIGHT = 103 - 1;

        var dict = Text().Lines().Select(x => x.Nums())
            .ToDictionary(x => (px: x[0], py: x[1], vx: x[2], vy: x[3]), e => (cx: e[0], cy: e[1]));

        foreach (var sec in Range(1, 100))
        {
            foreach (var (key, cp) in dict)
            {
                var nx = cp.cx + key.vx;

                if (nx < 0)
                {
                    nx = WIDTH + 1 + nx;
                }
                else if (nx > WIDTH)
                {
                    nx = nx - WIDTH - 1;
                }

                var ny = cp.cy + key.vy;
                if (ny < 0)
                {
                    ny = HEIGHT + 1 + ny;
                }
                else if (ny > HEIGHT)
                {
                    ny = ny - HEIGHT - 1;
                }


                dict[key] = (nx, ny);
            }
        }

        var tl = dict.Count(x => x.Value.cx < WIDTH / 2 && x.Value.cy < HEIGHT / 2);
        var tr = dict.Count(x => x.Value.cx > WIDTH / 2 && x.Value.cy < HEIGHT / 2);
        var bl = dict.Count(x => x.Value.cx < WIDTH / 2 && x.Value.cy > HEIGHT / 2);
        var br = dict.Count(x => x.Value.cx > WIDTH / 2 && x.Value.cy > HEIGHT / 2);

        (tl * tr * bl * br).Dump();
    }

    public override void Part2()
    {
        var text = Text();
        text.Dump();

        var WIDTH = 101 - 1;
        var HEIGHT = 103 - 1;

        var dict = Text().Lines().Select(x => x.Nums())
            .ToDictionary(x => (px: x[0], py: x[1], vx: x[2], vy: x[3]), e => (cx: e[0], cy: e[1]));

        // ctrl+f a load of X's in a row
        using (var writer = new StreamWriter(
                   "C:\\Source\\personal\\AdventOfCode\\AdventOfCode\\CSharp\\Aoc\\Aoc2024\\inputs\\grid_output.txt"))

        {
            foreach (var sec in Range(1, 20000))
            {
                foreach (var (key, cp) in dict)
                {
                    var nx = cp.cx + key.vx;

                    if (nx < 0)
                    {
                        nx = WIDTH + 1 + nx;
                    }
                    else if (nx > WIDTH)
                    {
                        nx = nx - WIDTH - 1;
                    }

                    var ny = cp.cy + key.vy;
                    if (ny < 0)
                    {
                        ny = HEIGHT + 1 + ny;
                    }
                    else if (ny > HEIGHT)
                    {
                        ny = ny - HEIGHT - 1;
                    }


                    dict[key] = (nx, ny);
                }

                writer.WriteLine("");
                writer.WriteLine($"==========after {sec} seconds");

                foreach (var y in Range(0, HEIGHT + 1))
                {
                    var line = "";
                    foreach (var x in Range(0, WIDTH + 1))
                    {
                        line += dict.Values.Contains((x, y)) ? 'X' : ' ';
                    }

                    writer.WriteLine(line);
                }
            }
        }
    }
}
