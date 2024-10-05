using Aoc.Utils.Grids;

namespace Aoc.Utils;
public static class ListExtensions
{
    public static T Pop<T>(this List<T> list, int elementIndexer = -1)
    {
        var elementIndex = elementIndexer < 0 ? list.Count + elementIndexer : elementIndexer;
        var element = list[elementIndex];
        list.RemoveAt(elementIndex);
        return element;
    }

    public static List<int> AllIndexOf(this string text, string str)
    {
        List<int> allIndexOf = [];
        int index = text.IndexOf(str);
        while (index != -1)
        {
            allIndexOf.Add(index);
            index = text.IndexOf(str, index + 1);
        }
        return allIndexOf;
    }

    //https://rosettacode.org/wiki/Shoelace_formula_for_polygonal_area#C#
    // gets area, includes, just verts, not edges.
    public static double CalculateShoelaceArea(this List<Coord> corners)
    {
        var n = corners.Count;
        double area = 0;

        for (var i = 0; i < n; i++)
        {
            var j = (i + 1) % n;
            area += corners[i].r * corners[j].c;
            area -= corners[j].r * corners[i].c;
        }

        area = Math.Abs(area) / 2.0;
        return area;
    }

    public static double CalculateShoelaceArea(this List<CoordL> corners)
    {
        var n = corners.Count;
        long area = 0;

        for (var i = 0; i < n; i++)
        {
            var j = (i + 1) % n;
            area += corners[i].r * corners[j].c;
            area -= corners[j].r * corners[i].c;
        }

        area = Math.Abs(area) / 2;
        return area;
    }

    // allows a list to be deconstructed to a tuple like in Python.
    public static void Deconstruct<T>(this IList<T> list, out T first)
    {
        first = list.Count > 0 ? list[0] : default;
    }

    public static void Deconstruct<T>(this IList<T> list, out T first, out T second)
    {
        first = list.Count > 0 ? list[0] : default;
        second = list.Count > 1 ? list[1] : default;
    }

    public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third)
    {
        first = list.Count > 0 ? list[0] : default;
        second = list.Count > 1 ? list[1] : default;
        third = list.Count > 2 ? list[2] : default;
    }

    public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third, out T fourth)
    {
        first = list.Count > 0 ? list[0] : default;
        second = list.Count > 1 ? list[1] : default;
        third = list.Count > 2 ? list[2] : default;
        fourth = list.Count > 3 ? list[3] : default;
    }

    public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third, out T fourth, out T fifth)
    {
        first = list.Count > 0 ? list[0] : default;
        second = list.Count > 1 ? list[1] : default;
        third = list.Count > 2 ? list[2] : default;
        fourth = list.Count > 3 ? list[3] : default;
        fifth = list.Count > 4 ? list[4] : default;
    }
    public static long FindLCM(this List<long> numbers)
    {
        // Ensure the list is not empty
        if (numbers.Count == 0)
        {
            throw new ArgumentException("The list cannot be empty.");
        }

        // Use the helper method to find the LCM of two numbers
        long lcm = numbers[0];
        for (int i = 1; i < numbers.Count; i++)
        {
            lcm = FindLCM(lcm, numbers[i]);
        }

        return lcm;
    }

    public static IEnumerable<int> GetDifferences(this IReadOnlyList<int> nums) => nums.Skip(1).Select((val, index) => val - nums[index - 1]);

    // Helper method to find the LCM of two numbers
    private static long FindLCM(long a, long b)
    {
        return Math.Abs(a * b) / FindGCD(a, b);
    }

    // Helper method to find the Greatest Common Divisor (GCD) of two numbers
    private static long FindGCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}
