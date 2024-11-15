namespace EverybodyCodes2024;

public abstract class Day
{
    public abstract void Part1();

    public abstract void Part2();

    public abstract void Part3();

    public string FilePath(int part) => $"inputs/{GetType().Name}_{part}.txt";
}
