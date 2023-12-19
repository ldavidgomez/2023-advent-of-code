using NUnit.Framework;

namespace _2023_advent_of_code.Day14;

public class Day14Test
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void should_return_valid_map_when_all_O_rotate_to_north()
    {
        var input = new[]
        {
            "O....#....",
            "O.OO#....#",
            ".....##...",
            "OO.#O....O",
            ".O.....O#.",
            "O.#..O.#.#",
            "..O..#O..O",
            ".......O..",
            "#....###..",
            "#OO..#...."
        };
        
        var expected = new[]
        {
            "OOOO.#.O..",
            "OO..#....#",
            "OO..O##..O",
            "O..#.OO...",
            "........#.",
            "..#....#.#",
            "..O..#.O.O",
            "..O.......",
            "#....###..",
            "#....#...."
        };
        
        var day14 = new Day14(input);
        day14.Rotate(Day14.Direction.North);
        
        Assert.AreEqual(string.Join("", expected),  string.Join("", day14.Map.Select(x=> string.Join("", x))));
    }
    
    [Test]
    public void should_return_total_loaded_when_all_O_rotate_to_north_136()
    {
        var input = new[]
        {
            "O....#....",
            "O.OO#....#",
            ".....##...",
            "OO.#O....O",
            ".O.....O#.",
            "O.#..O.#.#",
            "..O..#O..O",
            ".......O..",
            "#....###..",
            "#OO..#...."
        };
        
        const int expected = 136;
        
        var day14 = new Day14(input);
        day14.Rotate(Day14.Direction.North);
        
        Assert.AreEqual(expected, day14.GetTotalLoaded());
    }

    [Test]
    public void should_return_valid_total_loaded_from_file()
    {
        const int expected = 113456;
        
        var day14 = new Day14("Day14/input.txt");
        day14.Rotate(Day14.Direction.North);

        Assert.AreEqual(expected, day14.GetTotalLoaded());
    }

    [Test]
    public void should_return_valid_map_after_1_cycle()
    {
        var input = new[]
        {
            "O....#....",
            "O.OO#....#",
            ".....##...",
            "OO.#O....O",
            ".O.....O#.",
            "O.#..O.#.#",
            "..O..#O..O",
            ".......O..",
            "#....###..",
            "#OO..#...."
        };
        
        var expected = new[]
        {
            ".....#....",
            "....#...O#",
            "...OO##...",
            ".OO#......",
            ".....OOO#.",
            ".O#...O#.#",
            "....O#....",
            "......OOOO",
            "#...O###..",
            "#..OO#...."
        };

        var day14 = new Day14(input);
        day14.Iterate(1);
        
        Assert.AreEqual(string.Join("", expected),  string.Join("", day14.Map.Select(x=> string.Join("", x))));
    }

    [Test]
    public void should_return_valid_map_after_2_cycles()
    {
        var input = new[]
        {
            "O....#....",
            "O.OO#....#",
            ".....##...",
            "OO.#O....O",
            ".O.....O#.",
            "O.#..O.#.#",
            "..O..#O..O",
            ".......O..",
            "#....###..",
            "#OO..#...."
        };
        
        var expected = new[]
        {
            ".....#....",
            "....#...O#",
            ".....##...",
            "..O#......",
            ".....OOO#.",
            ".O#...O#.#",
            "....O#...O",
            ".......OOO",
            "#..OO###..",
            "#.OOO#...O"
        };

        var day14 = new Day14(input);
        day14.Iterate(2);
        
        Assert.AreEqual(string.Join("", expected),  string.Join("", day14.Map.Select(x=> string.Join("", x))));
    }
    
    [Test]
    public void should_return_valid_map_after_3_cycles()
    {
        var input = new[]
        {
            "O....#....",
            "O.OO#....#",
            ".....##...",
            "OO.#O....O",
            ".O.....O#.",
            "O.#..O.#.#",
            "..O..#O..O",
            ".......O..",
            "#....###..",
            "#OO..#...."
        };
        
        var expected = new[]
        {
            ".....#....",
            "....#...O#",
            ".....##...",
            "..O#......",
            ".....OOO#.",
            ".O#...O#.#",
            "....O#...O",
            ".......OOO",
            "#...O###.O",
            "#.OOO#...O"
        };

        var day14 = new Day14(input);
        day14.Iterate(3);
        
        Assert.AreEqual(string.Join("", expected),  string.Join("", day14.Map.Select(x=> string.Join("", x))));
    }
    
    [Test]
    public void should_return_valid_map_after_100_cycles()
    {
        var input = new[]
        {
            "O....#....",
            "O.OO#....#",
            ".....##...",
            "OO.#O....O",
            ".O.....O#.",
            "O.#..O.#.#",
            "..O..#O..O",
            ".......O..",
            "#....###..",
            "#OO..#...."
        };
        
        var expected = new[]
        {
            ".....#....",
            "....#...O#",
            ".....##...",
            "..O#......",
            ".....OOO#.",
            ".O#...O#.#",
            "....O#....",
            "......OOOO",
            "#...O###.O",
            "#.OOO#...O"
        };

        var day14 = new Day14(input);
        day14.Iterate(100);
        
        Assert.AreEqual(string.Join("", expected),  string.Join("", day14.Map.Select(x=> string.Join("", x))));
    }
    
    [Test]
    public void should_return_load_64_after_1000000000_cycles()
    {
        var input = new[]
        {
            "O....#....",
            "O.OO#....#",
            ".....##...",
            "OO.#O....O",
            ".O.....O#.",
            "O.#..O.#.#",
            "..O..#O..O",
            ".......O..",
            "#....###..",
            "#OO..#...."
        };

        const int expected = 64;

        var day14 = new Day14(input);
        
        Assert.AreEqual(expected, day14.Iterate(1000000000));
    }
    
    [Test]
    public void should_return_valid_total_loaded_after_1000000000_cycles()
    {
        const int expected = 118747;
        
        var day14 = new Day14("Day14/input.txt");

        Assert.AreEqual(expected, day14.Iterate(1000000000));
    }
}
