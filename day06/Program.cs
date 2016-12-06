using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var linelength = lines[0].Length;
            var output = new List<char>(linelength);
            var output2 = new List<char>(linelength);
            for (var i = 0; i < linelength; i++)
            {
                var chars = lines.Select(e => e[i])
                     .GroupBy(e => e)
                     .OrderBy(c => c.Key)
                     .ToDictionary(grp => grp.Key, grp => grp.Count());

                output.Add(chars
                    .OrderByDescending(e => e.Value)
                    .FirstOrDefault().Key);
                output2.Add(chars
                    .OrderBy(e => e.Value)
                    .FirstOrDefault().Key);
            }
            Console.WriteLine($"part1: {string.Concat(output)}");
            Console.WriteLine($"part2: {string.Concat(output2)}");
        }
    }
}
