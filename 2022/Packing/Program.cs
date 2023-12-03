using Packing;

Console.WriteLine("Load input data...");

var lines = (await File.ReadAllLinesAsync("input.txt"))
    .ToList();
    
var rucksacks = lines
    .Select((l, i) => new Rucksack(l, i / 3))
    .ToList();

foreach (var rucksack in rucksacks)
{
    Console.WriteLine($"{rucksack.Items}: Left {rucksack.LeftCompartment}, Right {rucksack.RightCompartment}, Both {rucksack.BothCompartments}, Priority {GetPriority(rucksack.BothCompartments)}");
}

var elfPrioritySum = rucksacks.Sum(r => GetPriority(r.BothCompartments));

Console.WriteLine($"Elf Priority sum: {elfPrioritySum}");

var groups = rucksacks
    .GroupBy(r => r.Group)
    .Select(g => new Group(g.Key, g.ToArray()))
    .ToList();

foreach (var group in groups)
{
    Console.WriteLine($"Group {group.Number}: Rucksacks {group.Rucksacks.Length}, Badge {group.Badge}, Priority {GetPriority(group.Badge)}");
}

var groupPrioritySum = groups.Sum(g => GetPriority(g.Badge));

Console.WriteLine($"Group priority sum: {groupPrioritySum}");

static int GetPriority(int item)
{
    return item switch
    {
        < 'a' => item - 'A' + 27,
        _ => item - 'a' + 1
    };
}