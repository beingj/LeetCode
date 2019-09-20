using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ExcelSheetColumnNumber
{
    public class Solution
    {
        public int TitleToNumber(string s)
        {
            var chrs = "abcdefghijklmnopqrstuvwxyz".ToUpper().ToList();
            var n = 0;
            for (var i = 0; i < s.Length; i++)
            {
                int subTotal = (int)Math.Pow(26, s.Length - i - 1);
                var idx = chrs.IndexOf(s[i]) + 1;
                n += subTotal * idx;
            }
            return n;
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
            lines = "0171-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}