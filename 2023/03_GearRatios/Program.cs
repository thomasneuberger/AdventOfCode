using System.Text.RegularExpressions;
using _03_GearRatios;

var lines = Inputs.Puzzle
    .Split("\n\r".ToArray(), StringSplitOptions.RemoveEmptyEntries);

var partNumbers = new List<Part>();
for (var i = 0; i < lines.Length; i++)
{
    var line = lines[i];

    var matches = Regex.Matches(line, "\\d+");
    foreach (Match match in matches)
    {
        if (match.Index > 0 && HasSymbol(line.Substring(match.Index - 1, 1)))
        {
            partNumbers.Add(new Part(i, match));
            continue;
        }

        if (match.Index + match.Length < line.Length && HasSymbol(line.Substring(match.Index + match.Length, 1)))
        {
            partNumbers.Add(new Part(i, match));
            continue;
        }
        
        var start = Math.Max(match.Index - 1, 0);
        var length = match.Length;
        if (match.Index > 0)
        {
            length++;
        }

        if (match.Index + match.Length < line.Length)
        {
            length++;
        }
        
        if (i > 0)
        {
            var above = lines[i-1].Substring(start, length);
            if (HasSymbol(above))
            {
                partNumbers.Add(new Part(i, match));
                continue;
            }
        }

        if (i < lines.Length - 1)
        {
            var below = lines[i + 1].Substring(start, length);
            if (HasSymbol(below))
            {
                partNumbers.Add(new Part(i, match));
                continue;
            }
        }
    }
}

foreach (var partNumber in partNumbers)
{
    Console.WriteLine(partNumber.Value);
}

Console.WriteLine($"Sum: {partNumbers.Sum(m => m.Value)}");

var gears = new List<Gear>();
for (var i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    var matches = Regex.Matches(line, "[*]");
    foreach (Match match in matches)
    {
        var parts = partNumbers
            .Where(p => p.LineNumber >= i - 1)
            .Where(p => p.LineNumber <= i + 1)
            .Where(p => p.End >= match.Index - 1)
            .Where(p => p.Start <= match.Index + match.Length)
            .ToArray();

        if (parts.Length == 2)
        {
            gears.Add(new Gear
            {
                LineNumber = i,
                Position = match.Index,
                Parts = parts,
                Ratio = parts[0].Value * parts[1].Value
            });
        }
    }
}

foreach (var gear in gears)
{
    Console.WriteLine($"Line {gear.LineNumber}, Index {gear.Position}, Ratio: {gear.Ratio}");
}

Console.WriteLine($"Total ratio: {gears.Sum(g => g.Ratio)}");

// Wrong: 467835, too low

bool HasSymbol(string text)
{
    return Regex.IsMatch(text, "[^0-9.]");
}