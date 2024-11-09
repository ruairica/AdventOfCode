using Dumpify;
using Utils;

namespace Aoc2023;

public class Day15 : Day
{
    public override void Part1()
    {
        FP.ReadFile(FilePath).Split(",")
            .Sum(s =>
                s.Aggregate(0, (acc, curr) => (acc + (int)curr) * 17 % 256))
            .Dump();
    }

    public override void Part2()
    {
        var boxes = Enumerable.Range(0, 256).Select(_ => new List<(string, string)>()).ToList();
        foreach (var chunk in FP.ReadFile(FilePath).Split(","))
        {
            var containsMinus = chunk.Contains('-');
            if (containsMinus)
            {
                var label = chunk.Substring(0, chunk.Length - 1);// .Aggregate(0, (acc, curr) => (acc + (int)curr) * 17 % 256))
                var hash = label.Aggregate(0, (acc, curr) => (acc + (int)curr) * 17 % 256);

                // go to the relevant box and remove the lens with the given label if it is present in the box.

                var indexOfLabel = boxes[hash].FindIndex(x => x.Item1 == label);

                if (indexOfLabel != -1)
                {
                    boxes[hash].RemoveAt(indexOfLabel);
                }
            }
            else // contains equals
            {
                var (label, focal) = chunk.Split("=");
                var hash = label.Aggregate(0, (acc, curr) => (acc + (int)curr) * 17 % 256);

                var indexOfLabel = boxes[hash].FindIndex(x => x.Item1 == label);
                if (indexOfLabel != -1)
                {
                    boxes[hash][indexOfLabel] = (label, focal);
                }
                else
                {
                    boxes[hash].Add((label, focal));
                }
            }
        }

        boxes
            .Select((box, bindex) =>
                box.Select((lens, lensindex) => (bindex + 1) * (lensindex + 1) * int.Parse(lens.Item2))
                    .Sum())
            .Sum()
            .Dump();
    }
}
