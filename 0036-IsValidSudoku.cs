using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace IsValidSudoku
{
    public class Solution
    {
        public bool IsValidSudoku(char[][] board)
        {
            Dictionary<char, bool> found = new Dictionary<char, bool>();
            char[] digits = "123456789".ToCharArray();
            foreach (var c in digits)
            {
                found.Add(c, false);
            }
            // row
            for (var i = 0; i < 9; i++)
            {
                foreach (var c in digits)
                {
                    found[c] = false;
                }
                for (int j = 0; j < 9; j++)
                {
                    var c = board[i][j];
                    if (c == '.')
                        continue;
                    if (found[c])
                    {
                        return false;
                    }
                    found[c] = true;
                }
            }
            // col
            for (var i = 0; i < 9; i++)
            {
                foreach (var c in digits)
                {
                    found[c] = false;
                }
                for (int j = 0; j < 9; j++)
                {
                    var c = board[j][i];
                    if (c == '.')
                        continue;
                    if (found[c])
                    {
                        return false;
                    }
                    found[c] = true;
                }
            }
            // 3x3 sub-boxes
            for (var i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    foreach (var c in digits)
                    {
                        found[c] = false;
                    }
                    for (int m = 0; m < 3; m++)
                    {
                        for (int n = 0; n < 3; n++)
                        {
                            var c = board[i * 3 + m][j * 3 + n];
                            if (c == '.')
                                continue;
                            if (found[c])
                            {
                                return false;
                            }
                            found[c] = true;
                        }
                    }
                }
            }

            return true;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("IsValidSudoku");
            char[][] input;
            bool exp, res;

            input = new char[][]{
                new char[] {'5','3','.','.','7','.','.','.','.'},
                new char[] {'6','.','.','1','9','5','.','.','.'},
                new char[] {'.','9','8','.','.','.','.','6','.'},
                new char[] {'8','.','.','.','6','.','.','.','3'},
                new char[] {'4','.','.','8','.','3','.','.','1'},
                new char[] {'7','.','.','.','2','.','.','.','6'},
                new char[] {'.','6','.','.','.','.','2','8','.'},
                new char[] {'.','.','.','4','1','9','.','.','5'},
                new char[] {'.','.','.','.','8','.','.','7','9'}
            };
            exp = true;
            using (new Timeit())
            {
                res = new Solution().IsValidSudoku(input);
            }
            Assert.Equal(exp, res);

            input = new char[][]{
                new char[] {'8','3','.','.','7','.','.','.','.'},
                new char[] {'6','.','.','1','9','5','.','.','.'},
                new char[] {'.','9','8','.','.','.','.','6','.'},
                new char[] {'8','.','.','.','6','.','.','.','3'},
                new char[] {'4','.','.','8','.','3','.','.','1'},
                new char[] {'7','.','.','.','2','.','.','.','6'},
                new char[] {'.','6','.','.','.','.','2','8','.'},
                new char[] {'.','.','.','4','1','9','.','.','5'},
                new char[] {'.','.','.','.','8','.','.','7','9'}
            };
            exp = false;
            using (new Timeit())
            {
                res = new Solution().IsValidSudoku(input);
            }
            Assert.Equal(exp, res);
        }
    }
}