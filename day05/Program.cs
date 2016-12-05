using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static string md5sum(MD5 md5, string input)
        {
            var bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
            // Check for 5 zeroes in the beginning of the bytes for more speed.
            if (bytes[0] != 0x0000 || bytes[1] != 0x0000 || bytes[2] > 0x00ff) return string.Empty;
            // Otherwise it's a valid MD5 string.
            return BitConverter.ToString(bytes).Replace("-", "");
        }
        public static void Main(string[] args)
        {
            var input = "wtnhxymk";
            var md5 = MD5.Create();
            var password = string.Empty;
            var password2 = "        ".ToCharArray();
            var password2chars = 0;
            for (uint i = 0; password.Length < 8 || password2chars < 8; i++)
            {
                var hash = md5sum(md5, input + i.ToString());
                if (hash.StartsWith("00000"))
                {
                    var val1 = hash[5];
                    var val2 = hash[6];

                    // part1
                    if (password.Length < 8) password += val1;

                    // part2
                    var val1Int = int.Parse(val1.ToString(), NumberStyles.HexNumber);
                    if (val1Int > 7 || password2[val1Int] != ' ') continue;
                    password2[val1Int] = val2;
                    password2chars = password2.Where(e => e != ' ').Count();
                }
            }
            Console.WriteLine($"part1: {password.ToLower()}");
            Console.WriteLine($"part2: {new String(password2).ToLower()}");
        }
    }
}
