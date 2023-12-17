using System.Runtime.CompilerServices;
using _07_CamelCards;

var hands = Inputs.ParseInput(Inputs.Puzzle);

foreach (var hand in hands)
{
    var occurrences = hand.Cards
        .GroupBy(c => c)
        .ToDictionary(c => c.Key, c => c.Count());

    var jokers = occurrences.GetValueOrDefault('J', 0);
    var pairs = occurrences.Values.Count(v => v == 2);
    
    if (occurrences.Count == 1 || (occurrences.Count == 2 && jokers > 0))
    {
        hand.Type = HandType.FiveOfAKind;
    }
    else if (jokers == 4 || occurrences.Where(o => o.Key != 'J').MaxBy(o => o.Value).Value >= 4 - jokers)
    {
        hand.Type = HandType.FourOfAKind;
    }
    else if (occurrences.ContainsValue(2) && occurrences.ContainsValue(3))
    {
        hand.Type = HandType.FullHouse;
    }
    else if (jokers == 1 && pairs == 2)
    {
        hand.Type = HandType.FullHouse;
    }
    else if (occurrences.Values.Max() >= 3 - jokers)
    {
        hand.Type = HandType.ThreeOfAKind;
    }
    else if (pairs == 2 || (pairs == 1 && jokers == 1))
    {
        hand.Type = HandType.TwoPair;
    }
    else if (pairs == 1 || jokers == 1)
    {
        hand.Type = HandType.OnePair;
    }
    else
    {
        hand.Type = HandType.HighCard;
    }

    if (jokers > 1)
    {
        
    }
}

var sortedHands = hands.Order(new HandComparer())
    .ToList();

var winning = 0L;
for (var index = 0; index < sortedHands.Count; index++)
{
    var hand = sortedHands[index];
    hand.Rank = index + 1;
    if (hand.Cards.Contains('J'))
    {
        Console.WriteLine($"{index + 1}: {hand}");
    }
    winning += hand.Rank.Value * hand.Bid;
}

Console.WriteLine($"Winning: {winning}");