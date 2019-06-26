using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Text;

namespace X022
{
    // Solution: http://wuchong.me/blog/2014/07/28/permutation-and-combination-realize/?nsukey=7uIcpqwYgy%2FTM3X%2FL6Ymk7R2oOSH%2FHHxH48ljCPL4hyLq1JqHoQHbQu5Ul2HQUz4WkdzPhEYxcxJrMIn5ggtGPFh5ZCI1x5b3Z58vDuOZ25Tiwz2luZGbHSuGA3SsqLE3UNZCnm78yU2KrBYg%2FGoEVlvbw99VbF33x2qgNDls%2BfxYnB4WDJs60xU4k3PpTCVD0Fr%2F4wlBSmPOLyzSE%2FmYg%3D%3D
    // Solution2: https://blog.csdn.net/Frank_Adam/article/details/79452782
    // Solution is much more better than Solution2
    public class Solution
    {
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
                    // var tmp = new string(cs);
                    SwapChars(cs, startIdx, i); // swap back to original s
                    // Console.WriteLine($"{tmp} => {new string(cs)}");
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
            // return s.Substring(startIdx, endIdx - startIdx).Contains(s[endIdx]);
            char c = s[endIdx];
            for (var i = startIdx; i < endIdx; i++)
            {
                if (s[i] == c)
                    return true;
            }
            return false;
        }
    }
    public class Solution2
    {
        public static List<string> Permutation(string s, Func<string, bool> isValid = null, Func<string, int, bool> isPreconditionValid = null)
        {
            char[] s2 = new char[s.Length];
            bool[] book = new bool[s.Length];
            for (var i = 0; i < book.Length; i++)
            {
                book[i] = false;
            }

            List<string> res, res2 = new List<string>();
            res = DFS(s, 0, s2, book, isValid, isPreconditionValid);
            foreach (var i in res)
            {
                if (!res2.Contains(i))
                    res2.Add(i);
            }
            Console.WriteLine($"remove dup: {res.Count} => {res2.Count}");
            return res2;
        }
        public static List<string> DFS(string s, int startIdx, char[] cs, bool[] book, Func<string, bool> isValid = null, Func<string, int, bool> isPreconditionValid = null)
        {
            int maxNum = s.Length;
            if (startIdx == maxNum)
            {
                // Console.WriteLine(new string(res));
                // return new List<string> { new string(cs) };
                var s2 = new string(cs);
                if ((isValid == null) || isValid(s2))
                {
                    return new List<string> { s2 };
                }
                return new List<string>();
            }

            //依次尝试每一张牌
            var res = new List<string>();
            for (int i = 0; i < maxNum; i++)
            {
                if (book[i] == false)
                {
                    book[i] = true;  //标记第i个数用过了
                    cs[startIdx] = s[i]; //i:1~maxNum
                    // Console.WriteLine($"isPreconditionValid {isPreconditionValid != null}");
                    // Console.WriteLine($"isPreconditionValid res: {isPreconditionValid(new string(cs), startIdx)}");
                    if ((isPreconditionValid != null) && !isPreconditionValid(new string(cs), startIdx))
                    {
                        book[i] = false;
                        // Console.WriteLine("skip");
                        // skip remain chars from startIdx + 1
                        continue;
                    }
                    res.AddRange(DFS(s, startIdx + 1, cs, book, isValid, isPreconditionValid));  //处理下一步
                    book[i] = false;  //标记第i个数用完了
                }
            }
            return res;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("X022-Permutation");
            Verify("abb", "abb,bab,bba");
            Verify("abb", "abb,bab,bba");
            Verify("abc", "abc,acb,bac,bca,cab,cba");
            Verify2("((()))", new List<string>(){
                    "((()))",
                    "(()())",
                    "(())()",
                    "()(())",
                    "()()()"});
            int n = 5;
            using (new Timeit())
            {
                var res = GenerateParenthesis(n);
                Console.WriteLine($"new n={n}, {res.Count}");
            }
            VerifySolution2("abc", "abc,acb,bac,bca,cab,cba");
            VerifySolution2("abb", "abb,bab,bba");
            using (new Timeit())
            {
                var res = GenerateParenthesis2(n);
                Console.WriteLine($"dfs n={n}, {res.Count}");
            }
        }
        static void Verify(string input, string exp)
        {
            List<string> res = new List<string> { input };
            using (new Timeit())
            {
                res.AddRange(Solution.Permutation(input, 0, (x) => true, (x, i) => true));
            }
            Assert.Equal(new HashSet<string>(exp.Split(',')), new HashSet<string>(res));
        }
        static void VerifySolution2(string input, string exp)
        {
            var exp2 = new List<string>(exp.Split(','));
            exp2.Sort();
            List<string> res;
            using (new Timeit())
            {
                res = Solution2.Permutation(input);
            }
            res.Sort();
            Assert.Equal(exp2, res);
            // Assert.Equal(new HashSet<string>(exp.Split(',')), new HashSet<string>(res));
        }
        static void Verify2(string input, List<string> exp)
        {
            List<string> res = new List<string> { input };
            using (new Timeit())
            {
                res.AddRange(Solution.Permutation(input, 0, IsValid, IsPreconditionValid));
            }
            Assert.Equal(new HashSet<string>(exp), new HashSet<string>(res));
        }
        public static IList<string> GenerateParenthesis(int n)
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
            List<string> res = Solution.Permutation(sb.ToString(), 0, IsValid2, IsPreconditionValid2);
            List<string> res2 = new List<string>();
            foreach (var i in res)
            {
                var j = string.Format("({0})", i);
                res2.Add(j);
            }
            // Console.WriteLine($"{n}: {res.Count} => {res2.Count}");
            return res2;
        }
        public static IList<string> GenerateParenthesis2(int n)
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
            List<string> res = Solution2.Permutation(sb.ToString(), IsValid2, IsPreconditionValid2);
            List<string> res2 = new List<string>();
            foreach (var i in res)
            {
                var j = string.Format("({0})", i);
                res2.Add(j);
            }
            // Console.WriteLine($"{n}: {res.Count} => {res2.Count}");
            return res2;
        }
        public static bool IsPreconditionValid(string s, int idx)
        {
            int sum = 0;
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
        public static bool IsPreconditionValid2(string s, int idx)
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
        public static bool IsValid2(string s)
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
}