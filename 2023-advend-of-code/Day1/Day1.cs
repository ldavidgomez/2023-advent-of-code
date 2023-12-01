using System.Text.RegularExpressions;

namespace _2023_advend_of_code.Day1;

public class Day1
{
    private readonly List<string> _input;

    public Day1(List<string> input)
    {
        _input = input;
    }
    
    public Day1(string path)
    {
        _input = ImportFromFile(path);
    }

    public int Solve()
    {
        var parsedNumbers = new List<int>();
        if (parsedNumbers == null) throw new ArgumentNullException(nameof(parsedNumbers));

        foreach (var str in _input)
        {
            // Extract digits from the string
            var digits = str.Where(char.IsDigit).Select(c => c.ToString()).ToList();
            if (digits.Count == 0) continue;

            // Concatenate the first and last digits
            var concatDigits = digits.First() + digits.Last();

            // Parse the concatenated digits to an integer with error handling
            if (!int.TryParse(concatDigits, out var number))
                throw new FormatException($"Cannot parse '{concatDigits}' into an integer.");

            // Add the parsed number to the list
            parsedNumbers.Add(number);
        }

        return parsedNumbers.Sum();
    }
    
    public List<string> ImportFromFile(string path)
    {
        var lines = File.ReadAllLines(path);
        return lines.ToList();
    }
}