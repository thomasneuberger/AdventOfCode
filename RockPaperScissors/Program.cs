using System.Threading.Tasks.Dataflow;
using RockPaperScissors;

Console.WriteLine("Load input data...");

var lines = (await File.ReadAllLinesAsync("input.txt"))
    .Select(l => l.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
    .ToList();

FirstGuess(lines);
SecondGuess(lines);

static void FirstGuess(IEnumerable<string[]> lines)
{
    var rounds = lines
        .Select(r =>
        {
            var opponentMove = r[0] switch
            {
                "A" => Move.Rock,
                "B" => Move.Paper,
                "C" => Move.Scissors
            };
            var ownMove = r[1] switch
            {
                "X" => Move.Rock,
                "Y" => Move.Paper,
                "Z" => Move.Scissors
            };
            return new Round(opponentMove, ownMove);
        })
        .ToArray();

    var totalPoints = GetTotalPoints(rounds);

    Console.WriteLine($"Total points first guess: {totalPoints}");
}

static void SecondGuess(IEnumerable<string[]> lines)
{
    var roundsResults = lines
        .Select(r =>
        {
            var opponentMove = r[0] switch
            {
                "A" => Move.Rock,
                "B" => Move.Paper,
                "C" => Move.Scissors
            };
            var result = r[1] switch
            {
                "X" => Result.Lose,
                "Y" => Result.Draw,
                "Z" => Result.Win
            };
            return new RoundResult(opponentMove, result);
        })
        .ToArray();
    var rounds = roundsResults
        .Select(rr =>
        {
            var ownMove = rr.OpponentMove switch
            {
                Move.Rock => rr.Result switch
                {
                    Result.Lose => Move.Scissors,
                    Result.Draw => Move.Rock,
                    Result.Win => Move.Paper
                },
                Move.Paper => rr.Result switch
                {
                    Result.Lose => Move.Rock,
                    Result.Draw => Move.Paper,
                    Result.Win => Move.Scissors
                },
                Move.Scissors => rr.Result switch
                {
                    Result.Lose => Move.Paper,
                    Result.Draw => Move.Scissors,
                    Result.Win => Move.Rock
                }
            };
            return new Round(rr.OpponentMove, ownMove);
        })
        .ToArray();

    var totalPoints = GetTotalPoints(rounds);

    Console.WriteLine($"Total points second guess: {totalPoints}");
}

static int GetTotalPoints(IEnumerable<Round> rounds)
{
    var totalPoints = 0;

    foreach (var round in rounds)
    {
        var roundPoints = GetRoundPoints(round);
        Console.WriteLine($"{round.OwnMove} <=> {round.OpponentMove} ==> {roundPoints} points.");
        totalPoints += roundPoints;
    }

    return totalPoints;
}

static int GetRoundPoints(Round round)
{
    switch (round.OwnMove)
    {
        case Move.Rock:
        {
            var movePoints = 1;
            switch (round.OpponentMove)
            {
                case Move.Rock: return 3 + movePoints;
                case Move.Paper: return 0 + movePoints;
                case Move.Scissors: return 6 + movePoints;
                default: throw new Exception();
            }
        }
        case Move.Paper:
        {
            var movePoints = 2;
            switch (round.OpponentMove)
            {
                case Move.Rock: return 6 + movePoints;
                case Move.Paper: return 3 + movePoints;
                case Move.Scissors: return 0 + movePoints;
                default: throw new Exception();
            }
        }
        case Move.Scissors:
        {
            var movePoints = 3;
            switch (round.OpponentMove)
            {
                case Move.Rock: return 0 + movePoints;
                case Move.Paper: return 6 + movePoints;
                case Move.Scissors: return 3 + movePoints;
                default: throw new Exception();
            }
        }
        default: throw new Exception();
    }
}