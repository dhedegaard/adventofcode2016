using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public struct Range
        {
            public uint from, to;

            public bool InRange(uint a)
            {
                return from <= a && a <= to;
            }

            public override string ToString()
            {
                return $"from: {from}, to: {to}";
            }
        }
        public static void Main(string[] args)
        {
            // Parse input.
            var blocklist = new List<Range>();
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var splitted = line.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                blocklist.Add(new Range
                {
                    from = uint.Parse(splitted[0]),
                    to = uint.Parse(splitted[1]),
                });
            }
            // Order by from-value first.
            blocklist = blocklist.OrderBy(e => e.from).ToList();

            uint? part1 = null;
            uint valid = uint.MinValue; // Iterate from min -> max 32-bit unsigned int.
            uint validAddrs = 0;  // The number of valid addresses in the address space.
            var i = 0;  // Index of the current blocklist element (ie Range object).
            var current = blocklist[i];
            while (valid < uint.MaxValue)
            {
                if (valid < validAddrs) break;
                if (valid >= current.from)
                {
                    if (valid <= current.to)
                    {
                        // If we're within a blocklist, move just beyond it and iterate again.
                        valid = current.to + 1;
                        continue;
                    }
                    // If the current blocklist is no longer valid, iterate to the next one.
                    i++;
                    current = blocklist[i];
                }
                else if (valid < current.from)
                {
                    // If we've below the current blocklist, it means we're within valid address space.
                    if (part1 == null) part1 = valid;
                    valid++;
                    validAddrs++;
                }
            }
            Console.WriteLine($"part1: {part1}");
            Console.WriteLine($"part2: {validAddrs}");
        }
    }
}
