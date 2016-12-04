using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApplication
{
    public class Program
    {
        public static Dictionary<char, int> buildOccurenceMap(string roomname)
        {
            var map = new Dictionary<char, int>();
            foreach (var c in roomname)
            {
                if (c == '-') continue;
                if (map.ContainsKey(c)) map[c]++;
                else map[c] = 1;
            }
            return map;
        }

        public static bool checkRoom(Dictionary<char, int> map, string checksum)
        {
            foreach (var e in map.OrderBy(e => e.Key).OrderByDescending(e => e.Value))
            {
                if (checksum.Length == 0) return true;
                if (checksum[0] == e.Key) checksum = checksum.Remove(0, 1);
                else return false;
            }
            return checksum.Length == 0;
        }

        public static string caesarCipher(string roomname, int roomvalue)
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            var iterations = roomvalue % alphabet.Length;
            var result = "";
            foreach (var c in roomname)
            {
                if (c == '-') result += " ";
                result += alphabet[(alphabet.IndexOf(c) + iterations) % alphabet.Length];
            }
            return result;
        }

        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var sum = 0;
            foreach (var line in lines)
            {
                var res = Regex.Match(line, @"(.*?)-(\d+)\[(.*)\]");
                var roomname = res.Groups[1].Value;
                var value = int.Parse(res.Groups[2].Value);
                var checksum = res.Groups[3].Value;
                var map = buildOccurenceMap(roomname);
                if (checkRoom(map, checksum))
                {
                    sum += value;
                    var decodedRoomname = caesarCipher(roomname, value);
                    if (decodedRoomname.Contains("north")) Console.WriteLine($"part2 roomname: {decodedRoomname}, value: {value}");
                }
            }
            Console.WriteLine($"part1: {sum}");
        }
    }
}
