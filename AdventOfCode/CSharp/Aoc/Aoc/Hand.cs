namespace Aoc;

public class Hand : IComparable<Hand>
{
    public string cards;
    readonly bool part2;

    private readonly List<char> _cardRank = new List<char>
    {
        'A',
        'K',
        'Q',
        'J',
        'T',
        '9',
        '8',
        '7',
        '6',
        '5',
        '4',
        '3',
        '2'
    };

    public Hand(string cards, bool part2)
    {
        this.cards = cards;
        this.part2 = part2;

        if (this.part2)
        {
            _cardRank.RemoveAt(3);
            _cardRank.Add('J');
        }
    }

    private int GetHandRank(string cards)
    {
        var groups = cards.GroupBy(x => x)
            .ToDictionary(x => x.Key.ToString(), x => x.Count());

        switch (groups.Keys.Count)
        {
            // 5 of a kind
            case 1:
                return 7;
            // 4 of a kind
            case 2 when groups.Values.Any(x => x == 4):
                {
                    return this.part2 && groups.ContainsKey("J") ? 7 : 6;
                }
            // full house
            case 2 when groups.Values.Any(x => x == 3):
                {
                    if (this.part2 && groups.TryGetValue("J", out var group))
                    {
                        return group switch
                        {
                            3 => 7,
                            2 => 7,
                            1 => 6,
                        };
                    }
                    return 5;
                }
            // 3 of a kind
            case 3 when groups.Values.Any(x => x == 3):
                {
                    if (this.part2 && groups.TryGetValue("J", out int group))
                    {
                        return group switch
                        {
                            3 => 5,
                            2 => 7,
                            1 => 6,
                        };
                    }
                    return 4;
                }
            // 2 pair
            case 3 when groups.Values.Count(x => x == 2) == 2:
                {
                    if (this.part2 && groups.TryGetValue("J", out var value))
                    {
                        return value switch
                        {
                            2 => 6,
                            1 => 5
                        };
                    }
                    return 3;
                }
            // single pair AA2J5
            case 4 when groups.Values.Count(x => x == 2) == 1:
                {
                    if (this.part2 && groups.ContainsKey("J"))
                    {
                        return 4;
                    }
                    return 2;
                }
            case 5 when this.part2 && groups.ContainsKey("J"):
                return 2;
            case 5:
                return 1;
            default:
                throw new Exception("No hand match foundd");
        }
    }

    public int CompareTo(Hand that)
    {
        var cardsleft = this.cards;
        var cardsright = that.cards;

        var lhr = GetHandRank(cardsleft);

        var rhr = GetHandRank(cardsright);

        if (lhr > rhr)
        {
            return -1;
        }

        if (lhr < rhr)
        {
            return 1;
        }

        for (int i = 0; i < 5; i++)
        {
            var cl = cardsleft[i];
            var cr = cardsright[i];

            var indexOfLeft = _cardRank.IndexOf(cl);

            var indexOfRight = _cardRank.IndexOf(cr);

            if (indexOfLeft < indexOfRight)
            {
                return -1;
            }

            if (indexOfLeft > indexOfRight)
            {
                return 1;
            }
        }

        return 0;
    }
}
