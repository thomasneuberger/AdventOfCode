using System.Buffers;
using System.Text.RegularExpressions;
using _01_Trebouchet;

Part2();

void Part1()
{
    var lines = Inputs.Puzzle
        .Split("\n\r".ToArray(), StringSplitOptions.RemoveEmptyEntries)
        .Select(l => l.ToArray()
            .Where(c => c >= '0' && c <= '9')
            .ToArray())
        .Select(l => l.First().ToString() + l.Last())
        .Select(n => int.Parse(n))
        .ToArray();
    foreach (var number in lines)
    {
        Console.WriteLine(number);
    }

    Console.WriteLine($"Total: {lines.Sum()}");
}



void Part2()
{
    var replacements = new[]
    {
        ("one", 1),
        ("two", 2),
        ("three", 3),
        ("four", 4),
        ("five", 5),
        ("six", 6),
        ("seven", 7),
        ("eight", 8),
        ("nine", 9)
    };
    
    var lines = Inputs.Puzzle
        .Split("\n\r".ToArray(), StringSplitOptions.RemoveEmptyEntries)
        .Select((l, i) => new Line(l, i))
        .ToList();

    foreach (var line in lines)
    {
        line.FirstDigit = line.FindFirstDigit(replacements);

        line.LastDigit = line.FindLastDigit(replacements);

        line.Number = $"{line.FirstDigit}{line.LastDigit}";

        line.Value = int.Parse(line.Number);
    }
    foreach (var line in lines)
    {
        Console.Write($"Original: {line.Original.PadRight(lines.Max(l => l.Original.Length) + 10)}");
        Console.Write($"Edited: {line.Edited.PadRight(lines.Max(l => l.Edited.Length) + 8)}");
        Console.Write($"First digit: {line.FirstDigit.ToString().PadRight(14)}");
        Console.Write($"Last digit: {line.LastDigit.ToString().PadRight(13)}");
        Console.Write($"Number: {line.Number.PadRight(lines.Max(l => l.Number.Length) + 8)}");
        Console.WriteLine($"Value: {line.Value}");
    }

    Console.WriteLine($"Total: {lines.Sum(l => l.Value)}");
    
    // Wrong: 53846 (too low)
}



