using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FindMinimumInRotatedSortedArray
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
[3,4,5,1,2]
1
[4,5,6,7,0,1,2]
0
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}