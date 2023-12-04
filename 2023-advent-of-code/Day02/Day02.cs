namespace _2023_advent_of_code.Day02;


public class Day02
{
    private readonly List<Game> _games;

    public Day02(List<Game> games)
    {
        _games = games;
    }

    public Day02(string path, ConfigGame configGame)
    {
        _games = ImportFromFile(path,configGame);
    }

    private List<Game> ImportFromFile(string path, ConfigGame configGame)
    {
        var lines = File.ReadAllLines(path);
        var games = new List<Game>();
        foreach (var line in lines)
        {
            var gameId = int.Parse(line.Split(":")[0].Split(" ")[1]);
            var rounds = line.Split(":")[1].Split(";");
            var roundResults = rounds.Select(CreateRoundResult).ToList();

            var game = new Game(gameId, roundResults, configGame);
            games.Add(game);
        }

        return games;
    }

    private static RoundResult CreateRoundResult(string round)
    {
        var roundResult = new RoundResult();
        var colors = round.Split(",");
        foreach (var color in colors)
        {
            var colorSplit = color.Trim().Split(" ");
            var colorName = colorSplit[1];
            var colorValue = int.Parse(colorSplit[0]);
            switch (colorName)
            {
                case "blue":
                    roundResult.Blue = colorValue;
                    break;
                case "red":
                    roundResult.Red = colorValue;
                    break;
                case "green":
                    roundResult.Green = colorValue;
                    break;
            }
        }

        return roundResult;
    }

    public int TotalResult()
    {
        return _games.Where(x => x.IsValidGame()).Select(x => x.Id).Sum();
    }

    public int TotalPowerValue()
    {
        return _games.Select(x => x.GetPowerValue()).Sum();
    }
}
public record RoundResult
{
    public int Blue { get; set; }
    public int Red { get; set; }
    public int Green { get; set; }
}

public record ConfigGame : RoundResult {}

public class Game
{
    public int Id { get; }
    private readonly ConfigGame _configGame;
    private List<RoundResult> Results { get; set; }


    public Game(int id, List<RoundResult> results, ConfigGame configGame)
    {
        Id = id;
        _configGame = configGame;
        Results = results;
    }

    public bool IsValidGame()
    {
        return ValidRounds() > 0;
    }

    private int ValidRounds()
    {
        return Results.Any(roundResult => roundResult.Blue > _configGame.Blue || roundResult.Red > _configGame.Red || roundResult.Green > _configGame.Green)
            ? 0 
            : Results.Count;
    }

    public int GetPowerValue()
    {
        var minValidConfig = GetMinValidConfig();
        return minValidConfig.Blue * minValidConfig.Red * minValidConfig.Green;
    }

    private ConfigGame GetMinValidConfig()
    {
        var minValidConfig = new ConfigGame();
        
        Results.ForEach(roundResult =>
        {
            if (roundResult.Blue > minValidConfig.Blue)
                minValidConfig.Blue = roundResult.Blue;
            if (roundResult.Red > minValidConfig.Red)
                minValidConfig.Red = roundResult.Red;
            if (roundResult.Green > minValidConfig.Green)
                minValidConfig.Green = roundResult.Green;
        });
        
        return minValidConfig;
    }
}