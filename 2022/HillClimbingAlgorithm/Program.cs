using System.Drawing;
using HillClimbingAlgorithm;

Console.WriteLine("Load input data...");

var input = (await File.ReadAllLinesAsync("input.txt"))
    .ToList();

var (originalArea, originalStart, end) = ParseInput(input);

Console.WriteLine($"Area: {originalArea.GetLength(0)}x{originalArea.GetLength(1)} Start: {originalStart}, End: {end}");

var startPoints = new List<(Point StartPoint, int Distance)>();
for (var x = 0; x < originalArea.GetLength(0); x++)
{
    for (var y = 0; y < originalArea.GetLength(1); y++)
    {
        var startPoint = originalArea[x, y];
        if (startPoint.Elevation == 'a')
        {
            var areaToTest = ParseInput(input).Area;
            var startSquare = areaToTest[x, y];
            startSquare.CalculateFromStart(areaToTest, areaToTest[end.X, end.Y]);
            var endSquare = areaToTest[end.X, end.Y];
            if (endSquare.Distance.HasValue)
            {
                startPoints.Add((new Point(x, y), endSquare.Distance.Value));
            }
        }
    }
}

var bestStartPoint = startPoints.MinBy(p => p.Distance);
Console.WriteLine($"Best start point: {bestStartPoint.StartPoint}: {bestStartPoint.Distance}");

(Square[,] Area, Point Start, Point End) ParseInput(IReadOnlyList<string> lines)
{
    var parsedArea = new Square[lines[0].Length, lines.Count];
    var startSquare = Point.Empty;
    var endSquare = Point.Empty;

    for (var row = 0; row < lines.Count; row++)
    {
        var line = lines[row];
        for (var column = 0; column < line.Length; column++)
        {
            var isStart = false;
            var isEnd = false;
            
            var elevation = line[column];
            if (elevation == 'S')
            {
                elevation = 'a';
                isStart = true;
            }
            if (elevation == 'E')
            {
                elevation = 'z';
                isEnd = true;
            }
            
            parsedArea[column, row] = new Square(new Point(column, row), elevation);

            if (isStart)
            {
                startSquare = new Point(column, row);
            }

            if (isEnd)
            {
                endSquare = new Point(column, row);
            }
        }
    }

    return (parsedArea, startSquare, endSquare);
}