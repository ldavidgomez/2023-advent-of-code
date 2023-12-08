using NUnit.Framework;

namespace _2023_advent_of_code.Day08;

public class Day08Test
{
    [SetUp]
    public void Setup()
    {
    }
    
    /*
       RL
       
       AAA = (BBB, CCC)
       BBB = (DDD, EEE)
       CCC = (ZZZ, GGG)
       DDD = (DDD, DDD)
       EEE = (EEE, EEE)
       GGG = (GGG, GGG)
       ZZZ = (ZZZ, ZZZ)
     */
    
    [Test]
    public void should_return_2()
    {
        const int expected = 2;
        var input = new[]
        {
            "RL",
            "AAA = (BBB, CCC)",
            "BBB = (DDD, EEE)",
            "CCC = (ZZZ, GGG)",
            "DDD = (DDD, DDD)",
            "EEE = (EEE, EEE)",
            "GGG = (GGG, GGG)",
            "ZZZ = (ZZZ, ZZZ)"
        };
        var day08 = new Day08(input);

        var result = day08.SolvePart1();
        Assert.AreEqual(expected, result);
    }
    
    /*
       LLR
       
       AAA = (BBB, BBB)
       BBB = (AAA, ZZZ)
       ZZZ = (ZZZ, ZZZ) 
     */
    
    [Test]
    public void should_return_6()
    {
        const int expected = 6;
        var input = new[]
        {
            "LLR",
            "AAA = (BBB, BBB)",
            "BBB = (AAA, ZZZ)",
            "ZZZ = (ZZZ, ZZZ)"
        };
        var day08 = new Day08(input);

        var result = day08.SolvePart1();
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void should_return_valid_result_from_file_14681()
    {
        const int expected = 14681;
        var day08 = new Day08("Day08/input.txt");

        var result = day08.SolvePart1();
        Assert.AreEqual(expected, result);
    }

    
    /*
       LR
       
       11A = (11B, XXX)
       11B = (XXX, 11Z)
       11Z = (11B, XXX)
       22A = (22B, XXX)
       22B = (22C, 22C)
       22C = (22Z, 22Z)
       22Z = (22B, 22B)
       XXX = (XXX, XXX)
     */
    [Test]
    public void should_return_step2_6()
    {
        const int expected = 6;
        var input = new[]
        {
            "LR",
            "11A = (11B, XXX)",
            "11B = (XXX, 11Z)",
            "11Z = (11B, XXX)",
            "22A = (22B, XXX)",
            "22B = (22C, 22C)",
            "22C = (22Z, 22Z)",
            "22Z = (22B, 22B)",
            "XXX = (XXX, XXX)"
        };
        var day08 = new Day08(input);

        var result = day08.SolvePart2();
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void should_return_valid_result_from_file_part_2()
    {
        const long expected = 14321394058031;
        var day08 = new Day08("Day08/input.txt");

        var result = day08.SolvePart2();
        Assert.AreEqual(expected, result);
    }
}
