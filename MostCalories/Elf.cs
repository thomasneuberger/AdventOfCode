namespace MostCalories;

public class Elf
{
    public Elf(int number)
    {
        Number = number;
    }

    public int Number { get; }

    public int Calories { get; set; } = 0;
}