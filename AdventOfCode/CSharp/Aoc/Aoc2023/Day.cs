namespace Aoc2023;

public abstract class Day
{
    public abstract void Part1();

    public abstract void Part2();

    public string FilePath => $"inputs/{GetType().Name}.txt"; // TODO copy to bin
}
