using System.Drawing;

namespace RegolithReservoir;

public class Cave
{
    public Cave(IReadOnlyList<(Point From, Point To)> rockLines)
    {
        var maxY = rockLines
            .Select(r => Math.Max(r.From.Y, r.To.Y))
            .Max();

        CaveArea = new char[1000, maxY + 2];
        Console.WriteLine($"Cave {CaveArea.GetLength(0)}x{CaveArea.GetLength(1)}");
        foreach (var coordinate in GetCoordinates())
        {
            CaveArea[coordinate.X, coordinate.Y] = '.';
        }

        foreach (var rockLine in rockLines)
        {
            Console.WriteLine($"Rock line from {rockLine.From} to {rockLine.To}");
            CaveArea[rockLine.From.X, rockLine.From.Y] = '#';
            if (rockLine.From.X != rockLine.To.X)
            {
                var startX = Math.Min(rockLine.From.X, rockLine.To.X);
                var endX = Math.Max(rockLine.From.X, rockLine.To.X);
                for (var x = startX; x <= endX; x++)
                {
                    CaveArea[x, rockLine.From.Y] = '#';
                }
            }

            if (rockLine.From.Y != rockLine.To.Y)
            {
                var startY = Math.Min(rockLine.From.Y, rockLine.To.Y);
                var endY = Math.Max(rockLine.From.Y, rockLine.To.Y);
                for (var y = startY; y <= endY; y++)
                {
                    CaveArea[rockLine.From.X, y] = '#';
                }
            }
        }

        SandIngress = new Point(500, 0);
        CaveArea[SandIngress.X, SandIngress.Y] = '+';
    }

    public char[,] CaveArea { get; }

    private int MaxX => CaveArea.GetLength(0) - 1;
    private int MaxY => CaveArea.GetLength(1) - 1;

    public Point SandIngress { get; }

    private IEnumerable<Point> GetCoordinates()
    {
        for (var row = 0; row < CaveArea.GetLength(0); row++)
        {
            for (var column = 0; column < CaveArea.GetLength(1); column++)
            {
                yield return new Point(row, column);
            }
        }
    }

    internal bool AddSand()
    {
        if (CaveArea[SandIngress.X, SandIngress.Y] == 'o')
        {
            return false;
        }
        
        var sand = SandIngress;

        Point movedSand = sand;
        do
        {
            sand = movedSand;
            movedSand = MoveSand(sand);
        } while (movedSand != sand);
        
        CaveArea[movedSand.X, movedSand.Y] = 'o';

        return true;
    }

    private Point MoveSand(Point sand)
    {
        if (sand.Y > MaxY || sand.X < 0 || sand.X > MaxX)
        {
            throw new Exception("Outside the box");
        }

        if (sand.Y == MaxY)
        {
            return sand;
        }
        
        if (CaveArea[sand.X, sand.Y + 1] == '.')
        {
            return sand with { Y = sand.Y + 1 };
        }

        if (sand.X == 0)
        {
            throw new Exception("Too far left");
        }
        
        if (CaveArea[sand.X - 1, sand.Y + 1] == '.')
        {
            return sand with { X = sand.X - 1,  Y = sand.Y + 1 };
        }

        if (sand.X >= MaxX)
        {
            throw new Exception("Too far right");
        }

        if (CaveArea[sand.X + 1, sand.Y + 1] == '.')
        {
            return sand with { X = sand.X + 1,  Y = sand.Y + 1 };
        }

        return sand;
    }

    internal void Print()
    {
        var firstColumn = FindFirstColumn();
        var lastColumn = FindLastColumn();
        for (var row = 0; row <= MaxY; row++)
        {
            Console.Write($"{row:0000} ");
            for (var column = firstColumn; column <= lastColumn; column++)
            {
                Console.Write(CaveArea[column, row]);
            }
            Console.WriteLine();
        }
    }

    private int FindFirstColumn()
    {
        for (var column = 0; column <= MaxX; column++)
        {
            for (var row = 0; row <= MaxY; row++)
            {
                if (CaveArea[column, row] != '.')
                {
                    return column;
                }
            }
        }

        return 0;
    }

    private int FindLastColumn()
    {
        for (var column = MaxX; column > 0; column--)
        {
            for (var row = 0; row <= MaxY; row++)
            {
                if (CaveArea[column, row] != '.')
                {
                    return column;
                }
            }
        }

        return MaxX;
    }
}