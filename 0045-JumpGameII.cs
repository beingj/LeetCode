using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace JumpGameII
{
    public class Solution
    {
        int minJumps = int.MaxValue;
        public int Jump(int[] nums)
        {
            if (nums.Length == 1)
                return 0;

            // JumpBackward(nums, nums.Length - 1, 0);
            // return minJumps;

            // find jump point from last one to first
            // the previous jump point must be the most left one meet "nums[idx] >= distance" to next jump point
            var idx = nums.Length - 1;
            var steps = 0;
            while (true)
            {
                for (var i = 0; i < idx; i++)
                {
                    var x = nums[i];
                    var dist = idx - i;
                    if (x >= dist)
                    {
                        steps++;
                        if (i == 0)
                        {
                            return steps;
                        }
                        idx = i;
                        break;
                    }
                }
            }
        }
        public void JumpBackward(int[] nums, int idx, int soFar)
        {
            for (var i = 0; i < idx; i++)
            {
                var x = nums[i];
                var dist = idx - i;
                if (x >= dist)
                {
                    if (i == 0)
                    {
                        minJumps = soFar + 1;
                        return;
                    }
                    JumpBackward(nums, i, soFar + 1);
                    break;
                }
            }
        }
    }
    public class Test
    {
        static void Verify(int[] nums, int exp)
        {
            Console.WriteLine($"{string.Join(',', nums)}");
            int res;
            using (new Timeit())
            {
                res = new Solution().Jump(nums);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("JumpGameII");
            int[] nums;
            int exp;

            nums = new int[] { 1, 2 };
            exp = 1;
            Verify(nums, exp);

            nums = new int[] { 2, 3, 1, 1, 4 };
            exp = 2;
            Verify(nums, exp);

            nums = new int[] { 0 };
            exp = 0;
            Verify(nums, exp);

            nums = new int[] { 1 };
            exp = 0;
            Verify(nums, exp);

            nums = new int[] { 2, 9, 6, 5, 7, 0, 7, 2, 7, 9, 3, 2, 2, 5, 7, 8, 1, 6, 6, 6, 3, 5, 2, 2, 6, 3 };
            exp = 5;
            Verify(nums, exp);

            nums = new int[] { 8, 2, 4, 4, 4, 9, 5, 2, 5, 8, 8, 0, 8, 6, 9, 1, 1, 6, 3, 5, 1, 2, 6, 6, 0, 4, 8, 6, 0, 3, 2, 8, 7, 6, 5, 1, 7, 0, 3, 4, 8, 3, 5, 9, 0, 4, 0, 1, 0, 5, 9, 2, 0, 7, 0, 2, 1, 0, 8, 2, 5, 1, 2, 3, 9, 7, 4, 7, 0, 0, 1, 8, 5, 6, 7, 5, 1, 9, 9, 3, 5, 0, 7, 5 };
            exp = 13;
            Verify(nums, exp);

        }
    }
}