using System;
using Xunit;

namespace FindMedianSortedArrays
{
    public class Solution
    {
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums1.Length == 0)
            {
                return FindMedian(nums2);
            }
            if (nums2.Length == 0)
            {
                return FindMedian(nums1);
            }
            int totaln = nums1.Length + nums2.Length;
            int mn = 0; // median position
            if (totaln % 2 == 1)
            {
                mn = (totaln - 1) / 2 + 1;
            }
            else
            {
                mn = (totaln / 2);
            }

            int[] n1 = nums1, n2 = nums2;
            // make sure n1[0] is smaller than n2[0]
            if (n1[0] > n2[0])
            {
                n1 = nums2;
                n2 = nums1;
            }
            int idx1 = 0, idx2 = 0, x1 = 0, x2 = 0;
            // idx1, idx2 save index of n1 and n2
            // x1, x2 save int num visited. to break loop if x1+x2==mn
            int z1, z2; // temp var

            int max = n1[idx1]; // save max int visited
            idx1 += 1;
            x1 += 1;

            while (true)
            {
                if ((x1 + x2) >= mn)
                {
                    break;
                }
                if (idx2 < n2.Length)
                {
                    z1 = z2 = n2[idx2];
                    if (z1 > max)
                    {
                        if (idx1 < n1.Length)
                        {
                            z2 = n1[idx1];
                        }
                        // in case:
                        // n1 { -2, -1 }
                        // n2 { 3 }
                        // z1 = 3, but max should be -1
                        if (z2 < z1)
                        {
                            max = z2;
                            idx2 -= 1;
                            x2 -= 1;
                        }
                        else
                        {
                            max = z1;
                        }
                    }
                    idx2 += 1;
                    x2 += 1;
                }

                if ((x1 + x2) >= mn)
                {
                    break;
                }
                if (idx1 < n1.Length)
                {
                    z1 = z2 = n1[idx1];
                    if (z1 > max)
                    {
                        if (idx2 < n2.Length)
                        {
                            z2 = n2[idx2];
                        }
                        if (z2 < z1)
                        {
                            max = z2;
                            idx1 -= 1;
                            x1 -= 1;
                        }
                        else
                        {
                            max = z1;
                        }
                    }
                    idx1 += 1;
                    x1 += 1;
                }
            }

            if (totaln % 2 == 1)
            {
                return max;
            }
            int max0 = max;

            // find next max, so median = (max + max0) / 2.0
            mn += 1;
            while (true)
            {
                if ((x1 + x2) >= mn)
                {
                    break;
                }
                if (idx2 < n2.Length)
                {
                    z1 = z2 = n2[idx2];
                    if (z1 > max)
                    {
                        if (idx1 < n1.Length)
                        {
                            z2 = n1[idx1];
                        }
                        if (z2 < z1)
                        {
                            max = z2;
                            idx2 -= 1;
                            x2 -= 1;
                        }
                        else
                        {
                            max = z1;
                        }
                    }
                    idx2 += 1;
                    x2 += 1;
                }
                if ((x1 + x2) >= mn)
                {
                    break;
                }
                if (idx1 < n1.Length)
                {
                    z1 = z2 = n1[idx1];
                    if (z1 > max)
                    {
                        if (idx2 < n2.Length)
                        {
                            z2 = n2[idx2];
                        }
                        if (z2 < z1)
                        {
                            max = z2;
                            idx1 -= 1;
                            x1 -= 1;
                        }
                        else
                        {
                            max = z1;
                        }
                    }
                    idx1 += 1;
                    x1 += 1;
                }
            }

            return (max + max0) / 2.0;
        }
        public double FindMedian(int[] nums)
        {
            if (nums.Length % 2 == 1)
            {
                return nums[(nums.Length - 1) / 2];
            }
            int i = (nums.Length / 2) - 1;
            return (nums[i] + nums[i + 1]) / 2.0;
        }
    }
    public class Test
    {
        static public void Run()
        {
            int[] nums1, nums2;
            double exp, res;

            Console.WriteLine("FindMedianSortedArrays");

            nums1 = new int[] { 1, 3 };
            nums2 = new int[] { 2 };
            exp = 2;
            res = new Solution().FindMedianSortedArrays(nums1, nums2);
            Assert.Equal(exp, res);

            nums1 = new int[] { 1, 2 };
            nums2 = new int[] { 3, 4 };
            exp = 2.5;
            res = new Solution().FindMedianSortedArrays(nums1, nums2);
            Assert.Equal(exp, res);

            nums1 = new int[] { };
            nums2 = new int[] { 1 };
            exp = 1;
            res = new Solution().FindMedianSortedArrays(nums1, nums2);
            Assert.Equal(exp, res);

            nums1 = new int[] { };
            nums2 = new int[] { 2, 3 };
            exp = 2.5;
            res = new Solution().FindMedianSortedArrays(nums1, nums2);
            Assert.Equal(exp, res);

            nums1 = new int[] { 1, 2 };
            nums2 = new int[] { 1, 2, 3 };
            exp = 2.0;
            res = new Solution().FindMedianSortedArrays(nums1, nums2);
            Assert.Equal(exp, res);

            nums1 = new int[] { 3 };
            nums2 = new int[] { -2, -1 };
            exp = -1;
            res = new Solution().FindMedianSortedArrays(nums1, nums2);
            Assert.Equal(exp, res);

            nums1 = new int[] { 1, 2, 4 };
            nums2 = new int[] { -1, 3 };
            exp = 2;
            res = new Solution().FindMedianSortedArrays(nums1, nums2);
            Assert.Equal(exp, res);

            nums1 = new int[] { 1, 2 };
            nums2 = new int[] { -1, 3 };
            exp = 1.5;
            res = new Solution().FindMedianSortedArrays(nums1, nums2);
            Assert.Equal(exp, res);
        }
    }
}