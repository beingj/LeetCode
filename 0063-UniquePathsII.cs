using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;

namespace UniquePathsII
{
    public class Solution
    {
        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            // NOTE: give up, copy solution from: https://leetcode.com/problems/unique-paths-ii/discuss/23250/Short-JAVA-solution
            int width = obstacleGrid[0].Length;
            int[] dp = new int[width];
            dp[0] = 1;
            foreach (var row in obstacleGrid)
            {
                for (int j = 0; j < width; j++)
                {
                    if (row[j] == 1)
                        dp[j] = 0;
                    else if (j > 0)
                        dp[j] += dp[j - 1];
                }
            }
            return dp[width - 1];
        }
        // public int UniquePathsWithObstacles1(int[][] obstacleGrid)
        // {
        //     if (obstacleGrid.Length > 0)
        //     {
        //         if (obstacleGrid[0].Length > 0)
        //         {
        //             if (obstacleGrid[0][0] == 1)
        //             {
        //                 return 0;
        //             }
        //         }
        //     }
        //     int m = obstacleGrid[0].Length;
        //     int n = obstacleGrid.Length;
        //     int res = 0;
        //     Move2(obstacleGrid, m, n, 0, ref res);
        //     return res;

        //     var cache = new Dictionary<int, int>();
        //     int m = obstacleGrid[0].Length;
        //     int n = obstacleGrid.Length;
        //     int res = 0;
        //     Move(obstacleGrid, m, n, 0, ref res, cache);
        //     return res;

        //     List<string> res = new List<string>();
        //     Move1(obstacleGrid, m, n, 0, res: res);
        //     return res.Count;
        // }
        // public static void Move(int[][] grid, int m, int n, int cell, ref int res, Dictionary<int, int> cache)
        // {
        //     if (cache.ContainsKey(cell))
        //     {
        //         // return cache[cell];
        //         return;
        //     }
        //     if (cell == (m * n - 1))
        //     {
        //         res++;
        //         return;
        //     }
        //     int row = cell / m;
        //     int col = cell % m;
        //     int resR = 0;
        //     if ((col + 1) < m)
        //     {
        //         if (grid[row][col + 1] == 0)
        //         {
        //             Move(grid, m, n, cell + 1, ref resR, cache);
        //         }
        //     }
        //     int resD = 0;
        //     if ((row + 1) < n)
        //     {
        //         if (grid[row + 1][col] == 0)
        //         {
        //             Move(grid, m, n, cell + m, ref resD, cache);
        //         }
        //     }
        //     res += resR + resD;
        //     cache[cell] = res;
        // }
        // public static void Move2(int[][] grid, int m, int n, int cell, ref int res)
        // {
        //     if (cell == (m * n - 1))
        //     {
        //         res++;
        //         return;
        //     }
        //     int row = cell / m;
        //     int col = cell % m;
        //     if ((col + 1) < m)
        //     {
        //         if (grid[row][col + 1] == 0)
        //         {
        //             Move(grid, m, n, cell + 1, ref res);
        //         }
        //     }
        //     if ((row + 1) < n)
        //     {
        //         if (grid[row + 1][col] == 0)
        //         {
        //             Move(grid, m, n, cell + m, ref res);
        //         }
        //     }
        // }
        // public static void Move1(int[][] grid, int m, int n, int cell, List<string> soFar = null, List<string> res = null)
        // {
        //     if (cell == (m * n - 1))
        //     {
        //         // Console.WriteLine($"cell {cell}");
        //         if (res == null)
        //         {
        //             res = new List<string>();
        //         }
        //         res.Add(string.Join("\t", soFar));
        //         // Console.WriteLine($"{res.Count}: {string.Join("->", soFar)}");
        //         Console.WriteLine($"{string.Join("\t", soFar)}");
        //     }
        //     int row = cell / m;
        //     int col = cell % m;
        //     if (soFar == null)
        //     {
        //         soFar = new List<string>();
        //     }
        //     var newSoFar = new List<string>();
        //     newSoFar.AddRange(soFar);
        //     if ((col + 1) < m)
        //     {
        //         if (grid[row][col + 1] == 0)
        //         {
        //             // Console.WriteLine(string.Format("{0}=>{1}",string.Join("->", soFar),"R"));
        //             newSoFar.Add("R");
        //             Move1(grid, m, n, cell + 1, newSoFar, res);
        //         }
        //     }
        //     newSoFar.Clear();
        //     newSoFar.AddRange(soFar);
        //     if ((row + 1) < n)
        //     {
        //         if (grid[row + 1][col] == 0)
        //         {
        //             // Console.WriteLine(string.Format("{0}=>{1}",string.Join("->", soFar),"D"));
        //             newSoFar.Add("D");
        //             Move1(grid, m, n, cell + m, newSoFar, res);
        //         }
        //     }
        // }
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
                res = new Solution().UniquePathsWithObstacles(grid);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("UniquePaths");
            // var res = new List<char>();
            // Solution.Move(99, 9, 0, res);
            // Console.WriteLine($"{res.Count}");
            // Solution.Move1(5, 4, 0, res: res);

            // return;
            var input = @"
[ [0,0,0], [0,1,0], [0,0,0] ]
2
[[1]]
0
[[0,0],[1,0]]
1
[[0,1]]
0
[[0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],[0,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0],[1,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,1,0,0,0,0,0,0,0,0,1,0,0,1],[0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0],[0,0,0,1,0,1,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,1,0],[1,0,1,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0],[0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,1,0,0,0,1,0,1,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,1,0],[0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,1,0,0,0,0,0],[0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0],[1,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,1,0,1,0,0,0,1,0,1,0,0,0,0,1],[0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,1,0,0,0,0,0,0,1,1,0,0,0,0,0],[0,1,0,1,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,0,0,0,0,0],[0,1,0,0,0,0,0,0,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,1,0,1],[1,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,1,1,0,0,1,0,0,0,0,0,0],[0,0,1,0,0,0,0,0,0,0,1,0,0,1,0,0,1,0,0,0,0,0,0,1,1,0,1,0,0,0,0,1,1],[0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,1,1,0,1,0,1],[1,1,1,0,1,0,0,0,0,1,0,0,0,0,0,0,1,0,1,0,1,1,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,1,1],[0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,1,0,0,0,1,0,0,0]]
1637984640
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