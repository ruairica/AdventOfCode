namespace Aoc2024;

public class Day13 : Day
{
    public override void Part1()
    {
        Text().Split("\n\n").Select(chunk =>
        {
            var lines = chunk.Split("\n");

            var (ax, ay) = lines[0].Nums();
            var (bx, by) = lines[1].Nums();
            var (px, py) = lines[2].Nums();

            var costs = new List<int>();
            var score = (x: 0, y: 0);

            var cc = 0;
            foreach (var _ in Range(1, 100))
            {
                if (score.x > px || score.y > py)
                {
                    break;
                }

                score = (score.x + ax, score.y + ay);
                cc += 3;

                if ((px - score.x) % bx == 0 && (py - score.y) % by == 0 && (px - score.x) / bx == (py - score.y) / by)
                {
                    costs.Add(cc + (px - score.x) / bx);
                }
            }

            costs.Dump();
            return costs.Count == 0 ? 0 : costs.Min();
        }).Sum().Dump();
    }

    public override void Part2()
    {

        "hello".Dump();
        Text().Split("\n\n").Select(chunk =>
        {
            var lines = chunk.Split("\n");

            var (ax, ay) = lines[0].Nums().Select(x => (decimal)x).ToList();
            var (bx, by) = lines[1].Nums().Select(x => (decimal)x).ToList();
            var (px, py) = lines[2].Nums().Select(x => (decimal)x).ToList();



            //https://en.wikipedia.org/wiki/Cramer%27s_rule
            // a1 * x + b1 * y = c1
            // a2 * x + b2 * y = c2


            //change x to i and y to j as I'm already using x and y
            // (ax * i) + (bx * j) = px
            // (ay * i) + (by * j) = py

            // i = (c1b2 - b1c2)/ (a1b2 - b1a2)
            // j = (a1c2 - c1a2) / (a1b2 -b1a2)


            var i = (px * by - bx * py) / (ax * by - bx * ay);
            var j  = (ax * py - px * ay) / (ax * by - bx * ay);
            // find i  j. return i * 3 + j because of costs

            $"{i}, {j}".Dump();
            if (i % 1 == 0 && j % 1 == 0)
            {
                return 3 * i + j;
            }
            else
            {
                return 0;
            }

        }).Sum().Dump();
        var extra = 10000000000000;
    }
}
