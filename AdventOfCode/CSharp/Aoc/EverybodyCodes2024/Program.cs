using System.Reflection;

namespace EverybodyCodes2024;

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
        else if (part == 2)
        {
            solution.Part2();
        }
        else if (part == 3)
        {
            solution.Part3();
        }
        else
        {
            throw new NotSupportedException("must be 1-5");
        }
    }
}
