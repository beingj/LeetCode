using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PascalsTriangleII
{
    public class Solution
    {
        public IList<int> GetRow(int rowIndex)
        {
            var last = new List<int> { 1 };
            for (var i = 1; i <= rowIndex; i++)
            {
                var r = new List<int>();
                r.Add(1);
                for (var j = 0; j < last.Count - 1; j++)
                {
                    r.Add(last[j] + last[j + 1]);
                }
                r.Add(1);
                last = r;
            }
            return last;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
3
[1,3,3,1]
4
[ 1,4,6,4,1] 
0
[1]
1
[1,1]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}