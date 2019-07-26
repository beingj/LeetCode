using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UniqueBinarySearchTrees
{
    public class Solution
    {
        public int NumTrees(int n)
        {
            if ((n == 0) || (n == 1))
            {
                return 1;
            }
            if (n == 2)
            {
                return 2;
            }
            if (n == 3)
            {
                return 5;
            }
            // f(n)=f(n-1)*2+f(n-2)*2+ f(n-3)*f(2) +f(n-4)*f(3) +...
            var sum = NumTrees(n - 1) * 2;
            sum += NumTrees(n - 2) * 2;
            var j = 2;
            for (var i = n - 3; i >= 2; i--)
            {
                sum += NumTrees(i) * NumTrees(j);
                j++;
            }
            return sum;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
0
1
1
1
2
2
3
5
4
14
5
42
6
132
7
429
10
16796
11
58786
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
            var n = "[1,null,2,3]".JsonToTreeNode();
            Console.WriteLine(n);
            Console.WriteLine();
            Console.WriteLine(n.ToStr(5, 3));
            Console.WriteLine();
            Console.WriteLine(n.ToStr(5, 3, true));

            n = "[1,null,2,3,4]".JsonToTreeNode();
            Console.WriteLine(n);
            Console.WriteLine();
            Console.WriteLine(n.ToStr(5, 3));
            Console.WriteLine();
            Console.WriteLine(n.ToStr(5, 3, true));
        }
    }
}