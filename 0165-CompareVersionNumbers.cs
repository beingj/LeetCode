using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CompareVersionNumbers
{
    public class Solution
    {
        public int CompareVersion(string version1, string version2)
        {
            var v1 = version1.Split('.').Select(i => i.Trim(new char[] { ' ', '.' })).Select(j => int.Parse(j)).ToArray();
            var v2 = version2.Split('.').Select(i => i.Trim(new char[] { ' ', '.' })).Select(j => int.Parse(j)).ToArray();
            var maxLen = Math.Max(v1.Length, v2.Length);
            for (var i = 0; i < maxLen; i++)
            {
                var n1 = i < v1.Length ? v1[i] : 0;
                var n2 = i < v2.Length ? v2[i] : 0;
                if (n1 != n2) return n1 > n2 ? 1 : -1;
            }
            return 0;
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
            lines = "0165-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}