using System.Text.RegularExpressions;

namespace _2023_advent_of_code.Day05;


public class Day05
{
    private readonly Dictionary<string, string> _map = new()
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

    private readonly Almanac _almanac;


    public Day05(string path)
    {
        var input = File.ReadAllLines(path).ToList();
        _almanac = ParseAlmanac(input);
    }

    public Day05(IEnumerable<string> almanac)
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
        var ranges = new HashSet<SeedRules>();
        for (var i = 1; i < input.Length; i++)
        {
            var range = input[i].Split(" ").Where(x => x != "").ToArray();
            ranges.UnionWith(CreateRanges(range, i-1));
        }
        
        
        return new Almanac(seeds, ranges);
    }

    private IEnumerable<SeedRules> CreateRanges(IReadOnlyList<string> range, int type)
    {
        var ranges = new HashSet<SeedRules>();
        for (var i = 0; i < range.Count; i+=3)
        {
            var destinationStart = long.Parse(range[i]);
            var sourceStart = long.Parse(range[i+1]);
            var length = long.Parse(range[i+2]);
            var from = _mapArray.GetValue(type, 0)?.ToString();
            var to = _mapArray.GetValue(type, 1)?.ToString();
            
            if (from == null || to == null) throw new ArgumentNullException();

            ranges.Add(new SeedRules(from, destinationStart, sourceStart, length));
        }

        return ranges;
    }

    public long SolvePart1()
    {
        var results = _almanac.Seeds
            .Select(seed => GetMappedValue("seed", seed))
            .Select(toSoil => GetMappedValue("soil", toSoil))
            .Select(toFertilizer => GetMappedValue("fertilizer", toFertilizer))
            .Select(toWater => GetMappedValue("water", toWater))
            .Select(toLight => GetMappedValue("light", toLight))
            .Select(toTemperature => GetMappedValue("temperature", toTemperature))
            .Select(toHumidity => GetMappedValue("humidity", toHumidity))
            .ToList();
                
        return results.Min();
    }
    
    private long GetMappedValue(string from, long value)
    {
        var matchingRules = _almanac.Ranges
            .Where(x => x.From == from && 
                        x.SourceStart <= value && 
                        value <= (x.SourceStart + x.Length));
        
        var seedRule = matchingRules.FirstOrDefault();

        return seedRule != null 
            ? seedRule.DestinationStart + (value - seedRule.SourceStart)
            : value;
    }

    public long SolvePart2()
    {
        var seeds = GetSeedList();

        var currentValue = _map.Aggregate(seeds, (current, mapping) 
            => GetValue(current, _almanac.Ranges.Where(x => x.From == mapping.Key).ToList()));

        return currentValue.Min() - 1;
    }

    private List<long> GetSeedList()
    {
        return _almanac.Seeds
            .Select((t, i) => i % 2 == 0 ? t : _almanac.Seeds[i - 1] + t - 1)
            .ToList();
    }

    private static List<long> GetValue(IReadOnlyList<long> ranges, IList<SeedRules> rules)
    {
        List<long> result = new();
        for(var i = 0; i < ranges.Count; i += 2)
        {
            var rangeStart = ranges[i];
            var rangeEnd = ranges[i + 1];
            var current = rangeStart;

            while (current <= rangeEnd)
            {
                result.Add(MapValue(current, rules));

                if (IsMapped(current, rules))
                {
                    var seedRules = GetRule(current, rules);
                    var lastValueInRange = Math.Min(rangeEnd, seedRules.SourceEnd);

                    result.Add(seedRules.MapValue(lastValueInRange));
                    current = lastValueInRange;

                    if (current == seedRules.SourceEnd)
                        current++;

                    if (current == rangeEnd)
                        break;
                }
                else
                {
                    var nextRule = rules.Where(x => x.SourceStart > current).MinBy(x => x.SourceStart);

                    if (nextRule == null || nextRule.SourceStart > rangeEnd)
                    {
                        result.Add(rangeEnd);
                        break;
                    }

                    if (nextRule.SourceStart == rangeEnd)
                    {
                        result.Add(rangeEnd - 1);
                        result.Add(nextRule.MapValue(rangeEnd));
                        result.Add(nextRule.MapValue(rangeEnd));
                        break;
                    }

                    result.Add(nextRule.SourceStart - 1);
                    current = nextRule.SourceStart;
                }
            }
        }

        return result;
    }

    private static long MapValue(long queryValue, IEnumerable<SeedRules> seedRules)
    {
        var seedRule = seedRules.FirstOrDefault(x => x.InRange(queryValue));
        return seedRule?.MapValue(queryValue) ?? queryValue;
    }

    private static bool IsMapped(long value, IEnumerable<SeedRules> rules)
        => rules.Any(x => x.InRange(value));

    private static SeedRules GetRule(long value, IEnumerable<SeedRules> rules)
        => rules.First(x => x.InRange(value));
}

public record Almanac(List<long> Seeds, HashSet<SeedRules> Ranges);

public record SeedRules(string From, long DestinationStart, long SourceStart, long Length)
{
    public long SourceEnd => SourceStart + Length;
    
    public bool InRange(long value)
        => value >= SourceStart && value <= SourceEnd;

    public long MapValue(long value)
        => DestinationStart + (value - SourceStart);
};
