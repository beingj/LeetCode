using System;
using Xunit;
using Util;
using Node = Util.GraphNode;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SingleNumberII
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
                    // dict.Remove(nums[i]);
                    dict[nums[i]] = false;
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
[2,2,3,2]
3
[0,1,0,1,0,1,99]
99
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}