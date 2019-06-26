using System;
using Xunit;

namespace LengthOfLongestSubstring
{
    public class Solution
    {
        public int LengthOfLongestSubstring(string s)
        {
            string b = "";
            int n = 0;
            for (var i = 0; i < s.Length; i++)
            {
                b = "";
                for (var j = i; j < s.Length; j++)
                {
                    if (b.Contains(s[j]))
                    {
                        break;
                    }
                    b = s.Substring(i, j - i + 1);
                }
                if (b.Length > n)
                {
                    n = b.Length;
                }
            }
            return n;
        }
    }
    public class Test
    {
        static public void Run()
        {
            string Input;
            int exp, res;

            Console.WriteLine("LengthOfLongestSubstring");

            Input = "abcabcbb";
            exp = 3;
            res = new Solution().LengthOfLongestSubstring(Input);
            Assert.Equal(exp, res);

            Input = "bbbbb";
            exp = 1;
            res = new Solution().LengthOfLongestSubstring(Input);
            Assert.Equal(exp, res);

            Input = "pwwkew";
            exp = 3;
            res = new Solution().LengthOfLongestSubstring(Input);
            Assert.Equal(exp, res);

            Input = " ";
            exp = 1;
            res = new Solution().LengthOfLongestSubstring(Input);
            Assert.Equal(exp, res);

            Input = "";
            exp = 0;
            res = new Solution().LengthOfLongestSubstring(Input);
            Assert.Equal(exp, res);

            Input = "au";
            exp = 2;
            res = new Solution().LengthOfLongestSubstring(Input);
            Assert.Equal(exp, res);
        }
    }
}