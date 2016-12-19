using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = 3014387;
            Console.WriteLine($"part1: {Part1(input)}");
            Console.WriteLine($"part2: {Part2(input)}");
        }

        public static int Part1(int input)
        {
            var elves = new BitArray(input, true);

            for (var i = 0; ; i = (i + 1) % elves.Length)
            {
                if (!elves[i]) continue;

                for (var j = (i + 1) % elves.Length; ; j = (j + 1) % elves.Length)
                {
                    if (i == j)
                        return i + 1;
                    if (elves[j])
                    {
                        elves[j] = false;
                        break;
                    }
                }
            }
        }

        public static int Part2(int input)
        {
            var list = new LinkedList<int>();
            for (var i = 0; i < input; i++)
                list.AddLast(i);

            var node = list.First;
            var middle = node;
            for (var i = 0; i < list.Count / 2; i++) middle = middle.Next;

            for (var i = 0; i < input - 1; i++)
            {
                var oldMiddle = middle;
                middle = middle.Next ?? list.First;
                list.Remove(oldMiddle);
                if ((input - i) % 2 == 1) middle = middle.Next ?? list.First;
                node = node.Next ?? list.First;
            }

            return node.Value + 1;
        }
    }
}
