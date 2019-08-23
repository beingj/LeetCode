using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WordBreakII
{
    public class Solution
    {
        Dictionary<string, IList<string>> Cache = new Dictionary<string, IList<string>>();
        public IList<string> WordBreak(string s, IList<string> wordDict)
        {
            var d = new Dictionary<char, List<string>>();
            foreach (var w in wordDict)
            {
                var c = w[0];
                if (!d.ContainsKey(c)) d[c] = new List<string>();
                d[c].Add(w);
            }
            return CachedBreakWords(s, wordDict, d);
        }
        IList<string> CachedBreakWords(string s, IList<string> wordDict, Dictionary<char, List<string>> d)
        {
            if (!Cache.ContainsKey(s))
            {
                var x = BreakWords(s, wordDict, d);
                Cache[s] = x != null ? x : new List<string>();
            }
            return Cache[s];
        }
        IList<string> BreakWords(string s, IList<string> wordDict, Dictionary<char, List<string>> d)
        {
            if (!d.ContainsKey(s[0])) return null;

            var lst = new List<string>();
            foreach (var w in d[s[0]])
            {
                if (s.Length < w.Length) continue;
                if (s.Length == w.Length)
                {
                    if (s == w)
                        lst.Add(w);
                }
                else
                {
                    var ss = s.Substring(0, w.Length);
                    if (ss != w) continue;
                    var x = CachedBreakWords(s.Substring(w.Length), wordDict, d);
                    if (x != null)
                    {
                        foreach (var i in x)
                        {
                            lst.Add($"{w} {i}");
                        }
                    }
                }
            }
            return lst;
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
";
            var lines = input.CleanInput();
            lines = "0140-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines, sortRet: true);
        }
    }
}