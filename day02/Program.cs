using System;
using System.IO;

namespace ConsoleApplication
{
    public class Program
    {
        public struct Position
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public static char?[,] grid = new char?[,] {
            {null, null, '1', null, null},
            {null, '2', '3', '4', null},
            {'5', '6', '7', '8', '9'},
            {null, 'A', 'B', 'C', null},
            {null, null, 'D', null, null},
        };

        public static void Main(string[] args)
        {
            // Read and split the input
            var input = File.ReadAllText("input.txt").Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            // Start in the middle
            var position1 = 5;
            var position2 = new Position { x = 0, y = 2 };
            var code1 = "";
            var code2 = "";
            // grid for part2.
            foreach (var line in input)
            {
                foreach (var instruction in line.ToCharArray())
                {
                    // Execute operation for part 1.
                    switch (instruction)
                    {
                        case 'U': if (position1 >= 4) position1 -= 3; break;
                        case 'D': if (position1 <= 6) position1 += 3; break;
                        case 'L': if (position1 % 3 != 1) position1 -= 1; break;
                        case 'R': if (position1 % 3 != 0) position1 += 1; break;
                    }
                    // Execute operation for part 2.
                    switch (instruction)
                    {
                        case 'U': if (position2.y > 0 && grid[position2.y - 1,position2.x] != null) position2.y -= 1; break;
                        case 'D': if (position2.y < 4 && grid[position2.y + 1,position2.x] != null) position2.y += 1; break;
                        case 'L': if (position2.x > 0 && grid[position2.y,position2.x - 1] != null) position2.x -= 1; break;
                        case 'R': if (position2.x < 4 && grid[position2.y,position2.x + 1] != null) position2.x += 1; break;
                    }
                }
                code1 += position1.ToString();
                code2 += grid[position2.y,position2.x];
            }
            Console.WriteLine($"part1: {code1}");
            Console.WriteLine($"part2: {code2}");
        }
    }
}
