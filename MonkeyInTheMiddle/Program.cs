using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using MonkeyInTheMiddle;

var stopwatch = new Stopwatch();

Console.WriteLine("Load input data...");

var lines = (await File.ReadAllLinesAsync("input.txt"))
    .ToList();

var monkeys = ParseMonkeys<long>(lines);

PrintMonkeys(monkeys);

var maxWorryLevel = monkeys
    .Aggregate(1L, (p, m) => m.TestDivider * p);

var printMonkeysInRounds = Enumerable.Range(0, 10000 / 1000)
    .Select(r => r * 1000)
    .ToHashSet();
printMonkeysInRounds.Add(1);
printMonkeysInRounds.Add(20);

stopwatch.Start();
var roundTimer = new Stopwatch();
for (int round = 1; round <= 10000; round++)
{
    roundTimer.Restart();
    foreach (var monkey in monkeys)
    {
        while (monkey.Items.TryDequeue(out var item))
        {
            monkey.Inspect(item, monkeys);
            item.WorryLevel %= maxWorryLevel;
        }
    }

    if (printMonkeysInRounds.Contains(round))
    {
        Console.WriteLine($"Round {round:0000} in {roundTimer.Elapsed} (after {stopwatch.Elapsed}):");
        PrintMonkeys(monkeys);
        
        var totalInspections = monkeys
            .Sum(m => m.Inspections);
        Console.WriteLine($"Total Inspections: {totalInspections} ({totalInspections / round} per round)");
    }
}
stopwatch.Stop();
Console.WriteLine($"Round 10000 (after {stopwatch.Elapsed}):");
PrintMonkeys(monkeys);

var mostActiveMonkeys = monkeys
    .OrderByDescending(m => m.Inspections)
    .Take(2)
    .ToArray();

Console.WriteLine("Most active monkeys:");
foreach (var monkey in mostActiveMonkeys)
{
    Console.WriteLine($"Monkey {monkey.Id} with {monkey.Inspections} inspections.");
}

long activity = mostActiveMonkeys.Aggregate(1L, (prev, m) => prev * m.Inspections);

Console.WriteLine($"Activity: {activity}");

Monkey<T>[] ParseMonkeys<T>(List<string> inputLines)
    where T : INumber<T>
{
    var parsedMonkeys = new List<Monkey<T>>();

    for (int i = 0; i < inputLines.Count; i += 7)
    {
        var monkey = ParseMonkey<T>(parsedMonkeys.Count, inputLines, i);
        parsedMonkeys.Add(monkey);
    }

    return parsedMonkeys.ToArray();
}

Monkey<T> ParseMonkey<T>(int index, IReadOnlyList<string> inputLines, int offset)
    where T : INumber<T>, IParsable<T>
{
    var startingItems = inputLines[offset + 1]
        .Trim()["Starting items:".Length..]
        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(s => T.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture))
        .ToArray();

    var operationDefinition = inputLines[offset + 2]
        .Trim()["Operation: ".Length..]
        .Split(' ')
        .ToArray();
    
    var operation = operationDefinition[3] switch
    {
        "+" => GetAddition<T>(index, operationDefinition[4]),
        "*" => GetMultiplication<T>(index, operationDefinition[4]),
        var sign => throw new Exception($"Arithmetic operation {sign} not recognized.")
    };

    var testDivider = T.Parse(inputLines[offset + 3]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Last(), NumberStyles.Integer, CultureInfo.InvariantCulture);
    
    var targetMonkeyTrue = int.Parse(inputLines[offset + 4]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Last());
    
    var targetMonkeyFalse = int.Parse(inputLines[offset + 5]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Last());

    var monkey = new Monkey<T>(index, operation, testDivider, targetMonkeyTrue, targetMonkeyFalse);

    for (var i = 0; i < startingItems.Length; i++)
    {
        var startingItem = startingItems[i];
        monkey.Items.Enqueue(new Item<T>((index * 1000) + i, startingItem));
    }
    
    return monkey;
}

void PrintMonkeys<T>(Monkey<T>[] monkeysToPrint)
    where T : INumber<T>
{
    foreach (var monkey in monkeysToPrint)
    {
        Console.WriteLine($"Monkey {monkey.Id}: TestDivider {monkey.TestDivider}, True: {monkey.TargetMonkeyTrue}, False: {monkey.TargetMonkeyFalse}, Items: {monkey.Items.Count}, Inspections: {monkey.Inspections}");
    }

    var totalItems = monkeysToPrint
        .Sum(m => m.Items.Count);
    Console.WriteLine($"Total items: {totalItems}");
}

Func<T, T> GetAddition<T>(int monkeyId, string value)
    where T : INumber<T>, IParsable<T>
{
    if (T.TryParse(value, CultureInfo.InvariantCulture, out var number))
    {
        Console.WriteLine($"Operation for monkey {monkeyId}: new = old + {number}");
        return old => old + number;
    }
    else
    {
        Console.WriteLine($"Operation for monkey {monkeyId}: new = old + old");
        return old => old + old;
    }
}

Func<T, T> GetMultiplication<T>(int monkeyId, string value)
    where T : INumber<T>, IParsable<T>
{
    if (T.TryParse(value, CultureInfo.InvariantCulture, out var number))
    {
        Console.WriteLine($"Operation for monkey {monkeyId}: new = old * {number}");
        return old => old * number;
    }
    else
    {
        Console.WriteLine($"Operation for monkey {monkeyId}: new = old * old");
        return old => old * old;
    }
}