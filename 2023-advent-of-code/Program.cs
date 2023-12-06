using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

namespace _2023_advent_of_code;

public class Program
{
    public static void Main()
    {
        var config = new ManualConfig()
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddValidator(JitOptimizationsValidator.DontFailOnError)
            .AddLogger(ConsoleLogger.Default)
            .AddColumnProvider(DefaultColumnProviders.Instance);
        
        BenchmarkRunner.Run<Benchy>(config);
        // var day05 = new Day05.Day05("Day05/input.txt");
        // var result05 = day05.SolvePart1();
        // Console.WriteLine($"Part 1: {result05}");
        // result05 = day05.SolvePart2();
        // Console.WriteLine($"Part 2: {result05}");
        
        // var day06 = new Day06.Day06("Day06/input.txt");
        // var result06 = day06.Solve();
        // Console.WriteLine($"Part 1: {result06}");
        // result06 = day06.SolveFastest();
        // Console.WriteLine($"Part 2: {result06}");

    }
    
    public class Benchy
    {
        private static readonly Day06.Day06 Day06 = new("Day06/input.txt", true);
        
        // [Benchmark]
        // public long Day06Part1() => Day06.Solve();
        //
        // [Benchmark]
        // public long Day06Part2() => Day06.SolveFastest();
        
        [Benchmark]
        public List<long> GetWinningOptions() => _2023_advent_of_code.Day06.Day06.GetWinningOptions(Day06.Races.First());
        
        
        [Benchmark]
        public long GetWinningOptionsFastest() => _2023_advent_of_code.Day06.Day06.GetWinningOptionsFastest(Day06.Races.First());
        
        
    }
}