namespace _02_CubeConundrum;

public class Game
{
    public required int Id { get; init; }

    public required string Line { get; set; }
    
    public required IReadOnlyList<Draw> Draws { get; init; }
    
    public int MaxRed { get; set; }
    
    public int MaxBlue { get; set; }
    
    public int MaxGreen { get; set; }

    public int Power => MaxRed * MaxBlue * MaxGreen;

    public class Draw
    {
        public int Red { get; set; } = 0;
        public int Blue { get; set; } = 0;
        public int Green { get; set; } = 0;
    }
}