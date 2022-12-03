namespace Packing;

public class Rucksack
{
    public Rucksack(string items, int group)
    {
        Items = items;
        Group = group;
    }

    public string Items { get; }

    public int Group { get; }

    public string LeftCompartment => Items[..(Items.Length / 2)];

    public string RightCompartment => Items[(Items.Length / 2)..];

    public char BothCompartments => LeftCompartment.Join(RightCompartment, i => i, i => i, (l, r) => l).Distinct().Single();
}