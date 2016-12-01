using System;
using System.Collections.Generic;
using System.Linq;

namespace Day01
{
    public class Program
    {
        public enum Direction
        {
            north = 0,
            west = 1,
            south = 2,
            east = 3
        }

        public struct Position
        {
            public int x { get; set; }
            public int y { get; set; }

            public int AbsFromZero
            {
                get { return Math.Abs(this.x) + Math.Abs(y); }
            }

            public override string ToString()
            {
                return $"x: {x}, y: {y}, abs: {AbsFromZero}";
            }
        }

        public static Direction currDir = Direction.north;
        public const string input = "R5, L2, L1, R1, R3, R3, L3, R3, R4, L2, R4, L4, R4, R3, L2, L1, L1, R2, R4, R4, L4, R3, L2, R1, L4, R1, R3, L5, L4, L5, R3, L3, L1, L1, R4, R2, R2, L1, L4, R191, R5, L2, R46, R3, L1, R74, L2, R2, R187, R3, R4, R1, L4, L4, L2, R4, L5, R4, R3, L2, L1, R3, R3, R3, R1, R1, L4, R4, R1, R5, R2, R1, R3, L4, L2, L2, R1, L3, R1, R3, L5, L3, R5, R3, R4, L1, R3, R2, R1, R2, L4, L1, L1, R3, L3, R4, L2, L4, L5, L5, L4, R2, R5, L4, R4, L2, R3, L4, L3, L5, R5, L4, L2, R3, R5, R5, L1, L4, R3, L1, R2, L5, L1, R4, L1, R5, R1, L4, L4, L4, R4, R3, L5, R1, L3, R4, R3, L2, L1, R1, R2, R2, R2, L1, L1, L2, L5, L3, L1";

        public static void Main(string[] args)
        {
            // We start at center, lookup north.
            var currDir = Direction.north;
            var currPos = new Position { x = 0, y = 0 };
            var path = new List<Position>();
            var foundFirstOverlap = false;
            foreach (var inst in input.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                // Change Direction
                switch (inst[0])
                {
                    case 'L':
                        currDir = (Direction)(((int)currDir + 1) % 4);
                        break;
                    case 'R':
                        currDir = (Direction)(((int)currDir + 4 - 1) % 4);
                        break;
                }

                // Iterate steps in the new direction
                var range = int.Parse(string.Concat(inst.Skip(1)));
                for (var i = 0; i < range; i++)
                {
                    // Move one position in a given direction
                    switch (currDir)
                    {
                        case Direction.north: currPos.y++; break;
                        case Direction.west: currPos.x--; break;
                        case Direction.south: currPos.y--; break;
                        case Direction.east: currPos.x++; break;
                    }
                    // Check to see if we've been here before
                    if (!foundFirstOverlap && path.Where(e => e.x == currPos.x && e.y == currPos.y).Any())
                    {
                        foundFirstOverlap = true;
                        Console.WriteLine($"part2: First overlap is {currPos.AbsFromZero} blocks away");
                    }
                    path.Add(currPos);
                }
            }
            // Now, where did we end up ?
            Console.WriteLine($"part1: The end location is {currPos.AbsFromZero} blocks away");
        }
    }
}