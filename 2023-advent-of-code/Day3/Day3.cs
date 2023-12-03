namespace _2023_advent_of_code.Day3;


public class Day3
{
    private readonly List<WordPosition> _wordPositions;
    private string[] _map = {};
    private const char IgnoreSymbol = '.';
    
    public Day3(string path)
    {
        ImportFromFile(path);
        _wordPositions = GetWordPositions();
    }

    public Day3(string[] map)
    {
        _map = map;
        _wordPositions = GetWordPositions();
    }

    private List<WordPosition> GetWordPositions()
    {
        var wordPositions = new List<WordPosition>();
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

    private static void CreateAndAddWord(string line, int startWord, int endWord, int posY, List<WordPosition> wordPositions)
    {
        var word = line.Substring(startWord, endWord - startWord + 1);
        wordPositions.Add(new WordPosition(word, new Position(startWord, endWord, posY)));
    }
    
    public int SolvePart1()
    {
        var validWords = (from wordPosition in _wordPositions let symbols = GetSurroundingSymbols(wordPosition) where !symbols.All(char.IsDigit) select wordPosition.Word).ToList();

        return validWords.Sum(int.Parse);
    }

    private List<char> GetSurroundingSymbols(WordPosition wordPosition)
    {
        var surroundingSymbols = new List<char>();
        var position = wordPosition.Position;
        
        for (var x = position.StartX; x <= position.EndX; x++)
        {
            var y = position.Y;
        
            var line = _map[y];
            var leftX = x - 1;
            var rightX = x + 1;
            var upperY = y - 1;
            var lowerY = y + 1;

            AddSymbolIfWithinBoundaries(surroundingSymbols, 
                                        leftX >= 0, 
                                        () => line[leftX]);

            AddSymbolIfWithinBoundaries(surroundingSymbols, 
                                        rightX < line.Length, 
                                        () => line[rightX]);

            AddSymbolIfWithinBoundaries(surroundingSymbols, 
                                        upperY >= 0, 
                                        () => _map[upperY][x]);

            AddSymbolIfWithinBoundaries(surroundingSymbols, 
                                        lowerY < _map.Length, 
                                        () => _map[lowerY][x]);

            AddSymbolIfWithinBoundaries(surroundingSymbols, 
                                        leftX >= 0 && upperY >= 0, 
                                        () => _map[upperY][leftX]);

            AddSymbolIfWithinBoundaries(surroundingSymbols, 
                                        leftX >= 0 && lowerY < _map.Length, 
                                        () => _map[lowerY][leftX]);

            AddSymbolIfWithinBoundaries(surroundingSymbols, 
                                        rightX < line.Length && upperY >= 0, 
                                        () => _map[upperY][rightX]);

            AddSymbolIfWithinBoundaries(surroundingSymbols, 
                                        rightX < line.Length && lowerY < _map.Length, 
                                        () => _map[lowerY][rightX]);
        }

        return surroundingSymbols.Where(x => x != IgnoreSymbol).ToList();
    }

    private void AddSymbolIfWithinBoundaries(ICollection<char> symbols, bool condition, Func<char> getSymbolFunc)
    {
        if (!condition) return;
        
        var symbol = getSymbolFunc();
        symbols.Add(symbol);
    }

    private void ImportFromFile(string path)
    {
        var lines = File.ReadAllLines(path);
        _map = lines.ToArray();
    }

    
}

public record WordPosition(string Word, Position Position);
public record Position(int StartX, int EndX, int Y);