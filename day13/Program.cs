using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public struct Point
        {
            public int x, y, steps;

            public override string ToString()
            {
                return $"x: {x}, y: {y}, steps: {steps}";
            }
        }

        public static int countBits(int value)
        {
            var count = 0;
            while (value > 0)
            {
                if ((value & 1) == 1) count++;
                value >>= 1;
            }
            return count;
        }

        public static bool isValid(int input, int y, int x)
        {
            if (x < 0 || y < 0) return false;
            return countBits(x * x + 3 * x + 2 * x * y + y + y * y + input) % 2 == 0;
        }

        public static void Main(string[] args)
        {
            var input = 1350;
            var endpoint = Tuple.Create(39, 31);

            for (var y = 0; y < 7; y++)
            {
                for (var x = 0; x < 10; x++)
                    Console.Write(isValid(input, y, x) ? "." : "#");
                Console.WriteLine();
            }

            var visited_below_50 = new HashSet<Tuple<int, int>>();
            var visited = new HashSet<Tuple<int, int>>();
            var queue = new Queue<Point>();
            queue.Enqueue(new Point { x = 1, y = 1, steps = 0 });
            while (queue.Any())
            {
                var current = queue.Dequeue();
                Console.WriteLine(current);
                if (current.y == endpoint.Item1 && current.x == endpoint.Item2)
                {
                    Console.WriteLine($"part1: {current.steps}");
                    break;
                }
                for (var dy = -1; dy < 2; dy++)
                    for (var dx = -1; dx < 2; dx++)
                    {
                        if (dy == 0 && dx == 0 || dy != 0 && dx != 0) continue;
                        var new_x = current.x + dx;
                        var new_y = current.y + dy;
                        if (isValid(input, new_y, new_x) &&
                            !visited.Contains(Tuple.Create(new_y, new_x)))
                        {
                            queue.Enqueue(new Point
                            {
                                x = new_x,
                                y = new_y,
                                steps = current.steps + 1,
                            });
                            var new_tuple_point = Tuple.Create(new_y, new_x);
                            visited.Add(new_tuple_point);
                            if (current.steps + 1 <= 50)
                                visited_below_50.Add(new_tuple_point);
                        }
                    }
            }
            Console.WriteLine($"part2: {visited_below_50.Count}");
        }
    }
}
