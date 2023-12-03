namespace ClearingSections;

public class Pair
{
    public Pair(Elf first, Elf second)
    {
        First = first;
        Second = second;
    }

    public Elf First { get; }
    public Elf Second { get; }

    internal bool IsFullyContained()
    {
        if (First.From <= Second.From && First.To >= Second.To)
        {
            return true;
        }

        if (First.From >= Second.From && First.To <= Second.To)
        {
            return true;
        }

        return false;
    }

    internal bool Overlap()
    {
        if (First.From <= Second.To && First.To >= Second.From)
        {
            return true;
        }

        return false;
    }
}