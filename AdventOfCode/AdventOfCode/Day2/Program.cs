//https://adventofcode.com/2022/day/2

var path = "C:\\Source\\AdventOfCode\\Day2\\input.txt";

var scores = new Dictionary<string, int>
{
    { "A", 1 },
    { "B", 2 },
    { "C", 3 },
    { "X", 1 },
    { "Y", 2 },
    { "Z", 3 }
};


//losing
var pairs = new Dictionary<string, string>
{
    { "A", "C" }, //rock
    { "B", "A" }, // paper
    { "C", "B" } // scissor
};


// winning
var pairsLose = new Dictionary<string, string>
{
    { "A", "B" }, //rock
    { "B", "C" }, // paper
    { "C", "A" } // scissor
};


var scoreCounter = 0;
var text = File.ReadAllText(path).Split("\n").ToList();

foreach (var line in text)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        break;
    }

    var opp = line[0].ToString();
    var me = line[^1].ToString();

    // draw
    if (me == "Y")
    {
        scoreCounter += 3 + scores[opp];
    }

    //lose
    if (me == "X")
    {
        scoreCounter += scores[pairs[opp]];
    }

    // win
    if (me == "Z")
    {
        scoreCounter += 6 + scores[pairsLose[opp]];
    }
}

Console.WriteLine(scoreCounter);



// helper methods (unused)
static class EnumerableExtensions {

  public static IEnumerable<IEnumerable<T>> GroupOnChange<T>(
    this IEnumerable<T> source,
    Func<T, T, Boolean> changePredicate
  ) {
    if (source == null)
      throw new ArgumentNullException("source");
    if (changePredicate == null)
      throw new ArgumentNullException("changePredicate");

    using (var enumerator = source.GetEnumerator()) {
      if (!enumerator.MoveNext())
        yield break;
      var firstValue = enumerator.Current;
      var currentGroup = new List<T>();
      currentGroup.Add(firstValue);
      while (enumerator.MoveNext()) {
        var secondValue = enumerator.Current;
        var change = changePredicate(firstValue, secondValue);
        if (change) {
          yield return currentGroup;
          currentGroup = new List<T>();
        }
        currentGroup.Add(secondValue);
        firstValue = secondValue;
      }
      yield return currentGroup;
    }
  }

      public static IEnumerable<IEnumerable<TSource>> Split<TSource>(this IEnumerable<TSource> source, TSource splitOn, IEqualityComparer<TSource> comparer = null)
    {
        if (source == null)
            throw new ArgumentNullException("source");
        return SplitIterator(source, splitOn, comparer);
    }

    private static IEnumerable<IEnumerable<TSource>> SplitIterator<TSource>(this IEnumerable<TSource> source, TSource splitOn, IEqualityComparer<TSource> comparer)
    {
        comparer = comparer ?? EqualityComparer<TSource>.Default;
        var current = new List<TSource>();
        foreach (var item in source)
        {
            if (comparer.Equals(item, splitOn))
            {
                if (current.Count > 0)
                {
                    yield return current;
                    current = new List<TSource>();
                }
            }
            else
            {
                current.Add(item);
            }
        }

        if (current.Count > 0)
            yield return current;
    }

}
