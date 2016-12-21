﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = "abcdefgh".ToCharArray();

            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var inst = line.Split(" ".ToCharArray());
                if (inst[0] == "swap" && inst[1] == "position")
                {
                    var pos1 = int.Parse(inst[2]);
                    var pos2 = int.Parse(inst[5]);
                    var tmp = input[pos1];
                    input[pos1] = input[pos2];
                    input[pos2] = tmp;
                }
                else if (inst[0] == "swap" && inst[1] == "letter")
                {
                    var letter1 = inst[2];
                    var letter2 = inst[5];
                    input = string.Concat(input)
                        .Replace(letter2, "_")
                        .Replace(letter1, letter2)
                        .Replace("_", letter1)
                        .ToCharArray();
                }
                else if (inst[0] == "reverse" && inst[1] == "positions")
                {
                    var fromIdx = int.Parse(inst[2]);
                    var toIdx = int.Parse(inst[4]);
                    var count = toIdx - fromIdx + 1;
                    input = input
                        .Take(fromIdx)
                        .Concat(input.Skip(fromIdx).Take(count).Reverse())
                        .Concat(input.Skip(fromIdx + count))
                        .ToArray();
                }
                else if (inst[0] == "rotate" && inst[1] == "left")
                {
                    var count = int.Parse(inst[2]);
                    input = input.Skip(count).Concat(input.Take(count)).ToArray();
                }
                else if (inst[0] == "rotate" && inst[1] == "right")
                {
                    var count = int.Parse(inst[2]);
                    count = (input.Length + count * -1) % input.Length;
                    input = input.Skip(count).Concat(input.Take(count)).ToArray();
                }
                else if (inst[0] == "move" && inst[1] == "position")
                {
                    var fromIdx = int.Parse(inst[2]);
                    var toIdx = int.Parse(inst[5]);
                    var list = input.ToList();
                    var elem = list[fromIdx];
                    list.RemoveAt(fromIdx);
                    list.Insert(toIdx, elem);
                    input = list.ToArray();
                }
                else if (inst[0] == "rotate" && inst[1] == "based")
                {
                    var elem = inst[6][0];
                    var idx = input.ToList().IndexOf(elem);
                    var steps = idx + 1;
                    if (idx >= 4)
                        steps++;
                    steps = steps % input.Length;
                    var newInput = new List<char>();
                    for (var i = 0; i < input.Length; i++)
                        newInput.Add(input[((i + -1 * steps) + input.Length) % input.Length]);
                    input = newInput.ToArray();
                }
                else
                {
                    Console.WriteLine($"DONT KNOW HOW TO: {line}");
                    return;
                }
            }
            Console.WriteLine($"part1: {string.Concat(input)}");
        }
    }
}
