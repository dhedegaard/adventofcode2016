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

            var visited_below_50 = new HashSet<Tuple<int, int>>();  // for part2
            // Locations we have visited.
            var visited = new HashSet<Tuple<int, int>>();
            // Locations to visit.
            var queue = new Queue<Point>();
            // The initial point of entry: (1, 1)
            queue.Enqueue(new Point { x = 1, y = 1, steps = 0 });
            while (queue.Any())
            {
                // Fetch the new position.
                var current = queue.Dequeue();
                // Check if we've hit part1.
                if (current.y == endpoint.Item1 && current.x == endpoint.Item2)
                {
                    Console.WriteLine($"part1: {current.steps}");
                    break;
                }
                // Find points to left, right, up, down from the current position.
                for (var dy = -1; dy < 2; dy++)
                    for (var dx = -1; dx < 2; dx++)
                    {
                        // Avoid diagonal points and dy, dx == 0, 0
                        if (dy == 0 && dx == 0 || dy != 0 && dx != 0) continue;
                        var new_x = current.x + dx;
                        var new_y = current.y + dy;
                        var new_tuple_point = Tuple.Create(new_y, new_x);
                        // Check if we hit a wall or an already visited point.
                        if (isValid(input, new_y, new_x) && !visited.Contains(new_tuple_point))
                        {
                            // We hit a new, valid position, queue it for later.
                            queue.Enqueue(new Point
                            {
                                x = new_x,
                                y = new_y,
                                steps = current.steps + 1,
                            });
                            // Mark the position as visited.
                            visited.Add(new_tuple_point);
                            // part2: If we're below 50 steps, add to visited below 50.
                            if (current.steps + 1 <= 50)
                                visited_below_50.Add(new_tuple_point);
                        }
                    }
            }
            Console.WriteLine($"part2: {visited_below_50.Count}");
        }
    }
}
