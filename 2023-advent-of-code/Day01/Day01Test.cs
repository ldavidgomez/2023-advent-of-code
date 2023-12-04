using NUnit.Framework;

namespace _2023_advent_of_code.Day01;

public class Day01Test
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void should_return_142()
    {
        const int expected = 142;
        var input = new List<string>
        {
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet"
        };

        var result = new _2023_advent_of_code.Day01.Day01(input).SolvePart1();

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void should_return_valid_result_from_file()
    {
        const int expected = 56397;
        const string path = "Day1/input_part_1.txt";

        var result = new _2023_advent_of_code.Day01.Day01(path).SolvePart1();

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void should_return_valid_from_string()
    {
        const int expect = 281;
        var input = new List<string>
        {
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen"
        };

        var day1 = new _2023_advent_of_code.Day01.Day01(input);
        Assert.AreEqual(expect, day1.SolvePart2());
    }
    
    [Test]
    public void should_return_valid_result_from_string()
    {
        const int expect = 85;
        var input = new List<string>
        {
            //8kgplfhvtvqpfsblddnineoneighthg"
            //"3xtwone"
            "8fmmthreeeight6five"
        };

        var day1 = new _2023_advent_of_code.Day01.Day01(input);
        Assert.AreEqual(expect, day1.SolvePart2());
    }

    [Test]
    public void should_return_valid_result_from_file_2()
    {
        const int expect = 55701;
        const string path = "Day1/input_part_2.txt";

        var day1 = new _2023_advent_of_code.Day01.Day01(path);

        Assert.AreEqual(expect, day1.SolvePart2());
    }
}