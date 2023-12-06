namespace _2023_advent_of_code.Day6;

public class Day06
{
    private readonly string[] _input;
    private IEnumerable<Race>? _races;

    public Day06(string path, bool kerning = false)
    {
        _input = File.ReadAllLines(path).ToArray();
        SetRaces(kerning);
    }
    public Day06(string[] input, bool kerning = false)
    {
        _input = input;
        SetRaces(kerning);
    }

    private void SetRaces(bool kerning)
    {
        if (kerning)
            SetRacesSingleValue();
        else
            SetRacesMultipleValues();
    }

    private void SetRacesSingleValue()
    {
        var times = ExtractLong(_input[0]);
        var distances = ExtractLong(_input[1]);
            
        _races = new List<Race> { new Race(distances, times) };
    }

    private void SetRacesMultipleValues()
    {
        var times = ExtractLongList(_input[0]);
        var distances = ExtractLongList(_input[1]);

        _races = times.Zip(distances, (t, d) => new Race(d, t)).ToList();
    }

    private static long ExtractLong(string input)
    {
        var splitInput = input.Split(':')[1].Trim();
        return long.Parse(splitInput.Replace(" ", ""));
    }

    private static IEnumerable<long> ExtractLongList(string input)
    {
        return input.Split(':')[1].Split(" ").Where(x => x != "").Select(long.Parse);
    }

    public long Solve()
    {
        return (_races ?? throw new InvalidOperationException())
            .Select(GetWinningOptions)
            .Aggregate<List<long>?, long>(1, (current, winningOptions) => current * winningOptions!.Count);
    }

    private static List<long> GetWinningOptions(Race race)
    {
        var duration = race.Duration;
        var recordDistance = race.RecordDistance;

        var winningOptions = new List<long>();
        for (var i = 0; i <= duration; i++)
        {
            var speed = i;
            var distance = speed * (duration - i);

            if (distance > recordDistance)
                winningOptions.Add(speed);
        }

        return winningOptions;
    }
}

public record Race(long RecordDistance, long Duration);