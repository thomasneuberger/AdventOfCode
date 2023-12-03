namespace DistressSignal;

public class Packet : IPacketValue
{
    public Packet(IPacketValue[] values)
    {
        Values = values;
    }

    public IPacketValue[] Values { get; }

    public override string ToString()
    {
        return "[" + string.Join(",", Values.Select(v => v.ToString())) + "]";
    }

    public bool? IsLessThan(IPacketValue other)
    {
        return other switch
        {
            NumberPacketValue numberValue => IsLessThan((IPacketValue)Parse($"[{numberValue.Value}]")),
            Packet packet => IsLessThan(packet),
            _ => throw new Exception("Unknown value type")
        };
    }

    private bool? IsLessThan(Packet packet)
    {
        var minValues = Math.Min(Values.Length, packet.Values.Length);
        for (var i = 0; i < minValues; i++)
        {
            var lessThan = Values[i].IsLessThan(packet.Values[i]);
            if (lessThan.HasValue)
            {
                return lessThan.Value;
            }
        }

        return Values.Length == packet.Values.Length ? null : Values.Length < packet.Values.Length;
    }

    public static Packet Parse(string input)
    {
        input = input.Substring(1, input.Length - 2);
        var packetValues = new List<IPacketValue>();
        var level = 0;
        var currentValueString = string.Empty;
        for (var i = 0; i < input.Length; i++)
        {
            if (level < 0)
            {
                break;
            }
            
            if (input[i] == ']')
            {
                level--;
            }
            else if (input[i] == '[')
            {
                level++;
            }
            
            if (level == 0 )
            {
                if (input[i] == ',')
                {
                    var packetValue = ParseValue(currentValueString);
                    packetValues.Add(packetValue);

                    currentValueString = string.Empty;
                }
                else
                {
                    currentValueString += input[i];
                }
            }
            else
            {
                currentValueString += input[i];
            }
        }

        if (!string.IsNullOrWhiteSpace(currentValueString))
        {
            var packetValue = ParseValue(currentValueString);
            packetValues.Add(packetValue);
        }

        var packet = new Packet(packetValues.ToArray());
        return packet;
    }

    private static IPacketValue ParseValue(string currentValueString)
    {
        if (int.TryParse(currentValueString, out _))
        {
            var value = int.Parse(currentValueString);
            var packetValue = new NumberPacketValue(value);
            return packetValue;
        }
        else
        {
            var subPacket = Parse(currentValueString);
            return subPacket;
        }
    }
}