using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;

namespace SearchA2DMatrix
{
    public class Solution
    {
        public bool SearchMatrix(int[][] matrix, int target)
        {
            int rows = matrix.Length;
            if (rows == 0)
            {
                return false;
            }
            int cols = matrix[0].Length;
            int idxStart = 0, idxEnd = rows * cols - 1;
            while (idxStart <= idxEnd)
            {
                // Console.Writene($"search {target} : {idxStart} {idxEnd}");
                // int middle = (idxStart + idxEnd) / 2;
                // 问题会出现在当 idxStart + idxEnd 的结果大于表达式结果类型所能表示的最大值时，
                // 产生溢出后再/2是不会产生正确结果的，而idxStart + ((idxEnd - idxStart) / 2) 不会溢出
                int middle = idxStart + ((idxEnd - idxStart) / 2);
                int row = middle / cols;
                int col = middle % cols;
                int v = matrix[row][col];
                if (target == v)
                {
                    return true;
                }
                else if (target > v)
                {
                    idxStart = middle + 1;
                }
                else if (target < v)
                {
                    idxEnd = middle - 1;
                }
            }
            return false;
        }
    }
    public class Test
    {
        static void Verify(int[][] matrix, int target, bool exp)
        {
            Console.WriteLine($"{matrix.Int2dToJson()}, {target}");
            bool res;
            using (new Timeit())
            {
                res = new Solution().SearchMatrix(matrix, target);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("SearchA2DMatrix");

            var input = @"
[ [1,   3,  5,  7], [10, 11, 16, 20], [23, 30, 34, 50] ]
3
true
[ [1,   3,  5,  7], [10, 11, 16, 20], [23, 30, 34, 50] ]
13
false
";
            var lines = input.CleanInput();
            int[][] matrix;
            int target;
            bool exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                matrix = lines[idx++].JsonToInt2d();
                target = int.Parse(lines[idx++]);
                exp = bool.Parse(lines[idx++]);
                Verify(matrix, target, exp);
            }
        }
    }
}