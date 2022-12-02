using MostCalories;

Console.WriteLine("Load input data...");

var lines = await File.ReadAllLinesAsync("input.txt");

var currentElf = new Elf(1);
var elves = new List<Elf>
{
    currentElf
};
foreach (var line in lines)
{
    if (int.TryParse(line, out var calories))
    {
        currentElf.Calories += calories;
    }
    else
    {
        currentElf = new Elf(currentElf.Number + 1);
        elves.Add(currentElf);
    }
}

Console.WriteLine($"{elves.Count} Elves found.");

var maxCalories = elves.MaxBy(e => e.Calories);

Console.WriteLine($"The elf with the most calories is elf #{maxCalories.Number} with {maxCalories.Calories} calories.");

var maxCaloryElves = elves.OrderByDescending(e => e.Calories).Take(3);

var numbers = string.Join(", ", maxCaloryElves.Select(e => e.Number));
var elfCalories = string.Join(", ", maxCaloryElves.Select(e => e.Calories));
var sumCalories = maxCaloryElves.Sum(e => e.Calories);
Console.WriteLine($"The elves with the most calories are elf {numbers} with {elfCalories} calories which makes a sum of {sumCalories} calories.");