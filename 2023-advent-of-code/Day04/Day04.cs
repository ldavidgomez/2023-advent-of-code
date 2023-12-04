using System.Text.RegularExpressions;

namespace _2023_advent_of_code.Day04;


public class Day04
{
    private List<string> _input;


    public Day04(string path)
    {
        _input = File.ReadAllLines(path).ToList();
        NormalizeSpaces();
    }

    public Day04(List<string> input)
    {
        _input = input;
        NormalizeSpaces();
    }

    private void NormalizeSpaces()
    {
        var options = RegexOptions.None;
        var regex = new Regex("[ ]{2,}", options);
        var normalizedList = _input.Select(s => regex.Replace(s, " ")).ToList();

        _input = normalizedList;
    }

    public int SolvePart1()
    {
        var cards = GetCards();
        var calculated = CalculateResult(cards);
        return calculated;
    }

    private List<Card> GetCards()
    {
        return (from s in _input 
            let id = int.Parse(s.Split(":")[0].Trim().Split(" ")[1]) 
            let winners = SplitCardIntoList(s, 1,0).Select(int.Parse).ToList() 
            let numbers = SplitCardIntoList(s, 1, 1).Select(int.Parse).ToList() 
            select new Card(id, winners, numbers))
            .ToList();
    }

    private static List<string> SplitIntoList(string s, int index)
    {
        return s.Split("|")[index].Trim().Split(" ").Where(x => !x.Equals("")).ToList();
    }
    
    private static List<string> SplitCardIntoList(string s, int index, int index2)
    {
        return s.Split(":")[index].Split("|")[index2].Trim().Split(" ").Where(x => !x.Equals("")).ToList();
    }

    private static int CalculateResult(List<List<int>> winners)
    {
        var totalPoints = winners
            .Where(x => x.Count > 0)
            .Select(GetPoints)
            .Sum();

        return totalPoints;
    }
    
    private static int CalculateResult(List<Card> cards)
    {
        return cards.Select(card => card.Winners.Intersect(card.Numbers).ToList()).Where(t => t.Any()).Sum(GetPoints);
    }
    
    private List<Card> GetWinningCards(List<Card> cards)
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

    private static int GetPoints(List<int> winnerList)
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
        var cards = GetCards();
        var winningCards = GetWinningCards(cards);
        var extraCards = GetAdditionalCardsWithMatches(cards, winningCards);
        return cards.Concat(extraCards).Count();
    }

    private static List<Card> GetAdditionalCardsWithMatches(List<Card> cards, List<Card> winningCards)
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

    private static List<Card> AddMatchedCards(List<Card> cards, int id, int matches)
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

public record Card(int Id, List<int> Winners, List<int> Numbers)
{
    public int Matches { get; set; }
};