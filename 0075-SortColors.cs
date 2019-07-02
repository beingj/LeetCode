using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;

namespace SortColors
{
    public class Solution
    {
        public void SortColors(int[] nums)
        {
            int r = 0, g = 0, b = 0;
            foreach (var n in nums)
            {
                switch (n)
                {
                    case 0:
                        r++;
                        break;
                    case 1:
                        g++;
                        break;
                    case 2:
                        b++;
                        break;
                }
            }

            int offset = 0;
            for (var i = 0; i < r; i++)
            {
                nums[offset + i] = 0;
            }
            offset += r;
            for (var i = 0; i < g; i++)
            {
                nums[offset + i] = 1;
            }
            offset += g;
            for (var i = 0; i < b; i++)
            {
                nums[offset + i] = 2;
            }
        }
    }
    public class Test
    {
        static void Verify(int[] nums, int[] exp)
        {
            Console.WriteLine($"{nums.Int1dToJson()} => {exp.Int1dToJson()}");
            using (new Timeit())
            {
                new Solution().SortColors(nums);
            }
            Assert.Equal(exp, nums);
        }
        static public void Run()
        {
            Console.WriteLine(typeof(Solution).Namespace);

            var input = @"
[2,0,2,1,1,0]
[0,0,1,1,2,2]
";
            var lines = input.CleanInput();
            int[] nums;
            int[] exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                nums = lines[idx++].JsonToInt1d();
                exp = lines[idx++].JsonToInt1d();
                Verify(nums, exp);
            }
        }
    }
}