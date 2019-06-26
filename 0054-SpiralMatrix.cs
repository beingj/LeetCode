using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace SpiralMatrix
{
    public class Solution
    {
        public IList<int> SpiralOrder(int[][] matrix)
        {
            if (matrix.Length == 0)
            {
                return new List<int>();
            }
            if (matrix.Length == 1)
            {
                return new List<int>(matrix[0]);
            }
            var res = new List<int>();
            Spiral(matrix, 0, matrix[0].Length, matrix.Length, res);
            return res;
        }
        void Spiral(int[][] matrix, int offset, int w, int h, IList<int> res)
        {
            // Console.WriteLine($"{w}x{h}");
            if (w <= 0 || h <= 0)
            {
                return;
            }
            for (var i = 0; i < w; i++)
            {
                res.Add(matrix[offset][offset + i]);
            }
            for (var i = 1; i < h; i++)
            {
                res.Add(matrix[offset + i][offset + w - 1]);
            }
            if (h == 1)
            {
                return;
            }
            for (var i = w - 2; i >= 0; i--)
            {
                res.Add(matrix[offset + h - 1][offset + i]);
            }
            if (w == 1)
            {
                return;
            }
            for (var i = h - 2; i > 0; i--)
            {
                res.Add(matrix[offset + i][offset]);
            }
            Spiral(matrix, offset + 1, w - 2, h - 2, res);
        }
    }
    public class Test
    {
        static void Verify(int[][] matrix, IList<int> exp)
        {
            Console.WriteLine(string.Join('\n', matrix.Take(10).Select(x => string.Join(',', x))));
            IList<int> res;
            using (new Timeit())
            {
                res = new Solution().SpiralOrder(matrix);
            }
            Console.WriteLine(string.Format("res: {0}", string.Join(',', res)));
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("SpiralMatrix");
            int[][] matrix;
            IList<int> exp;

            matrix = new int[][]{
                new int[]{1,2,3},
                new int[]{4,5,6},
                new int[]{7,8,9},
            };
            exp = new List<int> { 1, 2, 3, 6, 9, 8, 7, 4, 5 };
            Verify(matrix, exp);

            matrix = new int[][]{
                new int[]{1,2,3,4},
                new int[]{5,6,7,8},
                new int[]{9,10,11,12},
            };
            exp = new List<int> { 1, 2, 3, 4, 8, 12, 11, 10, 9, 5, 6, 7 };
            Verify(matrix, exp);

            matrix = new int[][]{
                new int[]{1,2,3},
                new int[]{4,5,6},
                new int[]{7,8,9},
                new int[]{10,11,12},
            };
            exp = new List<int> { 1, 2, 3, 6, 9, 12, 11, 10, 7, 4, 5, 8 };
            Verify(matrix, exp);

            matrix = new int[][] { };
            exp = new List<int>();
            Verify(matrix, exp);

            matrix = new int[][]{
                new int[]{7},
                new int[]{9},
                new int[]{6},
            };
            exp = new List<int> { 7, 9, 6 };
            Verify(matrix, exp);

        }
    }
}