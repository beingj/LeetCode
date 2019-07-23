using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RestoreIpAddresses
{
    public class Solution
    {
        char[] validCharsY = new char[] { '0', '1', '2', '3', '4' };
        char[] validChars = new char[] { '0', '1', '2', '3', '4', '5' };
        public IList<string> RestoreIpAddresses(string s)
        {
            if ((s.Length < 4) || (s.Length > 12))
            {
                return new List<string>();
            }
            var res = new List<string>();
            Split(s, 0, new List<string>(), res);
            return res;
        }
        void Split(string s, int idx, List<string> soFar, List<string> res)
        {
            var len = s.Length - idx;
            if (len == 0)
            {
                if (soFar.Count == 4)
                {
                    res.Add(string.Join('.', soFar));
                }
                return;
            }

            var x = s[idx];
            var soFar2 = new List<string>();
            soFar2.AddRange(soFar);
            soFar2.Add($"{x}");
            Split(s, idx + 1, soFar2, res);
            if (len == 1)
            {
                return;
            }

            var y = s[idx + 1];
            if (x != '0')
            {
                var soFar3 = new List<string>();
                soFar3.AddRange(soFar);
                soFar3.Add($"{x}{y}");
                Split(s, idx + 2, soFar3, res);
            }
            if (len == 2)
            {
                return;
            }

            var z = s[idx + 2];
            if ((x == '1') ||
                (x == '2' && validCharsY.Contains(y)) ||
                (x == '2' && (y == '5') && validChars.Contains(z)))
            {
                var soFar4 = new List<string>();
                soFar4.AddRange(soFar);
                soFar4.Add($"{x}{y}{z}");
                Split(s, idx + 3, soFar4, res);
            }
        }
    }
    public class Test
    {
        static public void Run()
        {
            var lines = "0093-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines, sortRet: true);
        }
    }
}