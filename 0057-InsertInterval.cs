using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace InsertInterval
{
    public class Solution
    {
        public int[][] Insert(int[][] intervals, int[] newInterval)
        {
            var lst0 = intervals.ToList();
            lst0.Add(newInterval);
            var lst = lst0.Select(x => string.Join(',', x.Select(y => string.Format("{0:d5}", y)))).ToList();
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
        static void Verify(int[][] intervals, int[] newInterval, int[][] exp)
        {
            Console.WriteLine(string.Format("{0} <= {1}", intervals.P(sep2: "|"), newInterval.P()));
            int[][] res;
            using (new Timeit())
            {
                res = new Solution().Insert(intervals, newInterval);
            }
            var res2 = res.P();
            var exp2 = exp.P();
            Assert.Equal(exp2, res2);
        }
        static public void Run()
        {
            Console.WriteLine("InsertInterval");
            int[][] intervals;
            int[] newInterval;
            int[][] exp;
            var input = @"
[[1,3],[6,9]]
[2,5]
[[1,5],[6,9]]
[[1,2],[3,5],[6,7],[8,10],[12,16]]
[4,8]
[[1,2],[3,10],[12,16]]
";
            var lines = input.Trim(new char[] { '\n', '\r', ' ' }).Split('\n').Select(x => x.Trim('\r')).ToArray();
            int idx = 0;
            while (idx < lines.Length)
            {
                intervals = lines[idx++].JsonToInt2d();
                newInterval = lines[idx++].JsonToInt1d();
                exp = lines[idx++].JsonToInt2d();
                Verify(intervals, newInterval, exp);
            }
        }
    }
}