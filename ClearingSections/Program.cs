using ClearingSections;

Console.WriteLine("Load input data...");

var lines = (await File.ReadAllLinesAsync("input.txt"))
    .ToList();
    
var pairs = lines
    .Select(l => l.Split(',', StringSplitOptions.TrimEntries))
    .Select(p => new
    {
        First = p[0].Split('-', StringSplitOptions.TrimEntries),
        Second = p[1].Split('-', StringSplitOptions.TrimEntries)
    })
    .Select(p => new Pair(new Elf(int.Parse(p.First[0]), int.Parse(p.First[1])), new Elf(int.Parse(p.Second[0]), int.Parse(p.Second[1]))))
    .ToList();

foreach (var pair in pairs)
{
    Console.WriteLine($"First: {pair.First.From}-{pair.First.To}, Second: {pair.Second.From}-{pair.Second.To}, Contains Fully: {pair.IsFullyContained()}, Overlap: {pair.Overlap()}");
}

var fullyContainedNumber = pairs.Count(p => p.IsFullyContained());

Console.WriteLine($"Fully contained pairs: {fullyContainedNumber}");

var overlappedNumber = pairs.Count(p => p.Overlap());

Console.WriteLine($"Overlapping pairs: {overlappedNumber}");