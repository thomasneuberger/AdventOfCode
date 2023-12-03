Console.WriteLine("Load input data...");

var lines = (await File.ReadAllLinesAsync("input.txt"))
    .ToList();
    
var allTrees = ParseTrees(lines);

Console.WriteLine($"Trees: {allTrees.GetLength(0)}x{allTrees.GetLength(1)}");

var visibleTrees = new List<(int Row, int Column)>(); 
var hiddenTrees = new List<(int Row, int Column)>(); 
for (var row = 0; row < allTrees.GetLength(0); row++)
{
    for (var column = 0; column < allTrees.GetLength(1); column++)
    {
        var isVisible = IsTreeVisible(allTrees, row, column);
        if (isVisible)
        {
            visibleTrees.Add((row, column));
        }
        else
        {
            hiddenTrees.Add((row, column));
        }
    }
}

Console.WriteLine($"Visible: {visibleTrees.Count}, Hidden: {hiddenTrees.Count}, Total: {allTrees.GetLength(0) * allTrees.GetLength(1)}");

var highestScore = 0;
// highestScore = hiddenTrees
//     .Select(t => GetScenicScore(allTrees, t.Row, t.Column))
//     .Max();
for (var row = 0; row < allTrees.GetLength(0); row++)
{
    for (var column = 0; column < allTrees.GetLength(1); column++)
    {
        var score = GetScenicScore(allTrees, row, column);
        highestScore = Math.Max(highestScore, score);
    }
}

Console.WriteLine($"Highest score: {highestScore}");

int[,] ParseTrees(List<string> inputLines)
{
    var treeLines = inputLines
        .Select(l => l.Select(t => int.Parse(t.ToString())).ToArray())
        .ToArray();

    var treeHeights = new int[treeLines.Length, treeLines.Max(l => l.Length)];
    for (var row = 0; row < treeLines.Length; row++)
    {
        for (int column = 0; column < treeLines[row].Length; column++)
        {
            treeHeights[row, column] = treeLines[row][column];
        }
    }

    return treeHeights;
}

bool IsTreeVisible(int[,] trees, int row, int column)
{
    if (row == 0 || row == trees.GetLength(0) - 1)
    {
        return true;
    }
    if (column == 0 || column == trees.GetLength(1) - 1)
    {
        return true;
    }

    var highestTop = 0;
    for (int i = 0; i < row; i++)
    {
        highestTop = Math.Max(highestTop, trees[i, column]);
    }

    var highestBottom = 0;
    for (var i = row + 1; i < trees.GetLength(0); i++)
    {
        highestBottom = Math.Max(highestBottom, trees[i, column]);
    }

    var highestLeft = 0;
    for (var i = 0; i < column; i++)
    {
        highestLeft = Math.Max(highestLeft, trees[row, i]);
    }

    var highestRight = 0;
    for (var i = column + 1; i < trees.GetLength(1); i++)
    {
        highestRight = Math.Max(highestRight, trees[row, i]);
    }

    var height = trees[row, column];
    var visible = height > highestTop || height > highestBottom || height > highestLeft || height > highestRight;
    Console.WriteLine($"Highest for tree {row}x{column}({height}): Top {highestTop}, Bottom {highestBottom}, Left {highestLeft}, Right {highestRight}, Visible: {visible}");
    return visible;
}

int GetScenicScore(int[,] trees, int row, int column)
{
    var lastRow = trees.GetLength(0) - 1;
    var lastColumn = trees.GetLength(1) - 1;
    if (row == 0 || row == lastRow)
    {
        return 0;
    }
    if (column == 0 || column == lastColumn)
    {
        return 0;
    }

    var visibleTop = 1;
    for (var i = row - 1; trees[i, column] < trees[row, column]; i--)
    {
        visibleTop++;
        if (i == 0)
        {
            visibleTop--;
            break;
        }
    }

    var visibleBottom = 1;
    for (var i = row + 1; i < lastRow && trees[i, column] < trees[row, column]; i++)
    {
        visibleBottom++;
        if (i == lastRow)
        {
            visibleBottom--;
            break;
        }
    }

    var visibleLeft = 1;
    for (var i = column - 1; i >= 0 && trees[row, i] < trees[row, column]; i--)
    {
        visibleLeft++;
        if (i == 0)
        {
            visibleLeft--;
            break;
        }
    }

    var visibleRight = 1;
    for (var i = column + 1; i < lastColumn && trees[row, i] < trees[row, column]; i++)
    {
        visibleRight++;
        if (i == lastColumn)
        {
            visibleRight--;
            break;
        }
    }
    
    var height = trees[row, column];
    var score = visibleTop * visibleBottom * visibleLeft * visibleRight;
    Console.WriteLine($"Highest for tree {row}x{column}({height}): Top {visibleTop}, Bottom {visibleBottom}, Left {visibleLeft}, Right {visibleRight}, Score {score}");
    
    return score;
}