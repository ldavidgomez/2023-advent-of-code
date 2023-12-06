namespace _2023_advent_of_code.Day06;

public class Day06
{
    private readonly string[] _input;
    public HashSet<Race>? Races { get; private set; }


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
            
        Races = new HashSet<Race> { new Race(distances, times) };
    }

    private void SetRacesMultipleValues()
    {
        var times = ExtractLongList(_input[0]);
        var distances = ExtractLongList(_input[1]);

        Races = times.Zip(distances, (t, d) => new Race(d, t)).ToHashSet();
    }

    private long ExtractLong(string input)
    {
        var splitInput = input.Split(':')[1].Trim();
        return long.Parse(splitInput.Replace(" ", ""));
    }

    private HashSet<long> ExtractLongList(string input)
    {
        return input.Split(':')[1].Split(" ")
            .Where(x => x != "")
            .Select(long.Parse)
            .ToHashSet();
    }

    public long Solve()
    {
        return (Races ?? throw new InvalidOperationException())
            .Select(GetWinningOptions)
            .Aggregate<List<long>?, long>(1, (current, winningOptions) => current * winningOptions!.Count);
    }

    public long SolveFastest()
    {
        return (Races ?? throw new InvalidOperationException())
            .Aggregate(1L, (current, race) => current * GetWinningOptionsFastest(race));
    }

    public static List<long> GetWinningOptions(Race race)
    {
        var duration = race.Duration;
        var recordDistance = race.RecordDistance;

        var winningOptions = new List<long>();
        for (var i = 0; i <= duration; i++)
        {
            var distance = i * (duration - i);

            if (distance > recordDistance)
                winningOptions.Add(i);
        }

        return winningOptions;
    }
    
    public static long GetWinningOptionsFastest(Race race)
    {
        var duration = race.Duration;
        var recordDistance = race.RecordDistance;

        var winningOptions = new List<long>();
        for (var i = 0; i <= duration; i++)
        {
            var distance = i * (duration - i);

            if (distance <= recordDistance) continue;
            
            winningOptions.Add(i);
            break;
        }

        for (var i = duration - 1; i >= winningOptions.First(); i--)
        {
            var distance = i * (duration - i);

            if (distance <= recordDistance) continue;
            
            winningOptions.Add(i);
            break;
        }

        return Math.Abs(winningOptions.Last() - winningOptions.First()) + 1;
    }
}

public record Race(long RecordDistance, long Duration);