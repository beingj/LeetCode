using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace RegularExpressionMatching
{
    public class Solution
    {
        public bool IsMatch(string s, string p)
        {
            var pList = new List<string>();
            foreach (var i in p)
            {
                if (i == '*')
                {
                    var prev = pList.Last();
                    pList.RemoveAt(pList.Count - 1);
                    pList.Add($"{prev}*");
                }
                else
                {
                    pList.Add(i.ToString());
                }
            }
            // Console.WriteLine(string.Join(", ", pList));
            return Match(s, pList, 0, 0);
        }
        bool Match(string s, List<string> pList, int si, int pi)
        {
            while (true)
            {
                if (pi >= pList.Count)
                {
                    if (si < s.Length)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                if (si >= s.Length)
                {
                    if (pi < pList.Count)
                    {
                        for (var i = pi; i < pList.Count; i++)
                        {
                            // if remain patt are c*, then Match is true
                            if (!pList[i].EndsWith('*'))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }

                var ps = pList[pi];
                if ((ps.Length == 1) && (ps != "."))
                {
                    if (s[si] != ps[0])
                    {
                        return false;
                    }
                    pi++;
                    si++;
                    continue;
                }
                if (ps == ".")
                {
                    pi++;
                    si++;
                    continue;
                }
                if (ps.EndsWith('*'))
                {
                    var c = ps[0];
                    while (si < s.Length)
                    {
                        if ((c != '.') && (s[si] != c))
                        {
                            break;
                        }
                        // try all possible c*
                        var m = Match(s, pList, si, pi + 1);
                        if (m)
                        {
                            return true;
                        }
                        si++;
                    }
                    pi++;
                    continue;
                }
            }
        }
    }
    public class Test
    {
        static void Verify(string s, string p, bool exp)
        {
            Console.WriteLine($"{s}=>/{p}/");
            bool res;
            using (new Timeit())
            {
                res = new Solution().IsMatch(s, p);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("RegularExpressionMatching");
            string s, p;
            bool exp;

            s = "aa";
            p = "a";
            exp = false;
            Verify(s, p, exp);

            s = "aa";
            p = "a*";
            exp = true;
            Verify(s, p, exp);

            s = "ab";
            p = ".*";
            exp = true;
            Verify(s, p, exp);

            s = "aab";
            p = "c*a*b";
            exp = true;
            Verify(s, p, exp);

            s = "mississippi";
            p = "mis*is*p*.";
            exp = false;
            Verify(s, p, exp);

            s = "ab";
            p = ".*c";
            exp = false;
            Verify(s, p, exp);

            s = "abc";
            p = ".*c";
            exp = true;
            Verify(s, p, exp);

            s = "abc";
            p = ".*.";
            exp = true;
            Verify(s, p, exp);

            s = "aaa";
            p = "aaaa";
            exp = false;
            Verify(s, p, exp);

            s = "aaa";
            p = "a*a";
            exp = true;
            Verify(s, p, exp);

            s = "baa";
            p = "c*c";
            exp = false;
            Verify(s, p, exp);

            s = "mississippi";
            p = "mis*is*ip*.";
            exp = true;
            Verify(s, p, exp);

            s = "aaa";
            p = "ab*a*c*a";
            exp = true;
            Verify(s, p, exp);

            s = "a";
            p = "ab*";
            exp = true;
            Verify(s, p, exp);
        }
    }
}