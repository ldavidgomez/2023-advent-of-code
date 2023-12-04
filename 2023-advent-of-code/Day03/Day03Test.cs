using NUnit.Framework;

namespace _2023_advent_of_code.Day03;

public class Day03Test
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void should_sum_numbers_adjacent_to_symbols_returns_4361()
    {
        const int expected = 4361;
        var input = new [] {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        };
        var day3 = new _2023_advent_of_code.Day03.Day03(input);

        var result = day3.SolvePart1();
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void should_return_valid_result_from_file()
    {
        const int expected = 559667;
        var day3 = new _2023_advent_of_code.Day03.Day03("Day3/input.txt");

        var result = day3.SolvePart1();
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void should_return_valid_result_gear_467835()
    {
        var expected = 467835;
        var input = new [] {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        };
        
        var day3 = new _2023_advent_of_code.Day03.Day03(input);
        var result = day3.SolvePart2();
        
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void should_return_valid_result_gear_from_file()
    {
        const int expected = 86841457;
        var day3 = new _2023_advent_of_code.Day03.Day03("Day3/input.txt");

        var result = day3.SolvePart2();
        Assert.AreEqual(expected, result);
    }
}
