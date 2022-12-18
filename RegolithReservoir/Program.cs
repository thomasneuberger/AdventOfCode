using System.Drawing;
using RegolithReservoir;

Console.WriteLine("Load input data...");

var input = (await File.ReadAllLinesAsync("input.txt"))
    .Where(l => !l.StartsWith("#"))
    .ToList();

var cave = ParseInput(input);

cave.Print();

var count = 0;
while (cave.AddSand())
{
    Console.WriteLine($"Sand {count + 1}");

    count++;
}

cave.Print();

Console.WriteLine($"Sand at rest: {count}");

Cave ParseInput(IReadOnlyList<string> lines)
{
    var rockLines = new List<(Point From, Point To)>();
    foreach (var line in lines)
    {
        var anchors = line.Split(" -> ")
            .Select(a => a.Split(','))
            .Select(a => new
            {
               X = int.Parse(a[0]),
               Y = int.Parse(a[1])
            })
            .Select(a => new Point(a.X, a.Y))
            .ToArray();

        for (var i = 1; i < anchors.Length; i++)
        {
            rockLines.Add((anchors[i-1], anchors[i]));
        }
    }

    return new Cave(rockLines);
}