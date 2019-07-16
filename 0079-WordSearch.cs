using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
    public class Solution
    {
        public bool Exist(char[][] board, string word)
        {
            int row = board.Length;
            int col = board[0].Length;
            for (var i = 0; i <= (row * col - 1); i++)
            {
                var e = CheckExist(board, row, col, i, word, 0, new List<int>());
                if (e)
                {
                    return true;
                }
            }
            return false;
        }
        bool CheckExist(char[][] board, int row, int col, int cell, string word, int wi, List<int> soFar)
        {
            int r = cell / col;
            int c = cell % col;

            char w = word[wi];
            if (board[r][c] != w)
            {
                return false;
            }
            wi++;
            if (wi == word.Length)
            {
                return true;
            }
            var newSoFar = new List<int>(soFar);
            newSoFar.Add(cell);

            int nextCell;
            bool e;
            if (r > 0)
            {
                nextCell = (r - 1) * col + c;
                if (!soFar.Contains(nextCell))
                {
                    e = CheckExist(board, row, col, nextCell, word, wi, newSoFar);
                    if (e) return true;
                }
            }

            if (r < (row - 1))
            {
                nextCell = (r + 1) * col + c;
                if (!soFar.Contains(nextCell))
                {
                    e = CheckExist(board, row, col, nextCell, word, wi, newSoFar);
                    if (e) return true;
                }
            }

            if (c > 0)
            {
                nextCell = r * col + c - 1;
                if (!soFar.Contains(nextCell))
                {
                    e = CheckExist(board, row, col, nextCell, word, wi, newSoFar);
                    if (e) return true;
                }
            }

            if (c < (col - 1))
            {
                nextCell = r * col + c + 1;
                if (!soFar.Contains(nextCell))
                {
                    e = CheckExist(board, row, col, nextCell, word, wi, newSoFar);
                    if (e) return true;
                }
            }
            return false;
        }
    }
    public class Test
    {
        static void Verify(char[][] board, string word, bool exp)
        {
            Console.WriteLine($"{string.Join(" ", board[0])}");
            bool res;
            using (new Timeit())
            {
                res = new Solution().Exist(board, word);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine(typeof(Solution).Namespace);

            var input = @"
#[ ['A','B','C','E'], ['S','F','C','S'], ['A','D','E','E'] ]
#ABCCED
#true
#[ ['A','B','C','E'], ['S','F','C','S'], ['A','D','E','E'] ]
#SEE
#true
#[ ['A','B','C','E'], ['S','F','C','S'], ['A','D','E','E'] ]
#ABCB
#false
[['a']]
a
true
";
            var lines = input.CleanInput();
            char[][] board;
            string word;
            bool exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                board = lines[idx++].JsonToChar2d();
                word = lines[idx++];
                exp = bool.Parse(lines[idx++]);
                Verify(board, word, exp);
            }
        }
    }
}