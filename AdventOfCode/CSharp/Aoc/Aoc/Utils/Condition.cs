namespace Aoc.Utils;

public record Condition(string Rule)
{
    public bool hasCondition()
    {
        return Rule.Contains('<') || Rule.Contains('>');
    }

    public string? GetOutput(Dictionary<string, int> rating)
    {
        if (!hasCondition())
        {
            return Rule;
        }

        if (Rule.Contains('<'))
        {
            var parts = Rule.Split('<');
            var left = parts[0];
            var right = parts[1][..parts[1].IndexOf(':')];

            if (rating[left] < int.Parse(right))
            {
                return Rule[(Rule.IndexOf(':') + 1)..];
            }

            return null;
        }

        if (Rule.Contains('>'))
        {
            var parts = Rule.Split('>');
            var left = parts[0];
            var right = parts[1][..parts[1].IndexOf(':')];

            if (rating[left] > int.Parse(right))
            {
                return Rule[(Rule.IndexOf(':') + 1)..];
            }

            return null;
        }

        throw new Exception("Couldn't find rule");
    }
}
