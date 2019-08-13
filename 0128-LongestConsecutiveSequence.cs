using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LongestConsecutiveSequence
{
    public class Solution
    {
        public int LongestConsecutive(int[] nums)
        {
            // give up.
            // copy from: https://leetcode.com/problems/longest-consecutive-sequence/discuss/41055/My-really-simple-Java-O(n)-solution-Accepted
            int res = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();
            foreach (int n in nums)
            {
                if (!map.ContainsKey(n))
                {
                    // See if n - 1 and n + 1 exist in the map, and if so,
                    // it means there is an existing sequence next to n.
                    // Variables left and right will be the length of those two sequences,
                    // while 0 means there is no sequence and n will be the boundary point later.
                    // Store (left + right + 1) as the associated value to key n into the map.
                    var left = map.GetValueOrDefault(n - 1, 0);
                    var right = map.GetValueOrDefault(n + 1, 0);
                    // sum: length of the sequence n is in
                    int sum = left + right + 1;
                    map[n] = sum;

                    // keep track of the max length 
                    res = Math.Max(res, sum);

                    // extend the length to the boundary(s)
                    // of the sequence
                    // will do nothing if n has no neighbors
                    if (left > 0) map[n - left] = sum;
                    if (right > 0) map[n + right] = sum;
                }
                else
                {
                    // duplicates
                    continue;
                }
            }
            return res;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[100, 4, 200, 1, 3, 2]
4
[8,5,10,30,7,3,6,9]
6
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}