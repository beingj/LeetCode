using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace TrappingRainWater
{
    public class Solution
    {
        enum Direct
        {
            none,
            raise,
            fall,
            eq
        }
        public int Trap1(int[] height)
        {
            // solution by me
            if (height.Length < 3)
                return 0;
            int idxStart = 0;
            int idxEnd;
            int idx = 0;
            int fallCount = 0;
            int prev = 0;
            Direct currDirect = Direct.eq;
            Direct prevDirect = Direct.eq;
            List<int> peaks = new List<int>();
            while (idx <= height.Length)
            {
                if (idx == height.Length)
                {
                    currDirect = Direct.fall;
                    fallCount += 1;
                }
                else
                {
                    if (height[idx] > prev)
                    {
                        currDirect = Direct.raise;
                    }
                    else if (height[idx] < prev)
                    {
                        currDirect = Direct.fall;
                        fallCount += 1;
                    }
                    else
                    {
                        currDirect = Direct.eq;
                    }
                }

                if ((currDirect == Direct.fall
                    && prevDirect != Direct.fall))
                {
                    if (fallCount == 1)
                    {
                        idxStart = idx - 1;
                        peaks.Add(idxStart);
                    }
                    else if (fallCount > 1)
                    {
                        idxEnd = idx - 1;
                        peaks.Add(idxEnd);
                        idxStart = idxEnd;
                        if (currDirect == Direct.fall)
                        {
                            fallCount = 1;
                        }
                        else
                        {
                            // eq
                            fallCount = 0;
                        }
                    }
                }

                if (idx < height.Length)
                {
                    prev = height[idx];
                    prevDirect = currDirect;
                }
                idx++;
            }

            // Console.WriteLine(string.Join(',', peaks));
            while (true)
            {
                var valley = new List<int>();
                for (var i = 1; i < peaks.Count - 1; i++)
                {
                    var nextIdx = i + 1;
                    while (true)
                    {
                        if (nextIdx >= peaks.Count)
                        {
                            break;
                        }
                        if (height[peaks[nextIdx]] != height[peaks[i]])
                        {
                            break;
                        }
                        nextIdx++;
                    }
                    if (nextIdx < peaks.Count)
                    {
                        if (height[peaks[i]] < height[peaks[i - 1]]
                            && height[peaks[i]] < height[peaks[nextIdx]])
                        {
                            valley.Add(peaks[i]);
                        }
                    }
                }
                if (valley.Count == 0)
                {
                    break;
                }
                // Console.WriteLine($"remove: {string.Join(',', valley)}");
                foreach (var i in valley)
                {
                    peaks.Remove(i);
                }
                // Console.WriteLine(string.Join(',', peaks));
            }

            int total = 0;
            for (var i = 0; i < peaks.Count - 1; i++)
            {
                idxStart = peaks[i];
                idxEnd = peaks[i + 1];
                int top = Math.Min(height[idxStart], height[idxEnd]);
                for (var j = idxStart + 1; j < idxEnd; j++)
                {
                    if (height[j] < top)
                    {
                        total += top - height[j];
                    }
                }
            }
            return total;
        }
        public int Trap(int[] height)
        {
            // solution by leetcode: 
            // https://leetcode.com/problems/trapping-rain-water/solution/
            // Approach 1: Brute force
            int total = 0;
            for (var i = 1; i < height.Length - 1; i++)
            {
                var left_max = height[i - 1];
                for (var j = i - 1; j >= 0; j--)
                {
                    if (height[j] > left_max)
                    {
                        left_max = height[j];
                    }
                }
                var right_max = height[i + 1];
                for (var j = i + 1; j < height.Length; j++)
                {
                    if (height[j] > right_max)
                    {
                        right_max = height[j];
                    }
                }
                var top = Math.Min(left_max, right_max);
                if (top > height[i])
                {
                    total += top - height[i];
                }
            }
            return total;
        }
    }

    public class Test
    {
        static void Verify(int[] input, int exp)
        {
            Console.WriteLine($"{string.Join(',', input)}");
            int res;
            using (new Timeit())
            {
                res = new Solution().Trap(input);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("TrappingRainWater");
            int[] input;
            int exp;

            input = new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
            exp = 6;
            Verify(input, exp);

            input = new int[] { 1, 0, 2 };
            exp = 1;
            Verify(input, exp);

            input = new int[] { 5, 4, 1, 2 };
            exp = 1;
            Verify(input, exp);

            input = new int[] { 5, 2, 1, 2, 1, 5 };
            exp = 14;
            Verify(input, exp);

            input = new int[] { 5, 2, 1, 2, 1, 5, 3, 4 };
            exp = 15;
            Verify(input, exp);

            input = new int[] { 5, 2, 1, 2, 1, 5, 3, 4, 3, 5 };
            exp = 19;
            Verify(input, exp);

            input = new int[] { 5, 5, 1, 7, 1, 1, 5, 2, 7, 6 };
            exp = 23;
            Verify(input, exp);

            input = new int[] { 6, 4, 2, 0, 3, 2, 0, 3, 1, 4, 5, 3, 2, 7, 5, 3, 0, 1, 2, 1, 3, 4, 6, 8, 1, 3 };
            exp = 83;
            Verify(input, exp);
        }
    }
}