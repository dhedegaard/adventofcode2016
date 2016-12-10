using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication
{
    public class Bot
    {
        public List<int> values = new List<int>();
        public string lowOutType, highOutType;
        public int lowOutNum, highOutNum;
    }

    public class Program
    {
        public static Dictionary<int, int> output = new Dictionary<int, int>();
        public static Dictionary<int, Bot> bots = new Dictionary<int, Bot>();

        public static void Main(string[] args)
        {
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var instruction = line.Split(" ".ToCharArray());
                switch (instruction[0])
                {
                    case "value":
                        var value = int.Parse(instruction[1]);
                        var botnumber = int.Parse(instruction[5]);
                        if (!bots.ContainsKey(botnumber)) bots[botnumber] = new Bot();
                        bots[botnumber].values.Add(value);
                        break;
                    case "bot":
                        botnumber = int.Parse(instruction[1]);
                        if (!bots.ContainsKey(botnumber)) bots[botnumber] = new Bot();
                        var bot = bots[botnumber];
                        bot.lowOutType = instruction[5];
                        bot.lowOutNum = int.Parse(instruction[6]);
                        bot.highOutType = instruction[10];
                        bot.highOutNum = int.Parse(instruction[11]);
                        break;
                }
            }

            while (bots.Any(e => e.Value.values.Count == 2))
            {
                var elem = bots.Where(e => e.Value.values.Count == 2).First();
                var bot = elem.Value;
                bot.values.Sort();

                if (bot.values[0] == 17 && bot.values[1] == 61)
                    Console.WriteLine($"part1: {elem.Key}");

                effectuateBotValues(bot.lowOutType, bot.lowOutNum, bot.values[0]);
                effectuateBotValues(bot.highOutType, bot.highOutNum, bot.values[1]);

                bot.values.Clear();
                bots.Remove(bots.First(e => e.Value == bot).Key);
            }

            var part2 = output
                    .Where(e => e.Key <= 2)
                    .Select(e => e.Value)
                    .Aggregate((acc, val) => acc * val);
            Console.WriteLine($"part2: {part2}");
        }

        public static void effectuateBotValues(string outType, int outNum, int value)
        {
            switch (outType)
            {
                case "bot":
                    bots[outNum].values.Add(value);
                    break;
                case "output":
                    if (!output.ContainsKey(outNum)) output[outNum] = 0;
                    output[outNum] += value;
                    break;
                default:
                    throw new Exception("WORD UP");
            }
        }
    }
}
