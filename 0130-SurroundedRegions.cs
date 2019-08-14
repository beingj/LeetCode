using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SurroundedRegions
{
    public class Solution
    {
        public void Solve(char[][] board)
        {
            if (board.Length <= 1)
                return;
            char X = 'X';
            char O = 'O';
            // var regs = new List<List<(int row, int col)>>();
            var regsCnt = 0;
            var regsOfCell = new Dictionary<(int row, int col), int>();
            var regsIsSur = new Dictionary<int, bool>();
            var maxRow = board.Length - 1;
            var maxCol = board[0].Length - 1;
            for (var r = 0; r < board.Length; r++)
            {
                for (var c = 0; c < board[r].Length; c++)
                {
                    if (board[r][c] == X)
                        continue;
                    // O
                    var isSur = true;
                    if ((r == 0) || (c == 0) || (r == maxRow) || (c == maxCol))
                    {
                        isSur = false;
                    }
                    var inReg = false;
                    var regId = regsCnt;
                    // up
                    if ((r > 0) && (board[r - 1][c] == O))
                    {
                        inReg = true;
                        regId = regsOfCell[(r - 1, c)];
                    }
                    // left
                    if ((c > 0) && (board[r][c - 1] == O))
                    {
                        if (inReg)
                        {
                            // combo up and left regins
                            var regId2 = regsOfCell[(r, c - 1)];
                            if (regId2 != regId)
                            {
                                if (regsIsSur[regId2] == false)
                                    regsIsSur[regId] = false;
                                // regsOfCell[(r, c - 1)] = regId;
                                var cells = regsOfCell.Where(i => i.Value == regId2).Select(j => j.Key).ToList();
                                foreach (var i in cells)
                                {
                                    regsOfCell[i] = regId;
                                }
                            }
                        }
                        else
                        {
                            inReg = true;
                            regId = regsOfCell[(r, c - 1)];
                        }
                    }

                    if (!inReg)
                    {
                        regsIsSur[regId] = true;
                        regsCnt++;
                    }
                    regsOfCell[(r, c)] = regId;

                    if (isSur == false)
                        regsIsSur[regId] = false;
                }
            }

            var q = regsOfCell.Where(i => regsIsSur[i.Value] == true).Select(j => j.Key);
            foreach (var rc in q)
            {
                board[rc.row][rc.col] = 'X';
            }
        }
    }
    public class Solution1
    {
        public void Solve(char[][] board)
        {
            var oDict = new Dictionary<(int row, int col), bool>();
            while (true)
            {
                var oCnt = 0;
                for (var r = 0; r < board.Length; r++)
                {
                    for (var c = 0; c < board[r].Length; c++)
                    {
                        if (board[r][c] == 'X')
                            continue;
                        oCnt++;
                        if (oDict.ContainsKey((r, c)))
                            continue;
                        CheckO(board, r, c, oDict);
                    }
                }
                if (oDict.Keys.Count == oCnt)
                {
                    break;
                }
            }

            var q = oDict.Where(i => i.Value == true).Select(j => j.Key);
            foreach (var rc in q)
            {
                board[rc.row][rc.col] = 'X';
            }
        }
        void CheckO(char[][] board, int r, int c, Dictionary<(int row, int col), bool> oDict)
        {
            var surrounded = 0;
            // up
            for (var i = r - 1; i >= 0; i--)
            {
                // if ((board[i][c] == 'X') || (oDict.GetValueOrDefault((i, c), false)))
                if (board[i][c] == 'X')
                {
                    surrounded += 1;
                    break;
                }
                else
                {
                    if (oDict.ContainsKey((i, c)))
                    {
                        if (!oDict[(i, c)])
                        {
                            oDict[(i, c)] = false;
                            return;
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
                }
            }

            // down
            for (var i = r + 1; i < board.Length; i++)
            {
                if ((board[i][c] == 'X') || (oDict.GetValueOrDefault((i, c), false)))
                {
                    surrounded += 1;
                    break;
                }
            }

            // left
            for (var i = c - 1; i >= 0; i--)
            {
                if ((board[r][i] == 'X') || (oDict.GetValueOrDefault((r, i), false)))
                {
                    surrounded += 1;
                    break;
                }
            }

            // right
            for (var i = c + 1; i < board[r].Length; i++)
            {
                if ((board[r][i] == 'X') || (oDict.GetValueOrDefault((r, i), false)))
                {
                    surrounded += 1;
                    break;
                }
            }

            oDict[(r, c)] = surrounded == 4 ? true : false;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
";
            var lines = input.CleanInput();
            lines = "0130-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines, checkParaIndex: 0);
        }
    }
}