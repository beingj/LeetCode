using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;

namespace ValidParentheses
{
    public class Solution
    {
        public bool IsValid(string s)
        {
            Dictionary<char, char> map = new Dictionary<char, char>{
                {')','('},
                {']','['},
                {'}','{'},
            };
            Stack parentheses = new Stack();
            foreach (var c in s)
            {
                if (map.ContainsValue(c))
                {
                    parentheses.Push(c);
                    continue;
                }
                if (map.ContainsKey(c))
                {
                    if (parentheses.Count == 0)
                        return false;
                    if ((char)parentheses.Pop() != map[c])
                        return false;
                }
            }
            if (parentheses.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("IsValid");

            string input;
            bool exp, res;

            using (new Timeit())
            {
                input = "()";
                exp = true;
                res = new Solution().IsValid(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "()[]{}";
                exp = true;
                res = new Solution().IsValid(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "(]";
                exp = false;
                res = new Solution().IsValid(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "([)]";
                exp = false;
                res = new Solution().IsValid(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "{[]}";
                exp = true;
                res = new Solution().IsValid(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "]";
                exp = false;
                res = new Solution().IsValid(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "[";
                exp = false;
                res = new Solution().IsValid(input);
                Assert.Equal(exp, res);
            }
        }
    }
}