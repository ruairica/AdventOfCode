using Dumpify;
using Utils;

namespace EverybodyCodes2024;

public class Day1 : Day
{
    public override void Part1()
    {
        var input = FP.ReadFile(FilePath(1));
        input.Dump();
        /*Ancient Ant (A): Not very dangerous. Can be managed without using any potions.
           Badass Beetle (B): A big and strong bug that requires 1 potion to defeat.
           Creepy Cockroach (C): Fast and aggressive! This creature requires 3 potions to defeat it.*/


        var sum = input.Select(x => x switch { 'A' => 0, 'B' => 1, 'C' => 3, _ => throw new NotSupportedException() }).Sum();
        sum.Dump();
    }

    public override void Part2()
    {
        var input = FP.ReadFile(FilePath(2));
        input.Dump();

        var sum = 0;
        foreach (var (x,y) in input.Chunk(2))
        {
            var bothMonsters = x != 'x' && y != 'x';

            sum += Score(x);
            sum += Score(y);

            if (bothMonsters)
            {
                sum += 2;
            }

            (x, y, sum).Dump();

        }

        sum.Dump();
        int Score(char letter) => letter switch
        { 'A' => 0, 'B' => 1, 'C' => 3, 'D' => 5, 'x' => 0, _ => throw new NotSupportedException() };
    }

    public override void Part3()
    {
        var input = FP.ReadFile(FilePath(3));

        input.Dump();


        var sum = 0;
        foreach (var triple in input.Chunk(3))
        {
            var creatures = triple.Count(a => a != 'x');
            var (x, y, z) = triple;

            sum += Score(x);
            sum += Score(y);
            sum += Score(z);


            if (creatures == 2)
            {
                sum += 2;
            }

            if (creatures == 3)
            {
                sum += 6;
            }

            (x, y, z, sum).Dump();

        }

        sum.Dump();
        int Score(char letter) => letter switch
            { 'A' => 0, 'B' => 1, 'C' => 3, 'D' => 5, 'x' => 0, _ => throw new NotSupportedException() };
    }
}
