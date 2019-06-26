using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace LongestValidParentheses
{
    public class Solution
    {
        // very slow version
        public int LongestValidParentheses2(string s)
        {
            int idx = 0;
            int longest = 0;
            while (true)
            {
                if (idx >= s.Length)
                {
                    break;
                }
                bool findFromIdx = false;
                for (var len = s.Length - idx; len > 1; len--)
                {
                    var sub = s.Substring(idx, len);
                    if (IsValid(sub))
                    {
                        findFromIdx = true;
                        if (len > longest)
                        {
                            longest = len;
                        }
                        idx += len;
                        break;
                    }
                }
                if (!findFromIdx)
                    idx++;
                while (true)
                {
                    if (idx == s.Length)
                    {
                        break;
                    }
                    if (s[idx] == '(')
                    {
                        break;
                    }
                    idx++;
                }
            }
            return longest;
        }
        public int LongestValidParentheses(string s)
        {
            int idx = 0;
            int longest = 0;

            while (true)
            {
                // find first '('
                while (true)
                {
                    if (idx >= s.Length)
                    {
                        break;
                    }
                    if (s[idx] == '(')
                    {
                        break;
                    }
                    idx++;
                }
                if (idx >= s.Length)
                {
                    break;
                }

                int sum = 0;
                int idx2 = idx;
                while (true)
                {
                    if (s[idx2] == '(')
                        sum += -1;
                    else
                        sum += 1;

                    int len = idx2 - idx + 1;
                    if (sum > 0)
                    {
                        // from idx to idx2 is invalid, 
                        // append anything will still be invalid
                        // so try index after idx2
                        idx = idx2 + 1;
                        break;
                    }
                    if (sum == 0)
                    {
                        if (len > longest)
                        {
                            longest = len;
                        }
                    }
                    if (idx2 == s.Length - 1)
                    {
                        // reach end without break
                        // try from next index
                        idx++;
                        break;
                    }
                    idx2++;
                }
            }
            return longest;
        }
        public static bool IsValid(string s)
        {
            int sum = 0;
            foreach (var c in s)
            {
                if (c == '(')
                    sum += -1;
                else
                    sum += 1;

                if (sum > 0)
                    return false;
            }
            if (sum == 0)
                return true;
            else
                return false;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("LongestValidParentheses");
            string input;
            int exp, res;

            input = "))";
            exp = 0;
            using (new Timeit())
            {
                res = new Solution().LongestValidParentheses(input);
            }
            Assert.Equal(exp, res);

            input = "(()";
            exp = 2; // The longest valid parentheses substring is "()"
            using (new Timeit())
            {
                res = new Solution().LongestValidParentheses(input);
            }
            Assert.Equal(exp, res);

            input = ")()())";
            exp = 4;
            using (new Timeit())
            {
                res = new Solution().LongestValidParentheses(input);
            }
            Assert.Equal(exp, res);

            input = "(()(()()()(()())()(()()))()()())()(((()())((())(()()((()((((())(())()()(())()(()(()(())))))))(()()()))()()))))))(()())))((())())))()(((()(()))())((())))(())(((()()))))())))((()((()()(()))())(()))(())((())()(((()(((()))))()))()()())()()()(()(()(()()()(()(())(())))())()))())(())((())(()((((())((())((())(()()(((()))(()(((())())()(())))(()))))))(()(()(()))())(()()(()(((()()))()(())))(()()(())))))(()(()()())))()()())))))((())(()()(((()(()()))(())))(((()))())())())(((()((()((()())((()))(()()((()(())())(()))()())())))))()(()())))()()))(((()(()))((()((((())((())))((())()()))())()(((()()(((()))))))(((()))()(()(((())(())()()()))))()))()))))()(()))()()()))))()(()))()()(()())))(()))()())(())()())(())()()))(()())))((()())))())))))((()))())()()))))()))(((())(())()))()()((()))(((()))))((()((()))(())(((()))()()()())())())))(()(((())()())(())(((()()((())()))(()()(((())))((()(((()))(((((()(((())())))(())(()()((()(()(())())(((((())((()()))())(()())))()()()(()(((((((())))(()()()()((()(((())())())())))())())())))()((((())(((()()()())()))()()(()(()()))()))(())(()())))))()())()())))()()(())))))))((())()()(((()))()))())))))((()())((()())(((())()())()))(()(()()(((()(())()))()())()()(())()(()))))()))()()))))(())(()()(())((()))(()))((())))))(())))()))(()()(())))())()((())()))((()()(()())()()(()))())(((())()(((()((())()(()()()((()(()())(()())())((((())))())())))(()))(((())((()))))((()()(((())((())()()()))((()())()()())())))))((((()((()())))(())(())()()()(((((())())()()()(())())()((()(()())(((())((((()((()(((()))(()()))())()()(()(()(())))()))())))(()()(()))))))(()()())()()))()(())()(";
            exp = 296;
            using (new Timeit())
            {
                res = new Solution().LongestValidParentheses(input);
            }
            Assert.Equal(exp, res);

            var fn = "0032-data.txt";
            using (var fs = File.OpenText(Path.Join(Directory.GetCurrentDirectory(), fn)))
            {
                input = fs.ReadToEnd().Trim('"');
            }
            exp = 2644;
            using (new Timeit())
            {
                res = new Solution().LongestValidParentheses(input);
            }
            Assert.Equal(exp, res);

            input = "))))())()()(()";
            exp = 4;
            using (new Timeit())
            {
                res = new Solution().LongestValidParentheses(input);
            }
            Assert.Equal(exp, res);

            input = ")(((((()())()()))()(()))(";
            exp = 22;
            using (new Timeit())
            {
                res = new Solution().LongestValidParentheses(input);
            }
            Assert.Equal(exp, res);
        }
    }
}