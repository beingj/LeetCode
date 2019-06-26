using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GenerateParentheses
{
    public class Solution1
    {
        public IList<string> GenerateParenthesis(int n)
        {
            if (n == 1)
            {
                return new List<string> { "()" };
            }
            int m = n - 1;
            char[] s = new char[m * 2];
            for (var i = 0; i < m; i++)
            {
                s[i] = '(';
            }
            for (var i = m; i < 2 * m; i++)
            {
                s[i] = ')';
            }
            List<char[]> res = Permutation(CopyStr(s), 0, IsValid, IsPreconditionValid);

            List<string> res2 = new List<string>();
            foreach (var i in res)
            {
                var j = PadParentheses(i);
                // if (res2.Contains(j))
                //     continue;
                res2.Add(j);
            }
            // Console.WriteLine($"{n}: {res.Count} => {res2.Count}");
            return res2;
        }
        static string PadParentheses(char[] s)
        {
            var res = new char[s.Length + 2];
            for (var i = 0; i < s.Length; i++)
            {
                res[i + 1] = s[i];
            }
            res[0] = '(';
            res[s.Length + 1] = ')';
            return new string(res);
            // return string.Format("({0})", new string(s));
        }
        public static bool IsPreconditionValid(char[] s, int idx)
        {
            int sum = -1;
            for (var i = 0; i <= idx; i++)
            {
                if (s[i] == '(')
                    sum += -1;
                else
                    sum += 1;

                if (sum > 0)
                    return false;
            }
            return true;
        }
        public static bool IsValid(char[] s)
        {
            int sum = -1;
            foreach (var c in s)
            {
                if (c == '(')
                    sum += -1;
                else
                    sum += 1;

                if (sum > 0)
                    return false;
            }
            if (sum == -1)
                return true;
            else
                return false;
        }
        public static List<char[]> Permutation(char[] s, int startIdx, Func<char[], bool> isValid = null, Func<char[], int, bool> isPreconditionValid = null)
        {
            if (startIdx == s.Length - 1)
            {
                // Console.WriteLine(s);
                return new List<char[]> { s };
            }
            var res = new List<char[]>();
            for (var i = startIdx; i < s.Length; i++)
            {
                var isSwap = IsSwap(s, startIdx, i);
                // Console.WriteLine($"{new string(s)} {startIdx} {i} => {isSwap}");
                var tmp = new string(s);
                if (isSwap)
                {
                    SwapChars(s, startIdx, i);
                    char[] s2 = CopyStr(s);
                    if ((isPreconditionValid != null) && !isPreconditionValid(s2, startIdx))
                    {
                        continue;
                    }
                    if ((isValid == null) || isValid(s2))
                    {
                        // Console.WriteLine($"{tmp} {startIdx} {i} {isSwap} => {new string(s2)}");
                        // res.Add(s2);
                    }

                    var x = Permutation(CopyStr(s2), startIdx + 1, isValid, isPreconditionValid);
                    // if (x != null)
                    // {
                    res.AddRange(x);
                    // }
                    SwapChars(s, startIdx, i); // swap back to original s
                }
            }
            return res;
        }
        public static void SwapChars(char[] s, int a, int b)
        {
            char c = s[a];
            s[a] = s[b];
            s[b] = c;
        }
        static bool IsSwap(char[] s, int startIdx, int endIdx)
        {
            char c = s[endIdx];
            for (var i = startIdx; i < endIdx; i++)
            {
                if (s[i] == c)
                    return false;
            }
            return true;
        }
        static char[] CopyStr(char[] src)
        {
            int len = src.Length;
            char[] dst = new char[len];
            for (var i = 0; i < len; i++)
            {
                dst[i] = src[i];
            }
            return dst;
        }
    }
    public class Solution
    {
        public IList<string> GenerateParenthesis(int n)
        {
            if (n == 1)
            {
                return new List<string> { "()" };
            }
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < n - 1; i++)
            {
                sb.Append("()");
            }
            List<string> res = Solution.Permutation(sb.ToString(), 0, IsValid, IsPreconditionValid);
            List<string> res2 = new List<string>();
            foreach (var i in res)
            {
                var j = string.Format("({0})", i);
                res2.Add(j);
            }
            // Console.WriteLine($"{n}: {res.Count} => {res2.Count}");
            return res2;
        }
        public static List<string> Permutation(string s, int startIdx, Func<string, bool> isValid = null, Func<string, int, bool> isPreconditionValid = null)
        {
            if (startIdx == s.Length - 1)
            {
                if ((isValid == null) || isValid(s))
                {
                    return new List<string> { s };
                }
                return new List<string>();
            }
            var res = new List<string>();
            char[] cs = s.ToCharArray();
            for (var i = startIdx; i < s.Length; i++)
            {
                if (IsDuplicated(s, startIdx, i))
                {
                    continue;
                }
                SwapChars(cs, startIdx, i);
                var s2 = new string(cs);

                if ((isPreconditionValid != null) && !isPreconditionValid(s2, startIdx))
                {
                    // skip remain chars from startIdx + 1
                    continue;
                }
                var x = Permutation(s2, startIdx + 1, isValid, isPreconditionValid);
                res.AddRange(x);
                SwapChars(cs, startIdx, i); // swap back to original s
            }
            return res;
        }
        public static void SwapChars(char[] cs, int a, int b)
        {
            char c = cs[a];
            cs[a] = cs[b];
            cs[b] = c;
        }
        static bool IsDuplicated(string s, int startIdx, int endIdx)
        {
            return s.Substring(startIdx, endIdx - startIdx).Contains(s[endIdx]);
        }
        public static bool IsPreconditionValid(string s, int idx)
        {
            int sum = -1;
            for (var i = 0; i <= idx; i++)
            {
                if (s[i] == '(')
                    sum += -1;
                else
                    sum += 1;

                if (sum > 0)
                    return false;
            }
            return true;
        }
        public static bool IsValid(string s)
        {
            int sum = -1;
            foreach (var c in s)
            {
                if (c == '(')
                    sum += -1;
                else
                    sum += 1;

                if (sum > 0)
                    return false;
            }
            if (sum == -1)
                return true;
            else
                return false;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("GenerateParentheses");
            // Assert.False(Solution.IsValid("))((".ToCharArray()));
            // return;
            // Assert.True(Solution.IsValid("(())"));
            // Assert.False(Solution.IsValid("(()"));
            // Assert.False(Solution.IsValid(")"));
            // Assert.False(Solution.IsValid("(()))("));
            // return;
            // abc、acb、bac、bca、cab 和 cba
            // Console.WriteLine(Solution.Swap("abc", 0, 1));
            // List<char[]> ss;
            // Console.WriteLine("abc,acb,bac,bca,cab,cba");
            // ss = Solution.Permutation("abc".ToArray(), 0);
            // Console.WriteLine(string.Join(',', ss.Select(i => new string(i))));
            // Console.WriteLine("abb,bab,bba");
            // ss = Solution.Permutation("abb".ToCharArray(), 0);
            // Console.WriteLine(string.Join(',', ss.Select(i => new string(i))));
            // return;

            int input;
            List<string> exp, res;

            using (new Timeit())
            {
                input = 3;
                exp = new List<string>(){
                    "((()))",
                    "(()())",
                    "(())()",
                    "()(())",
                    "()()()"};
                res = (List<string>)new Solution().GenerateParenthesis(input);
                exp.Sort();
                res.Sort();
                // foreach (var s in res)
                //     Console.WriteLine(s);
                Assert.Equal(exp, res);
            }
            // return;
            using (new Timeit())
            {
                input = 7;
                res = (List<string>)new Solution().GenerateParenthesis(input);
                // Console.WriteLine($"n={input}, {res.Count}");
            }
            using (new Timeit())
            {
                input = 8;
                res = (List<string>)new Solution().GenerateParenthesis(input);
                Console.WriteLine($"new: n={input}, {res.Count}");
            }
            using (new Timeit())
            {
                input = 8;
                res = (List<string>)new Solution1().GenerateParenthesis(input);
                Console.WriteLine($"old: n={input}, {res.Count}");
            }
            using (new Timeit())
            {
                input = 1;
                res = (List<string>)new Solution().GenerateParenthesis(input);
            }
            // 3: 11 => 5
            // 0s 10.7821ms
            // 7: 21715 => 429
            // 0s 51.4778ms
            // 8: 158930 => 1430
            // 0s 634.7995ms
        }
    }
}