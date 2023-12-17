using Microsoft.VisualBasic;

namespace _05_IfYouGiveASeedAFertilizer;

public record Mapping
{
    public required long Source { get; init; }

    public long SourceTo => Source + Length - 1;

    public required long Destination { get; init; }

    public long DestinationTo => Destination + Length - 1;

    public required long Length { get; init; }

    public override string ToString()
    {
        return $"Source: {Source}-{SourceTo}, Destination: {Destination}-{DestinationTo}, Difference: {Destination - Source}, Length: {Length}";
    }
}