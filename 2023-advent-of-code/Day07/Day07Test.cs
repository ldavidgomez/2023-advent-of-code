using NUnit.Framework;

namespace _2023_advent_of_code.Day07;

public class Day07Test
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void should_return_total_winning_6440()
    {
        const int expected = 6440;
        var input = new[]
        {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483"
        };
        var day7 = new Day07(input);

        var result = day7.Solve();
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void should_return_total_winning_35()
    {
        const int expected = 35;
        var input = new[]
        {
            "77888 10",
            "77788 15"
        };
        var day7 = new Day07(input);

        var result = day7.Solve();
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void should_return_total_winning_40()
    {
        const int expected = 40;
        var input = new[]
        {
            "2AAAA 10",
            "33332 15"
        };
        var day7 = new Day07(input);

        var result = day7.Solve();
        Assert.AreEqual(expected, result);
    }

    
    [Test]
    public void should_return_valid_result_from_file()
    {
        const int expected = 246409899;
        var day7 = new Day07("Day07/input.txt");
    
        var result = day7.Solve();
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void should_return_playing_with_jokers_total_winning_5905()
    {
        const int expected = 5905;
        var input = new[]
        {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483"
        };
        var day7 = new Day07(input);

        var result = day7.Solve(true);
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void should_return_playing_with_jokers_total_winning_35()
    {
        const int expected = 35;
        var input = new[]
        {
            "JKKK2 15",
            "QQQQ2 10"
        };
        var day7 = new Day07(input);

        var result = day7.Solve(true);
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void should_return_valid_result_playing_with_jokers_from_file()
    {
        const int expected = 244848487;
        var day7 = new Day07("Day07/input.txt");
    
        var result = day7.Solve(true);
        Assert.AreEqual(expected, result);
    }
}
