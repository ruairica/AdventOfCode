namespace Aoc2024;

public abstract class Day
{
    public abstract void Part1();

    public abstract void Part2();

    public string Text()
    {
        var path = $"./inputs/{GetType().Name}.txt";
        if (File.Exists(path))
        {
            return ReadFile(path);
        }

        var client = new HttpClient();

        client.DefaultRequestHeaders.Add("Cookie", ReadFile(@"C:\Source\personal\AdventOfCode\AdventOfCode\CSharp\Aoc\Aoc2024\token.txt"));

        var dayNumber = GetType().Name[3..];
        var response = client.GetAsync($"https://adventofcode.com/2024/day/{dayNumber}/input").Result;
        var text = response.Content.ReadAsStringAsync().Result;

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"{response.StatusCode} : {text}");
        }

        if (text.StartsWith("Please don't repeatedly request this endpoint before it unlocks!"))
        {
            throw new InvalidOperationException("Puzzle not available");
        }

        File.WriteAllText(path, text);
        return text;
    }
}
