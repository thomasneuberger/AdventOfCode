Console.WriteLine("Load input data...");

var lines = (await File.ReadAllLinesAsync("input.txt"))
    .ToList();
    
var register = 1;
var cycles = 0;

var sumOfSignalStrengths = 0;

var output = string.Empty;

foreach (var line in lines)
{
    if (line == "noop")
    {
        Noop();
    }
    else
    {
        var argument = int.Parse(line.Split(" ")[1]);
        AddX(argument);
    }
}

Console.WriteLine($"Sum of signal strengths after {cycles} cycles: {sumOfSignalStrengths}");

Print();

void Noop()
{
    Console.WriteLine("Noop");
    Cycle();
}

void AddX(int argument)
{
    Console.WriteLine($"Add {argument}");
    Cycle();
    
    Cycle();

    register += argument;
}

void Cycle()
{
    var column = cycles % 40;
    if (column >= register - 1 && column <= register +1)
    {
        output += "#";
    }
    else
    {
        output += ".";
    }
    cycles++;
    
    var is20 = cycles == 20;
    var islater = cycles > 20 &&  (cycles - 20) % 40 == 0;

    if (is20 || islater)
    {
        var strength = register * cycles;
        Console.WriteLine($"Cycle {cycles}: Register {register}, Signal Strength {strength}");
        sumOfSignalStrengths += strength;
    }
    else
    {
        Console.WriteLine($"Cycle {cycles}: Register {register}");
    }
}

void Print()
{
    Console.WriteLine($"Output: {output}");
    for (int row = 0; row < 6; row++)
    {
        var line = output.Substring(40 * row, 40);
        Console.WriteLine($"Row {row} ({40 * row:000}-{40 * (row + 1):000}: {line}");
    }
}