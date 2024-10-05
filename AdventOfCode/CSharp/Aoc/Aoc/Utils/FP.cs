namespace Aoc.Utils;
// File Parser
public static class FP
{
    public static string ReadFile(string path) =>
        File.ReadAllText(path)
            .Replace("\r\n", "\n") // standardize line breaks
            .Trim();

    public static string[] ReadLines(string path) =>
        ReadFile(path).Split("\n");

    public static int[][] ReadAsGrid(string path) =>
        ReadLines(path)
              .Select(x => x.Select(y => int.Parse(y.ToString())).ToArray())
              .ToArray();

    public static char[][] ReadAsCharGrid(string path) =>
        ReadLines(path)
            .Select(x => x.Select(y => y).ToArray())
            .ToArray();
}