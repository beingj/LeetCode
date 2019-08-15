using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PalindromePartitioning
{
    public class Solution
    {
        public IList<IList<string>> Partition(string s)
        {
            var res = new List<IList<string>>();
            if (s.Length == 1)
            {
                res.Add(new List<string> { s });
                return res;
            }
            bool IsPalindrome(string ss, int startIdx, int endIdx)
            {
                for (var i = startIdx; i < endIdx; i++)
                {
                    if (ss[i] != ss[endIdx - i])
                        return false;
                }
                return true;
            }
            var c = s[0];
            for (var i = 0; i < s.Length; i++)
            {
                if ((s[i] == c) && IsPalindrome(s, 0, i))
                {
                    if ((i + 1) < s.Length)
                    {
                        var subs = Partition(s.Substring(i + 1));
                        foreach (var sub in subs)
                        {
                            var lst = new List<string> { s.Substring(0, i + 1) };
                            lst.AddRange(sub);
                            res.Add(lst);
                        }
                    }
                    else
                    {
                        res.Add(new List<string> { s });
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
            var input = @"
#start line, to avoid removed by CleanInput
";
            var lines = input.CleanInput();
            lines = "0131-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines, sortRet: true);
        }
    }
}