using System.Collections.Immutable;
using System.ComponentModel;

namespace _2023_advent_of_code.Day09;

public class Day09
{
    #region Fields

    private readonly string[] _input;
    private IEnumerable<History> _history;

    #endregion

    #region Constructors

    public Day09(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Invalid file path.");
        }
        
        _input = File.ReadAllLines(path);
        SetHistory();
    }

    public Day09(string[] input)
    {
        _input = input ?? throw new ArgumentNullException(nameof(input));
        SetHistory();
    }

    private void SetHistory()
    {
        var histories = _input
            .Select(line => line.Split(" ").Select(long.Parse).ToList())
            .Select(HistoryFactory.CreateInstance)
            .ToList();

        _history = histories;
    }

    #endregion

    #region Public Methods

    public long SolvePart1()
    {
       return _history.Sum(x => x.LastNumber);
    }

    #endregion
    public long SolvePart2()
    {
        return _history.Sum(x => x.FirstNumber);
    }
}

public static class HistoryFactory
{
    public static History CreateInstance(IEnumerable<long> historyLine) => new History(new []{historyLine});
}

public class History
{
    private IEnumerable<IEnumerable<long>> Numbers { get; set; }
    public long LastNumber { get; private set; }
    public long FirstNumber { get; private set; }

    public History(IEnumerable<IEnumerable<long>> numbers)
    {
        Numbers = numbers;
        SetLinesToZero();
        SetLastNumber();
        SetFirstNumber();
    }

    private void SetLinesToZero()
    {
        while (true)
        {
            var newNumbers = CalculateDifferences(Numbers.Last().ToArray());
            Numbers = Numbers.Append(newNumbers);
            if (newNumbers.Any(x => x != 0)) 
                continue;
            break;
        }
    }

    private static List<long> CalculateDifferences(long[] lastLine)
    {
        var newNumbers = new List<long>();
        for (var i = 1; i < lastLine.Length; i++)
        {
            newNumbers.Add(lastLine[i] - lastLine[i - 1]);
        }
        return newNumbers;
    }

    private void SetLastNumber()
    {
        var array = Numbers.ToArray();
        LastNumber = array.Reverse().Aggregate(0L, (current, num) => num.Last() + current);
    }

    private void SetFirstNumber()
    {
        var array = Numbers.ToArray();
        FirstNumber = array.Reverse().Aggregate(0L, (current, num) => num.First() - current);
    }
}