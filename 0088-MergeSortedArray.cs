using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MergeSortedArray
{
    public class Solution
    {
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int i1 = 0;
            int p = 0;
            for (var i2 = 0; i2 < n; i2++)
            {
                while (i1 < (m + p))
                {
                    if (nums1[i1] > nums2[i2])
                    {
                        for (var i = m + p - 1; i >= i1; i--)
                        {
                            nums1[i + 1] = nums1[i];
                        }
                        nums1[i1] = nums2[i2];
                        p++;
                        break;
                    }
                    i1++;
                }
                if (i1 >= (m + p))
                {
                    nums1[i1] = nums2[i2];
                    i1++;
                }
            }
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
[1,2,3,0,0,0]
3
[2,5,6]
3
[1,2,2,3,5,6]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines, 0);
        }
    }
}