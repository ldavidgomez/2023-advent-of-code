using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace _2023_advent_of_code.Day05;


public class Day05
{
    public Dictionary<string, string> _map = new Dictionary<string, string>
    {
        {"seed", "soil"},
        {"soil", "fertilizer"},
        {"fertilizer", "water"},
        {"water", "light"},
        {"light", "temperature"},
        {"temperature", "humidity"},
        {"humidity", "location"}
    };

    private readonly Array _mapArray = new[,]
    {
        {"seed", "soil"},
        {"soil", "fertilizer"},
        {"fertilizer", "water"},
        {"water", "light"},
        {"light", "temperature"},
        {"temperature", "humidity"},
        {"humidity", "location"}
    };
    
    private readonly IEnumerable<string> _input;
    private readonly Almanac _almanac;


    public Day05(string path)
    {
        _input = File.ReadAllLines(path).ToList();
        _almanac = ParseAlmanac(_input);
    }

    public Day05(List<string> almanac)
    {
        _almanac = ParseAlmanac(almanac);
    }

    private Almanac ParseAlmanac(IEnumerable<string> almanac)
    {
        var input = Regex
            .Replace(
                Regex.Replace(string.Join(" ", almanac.Where(x => x != "")).Replace("map:", "").Replace(":", ""),
                    "[^.0-9\\s]", "-"), "[-]{2,}", "-").Split("-").Where(x => x != "").ToArray();

        var seeds = input[0].Split(" ").Where(x => x != "").Select(long.Parse).ToList();
        var ranges = new HashSet<Seed>();
        for (var i = 1; i < input.Length; i++)
        {
            var range = input[i].Split(" ").Where(x => x != "").ToArray();
            ranges.UnionWith(CreateRanges(range, i-1));
        }
        
        
        return new Almanac(seeds, ranges);
    }

    private HashSet<Seed> CreateRanges(IReadOnlyList<string> range, int type)
    {
        var ranges = new HashSet<Seed>();
        for (var i = 0; i < range.Count; i+=3)
        {
            var destinationStart = long.Parse(range[i]);
            var sourceStart = long.Parse(range[i+1]);
            var length = long.Parse(range[i+2]);
            var from = _mapArray.GetValue(type, 0)?.ToString();
            var to = _mapArray.GetValue(type, 1)?.ToString();
            
            if (from == null || to == null) throw new ArgumentNullException();

            ranges.Add(new Seed(from, to, destinationStart, sourceStart, length));
        }

        return ranges;
    }

    private IEnumerable<long> GetRange(long sourceStart, long length)
    {
        var range = new List<long>();
        for (var i = 0; i < length; i++)
        {
            range.Add(sourceStart + i);
        }

        return range;
    }

    public long SolvePart1()
    {
        var results = (
            from seed in _almanac.Seeds 
            select GetValue(seed, _almanac.Ranges.Where(x => x.From == "seed" && x.SourceStart <= seed && (x.SourceStart + x.Length) >= seed)) into toSoil 
            select GetValue(toSoil, _almanac.Ranges.Where(x => x.From == "soil" && x.SourceStart <= toSoil && (x.SourceStart + x.Length) >= toSoil)) into toFertilizer 
            select GetValue(toFertilizer, _almanac.Ranges.Where(x => x.From == "fertilizer" && x.SourceStart <= toFertilizer && (x.SourceStart + x.Length) >= toFertilizer)) into toWater 
            select GetValue(toWater, _almanac.Ranges.Where(x => x.From == "water" && x.SourceStart <= toWater && (x.SourceStart + x.Length) >= toWater)) into toLight 
            select GetValue(toLight, _almanac.Ranges.Where(x => x.From == "light" && x.SourceStart <= toLight && (x.SourceStart + x.Length) >= toLight)) into toTemperature 
            select GetValue(toTemperature, _almanac.Ranges.Where(x => x.From == "temperature" && x.SourceStart <= toTemperature && (x.SourceStart + x.Length) >= toTemperature)) into toHumidity 
            select GetValue(toHumidity, _almanac.Ranges.Where(x => x.From == "humidity" && x.SourceStart <= toHumidity && (x.SourceStart + x.Length) >= toHumidity))).ToList();

        return results.Min();
    }

