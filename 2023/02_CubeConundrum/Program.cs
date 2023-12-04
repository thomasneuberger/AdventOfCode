using _02_CubeConundrum;

var games = Inputs.ParseGames(Inputs.Puzzle);

foreach (var game in games)
{
    Console.WriteLine(game.Line);

    game.MaxRed = game.Draws.Max(d => d.Red);
    game.MaxBlue = game.Draws.Max(d => d.Blue);
    game.MaxGreen = game.Draws.Max(d => d.Green);
    
    Console.WriteLine($"Red: {game.MaxRed}, Blue: {game.MaxBlue}, Green: {game.MaxGreen}");
}

var possibleGames = games
    .Where(g => g.MaxRed <= 12)
    .Where(g => g.MaxBlue <= 14)
    .Where(g => g.MaxGreen <= 13)
    .ToArray();

foreach (var game in possibleGames)
{
    Console.WriteLine($"Possible: {game.Id}, Power: {game.Power}");
}

Console.WriteLine($"Total: {possibleGames.Sum(g => g.Id)}");
Console.WriteLine($"Total power: {games.Sum(g => g.Power)}");