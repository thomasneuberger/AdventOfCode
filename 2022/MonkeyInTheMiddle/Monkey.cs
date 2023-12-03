using System.Numerics;

namespace MonkeyInTheMiddle;

public class Monkey<T>
    where T : INumber<T>
{
    public Monkey(int id, Func<T, T> operation, T testDivider, int targetMonkeyTrue, int targetMonkeyFalse)
    {
        Operation = operation;
        TestDivider = testDivider;
        TargetMonkeyTrue = targetMonkeyTrue;
        TargetMonkeyFalse = targetMonkeyFalse;
        Id = id;
    }

    public int Id { get; }

    public Queue<Item<T>> Items { get; } = new();

    public Func<T, T> Operation { get; }

    public T TestDivider { get; }

    public int TargetMonkeyTrue { get; }
    public int TargetMonkeyFalse { get; }

    public long Inspections { get; private set; } = 0;

    public void Inspect(Item<T> item, Monkey<T>[] monkeys)
    {
        item.WorryLevel = Operation(item.WorryLevel);
        if (item.WorryLevel % TestDivider == T.Zero)
        {
            // Console.WriteLine($"Monkey {Id} throws item {item.Id} with worry level {item.WorryLevel} to monkey {TargetMonkeyTrue}");
            monkeys[TargetMonkeyTrue].Items.Enqueue(item);
        }
        else
        {
            // Console.WriteLine($"Monkey {Id} throws item {item.Id} with worry level {item.WorryLevel} to monkey {TargetMonkeyFalse}");
            monkeys[TargetMonkeyFalse].Items.Enqueue(item);
        }

        Inspections++;
    }
}