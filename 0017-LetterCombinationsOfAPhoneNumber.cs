using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace LetterCombinationsOfAPhoneNumber
{
    public class Solution
    {
        // 2abc
        // 3def
        // 4ghi
        // 5jkl
        // 6mno
        // 7pqrs
        // 8tuv
        // 9wxyz
        public IList<string> LetterCombinations(string digits)
        {
            Dictionary<char, char[]> map = new Dictionary<char, char[]>(){
               { '2', new char[]{'a','b','c'}},
               { '3', new char[]{'d','e','f'}},
               { '4', new char[]{'g','h','i'}},
               { '5', new char[]{'j','k','l'}},
               { '6', new char[]{'m','n','o'}},
               { '7', new char[]{'p','q','r','s'}},
               { '8', new char[]{'t','u','v'}},
               { '9', new char[]{'w','x','y','z'}},
            };

            List<string> res = new List<string>();
            for (var i = 0; i < digits.Length; i++)
            {
                res = LC(res, map[digits[i]]);
            }
            return res;
        }
        List<string> LC(List<string> ss, char[] cs)
        {
            var res = new List<string>();
            if (ss.Count == 0)
            {
                foreach (var c in cs)
                {
                    res.Add(c.ToString());
                }
            }
            else
            {
                foreach (var s in ss)
                {
                    foreach (var c in cs)
                    {
                        res.Add(string.Format("{0}{1}", s, c));
                    }
                }
            }
            return res;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("LetterCombinationsOfAPhoneNumber");

            string input;
            List<string> exp, res;

            using (new Timeit())
            {
                input = "23";
                exp = new List<string> { "ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf" };
                res = (List<string>)new Solution().LetterCombinations(input);
                // exp.Sort();
                // res.Sort();
                // Assert.Equal(exp, res);
            }
        }
    }
}