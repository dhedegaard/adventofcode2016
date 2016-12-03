using System;
using System.Linq;
using System.IO;

namespace ConsoleApplication
{
    public class Program
    {
        public static bool is_sorted(int[] arr)
        {
            var sorted_arr = arr.OrderBy(e => e).ToArray();
            return sorted_arr[0] + sorted_arr[1] > sorted_arr[2];
        }

        public static void Main(string[] args)
        {
            // Parse a piece
            var input = File.ReadAllLines("input.txt")
                .Select(line => line
                    .Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(e => int.Parse(e))
                    .ToArray())
                .ToArray();

            // part1
            var count = 0;
            foreach (var row in input)
                if (is_sorted(row)) count++;
            Console.WriteLine($"part1: {count}");

            // part2
            count = 0;
            for (var y = 0; y + 2 < input.Length; y += 3)
                for (var x = 0; x < input[y].Length; x++)
                    if (is_sorted(new int[] {
                        input[y][x],
                        input[y+1][x],
                        input[y+2][x],
                    })) count++;
            Console.WriteLine($"part2: {count}");
        }
    }
}
