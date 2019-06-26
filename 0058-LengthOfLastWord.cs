using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace LengthOfLastWord
{
    public class Solution
    {
        public int LengthOfLastWord(string s)
        {
            int len = 0;
            int lastLen = 0;
            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                {
                    if (len > 0)
                    {
                        lastLen = len;
                    }
                    len = 0;
                    continue;
                }
                len++;
            }
            if (len == 0)
            {
                return lastLen;
            }
            return len;
        }
    }
    public class Test
    {
        static void Verify(string s, int exp)
        {
            Console.WriteLine(s);
            int res;
            using (new Timeit())
            {
                res = new Solution().LengthOfLastWord(s);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("LengthOfLastWord");
            string s;
            int exp;
            var input = @"
Hello World
5
Hello World   
5
";
            var lines = input.Trim(new char[] { '\n', '\r', ' ' }).Split('\n').Select(x => x.Trim('\r')).ToArray();
            int idx = 0;
            while (idx < lines.Length)
            {
                s = lines[idx++];
                exp = int.Parse(lines[idx++]);
                Verify(s, exp);
            }
        }
    }
}