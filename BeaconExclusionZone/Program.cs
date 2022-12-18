using System.Diagnostics;
using System.Drawing;
using BeaconExclusionZone;

Console.WriteLine("Load input data...");

var input = File.ReadAllLines("input.txt")
    .Where(l => !l.StartsWith("#"))
    .ToList();
    
var sensors = input
    .Select(Sensor.Parse)
    .ToArray();

foreach (var sensor in sensors)
{
    Console.WriteLine($"Sensor at {sensor.Position} with beacon {sensor.BeaconAbsolute}, distance: {sensor.BeaconDistance}, X: {sensor.MinX}-{sensor.MaxX}, Y: {sensor.MinY}-{sensor.MaxY}");
}

var minY = sensors
    .Select(s => s.Position.X - s.BeaconDistance)
    .Min();
var maxY = sensors
    .Select(s => s.Position.X + s.BeaconDistance)
    .Max();
var minX = sensors
    .Select(s => s.Position.Y - s.BeaconDistance)
    .Min();
var maxX = sensors
    .Select(s => s.Position.Y + s.BeaconDistance)
    .Max();

var maxCoordinate = 4000000;

var possibleBeacons = FindPossibleBeacons();

foreach (var beacon in possibleBeacons)
{
    var tuningFrequency = (beacon.X * 4000000L) + beacon.Y;
    Console.WriteLine($"Possible beacon at {beacon} with frequency {tuningFrequency}");
}

IReadOnlyList<Point> FindPossibleBeacons()
{
    var beacons = new List<Point>();
    var timer = new Stopwatch();
    for (var row = Math.Max(minY, 0); row <= Math.Min(maxY, maxCoordinate); row++)
    {
        if (row % 10000 == 0)
        {
            Console.WriteLine($"Check row {row} after {timer.Elapsed}...");
            timer.Restart();
        }
        var y = row;

        var possibleSensors = sensors
            .Where(s => s.MinY <= y)
            .Where(s => s.MaxY >= y)
            .ToList();

        var sensorAreas = possibleSensors
            .Select(s =>
            {
                var lineDifference = Math.Abs(y - s.Position.Y);
                return new
                {
                    Left = Math.Max(s.MinX + lineDifference, 0),
                    Right = Math.Min(s.MaxX - lineDifference, maxCoordinate)
                };
            })
            .OrderBy(s => s.Left);

        var last = -1;
        foreach (var sensorArea in sensorAreas)
        {
            var column = sensorArea.Left - 1;
            if (sensorArea.Left - 1 > last && sensors.Any(s => !s.WouldDetect(column, y)))
            {
                beacons.Add(new Point(column, y));
            }
            else
            {
                last = Math.Max(last, sensorArea.Right);
            }
        }
    }

    return beacons;
}