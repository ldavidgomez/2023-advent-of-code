// namespace _2023_advent_of_code.Day10;
//
// public class Day10B
// {
//       #region Fields
//
//     private readonly string[] _input;
//     public static Point[] Map { get; private set; }
//
//     #endregion
//
//     #region Constructors
//
//     public Day10B(string path)
//     {
//         if (string.IsNullOrWhiteSpace(path))
//         {
//             throw new ArgumentException("Invalid file path.");
//         }
//         
//         _input = File.ReadAllLines(path);
//         SetMap();
//     }
//
//     public Day10B(string[] input)
//     {
//         _input = input ?? throw new ArgumentNullException(nameof(input));
//         SetMap();
//     }
//
//     private void SetMap()
//     {
//         Map =  _input.SelectMany((line, y) => line.Select((character, x) => new Point(x, y, character))).ToArray();
//     }
//
//     #endregion
//     
//     #region Public Methods
//     
//     public int SolvePart1()
//     {
//         var tree = new Tree(Map.First(p => p.Character == 'S'));
//         var result = tree.FindFarthestPosition();
//         return result.Item2;
//     }
//     
//     #endregion
// }
//
//
// public class Point
// {
//     #region Properties
//
//     public Coordinate Coordinate { get; init; }
//     public List<Point> Children { get; } = new List<Point>();
//     
//     public char Character {get; init; }
//     
//     public int Level { get; set; } = 0;
//     
//     #endregion
//
//     #region Constructors
//
//     public Point(int x, int y, char character)
//     {
//         Coordinate = new Coordinate(x, y);
//         Character = character;
//     }
//     
//     public void CalculateDistanceFromStart(Point start)
//     {
//         
//     }
//
//     #endregion
// }
//
// public record Coordinate(int X, int Y);
//
// public class Tree
// {
//     public Point Root { get; }
//     
//     /*
//        | is a vertical pipe connecting north and south.
//        - is a horizontal pipe connecting east and west.
//        L is a 90-degree bend connecting north and east.
//        J is a 90-degree bend connecting north and west.
//        7 is a 90-degree bend connecting south and west.
//        F is a 90-degree bend connecting south and east.
//        . is ground; there is no pipe in this tile.
//        S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
//      */
//
//     private static readonly Dictionary<string, Coordinate> _coords = new()
//     {
//         { "N", new Coordinate(0, -1) },
//         { "S", new Coordinate(0, 1) },
//         { "E", new Coordinate(1, 0) },
//         { "W", new Coordinate(-1, 0) }
//     };
//     
//     private readonly Dictionary<string, List<Coordinate>> _directions = new()
//     {
//         { "|", new List<Coordinate> { _coords["N"], _coords["S"]} },
//         { "-", new List<Coordinate> { _coords["E"], _coords["W"]} },
//         { "L", new List<Coordinate> { _coords["N"], _coords["E"]} },
//         { "J", new List<Coordinate> { _coords["N"], _coords["W"]} },
//         { "7", new List<Coordinate> { _coords["S"], _coords["W"]} },
//         { "F", new List<Coordinate> { _coords["S"], _coords["E"]} },
//         { "S", new List<Coordinate> { _coords["N"], _coords["S"], _coords["E"], _coords["W"]} }
//     };
//
//     public Tree(Point root)
//     {
//         Root = root;
//         root.Level = 0;
//         SetChildren(root);
//     }
//
//     private void SetChildren(Point parentNode)
//     {
//         var currentCharacter = parentNode.Character.ToString();
//         var currentCoordinate = parentNode.Coordinate;
//         var nextCoordinates = _directions[currentCharacter].Select(coordinate => new Coordinate(coordinate.X + currentCoordinate.X, coordinate.Y + currentCoordinate.Y));
//         var nextPoints = Day10B.Map.Where(point => nextCoordinates.Contains(point.Coordinate) && point.Character != '.' && point.Character != 'S' && point.Level == 0);
//         
//         foreach (var nextPoint in nextPoints)
//         {
//             Console.WriteLine("Current point: " + parentNode.Character);
//             Console.WriteLine("Next point: " + nextPoint.Character);
//         }
//         
//         foreach (var nextPoint in nextPoints)
//         {
//             nextPoint.Level = parentNode.Level + 1;
//             parentNode.Children.Add(nextPoint);
//             Console.WriteLine($"Root: {parentNode.Character} - Lvl {parentNode.Level} - Child: {nextPoint.Character} - Lvl {nextPoint.Level}" );
//             SetChildren(nextPoint);
//         }
//     }
//
//     public Tuple<Point, int> FindFarthestPosition()
//     {
//         if (Root == null)
//         {
//             throw new InvalidOperationException("El árbol está vacío.");
//         }
//
//         Queue<Tuple<Point, int>> queue = new Queue<Tuple<Point, int>>();
//         queue.Enqueue(Tuple.Create(Root, 0));
//
//         Tuple<Point, int> farthestPoint = null;
//
//         while (queue.Count > 0)
//         {
//             var current = queue.Dequeue();
//             var node = current.Item1;
//             var level = current.Item2;
//
//             // Actualiza el nodo más lejano si es necesario
//             if (farthestPoint == null || level > farthestPoint.Item2)
//             {
//                 farthestPoint = Tuple.Create(node, level);
//             }
//
//             // Agrega los hijos del nodo actual a la cola
//             foreach (var hijo in node.Children)
//             {
//                 queue.Enqueue(Tuple.Create(hijo, level + 1));
//             }
//         }
//
//         // Devuelve la posición más lejana como una tupla de (Valor del nodo, Nivel)
//         return Tuple.Create(farthestPoint.Item1, farthestPoint.Item2);
//     }
// }