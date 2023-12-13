namespace _2023_advent_of_code.Day10;

public class Day10
{
    #region Fields

    private readonly string[] _input;
    private Point[] _map;
    private Point[] _test;
    
    
    /*
       | is a vertical pipe connecting north and south.
       - is a horizontal pipe connecting east and west.
       L is a 90-degree bend connecting north and east.
       J is a 90-degree bend connecting north and west.
       7 is a 90-degree bend connecting south and west.
       F is a 90-degree bend connecting south and east.
       . is ground; there is no pipe in this tile.
       S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
     */

    private static readonly Dictionary<string, Coordinate> _coords = new()
    {
        { "N", new Coordinate(0, -1) },
        { "S", new Coordinate(0, 1) },
        { "E", new Coordinate(1, 0) },
        { "W", new Coordinate(-1, 0) }
    };
    
    private readonly Dictionary<string, List<Coordinate>> _directions = new()
    {
        { "|", new List<Coordinate> { _coords["N"], _coords["S"]} },
        { "-", new List<Coordinate> { _coords["E"], _coords["W"]} },
        { "L", new List<Coordinate> { _coords["N"], _coords["E"]} },
        { "J", new List<Coordinate> { _coords["N"], _coords["W"]} },
        { "7", new List<Coordinate> { _coords["S"], _coords["W"]} },
        { "F", new List<Coordinate> { _coords["S"], _coords["E"]} },
        { "S", new List<Coordinate> { _coords["N"], _coords["S"], _coords["E"], _coords["W"]} }
    };

    #endregion

    #region Constructors

    public Day10(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Invalid file path.");
        }
        
        _input = File.ReadAllLines(path);
        SetMap();
    }

    public Day10(string[] input)
    {
        _input = input ?? throw new ArgumentNullException(nameof(input));
        SetMap();
    }

    private void SetMap()
    {
        _map =  _input.SelectMany((line, y) => line.Select((character, x) => new Point(x, y, character))).ToArray();
        _test =  _input.SelectMany((line, y) => line.Select((character, x) => new Point(x, y, character))).ToArray();
    }

    #endregion

    #region Public Methods

    public int SolvePart1()
    {
        var startPoint = _map.First(point => point.Character == 'S');
        startPoint.DistanceFromStart = 0;
        var queue = new Queue<Point>();
        queue.Enqueue(startPoint);
        
        while (queue.Any())
        {
            var currentPoint = queue.Dequeue();
            var currentCharacter = currentPoint.Character.ToString();
            var currentCoordinate = currentPoint.Coordinate;
            var currentDistance = currentPoint.DistanceFromStart;
            var nextCoordinates = _directions[currentCharacter].Select(coordinate => new Coordinate(coordinate.X + currentCoordinate.X, coordinate.Y + currentCoordinate.Y));
            _test.First(x => x.Coordinate == currentCoordinate).Character = 'X';
            var nextPoints = _map.Where(point => nextCoordinates.Contains(point.Coordinate) && point.Character != '.' && point.Character != 'S');
            foreach (var nextPoint in nextPoints)
            {
                if (nextPoint.DistanceFromStart == 0)
                {
                    nextPoint.DistanceFromStart = currentDistance + 1;
                    queue.Enqueue(nextPoint);
                }
            }
        }
        

        var solvePart1 = _map.Where(point => point.Character != 'S' && point.Character != '.').Max(point => point.DistanceFromStart);
        return solvePart1;
    }

    #endregion
}

public class Point
{
    #region Properties

    public Coordinate Coordinate { get; init; }
    public List<Coordinate> LastNode { get; set; } = new();
    
    public char Character {get; set; }
    
    public int DistanceFromStart { get; set; }
    
    #endregion

    #region Constructors

    public Point(int x, int y, char character)
    {
        Coordinate = new Coordinate(x, y);
        Character = character;
    }
    
    public void CalculateDistanceFromStart(Point start)
    {
        
    }

    #endregion
}

public record Coordinate(int X, int Y);