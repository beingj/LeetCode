using System;
using Util;

namespace TwoSum
{
    public class Solution
    {
        public int[] TwoSum(int[] nums, int target)
        {
            int len = nums.Length;
            for (var i = 0; i < len - 1; i++)
            {
                for (var j = i + 1; j < len; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        return new int[] { i,j };
                    }
                }
            }
            throw new ArgumentException();
        }

    }
    public class Test
    {
        static public void Run()
        {
            var nums = new int[] { 2, 7, 11 };
            var target = 9;
            var res = new Solution().TwoSum(nums, target).P();
            Console.WriteLine($"TwoSum: ({nums.P()}), {target}\nExp: (0, 1)\nAct: {res}\n");
        }
    }
}