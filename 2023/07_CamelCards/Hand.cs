namespace _07_CamelCards;

public class Hand
{
    public required string Cards { get; init; }

    public required int Bid { get; init; }

    public int? Rank { get; set; }

    public HandType Type { get; set; } = HandType.HighCard;

    public override string ToString()
    {
        var text = $"Cards: {Cards}, Bid: {Bid}, Type: {Type}";

        if (Rank.HasValue)
        {
            text += $", Rank: {Rank}";
        }

        return text;
    }
}