using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MajorityElement
{
    public class Solution
    {
        public int MajorityElement(int[] nums)
        {
            var cnt = new Dictionary<int, int>();
            var halfTotal = nums.Length / 2;
            var x = nums[0];
            foreach (var n in nums)
            {
                if (!cnt.ContainsKey(n))
                {
                    cnt[n] = 1;
                }
                else
                {
                    cnt[n]++;
                }
                if (cnt[n] > halfTotal)
                {
                    x = n;
                    break;
                }
            }
            return x;
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[3,2,3]
3
[2,2,1,1,1,2,2]
2
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}