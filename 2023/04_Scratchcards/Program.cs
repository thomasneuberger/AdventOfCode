using _04_Scratchcards;

var cards = Inputs.Parse(Inputs.Puzzle);

for (var index = 0; index < cards.Length; index++)
{
    var card = cards[index];
    var hits = card.YourNumbers.Where(card.WinningNumbers.ContainsKey).Count();
    card.Points = Math.Max(0, (int)Math.Pow(2, hits - 1));
    for (int hitIndex = 0; hitIndex < hits; hitIndex++)
    {
        var targetIndex = index + hitIndex + 1;
        if (targetIndex < cards.Length - 1)
        {
            cards[targetIndex].Copies += card.Copies;
        }
    }
    Console.WriteLine(card);
}

var totalPoints = cards.Sum(c => c.Points!.Value);
var totalCount = cards.Sum(c => c.Copies);

Console.WriteLine($"Total points: {totalPoints}");
Console.WriteLine($"Total copies: {totalCount}");