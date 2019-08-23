using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WordBreak
{
    public class Solution
    {
        Dictionary<string, bool> Cache = new Dictionary<string, bool>();
        public bool WordBreak(string s, IList<string> wordDict)
        {
            var d = new Dictionary<char, List<string>>();
            foreach (var w in wordDict)
            {
                var c = w[0];
                if (!d.ContainsKey(c)) d[c] = new List<string>();
                d[c].Add(w);
            }
            return CachedCanBreak(s, wordDict, d);
        }
        bool CachedCanBreak(string s, IList<string> wordDict, Dictionary<char, List<string>> d)
        {
            if (!Cache.ContainsKey(s))
                Cache[s] = CanBreak(s, wordDict, d);
            return Cache[s];
        }
        bool CanBreak(string s, IList<string> wordDict, Dictionary<char, List<string>> d)
        {
            if (s.Length == 0) return true;
            if (!d.ContainsKey(s[0]))
                return false;
            foreach (var w in d[s[0]])
            {
                if (s.Length < w.Length) continue;
                var ss = s.Substring(0, w.Length);
                if (ss != w) continue;
                var x = CachedCanBreak(s.Substring(w.Length), wordDict, d);
                if (x) return true;
            }
            return false;
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
            lines = "0139-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}