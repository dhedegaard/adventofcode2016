using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace ConsoleApplication
{
    public struct Disc
    {
        public int maxPos;
        public int pos;

        public override string ToString()
        {
            return $"maxPos: {maxPos}, pos: {pos}";
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"part1: {hitIt(false)}");
            Console.WriteLine($"part2: {hitIt(true)}");
        }

        public static int hitIt(bool part2)
        {
            // Parse input.
            var discs = new Dictionary<int, Disc>();
            var re = new Regex(
                @"Disc .(?<discno>\d+) has (?<maxPos>\d+) positions; " +
                @"at time=0, it is at position (?<pos>\d+)\.",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var res = re.Match(line);
                if (!res.Success) throw new Exception("YAOI");
                discs[int.Parse(res.Groups["discno"].Value)] = new Disc
                {
                    maxPos = int.Parse(res.Groups["maxPos"].Value),
                    pos = int.Parse(res.Groups["pos"].Value),
                };
            }

            if (part2)
                discs[discs.Select(e => e.Key).Max() + 1] = new Disc
                {
                    maxPos = 11,
                    pos = 0,
                };

            // Make a copy of the original discs
            var origDiscs = Move(discs, 0);

            // Iterate
            for (var startTime = 0; ; startTime++)
            {
                discs = Move(origDiscs, 0);
                var horiz = 0;
                var failed = false;

                // Move discs based on startime to begin with.
                discs = Move(discs, startTime);

                // Iterate until we hit a failpoint or get past all the discs.
                while (horiz <= discs.Select(e => e.Key).Max())
                {
                    horiz++;
                    discs = Move(discs, 1);
                    if (discs.ContainsKey(horiz) && discs[horiz].pos != 0)
                    {
                        failed = true;
                        break;
                    }
                }
                // Check for failure state.
                if (!failed)
                    return startTime;
            }
        }

        public static Dictionary<int, Disc> Move(Dictionary<int, Disc> discs, int positions)
        {
            var nextDiscs = new Dictionary<int, Disc>();
            foreach (var pair in discs)
            {
                var disc = pair.Value;
                nextDiscs[pair.Key] = new Disc
                {
                    maxPos = disc.maxPos,
                    pos = (disc.pos + positions) % disc.maxPos,
                };
            }
            return nextDiscs;
        }
    }
}