using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LargestNumber
{
    public class Solution
    {
        public string LargestNumber(int[] nums)
        {
            // List<int> Split(int n)
            // {
            //     if (n == 0) return new List<int> { 0 };

            //     var x = new List<int>();
            //     while (n > 0)
            //     {
            //         x.Add(n % 10);
            //         n /= 10;
            //     }
            //     x.Reverse();
            //     return x;
            // }
            List<int> Split(int n)
            {
                return n.ToString().ToArray().Select(i => int.Parse(i.ToString())).ToList();
            }
            var nlist = nums.ToList().Select(i => Split(i)).ToList();
            // Console.WriteLine($"{string.Join(" | ", nlist.Select(i => string.Join(",", i)))}");
            nlist.Sort((a, b) =>
            {
                var minLen = Math.Min(a.Count, b.Count);
                for (var i = 0; i < minLen; i++)
                {
                    if (a[i] != b[i])
                    {
                        return a[i] - b[i];
                    }
                }
                var x = int.Parse(string.Join("", a.Concat(b)));
                var y = int.Parse(string.Join("", b.Concat(a)));
                return x - y;
            });
            nlist.Reverse();
            // Console.WriteLine($"{string.Join(" | ", nlist.Select(i => string.Join(",", i)))}");
            var s = string.Join("", nlist.Select(i => string.Join("", i))).TrimStart('0');
            return (s == "") ? "0" : s;
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
";
            var lines = input.CleanInput();
            lines = "0179-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}