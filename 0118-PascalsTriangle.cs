using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PascalsTriangle
{
    public class Solution
    {
        public IList<IList<int>> Generate(int numRows)
        {
            var pascalsTriangle = new List<IList<int>>();
            if (numRows == 0)
                return pascalsTriangle;
            pascalsTriangle.Add(new List<int> { 1 });
            for (var i = 1; i < numRows; i++)
            {
                var r = new List<int>();
                r.Add(1);
                var last = pascalsTriangle.Last();
                for (var j = 0; j < last.Count - 1; j++)
                {
                    r.Add(last[j] + last[j + 1]);
                }
                r.Add(1);
                pascalsTriangle.Add(r);
            }
            return pascalsTriangle;

        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
5
[ [1], [1,1], [1,2,1], [1,3,3,1], [1,4,6,4,1] ]
0
[]
1
[[1]]
2
[[1],[1,1]]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}