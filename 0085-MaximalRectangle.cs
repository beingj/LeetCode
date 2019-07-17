using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MaximalRectangle
{
    public class Solution
    {
        public int MaximalRectangle(char[][] matrix)
        {
            if (matrix.Length == 0)
            {
                return 0;
            }
            int max = 0;
            int row = matrix.Length;
            int col = matrix[0].Length;
            int total = row * col;

            for (int i = 0; i < total; i++)
            {
                var x = Rectangle(matrix, row, col, i);
                max = Math.Max(max, x);
            }
            return max;
        }
        int Rectangle(char[][] matrix, int row, int col, int cell)
        {
            int r = cell / col;
            int c = cell % col;
            if (matrix[r][c] == '0')
            {
                return 0;
            }

            int w = 1;
            for (int i = c + 1; i < col; i++)
            {
                if (matrix[r][i] == '0')
                {
                    break;
                }
                w++;
            }

            int h = 1;
            for (int i = r + 1; i < row; i++)
            {
                if (matrix[i][c] == '0')
                {
                    break;
                }
                h++;
            }

            int max = 0;
            for (int i = r + 1; i < r + h; i++)
            {
                for (int j = c + 1; j < c + w; j++)
                {
                    if (matrix[i][j] == '0')
                    {
                        var a1 = (i - r) * w;
                        max = Math.Max(max, a1);
                        w = j - c;
                        break;
                    }
                }
            }
            return Math.Max(max, w * h);
        }
    }
    public class Test
    {
        static void Verify(char[][] matrix, int exp)
        {
            Console.WriteLine($"{matrix.Char2dToJson('"')}");
            int res;
            using (new Timeit())
            {
                res = new Solution().MaximalRectangle(matrix);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine(typeof(Solution).Namespace);

            var lines = "0085-data.txt".InputFromFile();
            char[][] matrix;
            int exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                matrix = lines[idx++].JsonToChar2d('"');
                exp = int.Parse(lines[idx++]);
                Verify(matrix, exp);
            }
        }
    }
}