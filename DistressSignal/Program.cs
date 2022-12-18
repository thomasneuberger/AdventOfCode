using DistressSignal;

Console.WriteLine("Load input data...");

var input = (await File.ReadAllLinesAsync("input.txt"))
    .Where(l => !l.StartsWith("#"))
    .ToList();

var packets = ParseInput(input);

Console.WriteLine($"Packets: {packets.Length}");
for (var i = 0; i < packets.Length; i++)
{
    Console.WriteLine($"Pair {i + 1}:");
    var pair = packets[i];
    Console.WriteLine(pair.First.ToString());
    Console.WriteLine(pair.Second.ToString());
    Console.WriteLine($"Correct order: {pair.IsCorrectOrder}");
    Console.WriteLine();
}

var correctPairs = packets
    .Select((pair, i) => new
    {
        Index = i + 1,
        Pair = pair
    })
    .Where(p => p.Pair.IsCorrectOrder)
    .ToArray();
var indexSum = correctPairs.Sum(p => p.Index);
    
Console.WriteLine($"Correct indices: {indexSum}");

var dividers = new[]
{
    Packet.Parse("[[2]]"),
    Packet.Parse("[[6]]")
};
var allPackets = packets.Select(p => p.First)
    .Union(packets.Select(p => p.Second))
    .Union(dividers)
    .ToList();

allPackets.Sort((first, second) => first.IsLessThan(second) switch
    {
        true => -1,
        false => 1,
        _ => 0
    });

var dividerIndices = dividers.Select(d => new
    {
        Divider = d,
        Index = allPackets.IndexOf(d) + 1
    })
    .ToArray();

Console.WriteLine($"Dividers: {string.Join(", ", dividerIndices.Select(d => d.Index))}");
var decoderKey = dividerIndices[0].Index * dividerIndices[1].Index;
Console.WriteLine($"Decoder Key: {decoderKey}");

Pair[] ParseInput(IReadOnlyList<string> lines)
{
    var parsedPackets = new List<Pair>();

    Packet? first = null;

    foreach (var line in lines)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            first = null;
        }
        else
        {
            var packet = Packet.Parse(line);
            if (first is null)
            {
                first = packet;
            }
            else
            {
                parsedPackets.Add(new Pair(first, packet));
            }
        }
    }
    

    return parsedPackets.ToArray();
}