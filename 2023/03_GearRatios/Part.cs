using System.Text.RegularExpressions;

namespace _03_GearRatios;

public class Part(int lineNumber, Match match)
{
    public int LineNumber { get; set; } = lineNumber;

    public int Start { get; set; } = match.Index;
    public int Length { get; set; } = match.Length;
    public int End { get; set; } = match.Index + match.Length - 1;

    public int Value { get; set; } = int.Parse(match.Value);
}