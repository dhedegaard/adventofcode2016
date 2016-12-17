using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static MD5 md5 = MD5.Create();

        public static string md5sum(string input)
        {
            return BitConverter
                .ToString(md5.ComputeHash(Encoding.ASCII.GetBytes(input)))
                .Replace("-", "")
                .Substring(0, 4)
                .ToLower();
        }

        public struct Position
        {
            public int x, y;
            public string hash;
            public string path;
        }


        public static HashSet<char> validChars = new HashSet<char> { 'b', 'c', 'd', 'e', 'f' };

        public static void Main(string[] args)
        {
            var input = "mmsxrhfx";
            var part1 = false;
            var longestPath = 0;
            var queue = new Queue<Position>();
            queue.Enqueue(new Position
            {
                x = 0,
                y = 0,
                hash = input,
                path = "",
            });
            while (queue.Any())
            {
                var pos = queue.Dequeue();
                if (pos.x == 3 && pos.y == 3)
                {
                    if (!part1)
                    {
                        Console.WriteLine($"part1: {pos.path}");
                        part1 = true;
                    }
                    if (pos.path.Length > longestPath)
                        longestPath = pos.path.Length;
                    continue;
                }

                var udlr = md5sum(pos.hash);
                if (pos.y > 0 && validChars.Contains(udlr[0]))  // UP
                    queue.Enqueue(new Position
                    {
                        x = pos.x,
                        y = pos.y - 1,
                        hash = pos.hash + "U",
                        path = pos.path + "U",
                    });
                if (pos.y < 3 && validChars.Contains(udlr[1]))  // DOWN
                    queue.Enqueue(new Position
                    {
                        x = pos.x,
                        y = pos.y + 1,
                        hash = pos.hash + "D",
                        path = pos.path + "D",
                    });
                if (pos.x > 0 && validChars.Contains(udlr[2]))  // LEFT
                    queue.Enqueue(new Position
                    {
                        x = pos.x - 1,
                        y = pos.y,
                        hash = pos.hash + "L",
                        path = pos.path + "L",
                    });
                if (pos.x < 3 && validChars.Contains(udlr[3]))  // RIGHT
                    queue.Enqueue(new Position
                    {
                        x = pos.x + 1,
                        y = pos.y,
                        hash = pos.hash + "R",
                        path = pos.path + "R",
                    });
            }
            Console.WriteLine($"part2: {longestPath}");
        }
    }
}
