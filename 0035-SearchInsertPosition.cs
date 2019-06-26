using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace SearchInsertPosition
{
    public class Solution
    {
        public int SearchInsert(int[] nums, int target)
        {
            if (target < nums[0])
                return 0;
            if (target > nums[nums.Length - 1])
                return nums.Length;
            return BinSearch2(nums, target, 0, nums.Length - 1);
        }

        static int BinSearch2(int[] nums, int target, int idxStart, int idxEnd)
        {
            while (idxStart <= idxEnd)
            {
                // int middle = (idxStart + idxEnd) / 2;
                // idxStart + idxEnd may overflow
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
            // Console.WriteLine($"end {idxStart},{idxEnd}");
            // idxStart > idxEnd and idxStart==idxEnd+1 here
            return idxStart;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("SearchInsertPosition");
            int[] nums;
            int target;
            int exp, res;

            nums = new int[] { 1, 3, 5, 6 };
            target = 5;
            exp = 2;
            using (new Timeit())
            {
                res = new Solution().SearchInsert(nums, target);
            }
            Assert.Equal(exp, res);

            nums = new int[] { 1, 3, 5, 6 };
            target = 2;
            exp = 1;
            using (new Timeit())
            {
                res = new Solution().SearchInsert(nums, target);
            }
            Assert.Equal(exp, res);

            nums = new int[] { 1, 3, 5, 6 };
            target = 7;
            exp = 4;
            using (new Timeit())
            {
                res = new Solution().SearchInsert(nums, target);
            }
            Assert.Equal(exp, res);

            nums = new int[] { 1, 3, 5, 6 };
            target = 0;
            exp = 0;
            using (new Timeit())
            {
                res = new Solution().SearchInsert(nums, target);
            }
            Assert.Equal(exp, res);
        }
    }
}