namespace _03_GearRatios;

public class Gear
{
    public required int LineNumber { get; set; }
    public required int Position { get; set; }
    public required Part[] Parts { get; set; }

    public required int Ratio { get; set; }
}