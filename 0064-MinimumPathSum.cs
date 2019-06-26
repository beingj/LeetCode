using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;

namespace MinimumPathSum
{
    public class Solution
    {
        public int MinPathSum(int[][] grid)
        {
            int m = grid[0].Length;
            int n = grid.Length;
            int[] buf = new int[m];

            buf[0] = grid[0][0];
            for (var j = 1; j < m; j++)
            {
                buf[j] = grid[0][j] + buf[j - 1];
            }

            for (var i = 1; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    int min = buf[j];
                    if (j > 0)
                    {
                        min = Math.Min(min, buf[j - 1]);
                    }
                    buf[j] = grid[i][j] + min;
                }
                // Console.WriteLine(buf);
            }
            return buf[m - 1];
        }
        public static void Move(int[][] grid, int m, int n, int cell, int soFar, ref int res)
        {
            int row = cell / m;
            int col = cell % m;
            var sum = soFar + grid[row][col];
            if (cell == (m * n - 1))
            {
                // Console.WriteLine($"cell {cell}");
                if (res == 0 || sum < res)
                {
                    // Console.WriteLine($"{res} => {sum}");
                    res = sum;
                }
            }
            if (res != 0 && sum >= res)
            {
                return;
            }
            if ((col + 1) < m)
            {
                // Console.WriteLine(string.Format("{0}=>{1}", soFar, sum));
                Move(grid, m, n, cell + 1, sum, ref res);
            }
            if ((row + 1) < n)
            {
                // Console.WriteLine(string.Format("{0}=>{1}", soFar, sum));
                Move(grid, m, n, cell + m, sum, ref res);
            }
        }
    }

    public class Test
    {
        static void Verify(int[][] grid, int exp)
        {
            Console.WriteLine(grid.Int2dToJson());
            // Console.WriteLine(string.Join("\n",grid.Select(i=>string.Join(",",i))));
            int res;
            using (new Timeit())
            {
                res = new Solution().MinPathSum(grid);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("MinimumPathSum");

            var input = @"
[ [1,3,1], [1,5,1], [4,2,1] ]
7
";
            var lines = input.Trim(new char[] { '\n', '\r', ' ' }).Split('\n')
                            .Select(x => x.Trim(new char[] { '\r', ' ' })).Where(y => !y.StartsWith('#')).ToArray();
            int[][] grid;
            int exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                grid = lines[idx++].JsonToInt2d();
                exp = int.Parse(lines[idx++]);
                Verify(grid, exp);
            }
        }
    }
}