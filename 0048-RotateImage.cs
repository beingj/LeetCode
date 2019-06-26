using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace RotateImage
{
    public class Solution
    {
        public void Rotate(int[][] matrix)
        {
            Rotate90(matrix, matrix[0].Length);
        }
        void Rotate90(int[][] matrix, int n)
        {
            var tmp = new int[4];
            int N = matrix[0].Length;
            int end = n - 1;
            int offset = (N - n) / 2;
            for (var i = 0; i < end; i++)
            {
                tmp[0] = matrix[offset + 0][offset + i];
                tmp[1] = matrix[offset + i][offset + end];
                tmp[2] = matrix[offset + end][offset + end - i];
                tmp[3] = matrix[offset + end - i][offset + 0];

                matrix[offset + i][offset + end] = tmp[0];
                matrix[offset + end][offset + end - i] = tmp[1];
                matrix[offset + end - i][offset + 0] = tmp[2];
                matrix[offset + 0][offset + i] = tmp[3];
            }
            if (n > 2)
            {
                Rotate90(matrix, n - 2);
            }
        }
    }
    public class Test
    {
        static void Verify(int[][] matrix, int[][] exp)
        {
            var input = string.Join('\n', matrix.Select(x => string.Join<string>(',', x.Select(y => string.Format("{0:d2}", y)))));
            Console.WriteLine($"{input}");
            using (new Timeit())
            {
                new Solution().Rotate(matrix);
            }
            var modified = string.Join('\n', matrix.Select(x => string.Join<string>(',', x.Select(y => string.Format("{0:d2}", y)))));
            Console.WriteLine($"{modified}");
            Console.WriteLine("----");
            var expect = string.Join(" | ", exp.Select(x => string.Join<int>(',', x)));
            var output = string.Join(" | ", matrix.Select(x => string.Join<int>(',', x)));
            Assert.Equal(expect, output);
        }
        static public void Run()
        {
            Console.WriteLine("RotateImage");
            int[][] matrix;
            int[][] exp;

            matrix = new int[][]{
                new int[]{1,2},
                new int[]{3,4},
            };
            exp = new int[][]{
                new int[]{3,1},
                new int[]{4,2},
            };
            Verify(matrix, exp);

            matrix = new int[][]{
                new int[]{1,2,3},
                new int[]{4,5,6},
                new int[]{7,8,9},
            };
            exp = new int[][]{
                new int[]{7,4,1},
                new int[]{8,5,2},
                new int[]{9,6,3},
            };
            Verify(matrix, exp);

            matrix = new int[][]{
                new int[]{5,1,9,11},
                new int[]{2,4,8,10},
                new int[]{13,3,6,7},
                new int[]{15,14,12,16},
            };
            exp = new int[][]{
                new int[]{15,13,2,5},
                new int[]{14,3,4,1},
                new int[]{12,6,8,9},
                new int[]{16,7,10,11},
            };
            Verify(matrix, exp);
        }
    }
}