    public long SolvePart2()
    {
        var results = long.MaxValue;
        var results2 = new BlockingCollection<long>();
        var seeds = _almanac.Seeds.ToArray();
        for (var i = 0; i < seeds.Length; i += 2)
        {
            //toSoil = GetValue(
            //     _almanac.Ranges.Where(x => x.From == "seed")
            //         .First(x => x.SourceStart < seeds[i] && x.DestinationStart > seeds[i]).SourceEnd,
            //     _almanac.Ranges.Where(x => x.From == "seed")
            //         .Where(x => x.SourceStart < seeds[i] && x.DestinationStart > seeds[i]));
            // results2.Add(toSoil);

            // Parallel.For(seeds[i], seeds[i] + seeds[i + 1], seed =>
            // {
            //     var toSoil = GetValue(seed, _almanac.Ranges.Where(x => x.From == "seed" && x.SourceStart <= seed && (x.SourceStart + x.Length) >= seed));
            //     var toFertilizer = GetValue(toSoil, _almanac.Ranges.Where(x => x.From == "soil" && x.SourceStart <= toSoil && (x.SourceStart + x.Length) >= toSoil));
            //     var toWater = GetValue(toFertilizer, _almanac.Ranges.Where(x => x.From == "fertilizer" && x.SourceStart <= toFertilizer && (x.SourceStart + x.Length) >= toFertilizer));
            //     var toLight = GetValue(toWater, _almanac.Ranges.Where(x => x.From == "water" && x.SourceStart <= toWater && (x.SourceStart + x.Length) >= toWater));
            //     var toTemperature = GetValue(toLight, _almanac.Ranges.Where(x => x.From == "light" && x.SourceStart <= toLight && (x.SourceStart + x.Length) >= toLight));
            //     var toHumidity = GetValue(toTemperature, _almanac.Ranges.Where(x => x.From == "temperature" && x.SourceStart <= toTemperature && (x.SourceStart + x.Length) >= toTemperature));
            //     var toLocation = GetValue(toHumidity, _almanac.Ranges.Where(x => x.From == "humidity" && x.SourceStart <= toHumidity && (x.SourceStart + x.Length) >= toHumidity));
            //     
            //     Console.WriteLine(i + " " + seed + " " + toLocation);
            //
            //     if (results > toLocation) 
            //         results = toLocation;
            // });
            
            for (var seed = seeds[i]; seed < seeds[i] + seeds[i + 1]; seed++)
            {
                var toSoil = GetValue(seed, _almanac.Ranges.Where(x => x.From == "seed" && x.SourceStart <= seed && (x.SourceStart + x.Length) >= seed));
                var toFertilizer = GetValue(toSoil, _almanac.Ranges.Where(x => x.From == "soil" && x.SourceStart <= toSoil && (x.SourceStart + x.Length) >= toSoil));
                var toWater = GetValue(toFertilizer, _almanac.Ranges.Where(x => x.From == "fertilizer" && x.SourceStart <= toFertilizer && (x.SourceStart + x.Length) >= toFertilizer));
                var toLight = GetValue(toWater, _almanac.Ranges.Where(x => x.From == "water" && x.SourceStart <= toWater && (x.SourceStart + x.Length) >= toWater));
                var toTemperature = GetValue(toLight, _almanac.Ranges.Where(x => x.From == "light" && x.SourceStart <= toLight && (x.SourceStart + x.Length) >= toLight));
                var toHumidity = GetValue(toTemperature, _almanac.Ranges.Where(x => x.From == "temperature" && x.SourceStart <= toTemperature && (x.SourceStart + x.Length) >= toTemperature));
                var toLocation = GetValue(toHumidity, _almanac.Ranges.Where(x => x.From == "humidity" && x.SourceStart <= toHumidity && (x.SourceStart + x.Length) >= toHumidity));
            
                if (results > toLocation) 
                    results = toLocation;
            }
        }

        return results;
    }

    private static long GetValue(long value, IEnumerable<Seed> seeds)
    {
        
        foreach (var seed in seeds)
        {
            if (seed.SourceStart > value || value > seed.SourceStart + seed.Length) continue;
            
            return seed.DestinationStart + (value - seed.SourceStart);
        }

        return value;
    }
}

public record Almanac(List<long> Seeds, HashSet<Seed> Ranges);

public record Seed(string From, string To, long DestinationStart, long SourceStart, long Length)
{
    public long SourceEnd => SourceStart + Length;
};
