using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Triangle
{
    public class Solution
    {
        public int MinimumTotal(IList<IList<int>> triangle)
        {
            return MyMiniTotal(triangle, 0, 0, new Dictionary<(int, int), int>());
        }
        int CachedMyMiniTotal(IList<IList<int>> triangle, int row, int col, Dictionary<(int, int), int> cache)
        {
            var k = (row, col);
            if (!cache.ContainsKey(k))
                cache[k] = MyMiniTotal(triangle, row, col, cache);
            return cache[k];
        }
        int MyMiniTotal(IList<IList<int>> triangle, int row, int col, Dictionary<(int, int), int> cache)
        {
            if (row == (triangle.Count - 1))
            {
                return triangle[row][col];
            }
            var x = CachedMyMiniTotal(triangle, row + 1, col, cache);
            var y = CachedMyMiniTotal(triangle, row + 1, col + 1, cache);
            return triangle[row][col] + Math.Min(x, y);
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[[-1],[2,3],[1,-1,-3]]
-1
[ [2], [3,4], [6,5,7], [4,1,8,3] ]
11
";
            var lines = input.CleanInput();
            lines = "0120-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}