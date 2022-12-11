using System.Globalization;
using System.Numerics;
using MonkeyInTheMiddle;

Console.WriteLine("Load input data...");

var lines = (await File.ReadAllLinesAsync("input.txt"))
    .ToList();

var monkeys = ParseMonkeys(lines);

PrintMonkeys(monkeys);

for (int round = 1; round <= 20; round++)
{
    Console.WriteLine($"Round {round:00}:");
    foreach (var monkey in monkeys)
    {
        while (monkey.Items.TryDequeue(out var item))
        {
            monkey.Inspect(item, monkeys);
        }
    }
    PrintMonkeys(monkeys);
}

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

Monkey[] ParseMonkeys(List<string> inputLines)
{
    var parsedMonkeys = new List<Monkey>();

    for (int i = 0; i < inputLines.Count; i += 7)
    {
        var monkey = ParseMonkey(parsedMonkeys.Count, inputLines, i);
        parsedMonkeys.Add(monkey);
    }

    return parsedMonkeys.ToArray();
}

Monkey ParseMonkey(int index, IReadOnlyList<string> inputLines, int offset)
{
    var startingItems = inputLines[offset + 1]
        .Trim()["Starting items:".Length..]
        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(int.Parse)
        .ToArray();

    var operationDefinition = inputLines[offset + 2]
        .Trim()["Operation: ".Length..]
        .Split(' ')
        .ToArray();
    
    var operation = operationDefinition[3] switch
    {
        "+" => GetAddition<long>(index, operationDefinition[4]),
        "*" => GetMultiplication<long>(index, operationDefinition[4]),
        var sign => throw new Exception($"Arithmetic operation {sign} not recognized.")
    };

    var testDivider = int.Parse(inputLines[offset + 3]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Last());
    
    var targetMonkeyTrue = int.Parse(inputLines[offset + 4]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Last());
    
    var targetMonkeyFalse = int.Parse(inputLines[offset + 5]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Last());

    var monkey = new Monkey(index, operation, testDivider, targetMonkeyTrue, targetMonkeyFalse);

    for (var i = 0; i < startingItems.Length; i++)
    {
        var startingItem = startingItems[i];
        monkey.Items.Enqueue(new Item((index * 1000) + i)
        {
            WorryLevel = startingItem
        });
    }
    
    return monkey;
}

void PrintMonkeys(Monkey[] monkeysToPrint)
{
    foreach (var monkey in monkeysToPrint)
    {
        Console.WriteLine($"Monkey {monkey.Id}: TestDivider {monkey.TestDivider}, True: {monkey.TargetMonkeyTrue}, False: {monkey.TargetMonkeyFalse}, Items: {monkey.Items.Count}, Inspections: {monkey.Inspections}");
        if (monkey.Items.Count > 0)
        {
            Console.WriteLine($"Items: {string.Join(", ", monkey.Items.Select(i => $"{i.Id}: {i.WorryLevel}"))}");
        }
        else
        {
            Console.WriteLine("No items");
        }
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