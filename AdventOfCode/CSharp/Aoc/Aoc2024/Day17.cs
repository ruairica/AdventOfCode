namespace Aoc2024;

public class Day17 : Day
{
    public override void Part1()
    {
        var text = Text().Split("\n\n");

        var regs = text[0].Nums();


        var program = text[1].Nums();

        var output = new List<long>();

        regs.Dump();
        var i = 0;
        var isJump = false;
        while (i < program.Count)
        {
            var op = program[i];
            var operand = program[i + 1];

            $"{i} , {op}, {operand}".Dump();

            if (op == 0)
            {
                var res = regs[0] / Math.Pow(2, Combo(operand));
                regs[0] = (int)Math.Floor(res);
            }
            else if (op == 1)
            {
                var x = regs[1] ^ operand;
                regs[1] = x;
            }
            else if (op == 2)
            {
                regs[1] = Combo(operand) % 8;
            }
            else if (op == 3)
            {
                if (regs[0] != 0)
                {
                    i = operand;
                    "jumped".Dump();
                    continue;
                }
            }
            else if (op == 4)
            {
                regs[1] = regs[1] ^ regs[2];
            }
            else if (op == 5)
            {
                output.Add(Combo(operand) % 8);

                "outputing now".Dump();
                output.Dump();
                "--".Dump();
            }
            else if (op == 6)
            {
                var res = regs[0] / Math.Pow(2, Combo(operand));
                regs[1] = (int)Math.Floor(res);

            }
            else
            {
                var res = regs[0] / Math.Pow(2, Combo(operand));
                regs[2] = (int)Math.Floor(res);
            }


            regs.Dump();

            i += 2;


        }

        "---".Dump();
        regs.Dump();
        //0,7,1,4,1,4,5,1,6 wrong
        string.Join(',', output).Dump();

        int Combo(int n)
        {
            if (n <= 3)
            {
                return n;
            }

            if (n == 4) return regs[0];
            if (n == 5) return regs[1];
            if (n == 6) return regs[2];


            throw new Exception("invalid combo");
            /*Combo operands 0 through 3 represent literal values 0 through 3.
               Combo operand 4 represents the value of register A.
               Combo operand 5 represents the value of register B.
               Combo operand 6 represents the value of register C.
               Combo operand 7 is reserved and will not appear in valid programs.*/
        }
    }

    // maybe work backwards  to A or something
    public override void Part2()
    {

    }
}
