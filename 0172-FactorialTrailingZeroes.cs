using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FactorialTrailingZeroes
{
    public class Solution
    {
        public int TrailingZeroes(int n)
        {
            // give up
            // copy from: https://leetcode.com/problems/factorial-trailing-zeroes/discuss/52371/My-one-line-solutions-in-3-languages
            return n == 0 ? 0 : n / 5 + TrailingZeroes(n / 5);
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
3
0
#
5
1
#
13
2
#
15
3
#
19
3
#
32
7
#
30
7
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}