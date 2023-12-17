using _06_WaitForIt;

var races = Inputs.ParseInput(Inputs.Puzzle);

var margin = 1;
foreach (var race in races)
{
    var minButtonTime = Enumerable.Range(0, int.MaxValue)
        .SkipWhile(b => CalculateDistance(race.Time, b) <= race.Distance)
        .First();
    var maxButtonTime = Enumerable.Range(minButtonTime, int.MaxValue - minButtonTime)
        .TakeWhile(b => CalculateDistance(race.Time, b) > race.Distance)
        .Last();
    margin *= (maxButtonTime - minButtonTime + 1);
    Console.WriteLine($"{race} - Min: {minButtonTime}, Max: {maxButtonTime}");
}

Console.WriteLine($"Margin: {margin}");

long CalculateDistance(long time, long buttonTime)
{
    return buttonTime * (time - buttonTime);
}