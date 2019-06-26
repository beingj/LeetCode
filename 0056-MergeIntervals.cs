using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace MergeIntervals
{
    public class Solution
    {
        public int[][] Merge(int[][] intervals)
        {
            var lst = intervals.Select(x => string.Join(',', x.Select(y => string.Format("{0:d5}", y)))).ToList();
            lst.Sort();
            var sortedIntervals = lst.Select(x => x.Split(',').Select(y => int.Parse(y)).ToArray());

            var res = new List<int[]> { };
            foreach (var i in sortedIntervals)
            {
                if (res.Count == 0)
                {
                    res.Add(i);
                    continue;
                }
                if (res.Last()[1] >= i[0])
                {
                    res.Last()[1] = Math.Max(res.Last()[1], i[1]);
                }
                else
                {
                    res.Add(i);
                }
            }
            return res.ToArray();
        }
    }
    public class Test
    {
        static void Verify(int[][] intervals, int[][] exp)
        {
            Console.WriteLine(intervals.P(sep2: "|"));
            int[][] res;
            using (new Timeit())
            {
                res = new Solution().Merge(intervals);
            }
            var res2 = res.P();
            var exp2 = exp.P();
            Assert.Equal(exp2, res2);
        }
        static public void Run()
        {
            Console.WriteLine("MergeIntervals");
            int[][] intervals;
            int[][] exp;

            // Input: [[1,3],[2,6],[8,10],[15,18]]
            // Output: [[1,6],[8,10],[15,18]]
            // Explanation: Since intervals [1,3] and [2,6] overlaps, merge them into [1,6].
            intervals = "[[1,3],[2,6],[8,10],[15,18]]".JsonToInt2d();
            exp = "[[1,6],[8,10],[15,18]]".JsonToInt2d();
            Verify(intervals, exp);

            intervals = "[[1,4],[0,4]]".JsonToInt2d();
            exp = "[[0,4]]".JsonToInt2d();
            Verify(intervals, exp);

            intervals = "[[1,4],[4,5]]".JsonToInt2d();
            exp = "[[1,5]]".JsonToInt2d();
            Verify(intervals, exp);

            intervals = "[[1,4],[2,3]]".JsonToInt2d();
            exp = "[[1,4]]".JsonToInt2d();
            Verify(intervals, exp);
        }
    }
}