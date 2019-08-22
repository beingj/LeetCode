using System;
using Xunit;
using Util;
using Node = Util.GraphNode;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SingleNumber
{
    public class Solution
    {
        public int SingleNumber(int[] nums)
        {
            var dict = new Dictionary<int, bool>();
            for (var i = 0; i < nums.Length; i++)
            {
                if (!dict.ContainsKey(nums[i]))
                    dict[nums[i]] = true;
                else
                    dict.Remove(nums[i]);
                // dict[nums[i]] = false;
            }
            return dict.Where(i => i.Value == true).First().Key;
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[2,2,1]
1
[4,1,2,1,2]
4
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}