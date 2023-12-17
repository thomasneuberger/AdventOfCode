namespace _04_Scratchcards;

public record class Card
{
    public required int Id { get; init; }

    public int Copies { get; set; } = 1;

    public required IDictionary<int, int> WinningNumbers { get; init; }

    public required int[] YourNumbers { get; init; }

    public int? Points { get; set; }

    public override string ToString()
    {
        var text = $"Id: {Id}, Winning: {string.Join(", ", WinningNumbers.Keys.Select(n => n.ToString()))}, Your: {string.Join(", ", YourNumbers.Select(n => n.ToString()))}, Copies: {Copies}";

        if (Points.HasValue)
        {
            text += $", Points: {Points.Value}";
        }

        return text;
    }
}