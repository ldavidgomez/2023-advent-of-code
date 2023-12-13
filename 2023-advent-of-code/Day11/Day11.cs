using System.Text;

namespace _2023_advent_of_code.Day11;

public class Day11
{
    #region Fields

    private string[] _input;
    public Point[] Map { get; private set; }
    
    private List<int> _addedRows = new();
    private List<int> _addedColumns = new();
    private long _additionalDistance = 0;

    #endregion

    #region Constructors

    public Day11(string path, long additionalDistance = 0)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Invalid file path.");
        }
        
        _input = File.ReadAllLines(path);
        SetAdditionalDistance(additionalDistance);
        SetMap();
    }

    public Day11(string[] input, long additionalDistance = 0)
    {
        _input = input ?? throw new ArgumentNullException(nameof(input));
        SetAdditionalDistance(additionalDistance);
        SetMap();
    }

    private void SetAdditionalDistance(long additionalDistance)
    {
        _additionalDistance = additionalDistance == 0 ? 0 : additionalDistance - 1;
    }

    private void SetMap()
    {
        Map = _input.SelectMany((line, y) => line.Select((character, x) => new Point(x, y, character))).ToArray();
        var lastX = Map.Max(p => p.Coordinate.X) + 1;
        var lastY = Map.Max(p => p.Coordinate.Y) + 1;
        
        var rowsToExpand = new List<int>();
        var columnsToExpand = new List<int>();
        for (var i = 0; i < lastX; i++)
        {
            var line = Map.Where(p => p.Coordinate.X == i);
            if (line.All(x => x.Character == '.'))
                columnsToExpand.Add(i);
        }

        for (var i = 0; i < lastY; i++)
        {
            var line = Map.Where(p => p.Coordinate.Y == i);
            if (line.All(x => x.Character == '.'))
                rowsToExpand.Add(i);
        }

        if(_additionalDistance == 0)
        {
            var count = 0;
            foreach (var column in columnsToExpand)
            {
                for (var i = 0; i < _input.Length; i++)
                {
                    _input[i] = _input[i].Insert(column + count, ".");
                }

                count++;
            }

            var newRow = string.Empty;
            for (var i = 0; i < _input[0].Length; i++)
            {
                newRow += ".";
            }

            count = 0;
            var newLines = new List<string>();
            for (var i = 0; i < _input.Length; i++)
            {
                if (rowsToExpand.Contains(i))
                {
                    newLines.Add(newRow);
                    count++;
                }

                newLines.Add(_input[i]);
            }

            _input = newLines.ToArray();
        }
        
        Map = _input.SelectMany((line, y) => line.Select((character, x) => new Point(x, y, character))).ToArray();
        _addedColumns = columnsToExpand;
        _addedRows = rowsToExpand;
        
        //PrintMap();
    }

    private void PrintMap()
    {
        for (var y = 0; y <= Map.Max(p => p.Coordinate.Y); y++)
        {
            for (var x = 0; x <= Map.Max(p => p.Coordinate.X); x++)
            {
                Console.Write(Map.FirstOrDefault(p => p.Coordinate.X == x && p.Coordinate.Y == y)?.Character);
            }
            Console.WriteLine();
        }

        for (var y = 0; y <= Map.Max(p => p.Coordinate.Y); y++)
        {
            for (var x = 0; x <= Map.Max(p => p.Coordinate.X); x++)
            {
                var point = Map.Where(p => p.Coordinate.X == x && p.Coordinate.Y == y).Select(p => p.Character).Contains('#');
                if (point)
                    Console.WriteLine($"{y} -> ({x},{y})");
            }
        }
    }

    #endregion

    public long SumShortestPathBetweenGalaxies()
    {
        var shortestPath = GetShortestPathBetweenGalaxies();
        return shortestPath.Sum();
    }

    private IEnumerable<long> GetShortestPathBetweenGalaxies()
    {
        var minDistances = new List<long>();
        var galaxyCount = 1;

        var galaxies = Map.Where(p => p.Character == '#').OrderBy(p => p.Coordinate.Y).ThenBy(p => p.Coordinate.X).ToArray();
        for (var i = 0; i < galaxies.Length; i++)
        {
            var galaxy = galaxies[i];
            var distances = new List<long>();
            for (var j = i + 1; j < galaxies.Length; j++)
            {
                var pointCount = j + 1;
                var point = galaxies[j];
                if (galaxy == point)
                {
                    pointCount++;
                    continue;
                }
                var distance = galaxy.CalculateDistanceFrom(point);
                var additionalPoint = GetAdditionalPoint(galaxy, point);
                distance += additionalPoint * _additionalDistance;
                //Console.WriteLine($"Distance from {galaxyCount} ({galaxy.Coordinate.X},{galaxy.Coordinate.Y}) to {pointCount} ({point.Coordinate.X},{point.Coordinate.Y}) = {distance}");
                distances.Add(distance);
                pointCount++;
            }
            minDistances.Add(distances.Sum());
            galaxyCount++;
        }
        
        return minDistances;
    }

    private long GetAdditionalPoint(Point galaxy, Point point)
    {
        var rows = _addedRows
            .Where(r => (r > galaxy.Coordinate.Y && r < point.Coordinate.Y) 
                        || (r < galaxy.Coordinate.Y && r > point.Coordinate.Y))
                .ToArray();
        var columns = _addedColumns
            .Where(c => (c > galaxy.Coordinate.X && c < point.Coordinate.X)
            || (c < galaxy.Coordinate.X && c > point.Coordinate.X)).ToArray();
        var additionalPoints = rows.Length + columns.Length;
        
        return additionalPoints;
    }
}
public class Point
{
    #region Properties

    public Coordinate Coordinate { get; init; }
    public char Character {get; set; }
    
    #endregion

    #region Constructors

    public Point(int x, int y, char character)
    {
        Coordinate = new Coordinate(x, y);
        Character = character;
    }
    
    public long CalculateDistanceFrom(Point start)
    {
        return Math.Abs(start.Coordinate.X - Coordinate.X) + Math.Abs(start.Coordinate.Y - Coordinate.Y);
    }

    #endregion
}

public record Coordinate(int X, int Y);





