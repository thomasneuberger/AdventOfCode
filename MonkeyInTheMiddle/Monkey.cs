namespace MonkeyInTheMiddle;

public class Monkey
{
    public Monkey(int id, Func<long, long> operation, int testDivider, int targetMonkeyTrue, int targetMonkeyFalse)
    {
        Operation = operation;
        TestDivider = testDivider;
        TargetMonkeyTrue = targetMonkeyTrue;
        TargetMonkeyFalse = targetMonkeyFalse;
        Id = id;
    }

    public int Id { get; }

    public Queue<Item> Items { get; } = new();

    public Func<long, long> Operation { get; }

    public int TestDivider { get; }

    public int TargetMonkeyTrue { get; }
    public int TargetMonkeyFalse { get; }

    public int Inspections { get; set; } = 0;

    public void Inspect(Item item, Monkey[] monkeys)
    {
        Console.WriteLine($"Inspect item in monkey {Id}");
        item.WorryLevel = Operation(item.WorryLevel) / 3;
        if (item.WorryLevel % TestDivider == 0)
        {
            Console.WriteLine($"Throw item {item.Id} with worry level {item.WorryLevel} to monkey {TargetMonkeyTrue}");
            monkeys[TargetMonkeyTrue].Items.Enqueue(item);
        }
        else
        {
            Console.WriteLine($"Throw item {item.Id} with worry level {item.WorryLevel} to monkey {TargetMonkeyFalse}");
            monkeys[TargetMonkeyFalse].Items.Enqueue(item);
        }

        Inspections++;
    }
}