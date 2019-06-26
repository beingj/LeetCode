using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace JumpGame
{
    public class Solution
    {
        public bool CanJump(int[] nums)
        {
            if (nums.Length == 1)
                return true;

            // find jump point from last one to first
            // the previous jump point must be the most left one meet "nums[idx] >= distance" to next jump point
            var idx = nums.Length - 1;
            var steps = 0;
            bool canJump = false;
            while (true)
            {
                if (idx <= 0)
                {
                    break;
                }
                canJump = false;
                for (var i = 0; i < idx; i++)
                {
                    var x = nums[i];
                    var dist = idx - i;
                    if (x >= dist)
                    {
                        canJump = true;
                        steps++;
                        if (i == 0)
                        {
                            return canJump;
                        }
                        idx = i;
                        break;
                    }
                }
                if (!canJump)
                {
                    return false;
                }
            }
            return canJump;
        }
    }
    public class Test
    {
        static void Verify(int[] nums, bool exp)
        {
            Console.WriteLine($"{string.Join(',', nums)}");
            bool res;
            using (new Timeit())
            {
                res = new Solution().CanJump(nums);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("JumpGame");
            int[] nums;
            bool exp;

            nums = new int[] { 2, 3, 1, 1, 4 };
            exp = true;
            Verify(nums, exp);

            nums = new int[] { 3, 2, 1, 0, 4 };
            exp = false;
            Verify(nums, exp);

            nums = new int[] { 0 };
            exp = true;
            Verify(nums, exp);

            nums = new int[] { 1 };
            exp = true;
            Verify(nums, exp);

        }
    }
}