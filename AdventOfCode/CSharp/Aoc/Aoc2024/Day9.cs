namespace Aoc2024;

public class Day9 : Day
{
    public override void Part1()
    {
        var text = Text();
        long curentFileId = 0;
        var values = new List<long>();

        foreach (var ci in Range(0, text.Length))
        {
            if (ci % 2 == 0)
            {
                foreach (var _ in Range(ci, int.Parse(text[ci].ToString())))
                {
                    values.Add(curentFileId);
                }

                curentFileId++;
            }
            else
            {
                foreach (var _ in Range(ci, int.Parse(text[ci].ToString())))
                {
                    values.Add(-1);
                }
            }
        }

        try
        {
            foreach (var index in Range(0, values.Count))
            {
                if (values[index] == -1)
                {
                    while (values[^1] == -1)
                    {
                        values.Pop();
                    }

                    (values[index], values[^1]) = (values[^1], values[index]);
                    values.Pop();

                }
            }
        }
        catch (Exception e) // break on out of bounds check
        {
        }

        values.Index().Sum(x => x.val * x.index).Dump();
    }

    public override void Part2()
    {
        var text = Text();
        var values = new List<long>();
        var freeDict = new Dictionary<long, long>(); // {startingIndex, size}
        var blockDict = new Dictionary<long, (long size, long fileId)>();
        long curentFileId = 0;
        foreach (var ci in Range(0, text.Length))
        {
            if (ci % 2 == 0)
            {
                var count = int.Parse(text[ci].ToString());
                blockDict[values.Count] = (count, curentFileId);
                foreach (var _ in Range(ci, count))
                {
                    values.Add(curentFileId);
                }

                curentFileId++;
            }
            else
            {
                var count = int.Parse(text[ci].ToString());
                freeDict[values.Count] = count;
                foreach (var _ in Range(ci, count))
                {
                    values.Add(-1);
                }
            }
        }

        foreach (var (_, index) in values.Index().AsEnumerable().Reverse())
        {
            if (blockDict.TryGetValue(index, out var v))
            {
                var swapVal = freeDict
                    .OrderBy(x => x.Key)
                    .FirstOrDefault(x => x.Value >= v.size && x.Key < index);

                if (swapVal is { Key: 0, Value: 0 })
                {
                    continue;
                }

                // put the file id in the blank
                foreach (var i in Range((int)swapVal.Key, (int)v.size))
                {
                    values[i] = v.fileId;
                }

                // put the in the blank in old file id
                foreach (var i in Range(index, (int)v.size))
                {
                    values[i] = -1;
                }

                // update free space dictionary accordingly
                if (swapVal.Value == v.size)
                {
                    freeDict.Remove(swapVal.Key);
                }
                else
                {
                    freeDict.Remove(swapVal.Key);
                    freeDict.Add(swapVal.Key + v.size, swapVal.Value - v.size);
                }
            }
        }

        values
            .Index()
            .Where(x => x.val != -1)
            .Sum(x => x.val * x.index)
            .Dump();
    }
}
