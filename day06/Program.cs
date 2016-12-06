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
                var chars = new Dictionary<char, int>();

                foreach (var line in lines)
                {
                    var c = line[i];
                    if (!chars.ContainsKey(c)) chars[c] = 1;
                    else chars[c]++;
                }

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
