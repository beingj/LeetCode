using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace SearchInRotatedSortedArray
{
    public class Solution
    {
        public int Search(int[] nums, int target)
        {
            if (nums.Length < 1)
                return -1;
            if (target == nums[0])
                return 0;
            if (target == nums[nums.Length - 1])
                return nums.Length - 1;

            var maxIdx = FindMaxIdx(nums, 0, nums.Length - 1);
            // Console.WriteLine($"max: {maxIdx}=>{nums[maxIdx]}");
            if (target > nums[maxIdx])
                return -1;
            if (target == nums[maxIdx])
                return maxIdx;
            if (target < nums[0])
            {
                return SearchBetween(nums, target, maxIdx + 1, nums.Length - 1);
            }
            else
            {
                return SearchBetween(nums, target, 0, maxIdx);
            }
        }
        static int FindMaxIdx(int[] nums, int idxStart, int idxEnd)
        {
            while (idxStart <= idxEnd)
            {
                // Console.WriteLine($"{idxStart}->{idxEnd}");
                if ((idxEnd - idxStart) == 1)
                    return idxStart;
                int middle = (idxStart + idxEnd) / 2;
                if (nums[idxStart] < nums[middle])
                {
                    idxStart = middle;
                }
                else if (nums[idxStart] > nums[middle])
                {
                    idxEnd = middle;
                }
                else if (nums[idxStart] == nums[middle])
                {
                    break;
                }
            }
            return idxStart;
        }
        static int SearchBetween(int[] nums, int target, int idxStart, int idxEnd)
        {
            while (idxStart <= idxEnd)
            {
                // Console.Writene($"search {target} : {idxStart} {idxEnd}");
                int middle = (idxStart + idxEnd) / 2;
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
            Console.WriteLine("SearchInRotatedSortedArray");
            int[] nums;
            int target;
            int exp, res;

            nums = new int[] { 4, 5, 6, 7, 0, 1, 2 };
            target = 0;
            exp = 4;
            using (new Timeit())
            {
                res = new Solution().Search(nums, target);
            }
            Assert.Equal(exp, res);

            nums = new int[] { 4, 5, 6, 7, 0, 1, 2 };
            target = 3;
            exp = -1;
            using (new Timeit())
            {
                res = new Solution().Search(nums, target);
            }
            Assert.Equal(exp, res);

            nums = new int[] { 4, 5, 5, 6, 7, 0, 1, 2 };
            target = 0;
            exp = 5;
            using (new Timeit())
            {
                res = new Solution().Search(nums, target);
            }
            Assert.Equal(exp, res);

            nums = new int[] { 4, 5, 6, 7, 7, 0, 1, 2 };
            target = 0;
            exp = 5;
            using (new Timeit())
            {
                res = new Solution().Search(nums, target);
            }
            Assert.Equal(exp, res);

        }
    }
}