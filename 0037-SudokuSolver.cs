using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace SudokuSolver
{
    public class SolutionFromSO
    {
        // https://codereview.stackexchange.com/questions/37430/sudoku-solver-in-c
        static string Digits = "0123456789";
        public void SolveSudoku(char[][] board)
        {
            TrySolveSudoku(board, 0, 0);
        }
        bool IsAvailable(char[][] puzzle, int row, int col, int num)
        {
            int rowStart = (row / 3) * 3;
            int colStart = (col / 3) * 3;
            int i;

            for (i = 0; i < 9; ++i)
            {
                if (puzzle[row][i] == Digits[num]) return false;
                if (puzzle[i][col] == Digits[num]) return false;
                if (puzzle[rowStart + (i % 3)][colStart + (i / 3)] == Digits[num]) return false;
            }
            return true;
        }
        bool TrySolveSudoku(char[][] puzzle, int row, int col)
        {
            if (row < 9 && col < 9)
            {
                if (puzzle[row][col] != '.')
                {
                    if ((col + 1) < 9) return TrySolveSudoku(puzzle, row, col + 1);
                    else if ((row + 1) < 9) return TrySolveSudoku(puzzle, row + 1, 0);
                    else return true;
                }
                else
                {
                    for (var i = 1; i <= 9; ++i)
                    {
                        if (IsAvailable(puzzle, row, col, i))
                        {
                            puzzle[row][col] = Digits[i];
                            if (TrySolveSudoku(puzzle, row, col)) return true;
                            else puzzle[row][col] = '.';
                        }
                    }
                }
                return false;
            }
            else return true;
        }
    }
    public class Solution
    {
        public void SolveSudoku(char[][] board)
        {
            TryRowCol(board, 0, 0);
        }
        public bool TryRowCol(char[][] board, int row, int col)
        {
            if (row < 9 && col < 9)
            {
                if (board[row][col] != '.')
                {
                    if (col < 8)
                    {
                        return TryRowCol(board, row, col + 1);
                    }
                    else if (row < 8)
                    {
                        return TryRowCol(board, row + 1, 0);
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    List<char> ds = FoundValidDigits(board, row, col);
                    foreach (var d in ds)
                    {
                        board[row][col] = d;
                        if (TryRowCol(board, row, col))
                        {
                            return true;
                        }
                        else
                        {
                            board[row][col] = '.';
                        }
                    }
                }
                return false;
            }
            return true;
        }
        public List<char> FoundValidDigits(char[][] board, int row, int col)
        {
            var all = "123456789".ToList();
            char d;
            for (var i = 0; i < 9; i++)
            {
                // row
                d = board[row][i];
                if (d != '.')
                    all.Remove(d);
                // col
                d = board[i][col];
                if (d != '.')
                    all.Remove(d);
            }

            // 3x3 sub-boxes
            int r = row / 3;
            int c = col / 3;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    d = board[r * 3 + i][c * 3 + j];
                    if (d != '.')
                        all.Remove(d);
                }
            }
            // Console.WriteLine($"{row},{col} => {string.Join(',', all)}");
            return all;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("SudokuSolver");
            char[][] input;
            char[][] exp;

            Console.WriteLine("solution from me");
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
            exp = new char[][]{
                new char[] {'5','3','4','6','7','8','9','1','2'},
                new char[] {'6','7','2','1','9','5','3','4','8'},
                new char[] {'1','9','8','3','4','2','5','6','7'},
                new char[] {'8','5','9','7','6','1','4','2','3'},
                new char[] {'4','2','6','8','5','3','7','9','1'},
                new char[] {'7','1','3','9','2','4','8','5','6'},
                new char[] {'9','6','1','5','3','7','2','8','4'},
                new char[] {'2','8','7','4','1','9','6','3','5'},
                new char[] {'3','4','5','2','8','6','1','7','9'}
            };
            using (new Timeit())
            {
                new Solution().SolveSudoku(input);
            }
            Assert.Equal(exp, input);
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
            using (new Timeit())
            {
                new Solution().SolveSudoku(input);
            }
            Assert.Equal(exp, input);

            Console.WriteLine("solution from SO");
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
            using (new Timeit())
            {
                new SolutionFromSO().SolveSudoku(input);
            }
            Assert.Equal(exp, input);
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
            using (new Timeit())
            {
                new SolutionFromSO().SolveSudoku(input);
            }
            Assert.Equal(exp, input);
        }
    }
}