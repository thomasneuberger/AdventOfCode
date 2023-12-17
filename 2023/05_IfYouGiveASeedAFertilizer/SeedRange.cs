namespace _05_IfYouGiveASeedAFertilizer;

public class SeedRange
{
    public required long From { get; init; }
    
    public required long To { get; init; }
    
    public required long Length { get; init; }

    public override string ToString()
    {
        return $"Seed range: From: {From:N0}, To: {To:N0} (Length: {Length:N0})";
    }
}