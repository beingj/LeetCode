using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace NQueensII
{
    public class Solution
    {
        int Total = 0;
        public int TotalNQueens(int n)
        {
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
            TrySolveNQueens(n, 0, soFar, 0);
            return Total;
        }
        void TrySolveNQueens(int n, int cell, char[][] soFar, int cnt)
        {
            if (cell == (n * n))
            {
                if (cnt == n)
                {
                    Total++;
                }
                return;
            }

            // NOTE: don't know why the following is slower
            // if (cnt == n)
            // {
            //     // var solution = soFar.Select(x => string.Join("", x)).ToList();
            //     var solution=new List<string>();
            //     // Console.WriteLine("solution==>");
            //     // ShowChessboard(n, soFar);
            //     solutions.Add(solution);
            //     return;
            // }
            // if (cell == (n * n))
            // {
            //     return;
            // }

            var row = cell / n;
            var col = cell % n;

            var ok = checkAttack(n, row, col, soFar);
            if (ok)
            {
                soFar[row][col] = 'Q';
                TrySolveNQueens(n, cell + 1, soFar, cnt + 1);
            }
            soFar[row][col] = '.';
            TrySolveNQueens(n, cell + 1, soFar, cnt);
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
        static void Verify(int n, int exp)
        {
            Console.WriteLine($"{n}");
            int res;
            using (new Timeit())
            {
                res = new Solution().TotalNQueens(n);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("NQueens");
            int n;
            int exp;

            n = 4;
            exp = 2;
            Verify(n, exp);

            n = 7;
            exp = 40;
            Verify(n, exp);

            n = 8;
            exp = 92;
            Verify(n, exp);

            n = 9;
            exp = 352;
            Verify(n, exp);

        }
    }
}