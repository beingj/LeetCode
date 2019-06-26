using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace FirstMissingPositive
{
    public class Solution
    {
        public int FirstMissingPositive(int[] nums)
        {
            if (nums.Length < 1)
            {
                return 1;
            }
            int max = nums[0];
            var missing = new Dictionary<int, bool>();
            missing[1] = true;
            foreach (var n in nums)
            {
                // Console.WriteLine($"{n}=>");
                if (n <= 0)
                {
                    continue;
                }
                if (n > max)
                {
                    max = n;
                }
                // Console.WriteLine($"not missing");
                missing[n] = false;
                var xlst = new int[] { n - 1, n + 1 };
                foreach (var x in xlst)
                {
                    if (x <= 0)
                        continue;
                    if (!missing.ContainsKey(x))
                    {
                        missing[x] = true;
                        // Console.WriteLine($"{x} missing");
                    }
                }
            }
            var res = missing.Where(i => i.Value == true).Select(i => i.Key).ToList();
            res.Sort();
            if (res.Count == 0)
            {
                return max + 1;
            }
            return res.First();
        }
    }

    public class Test
    {
        static void Verify(int[] input, int exp)
        {
            Console.WriteLine($"{string.Join(',', input)}");
            int res;
            using (new Timeit())
            {
                res = new Solution().FirstMissingPositive(input);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("FirstMissingPositive");
            int[] input;
            int exp;

            input = new int[] { 1, 2, 0 };
            exp = 3;
            Verify(input, exp);

            input = new int[] { 3, 4, -1, 1 };
            exp = 2;
            Verify(input, exp);

            input = new int[] { 7, 8, 9, 11, 12 };
            exp = 1;
            Verify(input, exp);

            input = new int[] { 2147483647 };
            exp = 1;
            Verify(input, exp);

            input = new int[] { 4, 3, 4, 1, 1, 4, 1, 4 };
            exp = 2;
            Verify(input, exp);

            input = new int[] { 2, 1 };
            exp = 3;
            Verify(input, exp);
        }
    }
}