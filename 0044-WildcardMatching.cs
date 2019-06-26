using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace WildcardMatching
{
    public class Solution
    {
        public bool IsMatch(string s, string p)
        {
            if (s.Length == 0)
            {
                for (var i = 0; i < p.Length; i++)
                {
                    if (p[i] != '*')
                    {
                        return false;
                    }
                }
                // if patt are *, then Match is true
                return true;
            }
            if (p.Length == 0)
            {
                if (s.Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            var pList = new List<string>();
            int idx = 0;
            var cs = "";
            while (true)
            {
                if (idx >= p.Length)
                {
                    // add last cs
                    if ((p[idx - 1] != '?') && p[idx - 1] != '*')
                    {
                        pList.Add(cs);
                    }
                    break;
                }
                if ((p[idx] == '?') || p[idx] == '*')
                {
                    if (cs.Length > 0)
                    {
                        pList.Add(cs);
                        cs = "";
                    }
                    if ((p[idx] == '*') && (pList.Count > 0) && (pList.Last() == "*"))
                    {
                        // duplicated *
                    }
                    else
                    {
                        pList.Add($"{p[idx]}");
                    }
                }
                else
                {
                    cs = $"{cs}{p[idx]}";
                }
                idx++;
            }
            // Console.WriteLine(string.Join(", ", pList));

            if (pList.Count > 0)
            {
                var last = pList.Last();
                if ((last != "?") && (last != "*"))
                {
                    idx = s.Length - last.Length;
                    if ((idx >= 0) && (s.Substring(idx, last.Length) != last))
                    {
                        // last patt not match
                        return false;
                    }
                }
            }

            // check all patts(except *) can be found in s in order
            idx = 0;
            for (var pi = 0; pi < pList.Count; pi++)
            {
                if (idx >= s.Length)
                {
                    for (var j = pi; j < pList.Count; j++)
                    {
                        if (pList[j] != "*")
                        {
                            // s reaches end, but remain patts are not *, so not match
                            return false;
                        }
                    }
                    break;
                }

                var ps = pList[pi];
                if (ps == "?")
                {
                    idx++;
                }
                else if (ps != "*")
                {
                    var x = s.IndexOf(ps, idx);
                    if (x < 0)
                    {
                        // patt not found
                        return false;
                    }
                    idx = x + ps.Length;
                }
            }
            if ((pList.Count > 1) && !pList.Contains("?") && (pList.First() == "*") && (pList.Last() == "*"))
            {
                // *a*b*c*, if a,b,c are in order(checked above), then match is true
                return true;
            }

            // easy way end, try hard way
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
                            // if remain patt are *, then Match is true
                            if (pList[i] != "*")
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
                if ((ps != "?") && (ps != "*"))
                {
                    var subs = new string(s.Skip(si).Take(ps.Length).ToArray());
                    // Console.WriteLine(subs);
                    if (subs != ps)
                    {
                        return false;
                    }
                    pi++;
                    si += ps.Length;
                    continue;
                }
                if (ps == "?")
                {
                    pi++;
                    si++;
                    continue;
                }
                if (ps == "*")
                {
                    while (si < s.Length)
                    {
                        // try all possible *
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
            Console.WriteLine("WildcardMatching");
            string s, p;
            bool exp;

            s = "aa";
            p = "a";
            exp = false;
            Verify(s, p, exp);

            s = "aa";
            p = "*";
            exp = true;
            Verify(s, p, exp);

            s = "cb";
            p = "?a";
            exp = false;
            Verify(s, p, exp);

            s = "adceb";
            p = "*a*b";
            exp = true;
            Verify(s, p, exp);

            s = "acdcb";
            p = "a*c?b";
            exp = false;
            Verify(s, p, exp);

            s = "aaabbbaabaaaaababaabaaabbabbbbbbbbaabababbabbbaaaaba";
            p = "a*******b";
            exp = false;
            Verify(s, p, exp);

            s = "";
            p = "a";
            exp = false;
            Verify(s, p, exp);

            s = "b";
            p = "?";
            exp = true;
            Verify(s, p, exp);

            s = "bbbaaabaababbabbbaabababbbabababaabbaababbbabbbabb";
            p = "*b**b***baba***aaa*b***";
            exp = false;
            Verify(s, p, exp);

            s = "a";
            p = "";
            exp = false;
            Verify(s, p, exp);

            s = "abbabaaabbabbaababbabbbbbabbbabbbabaaaaababababbbabababaabbababaabbbbbbaaaabababbbaabbbbaabbbbababababbaabbaababaabbbababababbbbaaabbbbbabaaaabbababbbbaababaabbababbbbbababbbabaaaaaaaabbbbbaabaaababaaaabb";
            p = "**aa*****ba*a*bb**aa*ab****a*aaaaaa***a*aaaa**bbabb*b*b**aaaaaaaaa*a********ba*bbb***a*ba*bb*bb**a*b*bb";
            exp = false;
            Verify(s, p, exp);

            s = "baaaa";
            p = "*aaa";
            exp = true;
            Verify(s, p, exp);

            s = "aaaaaabbaabaaaaabababbabbaababbaabaababaaaaabaaaabaaaabababbbabbbbaabbababbbbababbaaababbbabbbaaaaaaabbaabbbbababbabbaaabababaaaabaaabaaabbbbbabaaabbbaabbbbbbbaabaaababaaaababbbbbaabaaabbabaabbaabbaaaaba";
            p = "*bbb**a*******abb*b**a**bbbbaab*b*aaba*a*b**a*abb*aa****b*bb**abbbb*b**bbbabaa*b**ba**a**ba**b*a*a**aaa";
            exp = false;
            Verify(s, p, exp);

            s = "a";
            p = "aa";
            exp = false;
            Verify(s, p, exp);

            s = "aaaabaabaabbbabaabaabbbbaabaaabaaabbabbbaaabbbbbbabababbaabbabbbbaababaaabbbababbbaabbbaabbaaabbbaabbbbbaaaabaaabaabbabbbaabababbaabbbabababbaabaaababbbbbabaababbbabbabaaaaaababbbbaabbbbaaababbbbaabbbbb";
            p = "**a*b*b**b*b****bb******b***babaab*ba*a*aaa***baa****b***bbbb*bbaa*a***a*a*****a*b*a*a**ba***aa*a**a*";
            exp = false;
            Verify(s, p, exp);

            s = "baaaba";
            p = "a*b??a";
            exp = false;
            Verify(s, p, exp);

            s = "zacabz";
            p = "*a?b*";
            exp = false;
            Verify(s, p, exp);

            s = "baaab";
            p = "*ab**ba*";
            exp = false;
            Verify(s, p, exp);

            s = "missingtest";
            p = "mi*ing?s*t";
            exp = false;
            Verify(s, p, exp);
        }
    }
}