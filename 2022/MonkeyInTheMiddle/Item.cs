using System.Numerics;

namespace MonkeyInTheMiddle;

public class Item<T>
    where T : INumber<T>
{
    public Item(int id, T initialWorryLevel)
    {
        Id = id;
        WorryLevel = initialWorryLevel;
    }

    public int Id { get; }
    public T WorryLevel { get; set; }
}