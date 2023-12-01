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
        var lines = FP.ReadFile($"{basePath}/day1.txt").Split("\n");

        var result = lines.Select(line => line.Where(char.IsDigit).ToList()).Select(digits => int.Parse($"{digits[0]}{digits[^1]}")).Sum();

        result.Dump();
    }

    [Test]
    public void day1_2_2023()
    {
        var numsStrings = new List<string>{ "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
        
        var dict = new Dictionary<string, int>();
        numsStrings.Enumerate().ToList().ForEach(x=> dict.Add(x.val, x.index+1));

        var collection = Enumerable.Range(1, 9).Select(x => x.ToString()).ToList();
        collection.ForEach(x => dict.Add(x, int.Parse(x)));

        numsStrings.AddRange(collection);
        var lines = FP.ReadFile($"{basePath}/day1.txt").Split("\n");

        var result = lines
            .Select(line => numsStrings
                .SelectMany(x => line
                    .AllIndexOf(x)
                    .Select(y => (y, x))
                    .Where(x => x.Item1 != -1)
                    .ToList()))
            .Select(digits => 
                int.Parse($"{dict[digits.MinBy(x => x.Item1).Item2]}{dict[digits.MaxBy(x => x.Item1).Item2]}"))
            .Sum();

        result.Dump();
    }
}