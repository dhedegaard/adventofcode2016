using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication
{
    public static class Program
    {
        public static IEnumerable<List<T>> Permutate<T>(List<T> input)
        {
            if (input.Count == 2) // this are permutations of array of size 2
            {
                yield return new List<T>(input);
                yield return new List<T> { input[1], input[0] };
            }
            else
            {
                foreach (T elem in input) // going through array
                {
                    var rlist = new List<T>(input); // creating subarray = array
                    rlist.Remove(elem); // removing element
                    foreach (List<T> retlist in Permutate(rlist))
                    {
                        retlist.Insert(0, elem); // inserting the element at pos 0
                        yield return retlist;
                    }

                }
            }
        }

        public struct Pos
        {
            public int x, y, dist;

            public override string ToString()
            {
                return $"y: {y}, x: {x}";
            }
        }

        public static Dictionary<char, Pos> ParsePositions(char[][] map)
        {
            var res = new Dictionary<char, Pos>();

            for (var y = 0; y < map.Length; y++)
                for (var x = 0; x < map[y].Length; x++)
                    if (char.IsDigit(map[y][x]))
                        res[map[y][x]] = new Pos { y = y, x = x };
            return res;
        }

        public static Dictionary<Tuple<char, char>, int> bfsCache = new Dictionary<Tuple<char, char>, int>();
        public static int BfsDistance(char[][] map, Pos from, Pos to)
        {
            var fromToTuple = Tuple.Create(from, to);

            // Valid delta moves.
            var moves = new HashSet<Pos>();
            moves.Add(new Pos { y = 0, x = -1 });
            moves.Add(new Pos { y = 0, x = 1 });
            moves.Add(new Pos { y = -1, x = 0 });
            moves.Add(new Pos { y = 1, x = 0 });

            var queue = new Queue<Pos>();
            queue.Enqueue(from);
            var visited = new HashSet<Tuple<int, int>>();
            while (queue.Any())
            {
                var pos = queue.Dequeue();
                if (pos.x == to.x && pos.y == to.y)
                    return pos.dist;
                foreach (var deltaMove in moves)
                {
                    var newPos = new Pos { y = pos.y + deltaMove.y, x = pos.x + deltaMove.x, dist = pos.dist + 1 };
                    var newVisit = new Tuple<int, int>(newPos.y, newPos.x);
                    if (map[newPos.y][newPos.x] != '#' && !visited.Contains(newVisit))
                    {
                        queue.Enqueue(newPos);
                        visited.Add(newVisit);
                    }
                }
            }
            return -1;
        }

        public static void Main(string[] args)
        {
            var map = File.ReadAllLines("input.txt")
                .Select(e => e.ToCharArray())
                .ToArray();

            var positions = ParsePositions(map);

            var lowest = int.MaxValue;
            foreach (var sequence in Permutate(positions.Keys.Where(e => e != '0').ToList()))
            {
                var length = 0;
                var prevChar = '0';
                foreach (var c in sequence)
                {
                    var fromToTuple = Tuple.Create(prevChar, c);
                    if (!bfsCache.ContainsKey(fromToTuple))
                        bfsCache[fromToTuple] = BfsDistance(map, positions[prevChar], positions[c]);
                    var dist = bfsCache[fromToTuple];

                    if (dist == -1) throw new Exception(dist.ToString());
                    length += dist;
                    prevChar = c;
                }
                if (length < lowest)
                    lowest = length;
            }
            Console.WriteLine($"part1: {lowest}");

            lowest = int.MaxValue;
            foreach (var sequence in Permutate(positions.Keys.Where(e => e != '0').ToList()))
            {
                var length = 0;
                var prevChar = '0';
                foreach (var c in sequence.Concat(new char[] { '0' }))
                {
                    var fromToTuple = Tuple.Create(prevChar, c);
                    if (!bfsCache.ContainsKey(fromToTuple))
                        bfsCache[fromToTuple] = BfsDistance(map, positions[prevChar], positions[c]);
                    var dist = bfsCache[fromToTuple];

                    if (dist == -1) throw new Exception(dist.ToString());
                    length += dist;
                    prevChar = c;
                }
                if (length < lowest)
                    lowest = length;
            }
            Console.WriteLine($"part2: {lowest}");
        }
    }
}
