using System.ComponentModel;

namespace _2023_advent_of_code.Day07;

public class Day07
{
    private readonly string[] _input;
    private List<Hand> _hands = new();
    
    private enum HandStrength
    {
        FiveOfAKind = 6,
        FourOfAKind = 5,
        FullHouse = 4,
        ThreeOfAKind = 3,
        TwoPairs = 2,
        OnePair = 1,
        HighCard = 0
    }
    
    private enum SpecialCards
    {
        [Description("1")]
        One = 1,
        [Description("2")]
        Two = 2,
        [Description("3")]
        Three = 3,
        [Description("4")]
        Four = 4,
        [Description("5")]
        Five = 5,
        [Description("6")]
        Six = 6,
        [Description("7")]
        Seven = 7,
        [Description("8")]
        Eight = 8,
        [Description("9")]
        Nine = 9,
        T = 10,
        J = 11,
        Q = 12,
        K = 13,
        A = 14
    }
    
    private enum SpecialCardsWithJoker
    {
        [Description("1")]
        One = 1,
        [Description("2")]
        Two = 2,
        [Description("3")]
        Three = 3,
        [Description("4")]
        Four = 4,
        [Description("5")]
        Five = 5,
        [Description("6")]
        Six = 6,
        [Description("7")]
        Seven = 7,
        [Description("8")]
        Eight = 8,
        [Description("9")]
        Nine = 9,
        T = 10,
        J = 0,
        Q = 12,
        K = 13,
        A = 14
    }


    public Day07(string path)
    {
        _input = File.ReadAllLines(path).ToArray();
        SetCards();
    }

    public Day07(string[] input)
    {
        _input = input;
        SetCards();
    }

    private void SetCards()
    {
        foreach (var line in _input)
        {
            var split = line.Split(" ");
            var cards = split[0].Trim();
            var bid = long.Parse(split[1].Trim());
            _hands.Add(new Hand(cards, bid));
        }
    }

    public long Solve(bool withJoker = false)
    {
        if (withJoker)
        {
            SetStrengthWithJoker();
        }
        else
        {
            SetStrength();
        }
        SetStrengthPosition();
        
        var result = GetTotalBid(withJoker);
        
        return result;
    }

    private void SetStrengthWithJoker()
    {
        var orderedHands = _hands.Select(GetHandStrengthWithJoker).ToList();
        _hands = orderedHands;
    }

    private static Hand GetHandStrengthWithJoker(Hand hand)
    {
        var tmp = hand.Cards.ToList();
        var dictionary = new Dictionary<char,long>();
        var cards = hand.Cards;
        
        foreach (var c in tmp.Where(c => !dictionary.TryAdd(c, 1)))
        {
            dictionary[c]++;
        }
        
        var jokerResults = dictionary.FirstOrDefault(x => x.Key == 'J').Value;
        {
            var jokerNumber = (int)jokerResults;
            dictionary.Remove('J');
            if(jokerNumber == 5)
            {
                cards = cards.Replace("J", "A");
                dictionary.Add('A', 5);
            }
            else
            {
                var max = dictionary.Max(x => x.Value);
                var maxKey = dictionary.First(x => x.Value == max).Key;
                cards = cards.Replace("J", maxKey.ToString());
                dictionary[maxKey] = max + jokerNumber;
            }
        
        }

        return hand with { Cards = cards, Strength = dictionary };
    }

    private void SetStrength()
    {
        var orderedHands = _hands.Select(GetHandStrength).ToList();
        _hands = orderedHands;
    }

    private void SetStrengthPosition()
    {
        var valuedHands = new List<Hand>();
        CalculateRank(valuedHands);
        
        _hands = valuedHands;
    }

    private void CalculateRank(List<Hand> valuedHands)
    {
        foreach (var hand in _hands)
        {
            var strength = hand.Strength;
            var strengthValue = 0L;
            if (strength.ContainsValue(5))
            {
                strengthValue = (long) HandStrength.FiveOfAKind;
            }
            else if (strength.ContainsValue(4))
            {
                strengthValue = (long) HandStrength.FourOfAKind;
            }
            else if (strength.ContainsValue(3) && strength.ContainsValue(2))
            {
                strengthValue = (long) HandStrength.FullHouse;
            }
            else if (strength.ContainsValue(3))
            {
                strengthValue = (long) HandStrength.ThreeOfAKind;
            }
            else if (strength.Count(x => x.Value == 2) == 2)
            {
                strengthValue = (long) HandStrength.TwoPairs;
            }
            else if (strength.ContainsValue(2))
            {
                strengthValue = (long) HandStrength.OnePair;
            }
            else
            {
                strengthValue = (long) HandStrength.HighCard;
            }

            var strengthValueHand = hand with { Rank = strengthValue };
            valuedHands.Add(strengthValueHand);
        }
    }

    private Hand GetHandStrength(Hand card)
    {
        var tmp = card.Cards.ToList();
        var dictionary = new Dictionary<char,long>();
        foreach (var c in tmp.Where(c => !dictionary.TryAdd(c, 1)))
        {
            dictionary[c]++;
        }
        
        return card with {Strength = dictionary};
    }

    private long GetTotalBid(bool withJoker = false)
    {
        var rank = 1;
        var totalBid = 0L;
        var orderedHands = withJoker 
            ? _hands.Select(hand => (hand, hand.OriginalCards.Select(card => (int)Enum.Parse(typeof(SpecialCardsWithJoker), card.ToString()))))
                .OrderBy(x => x.hand.Rank)
                .ThenBy(x => x.Item2.ElementAt(0))
                .ThenBy(x => x.Item2.ElementAt(1))
                .ThenBy(x => x.Item2.ElementAt(2))
                .ThenBy(x => x.Item2.ElementAt(3))
                .ThenBy(x => x.Item2.ElementAt(4)) 
            : _hands.Select(hand => (hand, hand.OriginalCards.Select(card => (int)Enum.Parse(typeof(SpecialCards), card.ToString()))))
                .OrderBy(x => x.hand.Rank)
                .ThenBy(x => x.Item2.ElementAt(0))
                .ThenBy(x => x.Item2.ElementAt(1))
                .ThenBy(x => x.Item2.ElementAt(2))
                .ThenBy(x => x.Item2.ElementAt(3))
                .ThenBy(x => x.Item2.ElementAt(4));
        foreach (var hand in orderedHands.Select(x => x.hand))
        {
            var currentBid = hand.Bid * rank;
            totalBid = currentBid + totalBid;
            rank++;
        }

        return totalBid;
    }
}

public record Hand(string Cards, long Bid)
{
    public Dictionary<char, long> Strength { get; init; } = new();
    public long Rank { get; init; }
    public string OriginalCards { get; init; } = Cards;
}

