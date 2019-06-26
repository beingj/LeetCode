using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace NextPermutation
{
    public class Solution
    {
        public void NextPermutation(int[] nums)
        {
            if (nums.Length < 2)
                return;
            for (var i = nums.Length - 1; i > 0; i--)
            {
                int right = nums[i];
                int left = nums[i - 1];
                if (left < right)
                {
                    SwapAndSort(nums, i - 1);
                    return;
                }
            }

            // else, already sorted in descending order,
            // so sorted it in ascending order: swap each pair from outer to inner
            int half = nums.Length / 2;
            int maxIdx = nums.Length - 1;
            for (var i = 0; i < half; i++)
            {
                int tmp = nums[i];
                nums[i] = nums[maxIdx - i];
                nums[maxIdx - i] = tmp;
            }
        }
        static void SwapAndSort(int[] nums, int startIdx)
        {
            int first = nums[startIdx];
            int nextFirstIdx = startIdx + 1;
            int nextFirst = nums[nextFirstIdx];
            for (var i = startIdx + 2; i < nums.Length; i++)
            {
                if ((nums[i] > first) && (nums[i] < nextFirst))
                {
                    nextFirstIdx = i;
                    nextFirst = nums[i];
                }
            }

            nums[startIdx] = nums[nextFirstIdx];
            nums[nextFirstIdx] = first;
            Sort(nums, startIdx + 1);
        }
        static void Sort(int[] nums, int startIdx)
        {
            // TODO: quicksort?
            for (var i = startIdx; i < nums.Length; i++)
            {
                int v = nums[i];
                for (var j = i + 1; j < nums.Length; j++)
                {
                    if (nums[j] < v)
                    {
                        nums[i] = nums[j];
                        nums[j] = v;
                        v = nums[i];
                    }
                }
            }
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("NextPermutation");
            int[] input;
            int[] exp;

            input = new int[] { 1, 2, 3 };
            exp = new int[] { 1, 3, 2 };
            using (new Timeit())
            {
                new Solution().NextPermutation(input);
            }
            Assert.Equal(string.Join(',', exp), string.Join(',', input));

            // 3,2,1 → 1,2,3
            input = new int[] { 3, 2, 1 };
            exp = new int[] { 1, 2, 3 };
            using (new Timeit())
            {
                new Solution().NextPermutation(input);
            }
            Assert.Equal(string.Join(',', exp), string.Join(',', input));

            // 1,1,5 → 1,5,1
            input = new int[] { 1, 1, 5 };
            exp = new int[] { 1, 5, 1 };
            using (new Timeit())
            {
                new Solution().NextPermutation(input);
            }
            Assert.Equal(string.Join(',', exp), string.Join(',', input));

            input = new int[] { 1, 3, 2 };
            // [3,1,2]=>wrong
            exp = new int[] { 2, 1, 3 };
            using (new Timeit())
            {
                new Solution().NextPermutation(input);
            }
            Assert.Equal(string.Join(',', exp), string.Join(',', input));

            // [5,4,7,5,3,2]
            // wrong [5,7,4,5,3,2]
            // exp [5,5,2,3,4,7]
            input = new int[] { 5, 4, 7, 5, 3, 2 };
            exp = new int[] { 5, 5, 2, 3, 4, 7 };
            using (new Timeit())
            {
                new Solution().NextPermutation(input);
            }
            Assert.Equal(string.Join(',', exp), string.Join(',', input));
        }
    }
}