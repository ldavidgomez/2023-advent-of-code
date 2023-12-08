using System.Collections.Immutable;
using System.ComponentModel;

namespace _2023_advent_of_code.Day08;

public class Day08
{
    #region Fields

    private readonly string[] _input;
    private ImmutableList<char> _instructions;
    private readonly Dictionary<string, (string, string)> _map = new();
    private readonly Dictionary<string, (string, int)> _knownPositions = new();

    #endregion

    #region Constructors

    public Day08(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Invalid file path.");
        }

        _input = File.ReadAllLines(path);
        SetMap();
    }

    public Day08(string[] input)
    {
        _input = input ?? throw new ArgumentNullException(nameof(input));
        SetMap();
    }

    #endregion

    #region Public Methods

    public long SolvePart1()
    {
        var step = 0L;
        var nextPosition = "AAA";

        while (nextPosition != "ZZZ")
        {
            (nextPosition, var outputSteps) = SolveMap(nextPosition, 0);
            step += outputSteps;
        }

        return step;
    }

    public long SolvePart2()
    {
        var steps = _map.Keys
            .Where(x => x.Last() == 'A')
            .Select(SolveMapSteps)
            .ToList();

        return LeastCommonMultiple(steps);
    }

    #endregion

    #region Private Methods

    private void SetMap()
    {
        _instructions = _input[0].ToImmutableList();

        var list = _input[1..].Where(x => !string.IsNullOrWhiteSpace(x));

        foreach (var x in list)
        {
            var keyValuePair = x.Replace("(", "").Replace(")", "").Split("=");
            var key = keyValuePair[0].Trim();
            var values = keyValuePair[1].Split(",").Select(v => v.Trim()).ToArray();
            var value = (values[0], values[1]);
            _map.Add(key, value);
        }
    }

    private long SolveMapSteps(string entry)
    {
        var step = 0L;
        var nextPosition = entry;

        while (nextPosition.Last().ToString() != "Z")
        {
            (nextPosition, var outputSteps) = SolveMap(nextPosition, 0);
            step += outputSteps;
        }

        return step;
    }

    private static long GreatestCommonDivisor(long a, long b)
    {
        while (b != 0)
        {
            (a, b) = (b, a % b);
        }

        return a;
    }

    private static long LeastCommonMultiple(IEnumerable<long> numbers)
    {
        return numbers.Aggregate((a, b) => a * b / GreatestCommonDivisor(a, b));
    }

    private (string, int) SolveMap(string inputPosition, int step)
    {
        var firstInstruction = true;
        var outputPosition = string.Empty;
        var nextPosition = inputPosition;
        var key = string.Empty;

        foreach (var instruction in _instructions)
        {
            if (firstInstruction)
            {
                key = inputPosition + instruction;
                if (_knownPositions.TryGetValue(key, out var knownPosition))
                {
                    return knownPosition;
                }

                firstInstruction = false;
            }

            outputPosition = instruction switch
            {
                'L' => _map[nextPosition].Item1,
                'R' => _map[nextPosition].Item2,
                _ => throw new InvalidEnumArgumentException()
            };

            step++;

            if (outputPosition == "ZZZ")
                return (outputPosition, step);

            nextPosition = outputPosition;
        }

        _knownPositions.TryAdd(key, (outputPosition, step));

        return (outputPosition, step);
    }

    #endregion
}