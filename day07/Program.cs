using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApplication
{
    public class Program
    {
        public static bool checkAbba(string part)
        {
            for (var i = 0; i < part.Length - 3; i++)
                if (part[i] == part[i + 3] && part[i + 1] == part[i + 2] && part[i] != part[i + 1])
                    return true;
            return false;
        }

        public static List<string> getAbas(string part)
        {
            var res = new List<string>();
            return res;
        }

        public static string[] splitIPv7(string ipv7)
        {
            return ipv7.Split("[]".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        public static bool checkIPv7ForAbba(string ipv7)
        {
            var parts = splitIPv7(ipv7);

            // Uneven parts are within the hypernet sequence.
            for (var i = 1; i < parts.Length; i += 2)
                if (checkAbba(parts[i]))
                    return false;

            // At least one even part must be true.
            for (var i = 0; i < parts.Length; i += 2)
                if (checkAbba(parts[i]))
                    return true;

            // If nothing is abba, the IP is invalid.
            return false;
        }

        public static bool checkIPv7ForAba(string ipv7)
        {
            var parts = splitIPv7(ipv7);

            var babs = new HashSet<string>();
            for (var j = 0; j < parts.Length; j += 2)
            {
                var part = parts[j];
                for (var i = 0; i < part.Length - 2; i++)
                    if (part[i] == part[i + 2] && part[i] != part[i + 1])
                        babs.Add(string.Concat(new char[] {
                            part[i + 1],
                            part[i],
                            part[i + 1]}));
            }
            foreach (var bab in babs)
                for (var j = 1; j < parts.Length; j += 2)
                    if (parts[j].Contains(bab))
                        return true;
            return false;
        }

        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var part1 = lines
                .Where(e => checkIPv7ForAbba(e))
                .Count();
            Console.WriteLine($"part1: {part1}");

            var part2 = lines
                .Where(e => checkIPv7ForAba(e))
                .Count();
            Console.WriteLine($"part2: {part2}");
        }
    }
}
