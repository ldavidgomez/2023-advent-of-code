// using NUnit.Framework;
//
// namespace _2023_advent_of_code.Day10;
//
// public class Day10BTest
// {
//     [SetUp]
//     public void Setup()
//     {
//     }
//     
//     /*
//        .....
//        .S-7.
//        .|.|.
//        .L-J.
//        .....
//      */
//     
//     [Test]
//     public void should_find_steps_from_S_to_farthest_char_returns_4()
//     {
//         var expected = 4;
//          var input = new[]
//          {
//              ".....",
//              ".S-7.",
//              ".|.|.",
//              ".L-J.",
//              "....."
//          };
//         
//         var day10 = new Day10B(input);
//         var actual = day10.SolvePart1();
//         
//         Assert.AreEqual(expected, actual);
//     }
//     
//     /*
//        ..F7.
//        .FJ|.
//        SJ.L7
//        |F--J
//        LJ...
//      */
//     
//     [Test]
//     public void should_find_steps_from_S_to_farthest_char_returns_8()
//     {
//         var expected = 8;
//         var input = new[]
//         {
//             "..F7.",
//             ".FJ|.",
//             "SJ.L7",
//             "|F--J",
//             "LJ..."
//         };
//         
//         var day10 = new Day10B(input);
//         var actual = day10.SolvePart1();
//         
//         Assert.AreEqual(expected, actual);
//     }
//     
//     [Test]
//     public void should_find_steps_from_S_to_farthest_char_returns_6()
//     {
//         var expected = 6;
//         var input = new[]
//         {
//             "S-7.",
//             "|.-|",
//             "|-F|",
//             "..-|"
//
//         };
//         
//         var day10 = new Day10B(input);
//         var actual = day10.SolvePart1();
//         
//         Assert.AreEqual(expected, actual);
//     }
//     
//     [Test]
//     public void should_find_steps_from_S_to_farthest_char_returns_8b()
//     {
//         var expected = 8;
//         var input = new[]
//         {
//             "S-7.",
//             "|.||",
//             "|-F|",
//             "-|-|"
//
//         };
//         
//         var day10 = new Day10B(input);
//         var actual = day10.SolvePart1();
//         
//         Assert.AreEqual(expected, actual);
//     }
//     
//     [Test]
//     public void void_should_return_farthest_char_from_file()
//     {
//         var expected = 8;
//         var day10 = new Day10B("Day10/input.txt");
//         var actual = day10.SolvePart1();
//         
//         Assert.AreEqual(expected, actual);
//     }
// }
