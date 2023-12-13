using NUnit.Framework;

namespace _2023_advent_of_code.Day11;

public class Day11Test
{
    [SetUp]
    public void Setup()
    {
    }

    // [Test]
    // public void should_return_valid_expanded_map()
    // {
    //     var expected = new[]
    //     {
    //         "....#........",
    //         ".........#...",
    //         "#............",
    //         ".............",
    //         ".............",
    //         "........#....",
    //         ".#...........",
    //         "............#",
    //         ".............",
    //         ".............",
    //         ".........#...",
    //         "#....#......."
    //     };
    //     
    //     var input = new[]
    //     {
    //         "...#......",
    //         ".......#..",
    //         "#.........",
    //         "..........",
    //         "......#...",
    //         ".#........",
    //         ".........#",
    //         "..........",
    //         ".......#..",
    //         "#...#....."
    //     };
    //     
    //     var day11 = new Day11(input);
    //     var actual = day11.Map;
    //     
    //     Assert.AreEqual(expected.Length, actual.Length);
    // }

    [Test]
    public void should_return_sum_shortest_path_between_galaxies_374()
    {
        var expected = 374;
        var input = new[]
        {
            "...#......",
            ".......#..",
            "#.........",
            "..........",
            "......#...",
            ".#........",
            ".........#",
            "..........",
            ".......#..",
            "#...#....."
        };
        
        var day11 = new Day11(input, 2);
        var actual = day11.SumShortestPathBetweenGalaxies();
        
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void should_return_sum_shortest_path_between_galaxies_with_10_expanded_374()
    {
        var expected = 1030;
        var input = new[]
        {
            "...#......",
            ".......#..",
            "#.........",
            "..........",
            "......#...",
            ".#........",
            ".........#",
            "..........",
            ".......#..",
            "#...#....."
        };
        
        var day11 = new Day11(input, 10);
        var actual = day11.SumShortestPathBetweenGalaxies();
        
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void should_return_sum_shortest_path_between_galaxies_with_100_expanded_374()
    {
        var expected = 8410;
        var input = new[]
        {
            "...#......",
            ".......#..",
            "#.........",
            "..........",
            "......#...",
            ".#........",
            ".........#",
            "..........",
            ".......#..",
            "#...#....."
        };
        
        var day11 = new Day11(input, 100);
        var actual = day11.SumShortestPathBetweenGalaxies();
        
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void should_return_sum_shortest_path_from_file_10289334()
    {
        var expected = 10289334;
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Day11", "input.txt");
        var day11 = new Day11(path, 2);
        var actual = day11.SumShortestPathBetweenGalaxies();
        
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void should_return_sum_shortest_path_from_file_with_additionl()
    {
        var expected = 649862989626;
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Day11", "input.txt");
        var day11 = new Day11(path, 1000000);
        var actual = day11.SumShortestPathBetweenGalaxies();
        
        Assert.AreEqual(expected, actual);
    }
}
