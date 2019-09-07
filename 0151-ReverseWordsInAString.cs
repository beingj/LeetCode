using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ReverseWordsInAString
{
    public class Solution
    {
        public string ReverseWords(string s)
        {
            var ws = s.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var s2 = string.Join(" ", ws.Reverse());
            return s2;
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
            lines = "0151-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}