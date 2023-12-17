namespace _06_WaitForIt;

public static class Inputs
{
    public const string Example1 = """
                                   Time:      7  15   30
                                   Distance:  9  40  200
                                   """;

    public const string Puzzle = """
                                 Time:        48     98     90     83
                                 Distance:   390   1103   1112   1360
                                 """;

    public static IReadOnlyList<Race> ParseInput(string input)
    {
        var lines = input.Split("\n\r".ToArray(), StringSplitOptions.RemoveEmptyEntries);

        // var times = lines[0].Split(':', StringSplitOptions.TrimEntries)[1]
        //     .Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
        // var distances = lines[1].Split(':', StringSplitOptions.TrimEntries)[1]
        //     .Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
        //
        // return times
        //     .Select((t, i) => new Race
        //     {
        //         Time = t,
        //         Distance = distances[i]
        //     })
        //     .ToArray();

        var time = long.Parse(lines[0].Split(':', StringSplitOptions.TrimEntries)[1].Replace(" ", ""));
        var distance = long.Parse(lines[1].Split(':', StringSplitOptions.TrimEntries)[1].Replace(" ", ""));
        return new[]
        {
            new Race
            {
                Time = time,
                Distance = distance
            }
        };
    }
}