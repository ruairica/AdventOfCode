using System.Globalization;
using Dumpify;
using Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var lines = File.ReadAllText("input.txt")
        .Trim()
        .Split("\n")
        .ToList();

        var part1 = 0;
        var part2 = 0;

        foreach (var line in lines)
        {
            var (range, targetPart, password) = line.Split(" ");

            var (min, max) = range.Split("-").Select(int.Parse).ToList();

            var target = targetPart.First();

            var totalMatches = password.Count(x => x == target);
            if (totalMatches >= min && totalMatches <= max)
            {
                part1 += 1;
            }
            if ((password[min - 1] == target && (password[max - 1] != target)) || (password[min - 1] != target && (password[max - 1] == target)))
            {
                part2 += 1;
            }
        }

        part1.Dump();
        part2.Dump();
    }
}

// wanted array destructuring...
public static class ListDeconstructionExtensions
{
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
}
