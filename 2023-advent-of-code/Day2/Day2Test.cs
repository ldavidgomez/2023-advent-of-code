using NUnit.Framework;

namespace _2023_advent_of_code.Day2;

public class Day2Test
{
    [SetUp]
    public void Setup()
    {
    }

    /*
     *
     * Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
     * Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
     * Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
     * Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
     * Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
     */

    
    [Test]
    public void should_return_8()
    {
        var configGame = new ConfigGame { Blue = 14, Red = 12, Green = 13 };
        
        var round1 = new List<RoundResult>
        {
            new RoundResult { Blue = 3, Red = 4, Green = 0 },
            new RoundResult { Blue = 6, Red = 1, Green = 2 },
            new RoundResult { Blue = 0, Red = 0, Green = 2 }
        };
        var round2 = new List<RoundResult>
        {
            new RoundResult { Blue = 1, Red = 0, Green = 2 },
            new RoundResult { Blue = 4, Red = 1, Green = 3 },
            new RoundResult { Blue = 1, Red = 0, Green = 1 }
        };
        var round3 = new List<RoundResult>
        {
            new RoundResult { Blue = 6, Red = 20, Green = 8 },
            new RoundResult { Blue = 5, Red = 4, Green = 13 },
            new RoundResult { Blue = 0, Red = 1, Green = 5 }
        };
        var round4 = new List<RoundResult>
        {
            new RoundResult { Blue = 6, Red = 3, Green = 1 },
            new RoundResult { Blue = 0, Red = 6, Green = 3 },
            new RoundResult { Blue = 15, Red = 14, Green = 3 }
        };
        var round5 = new List<RoundResult>
        {
            new RoundResult { Blue = 1, Red = 6, Green = 3 },
            new RoundResult { Blue = 2, Red = 1, Green = 3 },
        };
        
        var games = new List<Game>
        {
            new Game(1, round1, configGame),
            new Game(2, round2, configGame),
            new Game(3, round3, configGame),
            new Game(4, round4, configGame),
            new Game(5, round5, configGame),
            
        };

        var results = new Day2(games).TotalResult();
        
        Assert.AreEqual(8, results);
    }
    
    [Test]
    public void should_return_valid_result_from_file_part_1()
    {
        const int expected = 1853;
        const string path = "Day2/input.txt";
        var configGame = new ConfigGame { Blue = 14, Red = 12, Green = 13 };

        var result = new Day2(path, configGame).TotalResult();

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void should_return_valid_power_value_2286()
    {
        /*
         Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
         Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
         Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
         Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
         Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
         */
        
        var configGame = new ConfigGame();
        
        var round1 = new List<RoundResult>
        {
            new RoundResult { Blue = 3, Red = 4, Green = 0 },
            new RoundResult { Blue = 6, Red = 1, Green = 2 },
            new RoundResult { Blue = 0, Red = 0, Green = 2 }
        };
        var round2 = new List<RoundResult>
        {
            new RoundResult { Blue = 1, Red = 0, Green = 2 },
            new RoundResult { Blue = 4, Red = 1, Green = 3 },
            new RoundResult { Blue = 1, Red = 0, Green = 1 }
        };
        var round3 = new List<RoundResult>
        {
            new RoundResult { Blue = 6, Red = 20, Green = 8 },
            new RoundResult { Blue = 5, Red = 4, Green = 13 },
            new RoundResult { Blue = 0, Red = 1, Green = 5 }
        };
        var round4 = new List<RoundResult>
        {
            new RoundResult { Blue = 6, Red = 3, Green = 1 },
            new RoundResult { Blue = 0, Red = 6, Green = 3 },
            new RoundResult { Blue = 15, Red = 14, Green = 3 }
        };
        var round5 = new List<RoundResult>
        {
            new RoundResult { Blue = 1, Red = 6, Green = 3 },
            new RoundResult { Blue = 2, Red = 1, Green = 3 },
        };
        
        var games = new List<Game>
        {
            new Game(1, round1, configGame),
            new Game(2, round2, configGame),
            new Game(3, round3, configGame),
            new Game(4, round4, configGame),
            new Game(5, round5, configGame),
            
        };
        
        var result = new Day2(games).TotalPowerValue();
        
        Assert.AreEqual(2286, result);
    }
    
    [Test]
    public void should_return_valid_power_value_from_file()
    {
        const int expected = 72706;
        const string path = "Day2/input.txt";
        var configGame = new ConfigGame();

        var result = new Day2(path, configGame).TotalPowerValue();

        Assert.AreEqual(expected, result);
    }
  
}
