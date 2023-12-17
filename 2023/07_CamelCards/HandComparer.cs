namespace _07_CamelCards;

public class HandComparer : IComparer<Hand>
{
    public int Compare(Hand? x, Hand? y)
    {
        if (x.Type == y.Type)
        {
            for (int i = 0; i < int.Min(x.Cards.Length, y.Cards.Length); i++)
            {
                if (x.Cards[i] != y.Cards[i])
                {
                    return GetCardValue(x.Cards[i]) - GetCardValue(y.Cards[i]);
                }
            }
        }

        return x.Type - y.Type;
    }

    private int GetCardValue(char card)
    {
        return card switch
        {
            'T' => 10,
            'J' => 1,
            'Q' => 12,
            'K' => 13,
            'A' => 14,
            _ => int.Parse(card.ToString())
        };
    }
}