Console.WriteLine("Load input data...");

var lines = (await File.ReadAllLinesAsync("input.txt"))
    .ToList();

var crateLines = lines
    .TakeWhile(l => !string.IsNullOrWhiteSpace(l))
    .ToList();

var stacks = ParseStacks(crateLines);

PrintTopCrates(stacks);

var moveLines = lines
    .Skip(crateLines.Count + 1);

MoveCrates(moveLines, stacks);

PrintTopCrates(stacks);

var output = string.Join("", stacks.Select(s => s.Peek()));
Console.WriteLine(output);

void PrintTopCrates(Stack<string>[] stacksToPrint)
{
    for (var i = 0; i < stacksToPrint.Length; i++)
    {
        var stack = stacksToPrint[i];
        Console.WriteLine($"Stack {i + 1}: {stack.Peek()}");
    }
}

Stack<string>[] ParseStacks(List<string> stackLines)
{
    var parsedStacks = stackLines
        .Last()
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(s => new Stack<string>())
        .ToArray();

    foreach (var line in stackLines.Take(stackLines.Count - 1).Reverse())
    {
        for (int s = 0; s < parsedStacks.Length; s++)
        {
            var crate = line.Substring(s * 4, 3);
            if (!string.IsNullOrWhiteSpace(crate))
            {
                parsedStacks[s].Push(crate.Substring(1, 1));
            }
        }
    }

    return parsedStacks;
}

void MoveCrates(IEnumerable<string> moves, Stack<string>[] stacksToMove)
{
    foreach (var move in moves)
    {
        var tokens = move.Split(" ");
        var count = int.Parse(tokens[1]);
        var from = int.Parse(tokens[3]);
        var to = int.Parse(tokens[5]);
        var tempStack = new Stack<string>();
        for (int i = 0; i < count; i++)
        {
            var crate = stacksToMove[from - 1].Pop();
            tempStack.Push(crate);
        }
        for (int i = 0; i < count; i++)
        {
            var crate = tempStack.Pop();
            stacksToMove[to - 1].Push(crate);
        }
    }
}