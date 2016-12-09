using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApplication
{
    public class Program
    {
        public static Dictionary<string, ulong> cache = new Dictionary<string, ulong>();

        public static ulong decompress(string input, bool part2)
        {
            if (!input.Contains("("))
                return (ulong)input.Length;
            ulong output = 0;
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (c != '(')
                    output++;
                else
                {
                    var instance = new StringBuilder();
                    for (var j = i + 1; j < input.Length && input[j] != ')'; j++)
                    {
                        instance.Append(input[j]);
                    }
                    var arr = instance.ToString().Split("x".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    var charcount = int.Parse(arr[0]);
                    var repeatcount = int.Parse(arr[1]);
                    for (var a = 0; a < repeatcount; a++)
                        if (part2)
                        {
                            var subinput = input.Substring(i + instance.Length + 2, charcount);
                            if (!cache.ContainsKey(subinput))
                                cache[subinput] = decompress(subinput, true);
                            output += cache[subinput];
                        }
                        else
                            output += (ulong)input.Substring(i + instance.Length + 2, charcount).Length;
                    i += instance.Length + charcount + 1;
                }
            }
            return output;
        }

        public static void Main(string[] args)
        {
            ulong sum1 = 0;
            ulong sum2 = 0;
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var decomp1 = decompress(line, false);
                var decomp2 = decompress(line, true);
                sum1 += decomp1;
                sum2 += decomp2;
            }
            Console.WriteLine($"part1: {sum1}");
            Console.WriteLine($"part2: {sum2}");
        }
    }
}
