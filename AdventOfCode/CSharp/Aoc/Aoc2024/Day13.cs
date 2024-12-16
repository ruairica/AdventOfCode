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

            return costs.Count == 0 ? 0 : costs.Min();
        }).Sum().Dump();
    }

    public override void Part2()
    {
        var extra = 10000000000000;
        Text().Split("\n\n").Select(chunk =>
        {
            var lines = chunk.Split("\n");

            var (ax, ay) = lines[0].Longs();
            var (bx, by) = lines[1].Longs();
            var (px, py) = lines[2].Longs();

            (px, py) = (px + extra, py + extra);

            //https://en.wikipedia.org/wiki/Cramer%27s_rule
            // a1 * x + b1 * y = c1
            // a2 * x + b2 * y = c2


            //change x to i and y to j as I'm already using x and y
            // (ax * i) + (bx * j) = px
            // (ay * i) + (by * j) = py

            // i = (c1b2 - b1c2)/ (a1b2 - b1a2)
            // j = (a1c2 - c1a2) / (a1b2 -b1a2)

            var i = ((px * by - bx * py) % (ax * by - bx * ay) == 0)
                ? (px * by - bx * py) / (ax * by - bx * ay)
                : 0;

            //var i = (px * by - bx * py) / (ax * by - bx * ay);
            var j  = (ax * py - px * ay) % (ax * by - bx * ay) == 0
                ? (ax * py - px * ay) / (ax * by - bx * ay)
                : 0;


            // find i  j. return i * 3 + j because of costs
            if (i == 0 || j == 0)
            {
                return 0;
            }

            return 3 * i + j;

        }).Sum().Dump();

        Part2Redo();
    }

    public  void Part2Redo()
    {
        var extra = 10000000000000;
        Text().Split("\n\n").Select(chunk =>
        {
            var lines = chunk.Split("\n");

            var (ax, ay, bx, by, px, py) = chunk.Longs();
            (px, py) = (px + extra, py + extra);

            //https://en.wikipedia.org/wiki/Cramer%27s_rule
            // a1 * x + b1 * y = c1
            // a2 * x + b2 * y = c2


            //change x to i and y to j as I'm already using x and y
            // (ax * i) + (bx * j) = px
            // (ay * i) + (by * j) = py

            // i = (c1b2 - b1c2)/ (a1b2 - b1a2)
            // j = (a1c2 - c1a2) / (a1b2 -b1a2)

            // (ax * i) + (bx * j) = px
            // (ay * i) + (by * j) = py

            // rearrange top one to get what i equals ?
            // i = ( px - (bx * j) ) / ax
            // plug into second equation
            // (ay * (( px - (bx * j) ) / ax)) + (by * j) = py
            // find what j should be

            //  TODO try finish this

            var i = ((px * by - bx * py) % (ax * by - bx * ay) == 0)
                ? (px * by - bx * py) / (ax * by - bx * ay)
                : 0;

            //var i = (px * by - bx * py) / (ax * by - bx * ay);
            var j = (ax * py - px * ay) % (ax * by - bx * ay) == 0
                ? (ax * py - px * ay) / (ax * by - bx * ay)
                : 0;


            // find i  j. return i * 3 + j because of costs
            if (i == 0 || j == 0)
            {
                return 0;
            }

            return 3 * i + j;

        }).Sum().Dump();
    }
}
