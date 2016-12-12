using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static int evalValueOrMemory(string value, Dictionary<string, int> memory)
        {
            if (value.Any() && value.All(char.IsDigit))
                return int.Parse(value);
            else
                return memory[value];
        }

        public static void execute(string[] instructions, Dictionary<string, int> memory)
        {
            for (var i = 0; i < instructions.Length; i++)
            {
                var instruction = instructions[i].Split(" ".ToCharArray());
                switch (instruction[0])
                {
                    case "cpy":
                        memory[instruction[2]] = evalValueOrMemory(instruction[1], memory);
                        break;
                    case "inc":
                    case "dec":
                        memory[instruction[1]] += instruction[0] == "inc" ? 1 : -1;
                        break;
                    case "jnz":
                        if (evalValueOrMemory(instruction[1], memory) != 0)
                            i += int.Parse(instruction[2]) - 1;
                        break;
                }
            }
        }

        public static void Main(string[] args)
        {
            var instructions = File.ReadAllLines("input.txt");

            var memory = new Dictionary<string, int>
            {
                {"a", 0},
                {"b", 0},
                {"c", 0},
                {"d", 0},
            };

            execute(instructions, memory);
            var part1 = memory["a"];
            Console.WriteLine($"part1: {part1}");

            memory = new Dictionary<string, int>
            {
                {"a", 0},
                {"b", 0},
                {"c", 1},
                {"d", 0},
            };

            execute(instructions, memory);
            var part2 = memory["a"];
            Console.WriteLine($"part2: {part2}");
        }
    }
}
