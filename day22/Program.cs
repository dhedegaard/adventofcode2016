using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApplication
{
    public class Program
    {
        public struct Node
        {
            public int x, y, size, used, avail;

            public override string ToString()
            {
                return $"x: {x}, y: {y}, size: {size}, used: {used}, avail: {avail}";
            }

            // override object.Equals
            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                    return false;

                var other = (Node)obj;
                return x == other.x && y == other.y;
            }

            public override int GetHashCode()
            {
                return x.GetHashCode() * 3 + y.GetHashCode() * 7;
            }
        }

        public static void Main(string[] args)
        {
            var nodes = new List<Node>();
            var parseNode = new Regex(@".*-x(\d+)-y(\d+)");
            foreach (var line in File.ReadAllLines("input.txt").Skip(2))
            {
                var list = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var rc = parseNode.Match(list[0]);
                var x = rc.Groups[1];
                var y = rc.Groups[2];
                nodes.Add(new Node
                {
                    x = int.Parse(x.Value),
                    y = int.Parse(y.Value),
                    size = int.Parse(list[1].Replace("T", "")),
                    used = int.Parse(list[2].Replace("T", "")),
                    avail = int.Parse(list[3].Replace("T", "")),
                });
            }

            var part1 = nodes
                .Where(e => e.used > 0)
                .Select(e => nodes
                    .Where(f => !f.Equals(e) && f.avail >= e.used)
                    .Count())
                .Sum();
            Console.WriteLine($"part1: {part1}");

            var max_y = nodes.OrderByDescending(e => e.y).Select(e => e.y).First();
            var max_x = nodes.OrderByDescending(e => e.x).Select(e => e.x).First();
            Node? start = null, hole = null;
            var grid = new Node[max_y + 1, max_x + 1];
            foreach (var node in nodes)
                grid[node.y, node.x] = node;
            for (var y = 0; y < grid.GetLength(0); y++)
            {
                for (var x = 0; x < grid.GetLength(1); x++)
                {
                    var node = grid[y, x];
                    if (node.used == 0)
                        hole = node;
                    else if (node.size > 250 && start == null)
                        start = grid[y, x - 1];
                }
            }
            var part2 = Math.Abs(hole.Value.x - start.Value.x) + Math.Abs(hole.Value.y - start.Value.y) +
                Math.Abs(start.Value.x - max_x) + start.Value.y + 5 * (max_x - 1);
            Console.WriteLine($"part2: {part2}");
        }
    }
}
