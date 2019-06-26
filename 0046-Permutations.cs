using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace Permutations
{
    public class Solution
    {
        public IList<IList<int>> Permute(int[] nums)
        {
            var res = new List<IList<int>>();
            var x= Permutation(nums, 0);
            foreach(var i in x)
            {
                res.Add(i);
            }
            return res;
        }
        public static List<List<int>> Permutation(int[] nums, int startIdx)
        {
            if (startIdx == nums.Length - 1)
            {
                return new List<List<int>> { new List<int>(nums) };
            }
            var res = new List<List<int>>();
            for (var i = startIdx; i < nums.Length; i++)
            {
                if (IsDuplicated(nums, startIdx, i))
                {
                    continue;
                }
                SwapInts(nums, startIdx, i);
                int[] nums2 = new int[nums.Length];
                nums.CopyTo(nums2, 0);

                var x = Permutation(nums2, startIdx + 1);
                res.AddRange(x);
                SwapInts(nums, startIdx, i); // swap back to original nums
            }
            return res;
        }
        public static void SwapInts(int[] nums, int a, int b)
        {
            int c = nums[a];
            nums[a] = nums[b];
            nums[b] = c;
        }
        static bool IsDuplicated(int[] nums, int startIdx, int endIdx)
        {
            int c = nums[endIdx];
            for (var i = startIdx; i < endIdx; i++)
            {
                if (nums[i] == c)
                    return true;
            }
            return false;
        }
    }
    public class Test
    {
        static void Verify(int[] nums, IList<IList<int>> exp)
        {
            Console.WriteLine($"{string.Join(',', nums)}");
            IList<IList<int>> res;
            using (new Timeit())
            {
                res = new Solution().Permute(nums);
            }
            var exp2 = exp.Select(x => string.Join(',', x)).ToList();
            var res2 = res.Select(x => string.Join(',', x)).ToList();
            exp2.Sort();
            res2.Sort();
            // Console.WriteLine($"{string.Join(" | ", res2)}");
            Assert.Equal(exp2, res2);
        }
        static public void Run()
        {
            Console.WriteLine("Permutations");
            int[] nums;
            List<IList<int>> exp;

            nums = new int[] { 1, 2, 3 };
            exp = new List<IList<int>>{
                new List<int>{1,2,3},
                new List<int>{1,3,2},
                new List<int>{2,1,3},
                new List<int>{2,3,1},
                new List<int>{3,1,2},
                new List<int>{3,2,1},
            };
            Verify(nums, exp);

        }
    }
}