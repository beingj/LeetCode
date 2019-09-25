using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DungeonGame
{
    public class Solution
    {
        public int CalculateMinimumHP(int[][] dungeon)
        {
            // copy from: https://leetcode.com/problems/dungeon-game/discuss/52774/C++-DP-solution/53817
            int n = dungeon.Length, m = dungeon[0].Length;
            // vector<int> dp(n+1,INT_MAX);
            var dp = Enumerable.Repeat(int.MaxValue, n + 1).ToArray();
            dp[n - 1] = 1;
            for (int j = m - 1; j >= 0; j--)
            {
                for (int i = n - 1; i >= 0; i--)
                {
                    dp[i] = Math.Min(dp[i], dp[i + 1]) - dungeon[i][j];
                    dp[i] = Math.Max(1, dp[i]);
                }
            }
            return dp[0];
        }
    }
    public class Solution2
    {
        public int CalculateMinimumHP2(int[][] dungeon)
        {
            // give up
            // copy from: https://leetcode.com/problems/dungeon-game/discuss/52774/C%2B%2B-DP-solution
            int M = dungeon.Length;
            int N = dungeon[0].Length;
            // hp[i][j] represents the min hp needed at position (i, j)
            // Add dummy row and column at bottom and right side
            // var hp = new int[M + 1][];
            // for (var i = 0; i < hp.Length; i++)
            // {
            //     hp[i] = new int[N + 1];
            //     for (var j = 0; j < hp[i].Length; j++)
            //     {
            //         hp[i][j] = int.MaxValue;
            //     }
            // }
            // var hp = Enumerable.Range(0, M + 1).Select(i => Enumerable.Range(0, N + 1).Select(j => int.MaxValue).ToArray()).ToArray();
            var hp = Enumerable.Range(0, M + 1).Select(i => Enumerable.Repeat(1, N + 1).ToArray()).ToArray();
            // var hp = new int[M + 1][];
            // Array.Fill(hp, new int[N + 1]);

            hp[M][N - 1] = 1;
            hp[M - 1][N] = 1;
            for (int i = M - 1; i >= 0; i--)
            {
                for (int j = N - 1; j >= 0; j--)
                {
                    int need = Math.Min(hp[i + 1][j], hp[i][j + 1]) - dungeon[i][j];
                    hp[i][j] = need <= 0 ? 1 : need;
                }
            }
            return hp[0][0];
        }
    }
    public class Solution1
    {
        int MinX = 0;
        public int CalculateMinimumHP(int[][] dungeon)
        {
            MinX = 0;
            Walk(dungeon, 0, 0, 0, 0);
            return MinX > 0 ? MinX : 1;
        }
        void Walk(int[][] dungeon, int r, int c, int current, int x)
        {
            if (MinX > 0 && x >= MinX) return;
            current += dungeon[r][c];
            if (current <= 0)
            {
                x += 0 - current + 1;
                current = 1;
            }
            if ((r == (dungeon.Length - 1)) && (c == (dungeon[0].Length - 1)))
            {
                if (MinX == 0)
                {
                    MinX = x;
                }
                else
                {
                    MinX = Math.Min(MinX, x);
                }
                return;
            }
            if (r < (dungeon.Length - 1))
            {
                Walk(dungeon, r + 1, c, current, x);
            }
            if (c < (dungeon[0].Length - 1))
            {
                Walk(dungeon, r, c + 1, current, x);
            }
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[[100]]
1
[[-2,-3,3],[-5,-10,1],[10,30,-5]]
7
";
            var lines = input.CleanInput();
            lines = "0174-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
            Verify.Method(new Solution2(), lines);
        }
    }
}