using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LargestRectangleInHistogram
{
    public class Solution
    {
        public int LargestRectangleArea(int[] heights)
        {
            // give up.
            // https://www.geeksforgeeks.org/largest-rectangle-under-histogram/
            // How to calculate area with ‘x’ as smallest bar?
            // We need to know index of the first smaller (smaller than ‘x’) bar on left of ‘x’ and index of first smaller bar on right of ‘x’.
            if (heights.Length == 0)
            {
                return 0;
            }
            if (heights.Length == 1)
            {
                return heights[0];
            }
            int max = 0;
            int last = heights[0];
            for (int i = 0; i < heights.Length; i++)
            {
                if ((max > 0) && (heights[i] == last))
                {
                    continue;
                }
                last = heights[i];
                var m = RectangleArea(heights, i);
                max = Math.Max(max, m);
            }
            return max;
        }
        int RectangleArea(int[] heights, int idx)
        {
            // https://www.geeksforgeeks.org/largest-rectangle-under-histogram/
            // How to calculate area with ‘x’ as smallest bar?
            // We need to know index of the first smaller (smaller than ‘x’) bar on left of ‘x’ and index of first smaller bar on right of ‘x’.
            var x = heights[idx];
            var left = idx;
            while (left >= 0)
            {
                if (heights[left] < x)
                {
                    break;
                }
                left--;
            }
            var right = idx;
            while (right < heights.Length)
            {
                if (heights[right] < x)
                {
                    break;
                }
                right++;
            }
            return (right - left - 1) * heights[idx];
        }
        // return LargestRectangleArea1(heights);

        // var aList = new List<int>();
        // Split(heights, aList);
        // return aList.Max();

        // return Split(heights);

        // int Split(int[] heights)
        // {
        //     if (heights.Length == 0)
        //     {
        //         return 0;
        //     }
        //     if (heights.Length == 1)
        //     {
        //         return heights[0];
        //     }
        //     var a = heights.Min() * heights.Length;
        //     var idx = 0;
        //     for (int i = 0; i < heights.Length; i++)
        //     {
        //         if (heights[i] < heights[idx])
        //         {
        //             idx = i;
        //         }
        //     }
        //     var left = heights.Take(idx).ToArray();
        //     var right = heights.Skip(idx + 1).ToArray();
        //     var l = Split(left);
        //     var r = Split(right);
        //     return Math.Max(a, Math.Max(l, r));
        // }
        // void Split(int[] heights, List<int> aList)
        // {
        //     if (heights.Length == 0)
        //     {
        //         return;
        //     }
        //     if (heights.Length == 1)
        //     {
        //         aList.Add(heights[0]);
        //         return;
        //     }
        //     aList.Add(heights.Min() * heights.Length);
        //     var res = new List<List<int>>();
        //     var idx = 0;
        //     for (int i = 0; i < heights.Length; i++)
        //     {
        //         if (heights[i] < heights[idx])
        //         {
        //             idx = i;
        //         }
        //     }
        //     var left = heights.Take(idx).ToArray();
        //     var right = heights.Skip(idx + 1).ToArray();
        //     Split(left, aList);
        //     Split(right, aList);
        // }
        // public int LargestRectangleArea1(int[] heights)
        // {
        //     if (heights.Length == 0)
        //     {
        //         return 0;
        //     }
        //     int res = heights[0];
        //     for (var i = 0; i < heights.Length; i++)
        //     {
        //         for (var j = i; j < heights.Length; j++)
        //         {
        //             var h = heights[i];
        //             var w = 1;
        //             for (var k = i + 1; k <= j; k++)
        //             {
        //                 if (heights[k] == 0)
        //                 {
        //                     break;
        //                 }
        //                 h = Math.Min(heights[k], h);
        //                 w++;
        //             }
        //             var a = h * w;
        //             res = Math.Max(a, res);
        //         }
        //     }
        //     return res;
        // }
    }
    public class Test
    {
        static void Verify(int[] heights, int exp)
        {
            Console.WriteLine($"{new string(heights.Int1dToJson().Take(100).ToArray())}");
            int res;
            using (new Timeit())
            {
                res = new Solution().LargestRectangleArea(heights);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine(typeof(Solution).Namespace);

            var input = @"
[2,1,5,6,2,3]
10
[0,9]
9
[6, 2, 5, 4, 5, 1, 6]
12
[1,1]
2
";
            var lines = input.CleanInput();
            // var lines = "0084-data.txt".InputFromFile();
            // var lines = "0084-data-2.txt".InputFromFile();
            int[] heights;
            int exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                heights = lines[idx++].JsonToInt1d();
                exp = int.Parse(lines[idx++]);
                Verify(heights, exp);
            }
        }
    }
}