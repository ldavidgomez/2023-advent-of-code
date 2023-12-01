using NUnit.Framework;

namespace _2023_advend_of_code.Day1;

public class Day1Test
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

        var result = new Day1(input).Solve();

        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void should_return_142_from_file()
    {
        const int expected = 56397;
        const string path = "Day1/input_part_1.txt";

        var result = new Day1(path).Solve();

        Assert.AreEqual(expected, result);
    }
}