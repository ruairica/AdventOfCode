namespace Aoc2024;

public class Day7 : Day
{
    public override void Part1()
    {
        var text = Text();
        text.Lines().Sum(l =>
        {
            var nums = l.Longs();
            var target = nums[0];
            var rest = nums[1..];

            var q = new Queue<List<string>>();
            q.Enqueue(["+"]);
            q.Enqueue(["*"]);
            var operationCombinations = new List<List<string>>();
            while (q.Count > 0)
            {
                var cl = q.Dequeue();

                if (cl.Count == rest.Count - 1)
                {
                    operationCombinations.Add(cl);
                    continue;
                }

                q.Enqueue([..cl, "*"]);
                q.Enqueue([..cl, "+"]);
            }

            List<long> totals = [];
            foreach (var ocl in operationCombinations)
            {
                var currentTotal = rest[0];
                foreach (var (n, index) in rest.Index().Skip(1))
                {
                    if (ocl[index - 1] == "+")
                    {
                        currentTotal += n;
                    }
                    else
                    {
                        currentTotal *= n;
                    }
                }

                totals.Add(currentTotal);
            }

            return totals.Contains(target) ? target : 0;
        }).Dump();
    }

    public override void Part2()
    {
        var text = Text();
        text.Lines().Sum(l =>
        {
            var nums = l.Longs();
            var target = nums[0];
            var rest = nums[1..];

            var q = new Queue<List<string>>();
            q.Enqueue(["+"]);
            q.Enqueue(["*"]);
            q.Enqueue(["||"]);
            var operationCombinations = new List<List<string>>();
            while (q.Count > 0)
            {
                var cl = q.Dequeue();

                if (cl.Count == rest.Count - 1)
                {
                    operationCombinations.Add(cl);
                    continue;
                }

                q.Enqueue([.. cl, "*"]);
                q.Enqueue([.. cl, "+"]);
                q.Enqueue([.. cl, "||"]);
            }

            foreach (var cl in operationCombinations)
            {
                var currentTotal = rest[0];
                foreach (var (n, index) in rest.Index().Skip(1))
                {
                    if (cl[index - 1] == "+")
                    {
                        currentTotal += n;
                    }
                    else if (cl[index - 1] == "*")
                    {
                        currentTotal *= n;
                    }
                    else
                    {
                        currentTotal = long.Parse($"{currentTotal}{n}");
                    }
                }

                if (currentTotal == target)
                {
                    return target;
                }
            }

            return 0;
        }).Dump();
    }
}
