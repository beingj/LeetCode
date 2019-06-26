using System;
using Xunit;
using Util;
using System.Text;

namespace LongestCommonPrefix
{
    public class Solution
    {
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 0)
            {
                return "";
            }
            if (strs.Length == 1)
            {
                return strs[0];
            }
            int min = strs[0].Length;
            for (var i = 1; i < strs.Length; i++)
            {
                if (strs[i].Length < min)
                    min = strs[i].Length;
            }

            for (var i = 0; i < min; i++)
            {
                char c = strs[0][i];
                for (var j = 1; j < strs.Length; j++)
                {
                    if (strs[j][i] != c)
                    {
                        return strs[0].Substring(0, i);
                    }
                }
            }

            return strs[0].Substring(0, min);
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("LongestCommonPrefix");

            string[] input;
            string exp, res;

            using (new Timeit())
            {
                input = new string[] { "flower", "flow", "flight" };
                exp = "fl";
                res = new Solution().LongestCommonPrefix(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = new string[] { "dog", "racecar", "car" };
                exp = "";
                res = new Solution().LongestCommonPrefix(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = new string[] { "dog", "dogracecar", "dogcar" };
                exp = "dog";
                res = new Solution().LongestCommonPrefix(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = new string[] { "dogracecar", "dog", "dogcar" };
                exp = "dog";
                res = new Solution().LongestCommonPrefix(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = new string[] { };
                exp = "";
                res = new Solution().LongestCommonPrefix(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = new string[] { "a" };
                exp = "a";
                res = new Solution().LongestCommonPrefix(input);
                Assert.Equal(exp, res);
            }
        }
    }
}