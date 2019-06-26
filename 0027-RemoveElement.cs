using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace RemoveElement
{
    public class Solution
    {
        public int RemoveElement(int[] nums, int val)
        {
            if (nums.Length == 0)
            {
                return 0;
            }
            int len = nums.Length;
            int i = 0;
            while (true)
            {
                if (i == len)
                {
                    break;
                }
                if (nums[i] == val)
                {
                    int repeat = 1;
                    for (var j = i + 1; j < len; j++)
                    {
                        if (nums[j] != val)
                        {
                            break;
                        }
                        repeat++;
                    }

                    for (var j = i + repeat; j < len; j++)
                    {
                        nums[j - repeat] = nums[j];
                    }
                    len -= repeat;
                    continue;
                }
                i++;
            }
            return len;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("RemoveElement");
            int[] input;
            int val;
            int exp, res;
            int[] expModified;

            input = new int[] { 3, 2, 2, 3 };
            val = 3;
            exp = 2;
            expModified = new int[] { 2, 2 };
            using (new Timeit())
            {
                res = new Solution().RemoveElement(input, val);
            }
            Assert.Equal(exp, res);
            for (int i = 0; i < res; i++)
            {
                Assert.Equal(expModified[i], input[i]);
            }

            input = new int[] { 0, 1, 2, 2, 3, 0, 4, 2 };
            val = 2;
            exp = 5;
            expModified = new int[] { 0, 1, 3, 0, 4 };
            using (new Timeit())
            {
                res = new Solution().RemoveElement(input, val);
            }
            Assert.Equal(exp, res);
            for (int i = 0; i < res; i++)
            {
                Assert.Equal(expModified[i], input[i]);
            }
        }
    }
}