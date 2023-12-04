namespace _2023_advent_of_code.Day04;


public class Day04
{
    private readonly List<string> _input;


    public Day04(string path)
    {
        _input = File.ReadAllLines(path).ToList();
    }

    public Day04(List<string> input)
    {
        _input = input;
    }

    public int SolvePart1()
    {
        var winners = GetWinnerNumbers();
        var calculated = CalculateResult(winners);
        return calculated;
    }

    private List<List<int>> GetWinnerNumbers()
    {
        return (from s in _input
            let result = new List<int>()
            let winners = SplitIntoList(s, 0)
            let numbers = SplitIntoList(s, 1)
            select winners.Where(numbers.Contains)
                .Select(winner => Convert.ToInt32(winner))
                .ToList()).ToList();
    }

    private static List<string> SplitIntoList(string s, int index)
    {
        return s.Split("|")[index].Trim().Split(" ").Where(x => !x.Equals("")).ToList();
    }

    private static int CalculateResult(List<List<int>> winners)
    {
        var totalPoints = winners
            .Where(x => x.Count > 0)
            .Select(GetPoints)
            .Sum();

        return totalPoints;
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
}
