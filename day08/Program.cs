using System;
using System.IO;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static bool[,] copyGrid(bool[,] grid)
        {
            var result = new bool[6, 50];
            if (grid != null)
                Array.Copy(grid, result, grid.Length);
            return result;
        }
        public static void Main(string[] args)
        {
            var grid = copyGrid(null);
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var instruction = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine(line);
                switch (instruction[0])
                {
                    case "rect":
                        var coords = instruction[1].Split("x".ToCharArray());
                        for (var y = 0; y < int.Parse(coords[1]); y++)
                            for (var x = 0; x < int.Parse(coords[0]); x++)
                                grid[y, x] = true;
                        break;
                    case "rotate":
                        var index = int.Parse(instruction[2].Substring(2));
                        var step = int.Parse(instruction[4]);
                        var newGrid = copyGrid(grid);
                        switch (instruction[1])
                        {
                            case "column":
                                for (var y = 0; y < grid.GetLength(0); y++)
                                    newGrid[(y + step) % grid.GetLength(0), index] = grid[y, index];
                                break;
                            case "row":
                                for (var x = 0; x < grid.GetLength(1); x++)
                                    newGrid[index, (x + step) % grid.GetLength(1)] = grid[index, x];
                                break;
                        }
                        grid = newGrid;
                        break;
                }

                for (var y = 0; y < grid.GetLength(0); y++)
                {
                    for (var x = 0; x < grid.GetLength(1); x++)
                        Console.Write(grid[y, x] ? '#' : ' ');
                    Console.WriteLine();
                }
            }
            var part1 = (from bool var in grid where var select var).Count();
            Console.WriteLine($"part: {part1}");
        }
    }
}
