using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;

namespace EditDistance
{
    public class Solution
    {
        public int MinDistance(string word1, string word2)
        {
            // give up, my version (Solution1) is too slow
            // copy from: https://leetcode.com/problems/edit-distance/discuss/25846/C%2B%2B-O(n)-space-DP
            // explain: https://leetcode-cn.com/problems/edit-distance/comments/
            int m = word1.Length, n = word2.Length;
            var dp = new int[m + 1][];
            dp[0] = new int[n + 1];
            for (int i = 1; i <= m; i++)
            {
                dp[i] = new int[n + 1];
                dp[i][0] = i;
            }
            for (int j = 1; j <= n; j++)
            {
                dp[0][j] = j;
            }
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (word1[i - 1] == word2[j - 1])
                    {
                        dp[i][j] = dp[i - 1][j - 1];
                    }
                    else
                    {
                        dp[i][j] = Math.Min(dp[i - 1][j - 1], Math.Min(dp[i][j - 1], dp[i - 1][j])) + 1;
                    }
                }
            }
            return dp[m][n];
        }
    }
    public class Solution1
    {
        public struct StrIdx
        {
            public int Start;
            public int End;
            public int Len
            {
                get
                {
                    return End - Start + 1;
                }
            }
        }
        public struct MatchPat
        {
            public StrIdx Word1Head;
            public StrIdx Word1Body;
            public StrIdx Word1Tail;
            public StrIdx Word2Head;
            public StrIdx Word2Body;
            public StrIdx Word2Tail;
            public int Len;
        }
        public int MinDistance(string word1, string word2)
        {
            var ps = FindMatchPats(word1, word2);
            if (ps.Count == 0)
            {
                if (word1.Length < word2.Length)
                {
                    return word2.Length - word1.Length // insert
                            + word1.Length; //replace
                }
                else
                {
                    return word1.Length - word2.Length // remove
                            + word2.Length; //replace
                }
            }
            int minD = Math.Max(word1.Length, word2.Length);
            return ps.Select(p => CheckPat(word1, word2, p, ref minD)).Min();
        }
        public int CheckPat(string word1, string word2, MatchPat p, ref int minD)
        {
            int headCount = 0, bodyCount = 0, tailCount = 0;
            bodyCount = p.Word1Body.Len - p.Word2Body.Len;
            if (bodyCount >= minD)
            {
                return bodyCount;
            }

            if (p.Word2Head.Len > 0)
            {
                if (p.Word1Head.Len > 0)
                {
                    var md = MinDistance(
                                    word1.Substring(p.Word1Head.Start, p.Word1Head.Len),
                                    word2.Substring(p.Word2Head.Start, p.Word2Head.Len));
                    headCount = Math.Min(md, Math.Max(p.Word1Head.Len, p.Word2Head.Len));
                }
                else
                {
                    // p.Word1Head.Len == 0
                    headCount = p.Word2Head.Len; // add if any
                }
            }
            else
            {
                // p.Word2Head.Len == 0
                headCount = p.Word1Head.Len; // remove if any
            }

            if (p.Word2Tail.Len > 0)
            {
                if (p.Word1Tail.Len > 0)
                {
                    var md = MinDistance(
                                    word1.Substring(p.Word1Tail.Start, p.Word1Tail.Len),
                                    word2.Substring(p.Word2Tail.Start, p.Word2Tail.Len));
                    tailCount = Math.Min(md, Math.Max(p.Word1Tail.Len, p.Word2Tail.Len));
                }
                else
                {
                    // p.Word1Tail.Len == 0
                    tailCount = p.Word2Tail.Len; // add if any
                }
            }
            else
            {
                // p.Word2Tail.Len == 0
                tailCount = p.Word1Tail.Len; // remove if any
            }

            int dist = headCount + bodyCount + tailCount;

            return dist;
        }
        List<MatchPat> FindMatchPats(string word1, string word2)
        {
            var pats = new List<MatchPat>();
            for (var i = 0; i < word2.Length; i++)
            {
                for (var j = word2.Length - 1; j >= i; j--)
                {
                    var len = j - i + 1;
                    var sub = word2.Substring(i, len);
                    int start, end;
                    bool find = FindSmallestSubInStr(sub, word1, out start, out end);
                    if (find)
                    {
                        var pat = new MatchPat();
                        if (start > 0)
                        {
                            pat.Word1Head.Start = 0;
                            pat.Word1Head.End = start - 1;
                        }
                        else
                        {
                            pat.Word1Head.Start = -1;
                            pat.Word1Head.End = pat.Word1Head.Start - 1; // len=0
                        }
                        pat.Word1Body.Start = start;
                        pat.Word1Body.End = end;
                        pat.Word1Tail.Start = end + 1;
                        if (pat.Word1Tail.Start < word1.Length)
                        {
                            pat.Word1Tail.End = word1.Length - 1;
                        }
                        else
                        {
                            pat.Word1Tail.End = pat.Word1Tail.Start - 1; // len=0
                        }

                        if (i > 0)
                        {
                            pat.Word2Head.Start = 0;
                            pat.Word2Head.End = i - 1;
                        }
                        else
                        {
                            pat.Word2Head.Start = -1;
                            pat.Word2Head.End = pat.Word2Head.Start - 1; // len=0
                        }
                        pat.Word2Body.Start = i;
                        pat.Word2Body.End = j;
                        pat.Word2Tail.Start = j + 1;
                        if (pat.Word2Tail.Start < word2.Length)
                        {
                            pat.Word2Tail.End = word2.Length - 1;
                        }
                        else
                        {
                            pat.Word2Tail.End = pat.Word2Tail.Start - 1; //len=0
                        }
                        pat.Len = len;
                        pats.Add(pat);
                    }
                }
            }
            // pats.Count will be 0 if no match pat found
            return pats;
        }
        bool FindSmallestSubInStr(string sub, string word1, out int start, out int end)
        {
            var found = FindSubInStr(sub, word1, out start, out end);
            if (!found)
            {
                return false;
            }

            var matchLen = end - start + 1;
            if (matchLen > sub.Length)
            {
                int s, e;
                for (var i = start + 1; i <= (word1.Length - sub.Length); i++)
                {
                    var len = Math.Min(word1.Length - i, matchLen - 1);
                    var f = FindSubInStr(sub, word1.Substring(i, len), out s, out e);
                    if (f)
                    {
                        start = i + s;
                        end = i + e;
                        matchLen = end - start + 1;
                    }
                }
            }
            return true;
        }
        bool FindSubInStr(string sub, string str, out int start, out int end)
        {
            start = -1;
            end = -1;
            int idx = 0;
            foreach (var c in sub)
            {
                var i = str.IndexOf(c, idx);
                if (i < 0)
                {
                    return false;
                }
                if (start < 0)
                {
                    start = i;
                }
                idx = i + 1;
            }
            end = idx - 1;
            return true;
        }
    }

    public class Test
    {
        static void Verify(string word1, string word2, int exp)
        {
            Console.WriteLine($"{word1} => {word2}");
            int res;
            using (new Timeit())
            {
                res = new Solution().MinDistance(word1, word2);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("EditDistance");

            var input = @"
horse
ros
3
intention
execution
5
industry
interest
6
dinitrophenylhydrazine
acetylphenylhydrazine
6
";
            var lines = input.CleanInput();
            string word1, word2;
            int exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                word1 = lines[idx++];
                word2 = lines[idx++];
                exp = int.Parse(lines[idx++]);
                Verify(word1, word2, exp);
            }
        }
    }
}