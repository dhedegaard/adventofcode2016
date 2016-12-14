using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication
{
    public class Program
    {
        public static MD5 md5 = MD5.Create();
        public static Regex match3 = new Regex(@"(\w)\1\1", RegexOptions.Compiled);
        public static Dictionary<string, string> md5cache = new Dictionary<string, string>();
        public static Dictionary<string, string> stretchedMd5cache = new Dictionary<string, string>();

        public static string md5sum(string input, int stretch)
        {
            var cache = stretch > 0 ? stretchedMd5cache : md5cache;
            if (!cache.ContainsKey(input))
            {
                var temp = input;

                foreach (var i in Enumerable.Range(0, stretch + 1))
                {
                    temp = string.Join("",
                        from b in md5.ComputeHash(Encoding.ASCII.GetBytes(temp))
                        select b.ToString("x2"));
                }

                cache[input] = temp;
            }
            return cache[input];
        }

        public static void Main(string[] args)
        {
            var input = "cuanljph";
            var hits1 = 0;
            var hits2 = 0;
            var part1 = 0;
            var part2 = 0;

            for (var i = 1; hits1 < 64 || hits2 < 64; i++)
            {
                var in_ = input + i.ToString();

                var res1 = match3.Match(md5sum(in_, 0));
                if (hits1 < 64 && res1.Success)
                {
                    var target = res1.Groups[1].ToString();
                    while (target.Length < 5) target += target[0];

                    for (var j = i + 1; j < i + 1000; j++)
                    {
                        var in2 = input + j.ToString();
                        var out2 = md5sum(in2, 0);
                        if (out2.Contains(target))
                        {
                            Console.WriteLine($"part1({++hits1}): {out2} - {i},{j}");
                            part1 = i;
                            break;
                        }
                    }
                }
                var res2 = match3.Match(md5sum(in_, 2016));
                if (hits2 < 64 && res2.Success)
                {
                    var target = res2.Groups[1].ToString();
                    while (target.Length < 5) target += target[0];

                    for (var j = i + 1; j < i + 1000; j++)
                    {
                        var in2 = input + j.ToString();
                        var out2 = md5sum(in2, 2016);
                        if (out2.Contains(target))
                        {
                            Console.WriteLine($"part2({++hits2}): {out2} - {i},{j}");
                            part2 = i;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine($"part1: {part1}");
            Console.WriteLine($"part2: {part2}");
        }
    }
}
