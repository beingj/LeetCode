using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DecodeWays
{
    public class Solution
    {
        // char[] validChars = new char[] { '0', '1', '2', '3', '4', '5', '6' };
        // Dictionary<char, bool> validChars = new Dictionary<char, bool> { { '0', true }, { '1', true }, { '2', true }, { '3', true }, { '4', true }, { '5', true }, { '6', true } };
        Dictionary<char, bool> validChars = new Dictionary<char, bool> { { '0', true }, { '1', true }, { '2', true }, { '3', true }, { '4', true }, { '5', true }, { '6', true },{ '7', false }, { '8', false }, { '9', false } };
        public int NumDecodings(string s)
        {
            if (s.Length == 0)
            {
                return 0;
            }
            int res = 0;
            Split(s, 0, ref res);
            return res;
        }
        void Split(string s, int idx, ref int res)
        {
            if (s[idx] == '0')
            {
                return;
            }
            var len = s.Length - idx;
            if (len == 1)
            {
                res++;
                return;
            }
            // >1
            Split(s, idx + 1, ref res);
            if ((s[idx] == '1') ||
                // ((s[idx] == '2') && (validChars.Contains(s[idx + 1]))))
                // ((s[idx] == '2') && (validChars.ContainsKey(s[idx + 1]))))
                ((s[idx] == '2') && (validChars[s[idx + 1]])))
            {
                if (len == 2)
                {
                    res++;
                    return;
                }
                Split(s, idx + 2, ref res);
            }
        }
        void Split1(string s, List<int> soFar, List<List<int>> res)
        {
            if (s.Length == 0)
            {
                if (soFar.Count > 0)
                {
                    res.Add(soFar);
                }
                return;
            }
            var soFar2 = new List<int>();
            soFar2.AddRange(soFar);
            soFar2.Add(int.Parse(s.Substring(0, 1)));
            Split1(s.Substring(1), soFar2, res);
            if (s.Length == 1)
            {
                return;
            }
            var x = int.Parse(s.Substring(0, 2));
            if (x < 27)
            {
                var soFar3 = new List<int>();
                soFar3.AddRange(soFar);
                soFar3.Add(x);
                Split1(s.Substring(2), soFar3, res);
            }
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
12
2
226
3
127
2
123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789
177147
0
0
01
0
10
1
101
1
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}