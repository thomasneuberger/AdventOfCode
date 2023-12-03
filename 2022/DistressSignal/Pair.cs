namespace DistressSignal;

public class Pair
{
    public Pair(Packet first, Packet second)
    {
        First = first;
        Second = second;
        IsCorrectOrder = first.IsLessThan(second) ?? true;
    }

    public Packet First { get; }
    public Packet Second { get; }

    public bool IsCorrectOrder { get; }
}