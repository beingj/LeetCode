using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace RemoveDuplicatesFromSortedArray
{
    public class Solution
    {
        public int RemoveDuplicates(int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }
            int last = nums[0];
            int len = nums.Length;
            int i = 1;
            while (true)
            {
                if (i == len)
                {
                    break;
                }
                if (nums[i] == last)
                {
                    for (var j = i + 1; j < len; j++)
                    {
                        nums[j - 1] = nums[j];
                    }
                    len--;
                    continue;
                }
                last = nums[i];
                i++;
            }
            return len;
        }
        public int RemoveDuplicates2(int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }
            int last = nums[0];
            int len = nums.Length;
            int i = 1;
            while (true)
            {
                if (i == len)
                {
                    break;
                }
                if (nums[i] == last)
                {
                    int repeat = 1;
                    for (var j = i + 1; j < len; j++)
                    {
                        if (nums[j] != last)
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
                last = nums[i];
                i++;
            }
            return len;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("RemoveDuplicatesFromSortedArray");
            int[] input;
            int exp, res;
            int[] expModified;

            input = new int[] { 1, 1, 2 };
            exp = 2;
            expModified = new int[] { 1, 2 };
            using (new Timeit())
            {
                // any modification to nums in your function would be known by the caller.
                // using the length returned by your function, it prints the first len elements.
                res = new Solution().RemoveDuplicates(input);
            }
            Assert.Equal(exp, res);
            for (int i = 0; i < res; i++)
            {
                Assert.Equal(expModified[i], input[i]);
            }

            input = new int[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 };
            exp = 5;
            expModified = new int[] { 0, 1, 2, 3, 4 };
            using (new Timeit())
            {
                res = new Solution().RemoveDuplicates(input);
            }
            Assert.Equal(exp, res);
            for (int i = 0; i < res; i++)
            {
                Assert.Equal(expModified[i], input[i]);
            }

            input = new int[] { 1, 1 };
            exp = 1;
            expModified = new int[] { 1 };
            using (new Timeit())
            {
                res = new Solution().RemoveDuplicates(input);
            }
            Assert.Equal(exp, res);
            for (int i = 0; i < res; i++)
            {
                Assert.Equal(expModified[i], input[i]);
            }

            input = new int[] { 1 };
            exp = 1;
            expModified = new int[] { 1 };
            using (new Timeit())
            {
                res = new Solution().RemoveDuplicates(input);
            }
            Assert.Equal(exp, res);
            for (int i = 0; i < res; i++)
            {
                Assert.Equal(expModified[i], input[i]);
            }

            input = new int[] { };
            exp = 0;
            expModified = new int[] { };
            using (new Timeit())
            {
                res = new Solution().RemoveDuplicates(input);
            }
            Assert.Equal(exp, res);
            for (int i = 0; i < res; i++)
            {
                Assert.Equal(expModified[i], input[i]);
            }

            Console.WriteLine("RemoveDuplicatesFromSortedArray solution2");
            input = new int[] { 1, 1, 2 };
            exp = 2;
            expModified = new int[] { 1, 2 };
            using (new Timeit())
            {
                res = new Solution().RemoveDuplicates2(input);
            }
            Assert.Equal(exp, res);
            for (int i = 0; i < res; i++)
            {
                Assert.Equal(expModified[i], input[i]);
            }

            input = new int[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 };
            exp = 5;
            expModified = new int[] { 0, 1, 2, 3, 4 };
            using (new Timeit())
            {
                res = new Solution().RemoveDuplicates2(input);
            }
            Assert.Equal(exp, res);
            for (int i = 0; i < res; i++)
            {
                Assert.Equal(expModified[i], input[i]);
            }

            input = new int[] { 1, 1 };
            exp = 1;
            expModified = new int[] { 1 };
            using (new Timeit())
            {
                res = new Solution().RemoveDuplicates2(input);
            }
            Assert.Equal(exp, res);
            for (int i = 0; i < res; i++)
            {
                Assert.Equal(expModified[i], input[i]);
            }

            input = new int[] { 1 };
            exp = 1;
            expModified = new int[] { 1 };
            using (new Timeit())
            {
                res = new Solution().RemoveDuplicates2(input);
            }
            Assert.Equal(exp, res);
            for (int i = 0; i < res; i++)
            {
                Assert.Equal(expModified[i], input[i]);
            }

            input = new int[] { };
            exp = 0;
            expModified = new int[] { };
            using (new Timeit())
            {
                res = new Solution().RemoveDuplicates2(input);
            }
            Assert.Equal(exp, res);
            for (int i = 0; i < res; i++)
            {
                Assert.Equal(expModified[i], input[i]);
            }
        }
    }
}