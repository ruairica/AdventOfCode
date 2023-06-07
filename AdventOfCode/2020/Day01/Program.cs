using System.Globalization;
using Dumpify;


// Part1
var allNumbers = File.ReadAllText("input.txt")
.Trim()
.Split("\n")
.Select(int.Parse)
.ToList();

var hashed = allNumbers.ToHashSet();
var target = 2020;
/*foreach (var num in allNumbers)
{
    if (hashed.Contains(target - num))
    {
        (num * (target - num)).Dump();
        break;
    }
}*/
//nicer :
var res = allNumbers.First(x => hashed.Contains(target - x));
(res * (target - res)).Dump();

// Part 2
for (int i = 0; i < allNumbers.Count; i++)
{
    for (int j = 1; j < allNumbers.Count; j++)
    {
        if (j == i)
        {
            continue;
        }
        if (hashed.Contains(target - allNumbers[i] - allNumbers[j]))
        {
            ((target - allNumbers[i] - allNumbers[j]) * allNumbers[i] * allNumbers[j]).Dump();
        }
    }
}