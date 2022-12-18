using System.Drawing;

namespace HillClimbingAlgorithm;

public class Square
{
    public Square(Point position, int elevation)
    {
        Elevation = elevation;
        
        Position = position;
    }

    private Point Position { get; set; }

    public int Elevation { get; }

    public int? Distance { get; private set; }

    internal void CalculateFromStart(Square[,] area, Square endSquare)
    {
        Distance = 0;
        var waveFront = new HashSet<Square>
        {
            this
        };
        while (waveFront.Count > 0)
        {
            foreach (var square in waveFront)
            {
                CalculateMoves(area, square);
            }
            var newWaveFront = FindWaveFront(area);
            if (!IsWaveFrontDifferent(waveFront, newWaveFront))
            {
                break;
            }

            waveFront = newWaveFront;
        }

        if (area[endSquare.Position.X, endSquare.Position.Y].Distance.HasValue)
        {
            Console.WriteLine($"Total distance from {Position}: {area[endSquare.Position.X, endSquare.Position.Y].Distance}");
        }
        else
        {
            // Print(area, waveFront);
            Console.WriteLine($"No path found for starting point {Position}");
        }
    }

    private static void Print(Square[,] area, HashSet<Square> waveFront)
    {
        for (int x = 0; x < area.GetLength(0); x++)
        {
            for (int y = 0; y < area.GetLength(1); y++)
            {
                var square = area[x, y];
                if (!square.Distance.HasValue)
                {
                    Console.Write("0");
                }
                else
                {
                    if (waveFront.Contains(square))
                    {
                        Console.Write("+");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                }
            }
            Console.WriteLine();
        }
    }

    private static bool IsWaveFrontDifferent(HashSet<Square> oldWaveFront, HashSet<Square> newWaveFront)
    {
        if (oldWaveFront.Count != newWaveFront.Count)
        {
            return true;
        }

        foreach (var square in oldWaveFront)
        {
            if (!newWaveFront.Contains(square))
            {
                return true;
            }
        }

        return false;
    }

    private static void SetOtherDistance(Square other, Square square)
    {
        if (other.Elevation <= square.Elevation + 1)
        {
            if (!other.Distance.HasValue || other.Distance > square.Distance)
            {
                other.Distance = square.Distance + 1;
            }
        }
    }

    private static void CalculateMoves(Square[,] allSquares, Square square)
    {
        if (square.Position.X > 0)
        {
            var other = allSquares[square.Position.X - 1, square.Position.Y];
            SetOtherDistance(other, square);
        }

        if (square.Position.X < allSquares.GetLength(0) - 1)
        {
            var other = allSquares[square.Position.X + 1, square.Position.Y];
            SetOtherDistance(other, square);
        }

        if (square.Position.Y > 0)
        {
            var other = allSquares[square.Position.X, square.Position.Y - 1];
            SetOtherDistance(other, square);
        }

        if (square.Position.Y < allSquares.GetLength(1) - 1)
        {
            var other = allSquares[square.Position.X, square.Position.Y + 1];
            SetOtherDistance(other, square);
        }
    }

    private HashSet<Square> FindWaveFront(Square[,] area)
    {
        var waveFront = new HashSet<Square>();

        var allCoordinates = GetAllCoordinates(area);
        foreach (var coordinate in allCoordinates)
        {
            var square = area[coordinate.X, coordinate.Y];
            if (square.Distance.HasValue)
            {
                if (coordinate.X > 0)
                {
                    var other = area[coordinate.X - 1, coordinate.Y];
                    if (!other.Distance.HasValue)
                    {
                        waveFront.Add(square);
                        continue;
                    }
                }

                if (coordinate.X < area.GetLength(0) - 1)
                {
                    var other = area[coordinate.X + 1, coordinate.Y];
                    if (!other.Distance.HasValue)
                    {
                        waveFront.Add(square);
                        continue;
                    }
                }

                if (coordinate.Y > 0)
                {
                    var other = area[coordinate.X, coordinate.Y - 1];
                    if (!other.Distance.HasValue)
                    {
                        waveFront.Add(square);
                        continue;
                    }
                }

                if (coordinate.Y < area.GetLength(1) - 1)
                {
                    var other = area[coordinate.X, coordinate.Y + 1];
                    if (!other.Distance.HasValue)
                    {
                        waveFront.Add(square);
                    }
                }
            }
        }

        return waveFront;
    }

    internal static IEnumerable<Point> GetAllCoordinates(Square[,] area)
    {
        for (int x = 0; x < area.GetLength(0); x++)
        {
            for (int y = 0; y < area.GetLength(1); y++)
            {
                yield return new Point(x, y);
            }
        } 
    }
}