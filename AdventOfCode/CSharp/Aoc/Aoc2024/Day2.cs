
namespace Aoc2024;

public class Day2 :Day
{
    public override void Part1()
    {
        Text()
            .Lines()
            .Select(line => line.Nums())
            .Count(Safe)
            .Dump();
    }

    public override void Part2()
    {
        Text()
            .Lines()
            .Select(line => line.Nums())
            .Select(Variations)
            .Count(variations => variations.Any(Safe))
            .Dump();
    }

    private static bool Safe(List<int> nums)
    {
        if (nums.Distinct().Count() != nums.Count)
        {
            return false;
        }

        var isasc = nums.Zip(nums.OrderBy(x => x), (i1, i2) => i1 == i2).All(x => x);
        var isdesc = nums.Zip(nums.OrderByDescending(x => x), (i1, i2) => i1 == i2).All(x => x);

        if (!isdesc && !isasc)
        {
            return false;
        }

        return nums
            .Zip(nums.Skip(1), (i1, i2) => Math.Abs(i2 - i1) is >= 1 and <= 3)
            .All(x => x);
    }

    private static List<List<int>> Variations(List<int> nums) =>
        Range(0, nums.Count).Select(x => nums.Where((_, index) => index != x).ToList()).ToList();
}
