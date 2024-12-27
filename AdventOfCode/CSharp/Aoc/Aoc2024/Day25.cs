namespace Aoc2024;

public class Day25 : Day
{
    public override void Part1()
    {
        var text = Text();
        var splits = text.Split("\n\n");

        List<Grid<char>> keys = [];
        List<Grid<char>> locks = [];
        foreach (var split in splits)
        {
            var g = new Grid<char>(split.CharGrid());

            if (g.GetAllRows()[0].All(x => x == '.'))
            {
                keys.Add(g);
            }
            else
            {
                locks.Add(g);
            }
        }

        var sum = 0;

        foreach (var k in keys)
        {
            var ki = k.Columns().Select(x => x.Index().First(x => x.val != '.').index).ToList();
            foreach (var l in locks)
            {
                var li = l.Columns().Select(x => x.Index().First(x => x.val != '#').index).ToList();

                if (ki.Zip(li).All(x => x.First >= x.Second))
                {
                    sum++;
                }
            }
        }

        sum.Dump();
    }

    public override void Part2()
    {
        throw new NotImplementedException();
    }
}
