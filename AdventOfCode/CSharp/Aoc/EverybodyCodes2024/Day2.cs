using Dumpify;
using Utils;

namespace EverybodyCodes2024;

internal class Day2 : Day
{
    public override void Part1()
    {
        var input = FP.ReadFile(FilePath(1)).Split("\n\n");

        var words = input[0].Split(':')[1].Split(",");

        var sentence = input[1];
        var sum = words.Sum(word => sentence.AllIndexOf(word).Count);

        sum.Dump();
    }

    public override void Part2()
    {
        var input = FP.ReadFile(FilePath(2)).Split("\n\n");


        var words = input[0].Split(':')[1].Split(",").ToList();


        var sentences = input[1].Split("\n").ToList();

        var sum = 0;

        foreach (var sentence in sentences)
        {

            sentence.Dump();
            var indexesCovered = new List<int>();
            foreach (var word in words)
            {
                var reversed = string.Join("", word.Reverse());

                var i1 = sentence.AllIndexOf(word);

                indexesCovered.AddRange(i1.SelectMany(x => Enumerable.Range(x, word.Length)));
                var i2 = sentence.AllIndexOf(reversed);

                indexesCovered.AddRange(i2.SelectMany(x => Enumerable.Range(x, word.Length)));
            }
            sum += indexesCovered.Distinct().Count();

            indexesCovered.Distinct().Dump();


        }

        sum.Dump();
    }

    public override void Part3()
    {
        var input = FP.ReadFile(FilePath(3)).Split("\n\n").Dump();

        var words = input[0].Split(':')[1].Split(",").ToList();

        // get a grid, words up, down, left right, can wrap around from left to right also,
        // count all cells covered
        // grid should be okay, wrapping around is annoying

    }
}
