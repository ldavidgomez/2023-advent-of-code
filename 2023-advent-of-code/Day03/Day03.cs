namespace _2023_advent_of_code.Day03;


public class Day03
{
    private readonly List<WordLocation> _wordPositions;
    private string[] _map = {};
    private const char IgnoreSymbol = '.';
    private const char EngineSymbol = '*';
    
    public Day03(string path)
    {
        ImportFromFile(path);
        _wordPositions = GetWordPositions();
    }

    public Day03(string[] map)
    {
        _map = map;
        _wordPositions = GetWordPositions();
    }

    private List<WordLocation> GetWordPositions()
    {
        var wordPositions = new List<WordLocation>();
        for (var posY = 0; posY < _map.Length; posY++)
        {
            var startWord = 0;
            var endWord = 0;
            var isWord = false;

            var line = _map[posY];
            for (var posX = 0; posX < line.Length; posX++)
            {
                var c = line[posX];
                if (char.IsNumber(c))
                {
                    if (!isWord)
                    {
                        startWord = posX;
                        isWord = true;
                    }

                    endWord = posX;
                    if (posX != line.Length - 1) continue;

                    CreateAndAddWord(line, startWord, endWord, posY, wordPositions);
                    startWord = 0;
                    endWord = 0;
                    isWord = false;
                }
                else
                {
                    if (!isWord) continue;

                    CreateAndAddWord(line, startWord, endWord, posY, wordPositions);
                    startWord = 0;
                    endWord = 0;
                    isWord = false;
                }
            }
        }

        return wordPositions;
    }

    private static void CreateAndAddWord(string line, int startWord, int endWord, int posY, List<WordLocation> wordPositions)
    {
        var word = line.Substring(startWord, endWord - startWord + 1);
        wordPositions.Add(new WordLocation(word, new Location(startWord, endWord, posY)));
    }
    
    public int SolvePart1()
    {
        var validWords = 
            (from wordPosition in _wordPositions let symbols = 
                GetSurroundingSymbols(wordPosition) where 
                !symbols.Select(x => x.Symbol).All(char.IsDigit) 
                select wordPosition.Word).ToList();

        return validWords.Sum(int.Parse);
    }

    private void AddSymbol(ICollection<SymbolPosition> list, int x, int y, bool condition)
    {
        if (!condition) return;
        
        var item = new SymbolPosition(_map[y][x], new Position(x, y));
        list.Add(item);
    }

    private List<SymbolPosition> GetSurroundingSymbols(WordLocation wordLocation)
    {
        var surroundingSymbols = new List<SymbolPosition>();
        var position = wordLocation.Location;
    
        for (var i = position.StartX; i <= position.EndX; i++)
        {
            var posY = position.Y;
    
            var line = _map[posY];
            var left = i - 1;
            var right = i + 1;
            var top = posY - 1;
            var bottom = posY + 1;
        
            AddSymbol(surroundingSymbols, left, posY, left >= 0);
            AddSymbol(surroundingSymbols, right, posY, right < line.Length);
            AddSymbol(surroundingSymbols, i, top, top >= 0);
            AddSymbol(surroundingSymbols, i, bottom, bottom < _map.Length);
            AddSymbol(surroundingSymbols, left, top, left >= 0 && top >= 0);
            AddSymbol(surroundingSymbols, left, bottom, left >= 0 && bottom < _map.Length);
            AddSymbol(surroundingSymbols, right, top, right < line.Length && top >= 0);
            AddSymbol(surroundingSymbols, right, bottom, right < line.Length && bottom < _map.Length);
        }
        return surroundingSymbols.Where(x => !x.Symbol.Equals(IgnoreSymbol)).ToList();
    }

    private void ImportFromFile(string path)
    {
        var lines = File.ReadAllLines(path);
        _map = lines.ToArray();
    }


    public int SolvePart2()
    {
        var validWords = new List<WorldSymbol>();
        foreach (var wordPosition in _wordPositions)
        {
            var symbols = GetSurroundingSymbols(wordPosition);
            validWords.AddRange(from symbol in symbols where symbol.Symbol == EngineSymbol select new WorldSymbol(wordPosition, symbol));
        }

        var group = validWords.Distinct()
            .GroupBy(x => x.SymbolPosition)
            .Where(x => x.Count() > 1)
            .ToDictionary(k => k.Key, v => v.ToList())
            .Select(x => x.Value)
            .ToList();

        return group.Select(worldSymbols => 
            worldSymbols.Select(x => x.WordLocation.Word).ToList())
            .Select(words => words.Aggregate(1, (current, word) => current * int.Parse(word)))
            .Sum();
    }
}

public record WordLocation(string Word, Location Location);
public record SymbolPosition(char Symbol, Position Position);
public record Location(int StartX, int EndX, int Y);
public record Position(int X, int Y);
public record WorldSymbol(WordLocation WordLocation, SymbolPosition SymbolPosition);