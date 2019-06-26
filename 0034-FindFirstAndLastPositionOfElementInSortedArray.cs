using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace FindFirstAndLastPositionOfElementInSortedArray
{
    public class Solution
    {
        public int[] SearchRange(int[] nums, int target)
        {
            var idx = BinSearch(nums, target, 0, nums.Length - 1);
            if (idx == -1)
            {
                return new int[] { -1, -1 };
            }
            int idx1 = idx;
            while (idx1 >= 0)
            {
                if (nums[idx1] < target)
                {
                    break;
                }
                idx1--;
            }
            idx1++;
            int idx2 = idx;
            while (idx2 < nums.Length)
            {
                if (nums[idx2] > target)
                {
                    break;
                }
                idx2++;
            }
            idx2--;
            return new int[] { idx1, idx2 };
        }
        static int BinSearch(int[] nums, int target, int idxStart, int idxEnd)
        {
            while (idxStart <= idxEnd)
            {
                // Console.Writene($"search {target} : {idxStart} {idxEnd}");
                // int middle = (idxStart + idxEnd) / 2;
                // 问题会出现在当 idxStart + idxEnd 的结果大于表达式结果类型所能表示的最大值时，
                // 产生溢出后再/2是不会产生正确结果的，而idxStart + ((idxEnd - idxStart) / 2) 不会溢出
                int middle = idxStart + ((idxEnd - idxStart) / 2);
                if (target == nums[middle])
                {
                    return middle;
                }
                else if (target > nums[middle])
                {
                    idxStart = middle + 1;
                }
                else if (target < nums[middle])
                {
                    idxEnd = middle - 1;
                }
            }
            return -1;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("FindFirstAndLastPositionOfElementInSortedArray");
            int[] nums;
            int target;
            int[] exp, res;

            nums = new int[] { 5, 7, 7, 8, 8, 10 };
            target = 8;
            exp = new int[] { 3, 4 };
            using (new Timeit())
            {
                res = new Solution().SearchRange(nums, target);
            }
            Assert.Equal(string.Join(',', exp), string.Join(',', res));

            nums = new int[] { 5, 7, 7, 8, 8, 10 };
            target = 6;
            exp = new int[] { -1, -1 };
            using (new Timeit())
            {
                res = new Solution().SearchRange(nums, target);
            }
            Assert.Equal(string.Join(',', exp), string.Join(',', res));

            nums = new int[] { 1 };
            target = 1;
            exp = new int[] { 0, 0 };
            using (new Timeit())
            {
                res = new Solution().SearchRange(nums, target);
            }
            Assert.Equal(string.Join(',', exp), string.Join(',', res));
        }
    }
}