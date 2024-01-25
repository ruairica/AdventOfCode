using System.Reflection;
using Aoc2023;

class Program
{
    static void Main(string[] args)
    {
        // this command line arguments should be called like dotnet run 1 1
        // parse command line arguments below

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