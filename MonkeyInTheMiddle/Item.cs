namespace MonkeyInTheMiddle;

public class Item
{
    public Item(int id)
    {
        Id = id;
    }

    public int Id { get; }
    public long WorryLevel { get; set; }
}