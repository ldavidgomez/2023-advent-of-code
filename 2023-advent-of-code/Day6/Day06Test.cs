using NUnit.Framework;

namespace _2023_advent_of_code.Day6;

public class Day06Test
{
    [SetUp]
    public void Setup()
    {
    }

    /*
       Time:      7  15   30
       Distance:  9  40  200
     */

    [Test]
    public void should_return_margin_288()
    {
        const int expected = 288;
        var input = new[]
        {
            "Time:      7  15   30",
            "Distance:  9  40  200"
        };
        var day6 = new Day06(input);

        var result = day6.SolvePart1();
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void should_return_valid_result_from_file()
    {
        const int expected = 303600;
        var day6 = new Day06("Day6/input.txt");

        var result = day6.SolvePart1();
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void should_return_margin_71503()
    {
        const int expected = 71503;
        var input = new[]
        {
            "Time:      7  15   30",
            "Distance:  9  40  200"
        };
        var day6 = new Day06(input, true);

        var result = day6.SolvePart1();
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void should_return_valid_result_from_file_with_kerning()
    {
        const int expected = 23654842;
        var day6 = new Day06("Day6/input.txt", true);

        var result = day6.SolvePart1();
        Assert.AreEqual(expected, result);
    }
    
}
