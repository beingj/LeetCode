using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace SpiralMatrixII
{
    public class Solution
    {
        public int[][] GenerateMatrix(int n)
        {
            var res = new List<List<int>>();
            for (var i = 0; i < n; i++)
            {
                var lst = new List<int>();
                for (var j = 0; j < n; j++)
                {
                    lst.Add(0);
                }
                res.Add(lst);
            }
            Spiral(res, 0, n, n, 1);
            return res.Select(x => x.ToArray()).ToArray();
        }
        void Spiral(List<List<int>> matrix, int offset, int w, int h, int num)
        {
            // Console.WriteLine($"{w}x{h}");
            if (w <= 0 || h <= 0)
            {
                return;
            }
            for (var i = 0; i < w; i++)
            {
                matrix[offset][offset + i] = num++;
            }
            for (var i = 1; i < h; i++)
            {
                matrix[offset + i][offset + w - 1] = num++;
            }
            if (h == 1)
            {
                return;
            }
            for (var i = w - 2; i >= 0; i--)
            {
                matrix[offset + h - 1][offset + i] = num++;
            }
            if (w == 1)
            {
                return;
            }
            for (var i = h - 2; i > 0; i--)
            {
                matrix[offset + i][offset] = num++;
            }
            Spiral(matrix, offset + 1, w - 2, h - 2, num);
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
3
[ [ 1, 2, 3 ], [ 8, 9, 4 ], [ 7, 6, 5 ] ]
";
            var lines=input.CleanInput();
            Verify.Method(new Solution(), lines, sortRet: true);
        }
    }
}