using NUnit.Framework;

namespace _2023_advent_of_code.Day05;

public class Day05Test
{
    [SetUp]
    public void Setup()
    {
    }

    /*
      seeds: 79 14 55 13
       
        seed-to-soil map:
       50 98 2
       52 50 48
       
       soil-to-fertilizer map:
       0 15 37
       37 52 2
       39 0 15
       
       fertilizer-to-water map:
       49 53 8
       0 11 42
       42 0 7
       57 7 4
       
       water-to-light map:
       88 18 7
       18 25 70
       
       light-to-temperature map:
       45 77 23
       81 45 19
       68 64 13
       
       temperature-to-humidity map:
       0 69 1
       1 0 69
       
       humidity-to-location map:
       60 56 37
       56 93 4
     */

    [Test]
    public void should_return_lower_location_35()
    {
        const int expected = 35;
        var input = new List<string>
        {
            "seeds: 79 14 55 13",
            "",
            "seed-to-soil map:",
            "50 98 2",
            "52 50 48",
            "",
            "soil-to-fertilizer map:",
            "0 15 37",
            "37 52 2",
            "39 0 15",
            "",
            "fertilizer-to-water map:",
            "49 53 8",
            "0 11 42",
            "42 0 7",
            "57 7 4",
            "",
            "water-to-light map:",
            "88 18 7",
            "18 25 70",
            "",
            "light-to-temperature map:",
            "45 77 23",
            "81 45 19",
            "68 64 13",
            "",
            "temperature-to-humidity map:",
            "0 69 1",
            "1 0 69",
            "",
            "humidity-to-location map:",
            "60 56 37",
            "56 93 4"
        };
        
        var day05 = new Day05(input);
        
        Assert.AreEqual(expected, day05.SolvePart1());
    }
    
    [Test]
    public void should_return_lower_location_from_file()
    {
        const int expected = 177942185;
        var day05 = new Day05("Day05/input.txt");
        
        Assert.AreEqual(expected, day05.SolvePart1());
    }
    
    [Test]
    public void should_return_valid_result_part_2_46()
    {
        const long expected = 46;
        var input = new List<string>
        {
            "seeds: 79 14 55 13",
            "",
            "seed-to-soil map:",
            "50 98 2",
            "52 50 48",
            "",
            "soil-to-fertilizer map:",
            "0 15 37",
            "37 52 2",
            "39 0 15",
            "",
            "fertilizer-to-water map:",
            "49 53 8",
            "0 11 42",
            "42 0 7",
            "57 7 4",
            "",
            "water-to-light map:",
            "88 18 7",
            "18 25 70",
            "",
            "light-to-temperature map:",
            "45 77 23",
            "81 45 19",
            "68 64 13",
            "",
            "temperature-to-humidity map:",
            "0 69 1",
            "1 0 69",
            "",
            "humidity-to-location map:",
            "60 56 37",
            "56 93 4"
        };
        
        var day05 = new Day05(input);
        
        Assert.AreEqual(expected, day05.SolvePart2());
    }
    
    [Test]
    public void should_return_valid_result_part_2_from_file()
    {
        const long expected = 177942185;
        var day05 = new Day05("Day05/input.txt");
        
        Assert.AreEqual(expected, day05.SolvePart2());
    }
}
