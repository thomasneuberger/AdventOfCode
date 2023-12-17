namespace _06_WaitForIt;

public class Race()
{
    public required long Time { get; init; }

    public required long Distance { get; init; }

    public override string ToString()
    {
        return $"Time: {Time}, Distance: {Distance}";
    }
}