var path = "C:\\Source\\AdventOfCode\\Day3\\input.txt";

var lines = File.ReadAllText(path).Split("\r\n").ToList();


Console.WriteLine(
    File.ReadAllText(path)
    .Split("\r\n")
    .Select(line => line[..(line.Length / 2)].ToCharArray().Intersect(line[(line.Length / 2)..].ToCharArray()))
    .SelectMany(x => x).Sum(x => x.ToString() == x.ToString().ToUpper() ? x.ToString().ToLower()[0] - 96 + 26 : x -96)
    );

Console.WriteLine(
    File.ReadAllText(path)
    .Split("\r\n")
    .Select(x => x.ToCharArray().ToList())
    .Chunk(3)
    .Select(c => c.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList()).First())
    .Sum(x => x.ToString() == x.ToString().ToUpper() ? x.ToString().ToLower()[0] - 96 + 26 : x -96)
    );
