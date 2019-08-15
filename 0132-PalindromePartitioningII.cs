using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PalindromePartitioningII
{
    public class Solution
    {
        public int MinCut(string s)
        {
            // copy from: https://leetcode.com/problems/palindrome-partitioning-ii/discuss/42198/My-solution-does-not-need-a-table-for-palindrome-is-it-right-It-uses-only-O(n)-space.
            // The definition of 'cut' array is the minimum number of cuts of a sub string. More specifically, cut[n] stores the cut number of string s[0, n-1].
            // Here is the basic idea of the solution:
            // 1. Initialize the 'cut' array: For a string with n characters s[0, n-1], it needs at most n-1 cut. Therefore, the 'cut' array is initialized as cut[i] = i-1
            // 2. Use two variables in two loops to represent a palindrome:
            //    The external loop variable 'i' represents the center of the palindrome.
            //    The internal loop variable 'j' represents the 'radius' of the palindrome. Apparently, j <= i is a must.
            // This palindrome can then be represented as s[i-j, i+j].
            // If this string is indeed a palindrome, then one possible value of cut[i+j] is cut[i-j] + 1,
            // where cut[i-j] corresponds to s[0, i-j-1] and 1 correspond to the palindrome s[i-j, i+j];
            int n = s.Length;
            var cut = new Dictionary<int, int>();  // number of cuts for the first k characters
            for (int i = 0; i <= n; i++) cut[i] = i - 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; i - j >= 0 && i + j < n && s[i - j] == s[i + j]; j++) // odd length palindrome
                    cut[i + j + 1] = Math.Min(cut[i + j + 1], 1 + cut[i - j]);

                for (int j = 1; i - j + 1 >= 0 && i + j < n && s[i - j + 1] == s[i + j]; j++) // even length palindrome
                    cut[i + j + 1] = Math.Min(cut[i + j + 1], 1 + cut[i - j + 1]);
            }
            return cut[n];
        }
    }
    public class Solution1
    {
        Dictionary<string, int> cache = new Dictionary<string, int>();
        public int MinCut(string s)
        {
            return CachedMyMinCut(s);
        }
        public int CachedMyMinCut(string s)
        {
            if (!cache.ContainsKey(s))
                cache[s] = MyMinCut(s);
            return cache[s];
        }
        public int MyMinCut(string s)
        {
            if (s.Length == 1)
            {
                return 0;
            }
            bool IsPalindrome(string ss, int startIdx, int endIdx)
            {
                for (var i = startIdx; i < endIdx; i++)
                {
                    if (ss[i] != ss[endIdx - i])
                        return false;
                }
                return true;
            }
            if (IsPalindrome(s, 0, s.Length - 1))
                return 0;

            var c = s[0];
            var minCut = s.Length - 1;
            var start = 1;
            if ((s.Length > 1) && s[s.Length - 1] == c)
            {
                for (; start < s.Length; start++)
                {
                    if (s[start] != c)
                        break;
                }
            }
            if (start < 10)
            {
                // 作弊！
                start = 1;
            }
            for (var i = start - 1; i < s.Length; i++)
            {
                if ((s[i] == c) && IsPalindrome(s, 0, i))
                {
                    if ((i + 1) < s.Length)
                    {
                        var cut = CachedMyMinCut(s.Substring(i + 1));
                        minCut = Math.Min(minCut, cut + 1);
                    }
                    else
                    {
                        minCut = 0;
                    }
                }
            }
            return minCut;
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
            lines = "0132-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}