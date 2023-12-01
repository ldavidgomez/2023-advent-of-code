using System.Text.RegularExpressions;

namespace _2023_advend_of_code.Day1;

public class Day1
{
    private readonly List<string> _input;

    private static Dictionary<string, string> Numbers => new()
        {
            {"one", "1"},
            {"two", "2"},
            {"three", "3"},
            {"four", "4"},
            {"five", "5"},
            {"six", "6"},
            {"seven", "7"},
            {"eight", "8"},
            {"nine", "9"}
        };

    public Day1(List<string> input)
    {
        _input = input;
    }
    
    public Day1(string path)
    {
        _input = ImportFromFile(path);
    }

    public int SolvePart1()
    {
        var parsedNumbers = new List<int>();
        if (parsedNumbers == null) throw new ArgumentNullException(nameof(parsedNumbers));

        foreach (var concatDigits in from str in _input select str.Where(char.IsDigit).Select(c => c.ToString()).ToList() into digits where digits.Count != 0 select digits.First() + digits.Last())
        {
            // Parse the concatenated digits to an integer with error handling
            if (!int.TryParse(concatDigits, out var number))
                throw new FormatException($"Cannot parse '{concatDigits}' into an integer.");

            // Add the parsed number to the list
            parsedNumbers.Add(number);
        }

        return parsedNumbers.Sum();
    }
    
    public int SolvePart2()
    {
        CastInputStringToNumbers();
        return SolvePart1();
    }

    private void CastInputStringToNumbers()
    {
        var castedInput = new List<string>();
        foreach (var s in _input)
        {
            var newInput = s;

                for (var i = 1; i <= newInput.Length; i++)
                {
                    var inputPart = newInput[..i];
                    foreach (var keyValuePair in Numbers.Where(keyValuePair => inputPart.Contains(keyValuePair.Key)))
                    {
                        inputPart = inputPart.Replace(keyValuePair.Key, keyValuePair.Value);
                        newInput = newInput.Replace(newInput[..(i-1)], inputPart);
                        i = 1;
                    }
                }
                
            castedInput.Add(newInput);
        }
        
        _input.Clear();
        _input.AddRange(castedInput);
    }

    private static List<string> ImportFromFile(string path)
    {
        var lines = File.ReadAllLines(path);
        return lines.ToList();
    }
}