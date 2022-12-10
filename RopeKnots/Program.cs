using System.Drawing;

Console.WriteLine("Load input data...");

var lines = (await File.ReadAllLinesAsync("input.txt"))
    .ToList();

var movements = lines
    .Select(l => l.Split(" "))
    .Select(l => new
    {
        Direction = l[0],
        Distance = int.Parse(l[1])
    })
    .Select(m => (m.Distance, m.Direction))
    .ToList();

var visited = new Dictionary<Point, Point>();
var knots = new Point[10];
visited[knots[9]] = knots[9];

foreach (var movement in movements)
{
    Console.WriteLine($"Move {movement.Distance} to {movement.Direction}");
    
    for (var i = 0; i < movement.Distance; i++)
    {
        MoveHead(movement);

        for (var j = 1; j < knots.Length; j++)
        {
            MoveKnot(ref knots[j], knots[j - 1]);
        }

        PrintPositions();
        
        visited[knots[9]] = knots[9];
    }
}

Console.WriteLine($"Tail positions: {visited.Count}");

void PrintPositions()
{
    Console.WriteLine($"Head: {knots[0]}, {string.Join(", ", knots[1..^1])} , Tail: {knots[9]}");
}

void MoveHead((int Distance, string Direction) valueTuple)
{
    switch (valueTuple.Direction)
    {
        case "U":
            MoveUp(ref knots[0]);
            break;
        case "D":
            MoveDown(ref knots[0]);
            break;
        case "L":
            MoveLeft(ref knots[0]);
            break;
        case "R":
            MoveRight(ref knots[0]);
            break;
    }
}

void MoveKnot(ref Point knot, Point prev)
{
    if (prev.X > knot.X + 1)
    {
        MoveRight(ref knot);
        knot = knot with { Y = GetSecondDiagonalMoveComponent(knot.Y, prev.Y) };
    }

    if (prev.X < knot.X - 1)
    {
        MoveLeft(ref knot);
        knot = knot with { Y = GetSecondDiagonalMoveComponent(knot.Y, prev.Y) };
    }

    if (prev.Y > knot.Y + 1)
    {
        MoveUp(ref knot);
        knot = knot with { X = GetSecondDiagonalMoveComponent(knot.X, prev.X) };
    }

    if (prev.Y < knot.Y - 1)
    {
        MoveDown(ref knot);
        knot = knot with { X = GetSecondDiagonalMoveComponent(knot.X, prev.X) };
    }
}

int GetSecondDiagonalMoveComponent(int knotComponent, int previousComponent)
{
    if (knotComponent < previousComponent - 1)
    {
        return knotComponent + 1;
    }

    if (knotComponent > previousComponent + 1)
    {
        return knotComponent - 1;
    }

    return previousComponent;
} 

void MoveUp(ref Point knot)
{
    knot = knot with { Y = knot.Y + 1 };
}

void MoveDown(ref Point knot)
{
    knot = knot with { Y = knot.Y - 1 };
}

void MoveLeft(ref Point knot)
{
    knot = knot with { X = knot.X - 1 };
}

void MoveRight(ref Point knot)
{
    knot = knot with { X = knot.X + 1 };
}