using System.Reflection;

namespace Aoc2023;

public class Program
{
    static void Main(string[] args)
    {
        // dotnet watch run 1 1 --property WarningLevel=0
        var (day, part) = (int.Parse(args[0]), int.Parse(args[1]));

        var solution = (Day)Activator.CreateInstance(Assembly.GetExecutingAssembly()
            .GetTypes()
            .First(t => t.Name == $"Day{day}"));

        if (part == 1)
        {
            solution.Part1();
        }
        else
        {
            solution.Part2();
        }
    }
}
