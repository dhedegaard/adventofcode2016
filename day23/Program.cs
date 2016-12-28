using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication
{
    public class Program
    {
        public static int evalValueOrMemory(string value, Dictionary<string, int> memory)
        {
            int result;
            if (int.TryParse(value, out result))
                return result;
            else
                return memory[value];
        }

        public static void execute(string[] instructions, Dictionary<string, int> memory)
        {
            for (var i = 0; i >= 0 && i < instructions.Length; i++)
            {
                var instruction = instructions[i].Split(" ".ToCharArray());
                switch (instruction[0])
                {
                    case "cpy":
                        memory[instruction[2]] = evalValueOrMemory(instruction[1], memory);
                        break;
                    case "inc":
                    case "dec":
                        if (instruction.Length > 2) continue;
                        memory[instruction[1]] += instruction[0] == "inc" ? 1 : -1;
                        break;
                    case "jnz":
                        if (evalValueOrMemory(instruction[1], memory) != 0)
                        {
                            var steps = evalValueOrMemory(instruction[2], memory);
                            if (steps != 0)
                                i += steps - 1;
                        }
                        break;
                    case "tgl":
                        var transIdx = i + evalValueOrMemory(instruction[1], memory);
                        if (transIdx < 0 || transIdx >= instructions.Length) continue;
                        var transInst = instructions[transIdx].Split(" ".ToCharArray());
                        if (transIdx == i) throw new Exception("YAOI");
                        if (transIdx < 0 || transIdx >= instructions.Length) throw new Exception("YAOI!");
                        switch (transInst.Length)
                        {
                            case 2:
                                switch (transInst[0])
                                {
                                    case "inc":
                                        transInst[0] = "dec";
                                        break;
                                    default:
                                        transInst[0] = "inc";
                                        break;
                                }
                                break;
                            case 3:
                                switch (transInst[0])
                                {
                                    case "jnz":
                                        transInst[0] = "cpy";
                                        break;
                                    default:
                                        transInst[0] = "jnz";
                                        break;
                                }
                                break;
                            default:
                                throw new Exception(transInst.Length.ToString());
                        }
                        instructions[transIdx] = string.Join(" ", transInst);
                        break;
                    default:
                        Console.WriteLine($"Don't know how to: {instruction[0]}");
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public static void Main(string[] args)
        {
            var instructions = File.ReadAllLines("input.txt");
            var memory = new Dictionary<string, int>
            {
                {"a", 7},
                {"b", 0},
                {"c", 0},
                {"d", 0},
            };
            execute(instructions, memory);
            var part1 = memory["a"];
            Console.WriteLine($"part1: {part1}");

            // Make sure to reread instructions from the input again.
            instructions = File.ReadAllLines("input.txt");
            memory = new Dictionary<string, int>
            {
                {"a", 12},
                {"b", 0},
                {"c", 0},
                {"d", 0},
            };
            // Part2 is like really slow, should probably look into some optimization.
            execute(instructions, memory);
            var part2 = memory["a"];
            Console.WriteLine($"part2: {part2}");
        }
    }
}
