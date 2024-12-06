using System.Reflection;
using Aoc2024;

public class Program
{
    static void Main(string[] args)
    {
        /*
        dotnet run {day} {part}
        use below for hot reload + less console output
        dotnet watch run 1 1 -q --property WarningLevel=0
         */
        var (day, part) = (int.Parse(args[0]), args[1]);

        var solution = (Day)Activator.CreateInstance(Assembly.GetExecutingAssembly()
            .GetTypes()
            .First(t => t.Name == $"Day{day}"));

        if (part == "1")
        {
            solution.Part1();
        }
        else if (part == "2")
        {
            solution.Part2();
        }
        else if (part == "both")
        {
            "Part 1:".Dump();
            solution.Part1();
            "Part 2:".Dump();
            solution.Part2();
        }
    }
}
