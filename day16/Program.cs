using System;
using System.Linq;
using System.Text;

namespace ConsoleApplication
{
    public class Program
    {

        public static string dragonCurve(string input)
        {
            var b = string.Concat(input
                .Reverse()
                .Select(e => e == '1' ? '0' : '1'));
            return input + "0" + b;
        }

        public static string checksum(string input, bool first = true)
        {
            if (!first && input.Length % 2 != 0) return input;
            var result = new StringBuilder();
            for (var i = 0; i + 1 < input.Length; i += 2)
                result.Append(input[i] == input[i + 1] ? '1' : '0');
            return checksum(result.ToString(), first = false);
        }

        public static string dragonChecksum(string input, int diskLength)
        {
            while (input.Length < diskLength)
                input = dragonCurve(input);
            input = input.Substring(0, diskLength);
            return checksum(input);
        }

        public static void Main(string[] args)
        {
            var input = "11011110011011101";
            Console.WriteLine($"part1: {dragonChecksum(input, 272)}");
            Console.WriteLine($"part2: {dragonChecksum(input, 35651584)}");
        }
    }
}
