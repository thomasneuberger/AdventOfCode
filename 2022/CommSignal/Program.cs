Console.WriteLine("Load input data...");

var signal = await File.ReadAllTextAsync("input.txt");

var startPacket = FindStartMarker(signal, 4);

Console.WriteLine($"Start of packet: {startPacket}");

var startMessage = FindStartMarker(signal, 14);

Console.WriteLine($"Start of message: {startMessage}");

int FindStartMarker(string s, int markerLength)
{
    for (var i = markerLength - 1; i < s.Length; i++)
    {
        var marker = s.Substring(i - markerLength + 1, markerLength);
        if (marker.Distinct().Count() == markerLength)
        {
            return i + 1;
        }
    }

    throw new Exception("Start not found.");
}