namespace _2023_advent_of_code;

public class Program
{
    public static void Main()
    {
        var day05 = new Day05.Day05("Day05/input.txt");
        var result = day05.SolvePart1();
        Console.WriteLine($"Part 1: {result}");
        result = day05.SolvePart2();
        Console.WriteLine($"Part 2: {result}");

    }
}