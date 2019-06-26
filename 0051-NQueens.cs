using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace NQueens
{
    public class Solution
    {
        public IList<IList<string>> SolveNQueens(int n)
        {
            var solutions = new List<IList<string>>();
            var soFar = new char[n][];
            for (var i = 0; i < n; i++)
            {
                var row = new char[n];
                for (var j = 0; j < n; j++)
                {
                    row[j] = '.';
                }
                soFar[i] = row;
            }
            TrySolveNQueens(n, 0, soFar, 0, solutions);
            // foreach (var s in solutions)
            // {
            //     Console.WriteLine(string.Join('\n', s));
            //     Console.WriteLine("----");
            // }
            return solutions;
        }
        void TrySolveNQueens(int n, int cell, char[][] soFar, int cnt, List<IList<string>> solutions)
        {
            // Console.WriteLine($"cell: {cell}");
            // if (cell == (n * n))
            // {
            //     if (cnt == n)
            //     {
            //         var solution = soFar.Select(x => string.Join("", x)).ToList();
            //         // Console.WriteLine("solution==>");
            //         // ShowChessboard(n, soFar);
            //         solutions.Add(solution);
            //     }
            //     return;
            // }

            if (cnt == n)
            {
                var solution = soFar.Select(x => string.Join("", x)).ToList();
                // Console.WriteLine("solution==>");
                // ShowChessboard(n, soFar);
                solutions.Add(solution);
                return;
            }
            if (cell == (n * n))
            {
                return;
            }

            var row = cell / n;
            var col = cell % n;

            var ok = checkAttack(n, row, col, soFar);
            // Console.WriteLine($"check: {cell}");
            if (ok)
            {
                soFar[row][col] = 'Q';
                TrySolveNQueens(n, cell + 1, soFar, cnt + 1, solutions);
            }
            soFar[row][col] = '.';
            TrySolveNQueens(n, cell + 1, soFar, cnt, solutions);
        }
        bool checkAttack(int n, int row, int col, char[][] soFar)
        {
            // Console.WriteLine($"{row},{col}=>");
            // ShowChessboard(n, soFar);
            for (var i = 0; i < n; i++)
            {
                if (soFar[row][i] == 'Q')
                {
                    return false;
                }
            }
            for (var i = 0; i < n; i++)
            {
                if (soFar[i][col] == 'Q')
                {
                    return false;
                }
            }
            int r = row, c = col;
            while (true)
            {
                r--;
                c--;
                if (r < 0 || c < 0)
                    break;
                if (soFar[r][c] == 'Q')
                {
                    return false;
                }
            }
            r = row; c = col;
            while (true)
            {
                r--;
                c++;
                if (r < 0 || c > (n - 1))
                    break;
                if (soFar[r][c] == 'Q')
                {
                    return false;
                }
            }
            r = row; c = col;
            while (true)
            {
                r++;
                c--;
                if (r > (n - 1) || c < 0)
                    break;
                if (soFar[r][c] == 'Q')
                {
                    return false;
                }
            }
            r = row; c = col;
            while (true)
            {
                r++;
                c++;
                if (r > (n - 1) || c > (n - 1))
                    break;
                if (soFar[r][c] == 'Q')
                {
                    return false;
                }
            }
            return true;
        }
        void ShowChessboard(int n, char[][] soFar)
        {
            var solution = soFar.Select(x => string.Join("", x)).ToList();
            Console.WriteLine(string.Join('\n', solution));
            // Console.ReadKey();
        }
    }
    public class Test
    {
        static void Verify(int n, IList<IList<string>> exp)
        {
            Console.WriteLine($"{n}");
            IList<IList<string>> res;
            using (new Timeit())
            {
                res = new Solution().SolveNQueens(n);
            }
            var exp2 = exp.Select(x => string.Join(',', x)).ToList();
            var res2 = res.Select(x => string.Join(',', x)).ToList();
            exp2.Sort();
            res2.Sort();
            // Console.WriteLine($"{string.Join(" | ", res2)}");
            Assert.Equal(exp2, res2);
        }
        static public void Run()
        {
            Console.WriteLine("NQueens");
            int n;
            IList<IList<string>> exp;

            // Input: 4
            // Output: [
            //  [".Q..",  // Solution 1
            //   "...Q",
            //   "Q...",
            //   "..Q."],

            //  ["..Q.",  // Solution 2
            //   "Q...",
            //   "...Q",
            //   ".Q.."]
            // ]
            n = 4;
            exp = new List<IList<string>>{
                new List<string>{
                    ".Q..",
                    "...Q",
                    "Q...",
                    "..Q."
                },
                new List<string>{
                    "..Q.",
                    "Q...",
                    "...Q",
                    ".Q.."
                },
            };
            Verify(n, exp);

            n = 7;
            Verify(n, exp);

            n = 8;
            Verify(n, exp);

            n = 9;
            Verify(n, exp);

        }
    }
}