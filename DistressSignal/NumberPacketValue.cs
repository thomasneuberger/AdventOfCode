namespace DistressSignal;

public class NumberPacketValue : IPacketValue
{
    public NumberPacketValue(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public override string ToString()
    {
        return Value.ToString();
    }

    public bool? IsLessThan(IPacketValue other)
    {
        return other switch
        {
            NumberPacketValue numberValue => numberValue.Value == Value ? null : Value < numberValue.Value,
            Packet packet => Packet.Parse($"[{Value}]").IsLessThan(packet),
            _ => throw new Exception("Unknown value type")
        };
    }
}