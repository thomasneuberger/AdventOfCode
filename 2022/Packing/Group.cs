namespace Packing;

public class Group
{
    public Group(int number, Rucksack[] rucksacks)
    {
        Rucksacks = rucksacks;
        Number = number;
    }

    public int Number { get; }
    
    public Rucksack[] Rucksacks { get; }

    public char Badge
    {
        get
        {
            return Rucksacks[0].Items
                .Join(Rucksacks[1].Items, i => i, i => i, (i, _) => i)
                .Join(Rucksacks[2].Items, i => i, i => i, (i, _) => i)
                .Distinct()
                .Single();
        }
    }
}