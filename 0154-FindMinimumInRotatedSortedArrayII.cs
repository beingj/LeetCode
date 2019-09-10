using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FindMinimumInRotatedSortedArrayII
{
    public class Solution
    {
        public int FindMin(int[] nums)
        {
            var min = nums[0];
            var prev = min;
            for (var i = 1; i < nums.Length; i++)
            {
                if (nums[i] < prev)
                {
                    min = nums[i];
                    break;
                }
                prev = nums[i];
            }
            return min;
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[1,3,5]
1
[2,2,2,0,1]
0
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}