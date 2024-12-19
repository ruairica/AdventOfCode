namespace Aoc2024;

public class Day19 : Day
{
    public override void Part1()
    {
        var (f, l) = Text().Split("\n\n");

        var towels = f.Trim().Split(", ").OrderByDescending(x => x.Length).ToList();

        var sum = 0;
        var sequences = l.Trim().Split("\n");

        foreach (var seq in sequences)
        {
            var stack = new Stack<(int, List<string>)>();
            stack.Push((0, []));
            var visited = new HashSet<(int, string)>();

            while (stack.TryPop(out var e))
            {
                var (ci, path) = e;

                if (path.Count > 0 && !visited.Add((ci - path[^1].Length, path[^1])))
                {
                    continue;
                }

                if (ci > seq.Length)
                {
                    continue;
                }

                if (ci == seq.Length)
                {
                    sum++;
                    break;
                }

                var suitableTs = towels
                    .Where(x => x.Index().All(e => (ci + e.index < seq.Length) && e.val == seq[ci + e.index])).ToList();

                foreach (var rt in suitableTs)
                {
                    stack.Push((ci + rt.Length, [..path, rt]));
                }
            }

        }

        sum.Dump();
    }

    public override void Part2()
    {
        var (f, l) = Text().Split("\n\n");

        var towels = f.Trim().Split(", ").OrderByDescending(x => x.Length).ToList();
        var sequences = l.Trim().Split("\n");
        var cache = new Dictionary<string, long>();

        sequences.Sum(WaysToTarget).Dump();

        long WaysToTarget(string target)
        {
            var ans = 0L;
            if (cache.TryGetValue(target, out var v))
            {
                return v;
            }
            if (string.IsNullOrEmpty(target))
            {
                ans++;
            }


            foreach (var t in towels.Where(target.StartsWith))
            {
                ans += WaysToTarget(target[t.Length..]);
            }

            cache.Add(target, ans);
            return ans;
        }
    }
}
