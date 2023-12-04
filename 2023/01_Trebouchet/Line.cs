namespace _01_Trebouchet;

public class Line(string line, int index)
{
    public int Index { get; init; } = index;
    public string Original { get; init; } = line;

    public string Edited { get; set; } = line;

    public char FirstDigit { get; set; } = '0';

    public char LastDigit { get; set; } = '0';

    public char[] Digits { get; set; } = Array.Empty<char>();

    public string Number { get; set; } = string.Empty;

    public int Value { get; set; } = -1;
    
    public char FindFirstDigit((string, int)[] replacements)
    {
        for (var i = 0; i < Original.Length; i++)
        {
            var charAtPosition = Original[i];
            if (charAtPosition >= '0' && charAtPosition <= '9')
            {
                return charAtPosition;
            }
            else
            {
                foreach (var replacement in replacements)
                {
                    if (Original.Substring(i).StartsWith(replacement.Item1))
                    {
                        return replacement.Item2.ToString()[0];
                    }
                }
            }
        }

        throw new Exception($"First digit in line {Index} not found: {Original}");
    }

    public char FindLastDigit((string, int)[] replacements)
    {
        for (var i = Original.Length - 1; i >= 0; i--)
        {
            var charAtPosition = Original[i];
            if (charAtPosition >= '0' && charAtPosition <= '9')
            {
                return charAtPosition;
            }
            else
            {
                foreach (var replacement in replacements)
                {
                    if (Original.Substring(i).StartsWith(replacement.Item1))
                    {
                        return replacement.Item2.ToString()[0];
                    }
                }
            }
        }

        throw new Exception($"Last digit in line {Index} not found: {Original}");
    }
}