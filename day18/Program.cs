using System;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = ".^^^.^.^^^.^.......^^.^^^^.^^^^..^^^^^.^.^^^..^^.^.^^..^.^..^^...^.^^.^^^...^^.^.^^^..^^^^.....^....";

            var part1 = 0;
            var part2 = 0;
            var row = ParseRow(input);
            part1 += row.Where(e => !e).Count();
            part2 += row.Where(e => !e).Count();
            for (var i = 1; i < 400000; i++)
            {
                row = IterateRow(row);
                if (i < 40)
                    part1 += row.Where(e => !e).Count();
                part2 += row.Where(e => !e).Count();
            }
            Console.WriteLine($"part1: {part1}");
            Console.WriteLine($"part2: {part2}");
        }

        public static bool[] IterateRow(bool[] row)
        {
            var result = new bool[row.Length];
            for (var i = 0; i < row.Length; i++)
            {
                if ((i == 0 || !row[i - 1]) && (i < row.Length - 1 && row[i + 1]))
                    result[i] = true;
                else if ((i > 0 && row[i - 1]) && (i == row.Length - 1 || !row[i + 1]))
                    result[i] = true;
            }

            return result;
        }

        public static bool[] ParseRow(string input)
        {
            return input
                .Select(e => e == '^')
                .ToArray();
        }
    }
}
