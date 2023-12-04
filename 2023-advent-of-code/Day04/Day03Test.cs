using NUnit.Framework;

namespace _2023_advent_of_code.Day04;

public class Day04Test
{
    [SetUp]
    public void Setup()
    {
    }

    /*
       Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
       Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
       Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
       Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
       Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
       Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
     */
    
    [Test]
    public void should_sum_winning_number_return_13()
    {
        const int expected = 13;
        var input = new List<string>
        {
            "41 48 83 86 17 | 83 86  6 31 17  9 48 53",
            "13 32 20 16 61 | 61 30 68 82 17 32 24 19",
            " 1 21 53 59 44 | 69 82 63 72 16 21 14  1",
            "41 92 73 84 69 | 59 84 76 51 58  5 54 83",
            "87 83 26 28 32 | 88 30 70 12 93 22 82 36",
            "31 18 13 56 72 | 74 77 10 23 35 67 36 11"
        };
        
        var day4 = new Day04(input);
        var result = day4.SolvePart1();
        
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void should_return_valid_result_from_file()
    {
        const int expected = 15205;
        var day4 = new Day04("Day04/input.txt");

        var result = day4.SolvePart1();
        Assert.AreEqual(expected, result);
    }
}
