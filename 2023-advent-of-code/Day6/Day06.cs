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
        {
            SetRacesWithKerning();
        }
        else
        {
            SetRacesWithoutKerning();
        }
    }

    private void SetRacesWithKerning()
    {
        var times = _input[0].Trim().Split(":")[1].Trim().Replace(" ","");
        var distances = _input[1].Trim().Split(":")[1].Trim().Replace(" ","");

        _races = new List<Race> {new Race(long.Parse(distances), long.Parse(times))};
    }

    private void SetRacesWithoutKerning()
    {
        var times = _input[0].Split(":")[1].Split(" ").Where(x => x != "").Select(long.Parse).ToList();
        var distances = _input[1].Split(":")[1].Split(" ").Where(x => x != "").Select(long.Parse).ToList();

        _races = times.Select((t, i) => new Race(distances[i], t)).ToList();
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