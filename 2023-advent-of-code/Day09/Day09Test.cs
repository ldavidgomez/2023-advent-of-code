using NUnit.Framework;

namespace _2023_advent_of_code.Day09;

public class Day09Test
{
    [SetUp]
    public void Setup()
    {
    }
    
    /*
       0 3 6 9 12 15
       1 3 6 10 15 21
       10 13 16 21 30 45
     */

    [Test]
    public void should_find_next_step_and_sum_returns_114()
    {
        var expected = 114;
        var input = new[]
        {
            "0 3 6 9 12 15",
            "1 3 6 10 15 21",
            "10 13 16 21 30 45"
        };
        
        var day09 = new Day09(input);
        var actual = day09.SolvePart1();
        
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void should_find_previous_step_and_sum_returns_2()
    {
        var expected = 2;
        var input = new[]
        {
            "0 3 6 9 12 15",
            "1 3 6 10 15 21",
            "10 13 16 21 30 45"
        };
        
        var day09 = new Day09(input);
        var actual = day09.SolvePart2();
        
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void should_return_valid_result_set_last_number_from_file()
    {
        var expected = 1834108701;
        var day09 = new Day09("Day09/input.txt");
        var actual = day09.SolvePart1();
        
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void should_return_valid_result_set_first_number_from_file()
    {
        var expected = 993;
        var day09 = new Day09("Day09/input.txt");
        var actual = day09.SolvePart2();
        
        Assert.AreEqual(expected, actual);
    }
}
