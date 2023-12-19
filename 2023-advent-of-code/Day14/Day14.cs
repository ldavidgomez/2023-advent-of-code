
using System.Text;

namespace _2023_advent_of_code.Day14;

public class Day14
{
    #region Fields

    private string[] _input;
    internal char[][] Map = null;
    int Rows = 0;
    int Columns = 0;

    #endregion
    
    #region enums
    
    public enum Direction
    {
        North,
        East,
        South,
        West
    }
    
    #endregion

    #region Constructors

    public Day14(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Invalid file path.");
        }

        _input = File.ReadAllLines(path);
        SetMap();
    }

    public Day14(string[] input)
    {
        _input = input ?? throw new ArgumentNullException(nameof(input));
        SetMap();
    }

    #endregion

    private void SetMap()
    {
        Map = new char[_input.Length][];

        foreach (var item in Enumerable.Range(0, _input.Length))
            Map[item] = _input[item].ToCharArray();

        Rows = _input.Length;
        Columns = _input[0].Length;
    }
    
    public void Rotate(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                MoveCharactersToNorth();
                break;
            case Direction.East:
                MoveCharactersToEast();
                break;
            case Direction.South:
                MoveCharactersToSouth();
                break;
            case Direction.West:
                MoveCharactersToWest();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    private void MoveCharactersToNorth()
    {
        for (var y = 1; y < Rows; y++)
        {
            for (var x = 0; x < Columns; x++)
            {
                if (Map[y][x] != 'O')
                    continue;

                var newRow = y - 1;
                while (newRow >= 0 && Map[newRow][x] == '.')
                    newRow--;
                newRow++;

                Map[y][x] = '.';
                Map[newRow][x] = 'O';
            }
        }
    }

    private void MoveCharactersToSouth()
    {
        for (var y = Rows-2; y >=0; y--)
            for (var x = 0; x < Columns; x++)
            {
                if (Map[y][x] != 'O')
                    continue;

                var newRow = y + 1;
                while (newRow < Rows && Map[newRow][x] == '.')
                    newRow++;
                
                newRow--;
                Map[y][x] = '.';
                Map[newRow][x] = 'O';
            }
    }

    private void MoveCharactersToEast()
    {
        for (var x = Columns-2; x >= 0; x--)
            for (var y =0; y < Rows; y++)
            {
                if (Map[y][x] != 'O')
                    continue;

                var newColumn = x + 1;
                while (newColumn < Columns && Map[y][newColumn] == '.')
                    newColumn++;
                
                newColumn--;
                Map[y][x] = '.';
                Map[y][newColumn] = 'O';
            }
    }

    private void MoveCharactersToWest()
    {
        for (var x = 1; x < Columns; x++)
            for (var y = 0; y < Rows; y++)
            {
                if (Map[y][x] != 'O')
                    continue;

                var newColumn = x - 1;
                while (newColumn >=0 && Map[y][newColumn] == '.')
                    newColumn--;
                
                newColumn++;
                Map[y][x] = '.';
                Map[y][newColumn] = 'O';
            }
    }

    private void DoCycle()
    {
        MoveCharactersToNorth();
        MoveCharactersToWest();
        MoveCharactersToSouth();
        MoveCharactersToEast();
    }

    internal int GetTotalLoaded()
    {
        var result = 0;
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Rows; x++)
            {
                if (Map[y][x] == 'O')
                    result += Rows - y;
            }
        }
        return result;
    }
    
    internal long Iterate(long cycles)
    {
        var cycleResults = new Dictionary<long, string>();
        var currentCycle = 0;
        var currentResult = string.Empty;
        
        for (currentCycle = 1; currentCycle <= cycles; currentCycle++)
        {
            DoCycle();
            currentResult = string.Join("", Map.Select(x=> string.Join("", x)));
            if (cycleResults.Values.Contains(currentResult))
                break;
            
            cycleResults[currentCycle] = currentResult;
        }

        var firstMatch = cycleResults.Keys.First(x => cycleResults[x] == currentResult);
        var length = currentCycle - firstMatch;
        var loop = (cycles-firstMatch) / length;
        var remainingCycles = cycles - (firstMatch + loop * length);
        
        for (var i = 0; i < remainingCycles; i++)
            DoCycle();

        return GetTotalLoaded();
    }
}





