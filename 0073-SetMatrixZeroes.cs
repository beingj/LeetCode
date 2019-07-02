using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;

namespace SetMatrixZeroes
{
    public class Solution
    {
        public void SetZeroes(int[][] matrix)
        {
            var rows = new HashSet<int>();
            var cols = new HashSet<int>();
            for (var i = 0; i < matrix.Length; i++)
            {
                for (var j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        rows.Add(i);
                        cols.Add(j);
                    }
                }
            }
            foreach (var r in rows)
            {
                for (var j = 0; j < matrix[r].Length; j++)
                {
                    matrix[r][j] = 0;
                }
            }
            foreach (var c in cols)
            {
                for (var j = 0; j < matrix.Length; j++)
                {
                    matrix[j][c] = 0;
                }
            }
        }
    }
    public class Test
    {
        static void Verify(int[][] matrix, int[][] exp)
        {
            Console.WriteLine($"{matrix.Int2dToJson()} => {exp.Int2dToJson()}");
            using (new Timeit())
            {
                new Solution().SetZeroes(matrix);
            }
            Assert.Equal(exp, matrix);
        }
        static public void Run()
        {
            Console.WriteLine("SetMatrixZeroes");

            var input = @"
[ [1,1,1], [1,0,1], [1,1,1] ]
[ [1,0,1], [0,0,0], [1,0,1] ]
[ [0,1,2,0], [3,4,5,2], [1,3,1,5] ]
[ [0,0,0,0], [0,4,5,0], [0,3,1,0] ]
";
            var lines = input.CleanInput();
            int[][] matrix, exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                matrix = lines[idx++].JsonToInt2d();
                exp = lines[idx++].JsonToInt2d();
                Verify(matrix, exp);
            }
        }
    }
}