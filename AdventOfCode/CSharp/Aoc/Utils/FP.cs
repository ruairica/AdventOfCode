namespace Utils;
// File Parser
public static class FP
{
    public static string ReadFile(string path) =>
        File.ReadAllText(path)
            .Replace("\r\n", "\n") // standardize line breaks
            .Trim();

    public static string[] ReadLines(string path) =>
        ReadFile(path).Split("\n");

    public static List<List<int>> ReadAsGrid(string path) =>
        ReadLines(path)
              .Select(x => x.Select(y => int.Parse(y.ToString())).ToList())
              .ToList();

    public static List<List<int>> Grid(this string text) =>
        text.Lines()
            .Select(x => x.Select(y => int.Parse(y.ToString())).ToList())
            .ToList();

    public static List<List<char>> ReadAsCharGrid(string path) =>
        ReadLines(path)
            .Select(x => x.Select(y => y).ToList())
            .ToList();

    public static string[] Lines(this string text) => text.Split('\n');

    public static List<List<char>> CharGrid(this string text) =>
        text.Lines()
            .Select(x => x.Select(y => y).ToList())
            .ToList();

    public static string ReadFileNoReplace(string path) =>
        File.ReadAllText(path)
            .Trim();
}
