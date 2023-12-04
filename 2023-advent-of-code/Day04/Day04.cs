using System.Text.RegularExpressions;

namespace _2023_advent_of_code.Day04;


public class Day04
{
    private IEnumerable<string> _input;


    public Day04(string path)
    {
        _input = File.ReadAllLines(path).ToList();
        RemoveExtraSpaces();
    }

    public Day04(List<string> input)
    {
        _input = input;
        RemoveExtraSpaces();
    }

    private void RemoveExtraSpaces()
    {
        var spaceRegex = new Regex("[ ]{2,}", RegexOptions.None);
        _input = _input.Select(i => spaceRegex.Replace(i, " ")).ToList();
    }

    public int SolvePart1()
    {
        var cards = ParseGameCards();
        return CalculateScore(cards);
    }

    private IEnumerable<Card> ParseGameCards()
    {
        return _input.Select(s => new Card(ParseId(s), ParseWinners(s), ParseNumbers(s))).ToList();
    }

    private static int ParseId(string s)
    {
        return int.Parse(s.Split(":")[0].Trim().Split(" ")[1]);
    }
    
    
    private static IEnumerable<int> ParseWinners(string s)
    {
        return ParseSequence(s, 1, 0);
    }
    
    private static IEnumerable<int> ParseNumbers(string s)
    {
        return ParseSequence(s, 1, 1);
    }
    
    private static IEnumerable<int> ParseSequence(string s, int index1, int index2)
    {
        return s.Split(":")[index1].Split("|")[index2].Trim().Split(" ").Where(x => !x.Equals("")).Select(int.Parse).ToList();
    }
    
    private static int CalculateScore(IEnumerable<Card> cards)
    {
        return cards.Select(card => card.Winners.Intersect(card.Numbers).ToList()).Where(t => t.Any()).Sum(CalculatePoints);
    }
    
    private static IEnumerable<Card> GetWinningCards(IEnumerable<Card> cards)
    {
        var winningCards = new List<Card>();
        foreach (var card in cards)
        {
            var matches = card.Numbers.Intersect(card.Winners).ToList();
            if (matches.Count == 0) continue;
            
            card.Matches = matches.Count;
            winningCards.Add(card);
        }

        return winningCards;
    }

    private static int CalculatePoints(IEnumerable<int> winnerList)
    {
        var result = 0;
        var points = 1;
        foreach (var winner in winnerList)
        {
            if (result == 0)
            {
                points = 1;
            }
            else
            {
                points *= 2;
            }
            result += points;
        }
        return points;
    }

    public int SolvePart2()
    {
        var cards = ParseGameCards();
        var cardList = cards.ToList();
        var winningCards = GetWinningCards(cardList);
        var extraCards = GetAdditionalCardsWithMatches(cardList, winningCards);
        return cardList.Concat(extraCards).Count();
    }

    private static IEnumerable<Card> GetAdditionalCardsWithMatches(IEnumerable<Card> cards, IEnumerable<Card> winningCards)
    {
        var extraCards = new List<Card>();
        foreach (var additionalCards in 
                 from card in winningCards 
                 where card.Matches != 0 select AddMatchedCards(cards, card.Id, card.Matches))
        {
            extraCards.AddRange(additionalCards);
        }

        if (!extraCards.Any())
            return extraCards;

        var extraCardsWinning = GetAdditionalCardsWithMatches(cards, extraCards);
        extraCards.AddRange(extraCardsWinning);
    
        return extraCards;
    }

    private static IEnumerable<Card> AddMatchedCards(IEnumerable<Card> cards, int id, int matches)
    {
        var additionalCards = new List<Card>();
        var count = 0;
        foreach (var extraCard in cards.Where(card => id < card.Id))
        {
            additionalCards.Add(extraCard);
            count++;
            
            if(count == matches) break;
        }
        return additionalCards;
    }
}

public record Card(int Id, IEnumerable<int> Winners, IEnumerable<int> Numbers)
{
    public int Matches { get; set; }
};