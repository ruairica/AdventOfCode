
namespace Aoc2024;

public class Day2 :Day
{
    public override void Part1()
    {
        var text = Text();

        var count = text.Lines().Select(line => line.GetNums()).Count(Safe);

        count.Dump();
    }

    public override void Part2()
    {
        var text = Text().Dump();
        var count = text.Lines().Select(line => line.GetNums()).Select(Copies).Count(copies => copies.Any(Safe));

        count.Dump();
    }

    private static bool Safe(List<int> nums)
    {
        if (nums.Distinct().Count() != nums.Count)
        {
            return false;
        }

        var isasc = false;
        var asc = nums.OrderBy(x => x).ToList();
        foreach (var i in Range(0, nums.Count))
        {
            isasc = nums[i] == asc[i];
            if (!isasc)
            {
                break;
            }
        }

        var desc = nums.OrderByDescending(x => x).ToList();
        var isdesc = false;
        foreach (var i in Range(0, nums.Count))
        {
            isdesc = nums[i] == desc[i];
            if (!isdesc)
            { break; }
        }


        if (!isdesc && !isasc)
        {
            return false;
        }


        var s = nums.Zip(nums.Skip(1), (i1, i2) =>
        {
            var diff = Math.Abs(i2 - i1);
            return diff is >= 1 and <= 3;
        });

        return s.All(x => x);
    }

    private List<List<int>> Copies(List<int> nums) =>
        Range(0, nums.Count).Select(x => nums.Where((el, index) => index != x).Select(e => e).ToList()).ToList();
}
