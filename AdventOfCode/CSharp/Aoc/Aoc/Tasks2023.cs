using Aoc.Utils;
using Dumpify;
using NUnit.Framework;

namespace Aoc;

[TestFixture]
public class Tasks2023
{
    private const string basePath = "./inputs/2023";

    [Test]
    public void day1_1_2023()
    {
        "2023 here".Dump();
        FP.ReadFile($"{basePath}/day1.txt")
            .Split("\n\n")
            .Select(x => x.Split("\n").Select(int.Parse).Sum())
            .MaxBy(x => x)
            .Dump();
    }

    // Question: how do I run a dotnet test for the current file with nunit?
    // Answer: dotnet test --filter "FullyQualifiedName~Tasks2023.day1_2_2023"
}