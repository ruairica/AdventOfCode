using Dumpify;
using Utils;

namespace Aoc2023;

public class Day8 : Day
{
    public override void Part1()
    {
        var text = FP.ReadFile(FilePath);

        var (directions, maps) = text.Split("\n\n");
        var dict = maps.Split("\n").Select(x =>
        {
            var splits = x.Split("=");
            var key = splits[0].Trim();
            var (left, right) = splits[1].Trim().Split(',');
            left = left.Trim().TrimStart('(');
            right = right.Trim().TrimEnd(')');

            return (key, left, right);

        }).ToDictionary(x => x.key, x => (x.left, x.right));

        var step = 0;
        var found = false;
        var current = "AAA";
        var target = "ZZZ";
        while (!found)
        {
            foreach (var direction in directions)
            {
                current = direction == 'L' ? dict[current].left : dict[current].right;
                step++;

                if (current == target)
                {
                    found = true;
                }
            }
        }

        step.Dump();
    }

    public override void Part2()
    {
        var text = FP.ReadFile(FilePath);

        var (directions, maps) = text.Split("\n\n");
        var dict = maps.Split("\n").Select(x =>
        {
            var splits = x.Split("=");
            var key = splits[0].Trim();
            var (left, right) = splits[1].Trim().Split(',');
            left = left.Trim().TrimStart('(');
            right = right.Trim().TrimEnd(')');

            return (key, left, right);

        }).ToDictionary(x => x.key, x => (x.left, x.right));

        // once it hits a z, find the number of times it takes to hit that z again, find LCM to see where cycles intercept
        var nodes = dict.Where(x => x.Key[^1] == 'A').Select(x => x.Key).ToList();

        List<long> cycleLengths = [];
        foreach (var node in nodes)
        {
            var step = 0;
            var current = node;
            var foundCycleAt = 0;
            var cycleLength = 0;

            while (cycleLength == 0)
            {
                foreach (var dir in directions)
                {
                    current = dir == 'L' ? dict[current].left : dict[current].right;
                    step++;

                    if (current.EndsWith('Z'))
                    {
                        if (foundCycleAt == 0)
                        {
                            foundCycleAt = step;
                        }
                        else
                        {
                            cycleLength = step - foundCycleAt;
                        }
                    }
                }
            }

            cycleLengths.Add(cycleLength);

        }

        cycleLengths.FindLCM().Dump();
    }
}
