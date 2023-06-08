using Dumpify;

internal class Program
{
    private static void Main(string[] args)
    {
        var lines = File.ReadAllText("input.txt")
        .Trim()
        .Split("\n")
        .ToList();

        var tree = '#';

        var inputs = new List<(int, int)> { new(1, 1), new(3, 1), new(5, 1), new(7, 1), new(1, 2) };
        var answers = new List<long>();
        foreach (var (horPos, vertPos) in inputs)
        {
            var treeCounter = 0;
            // make mutable copies
            var h = horPos;
            var v = vertPos;
            while (v < lines.Count)
            {
                if (lines[v][h] == tree) treeCounter += 1;

                h += horPos;
                v += vertPos;

                if (h > 30) h -= 31;

            }

            answers.Add(treeCounter);
        }

        answers.Dump();
        $"Part 1: {answers[1]}".Dump();
        $"Part 2: {answers.Aggregate((long)1, (x, y) => x * y)}".Dump();
    }
}
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
