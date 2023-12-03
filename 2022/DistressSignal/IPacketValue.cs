namespace DistressSignal;

public interface IPacketValue
{
    bool? IsLessThan(IPacketValue other);
}