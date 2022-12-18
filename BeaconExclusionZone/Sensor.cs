using System.Drawing;

namespace BeaconExclusionZone;

public class Sensor
{
    private readonly int _positionX;
    private readonly int _positionY;
    private readonly int _beaconX;
    private readonly int _beaconY;
    private readonly int _minX;
    private readonly int _maxX;
    private readonly int _minY;
    private readonly int _maxY;

    private Sensor(Point position, Point beacon)
    {
        Position = position;
        _positionX = position.X;
        _positionY = position.Y;
        BeaconAbsolute = beacon;
        _beaconX = beacon.X;
        _beaconY = beacon.Y;
        BeaconDistance = GetDistance(BeaconAbsolute.X, BeaconAbsolute.Y);
        _minX = position.X - BeaconDistance;
        _maxX = position.X + BeaconDistance;
        _minY = position.Y - BeaconDistance;
        _maxY = position.Y + BeaconDistance;
    }

    private int GetDistance(int x, int y)
    {
        var distanceX = Math.Abs(_positionX - x);
        var distanceY = Math.Abs(_positionY - y);
        return distanceX + distanceY;
    }

    public Point Position { get; }

    public Point BeaconAbsolute { get; }

    public int BeaconDistance { get; }

    public int MinX => _minX;
    public int MaxX => _maxX;
    public int MinY => _minY;
    public int MaxY => _maxY;

    internal bool WouldDetect(int x, int y)
    {
        if (x < _minX || x > _maxX || y < _minY || y > _maxY)
        {
            return false;
        }
        
        if ((_beaconX == x && _beaconY == y) || (_positionX == x && _positionY == y))
        {
            return true;
        }

        var distance = GetDistance(x, y);
        
        var wouldDetect = distance <= BeaconDistance;

        return wouldDetect;
    }

    internal static Sensor Parse(string line)
    {
        var lineParts = line
            .Replace("Sensor at ", string.Empty)
            .Replace(" closest beacon is at ", string.Empty)
            .Replace("x=", string.Empty)
            .Replace("y=", string.Empty)
            .Split(new[] { ":", "," }, StringSplitOptions.TrimEntries)
            .Select(int.Parse)
            .ToArray();

        return new Sensor(new Point(lineParts[0], lineParts[1]), new Point(lineParts[2], lineParts[3]));
    }
}