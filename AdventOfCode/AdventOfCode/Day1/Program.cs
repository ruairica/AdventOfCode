
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
var input = new List<int?>();

var path = "C:\\Source\\AdventOfCode\\Day1\\input.txt";

// part 2
Console.WriteLine(File.ReadAllText(path).Split("\n\n").SkipLast(1).Select(x => x.Split("\n").Select(int.Parse).Sum()).OrderByDescending(x => x).Take(3).Sum());
