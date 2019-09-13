using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MaximumGap
{
    public class Solution
    {
        public int MaximumGap(int[] nums)
        {
            if (nums.Length < 2)
                return 0;
            var n2 = nums.OrderBy(i => i).ToArray();
            var maxGap = 0;
            for (var i = 1; i < n2.Length; i++)
            {
                var gap = n2[i] - n2[i - 1];
                maxGap = Math.Max(maxGap, gap);
            }
            return maxGap;
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[3,6,9,1]
3
[10]
0
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}