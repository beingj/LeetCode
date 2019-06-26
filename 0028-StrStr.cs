using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace StrStr
{
    public class Solution
    {
        public int StrStr(string haystack, string needle)
        {
            if (needle == "")
                return 0;
            if (haystack.Length < needle.Length)
                return -1;
            for (var i = 0; i < haystack.Length; i++)
            {
                if (haystack[i] == needle[0])
                {
                    bool isSame = true;
                    for (var j = 1; j < needle.Length; j++)
                    {
                        int idx = i + j;
                        if (idx > haystack.Length - 1)
                            return -1;
                        if (haystack[idx] != needle[j])
                        {
                            isSame = false;
                            break;
                        }
                    }
                    if (isSame)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("StrStr");
            string haystack, needle;
            int exp, res;

            // Example 1:
            // Input: haystack = "hello", needle = "ll"
            // Output: 2
            haystack = "hello";
            needle = "ll";
            exp = 2;
            using (new Timeit())
            {
                res = new Solution().StrStr(haystack, needle);
            }
            Assert.Equal(exp, res);

            // Example 2:
            // Input: haystack = "aaaaa", needle = "bba"
            // Output: -1
            haystack = "aaaaa";
            needle = "bba";
            exp = -1;
            using (new Timeit())
            {
                res = new Solution().StrStr(haystack, needle);
            }
            Assert.Equal(exp, res);

            haystack = "aaaaa";
            needle = "";
            exp = 0;
            using (new Timeit())
            {
                res = new Solution().StrStr(haystack, needle);
            }
            Assert.Equal(exp, res);

            haystack = "aaa";
            needle = "aaaa";
            exp = -1;
            using (new Timeit())
            {
                res = new Solution().StrStr(haystack, needle);
            }
            Assert.Equal(exp, res);

            haystack = "mississippi";
            needle = "issipi";
            exp = -1;
            using (new Timeit())
            {
                res = new Solution().StrStr(haystack, needle);
            }
            Assert.Equal(exp, res);
        }
    }
}