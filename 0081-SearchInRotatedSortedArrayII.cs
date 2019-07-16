using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SearchInRotatedSortedArrayII
{
    public class Solution
    {
        public bool Search(int[] nums, int target)
        {
            foreach (var n in nums)
            {
                if (n == target)
                {
                    return true;
                }
            }
            return false;
            // // Console.WriteLine($"len: {nums.Length}");
            // if (nums.Length < 100)
            // {
            //     foreach (var n in nums)
            //     {
            //         if (n == target)
            //         {
            //             return true;
            //         }
            //     }
            //     return false;
            // }
            // else
            // {
            //     if (SearchIndex(nums, target) < 0)
            //     {
            //         return false;
            //     }
            //     return true;
            // }
        }
        public int SearchIndex(int[] nums, int target)
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
                    idxStart = middle;
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
        static void Verify(int[] nums, int target, bool exp)
        {
            Console.WriteLine($"{nums.Int1dToJson()}");
            bool res;
            using (new Timeit())
            {
                res = new Solution().Search(nums, target);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine(typeof(Solution).Namespace);

            var input = @"
[2,5,6,0,0,1,2]
0
true
[2,5,6,0,0,1,2]
3
false
[1,1,3,1]
3
true
[1]
0
false
[1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1]
2
true
";
            var lines = input.CleanInput();
            int[] nums;
            int target;
            bool exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                nums = lines[idx++].JsonToInt1d();
                target = int.Parse(lines[idx++]);
                exp = bool.Parse(lines[idx++]);
                Verify(nums, target, exp);
            }
        }
    }
}