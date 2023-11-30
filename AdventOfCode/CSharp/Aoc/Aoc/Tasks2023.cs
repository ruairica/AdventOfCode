using Aoc.Utils;
using Dumpify;
using NUnit.Framework;

namespace Aoc;

[TestFixture]
public class Tasks2023
{
    //run single test with eg dotnet watch test --filter day1_1_2023
    private const string basePath = "./inputs/2023";

    [Test]
    public void day1_1_2023()
    {
        var result = 0;
        var list = new List<int>();
        var lines = FP.ReadFile($"{basePath}/day1.txt").Split("\n");

        lines.Dump();

        foreach (var (val, index) in lines.Enumerate())
        {
            // var num = int.Parse(val);
        }

        "day1 part 1".Dump();
    }

    [Test]
    public void day1_2_2023()
    {
        var result = 0;
        var list = new List<int>();
        var lines = FP.ReadFile($"{basePath}/day1.txt").Split("\n");

        lines.Dump();

        foreach (var (val, index) in lines.Enumerate())
        {
            // var num = int.Parse(val);
        }

        "day1 par 2".Dump();
    }
}