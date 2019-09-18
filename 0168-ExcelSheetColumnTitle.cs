using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ExcelSheetColumnTitle
{
    public class Solution
    {
        public string ConvertToTitle(int n)
        {
            var chrs = "abcdefghijklmnopqrstuvwxyz".ToUpper().ToArray();
            if (n <= 26)
            {
                return chrs[(n - 1) % 26].ToString();
            }
            var title = new List<char>();
            int i = 1;
            long m = n;
            var subTotals = new List<long>();
            while (true)
            {
                long subTotal = (long)Math.Pow(26, i);
                if (m <= subTotal) break;
                subTotals.Add(subTotal);
                m -= subTotal;
                i++;
            }
            for (var j = subTotals.Count - 1; j >= 0; j--)
            {
                var a = m / subTotals[j];
                var x = m % subTotals[j];
                if (x == 0)
                {
                    a -= 1;
                }
                title.Add(chrs[a]);
                m = x;
            }
            title.Add(chrs[(n - 1) % 26]);
            return string.Join("", title);
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
            lines = "0168-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}