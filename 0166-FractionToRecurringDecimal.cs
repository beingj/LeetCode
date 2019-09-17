using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FractionToRecurringDecimal
{
    public class Solution
    {
        public string FractionToDecimal(int numerator, int denominator)
        {
            // https://www.mathsisfun.com/long_division.html
            if (numerator == 0) return "0";
            if (denominator == 1) return numerator.ToString();

            int maxDecLen = 1000;
            var isNeg = false;
            long dividend = numerator;
            long divisor = denominator;
            if (dividend < 0 || divisor < 0)
            {
                if (dividend < 0 && divisor < 0)
                {

                }
                else
                {
                    isNeg = true;
                }
                if (dividend < 0) dividend = -dividend;
                if (divisor < 0) divisor = -divisor;
            }

            if (divisor == 1)
            {
                long abs = Math.Abs(dividend);
                if (isNeg)
                    return (-abs).ToString();
                else
                    return abs.ToString();
            }

            var digitN = (int)Math.Floor(Math.Log10(dividend));
            var ints = new List<int>();
            long remainder = 0;
            for (var i = digitN; i >= 0; i--)
            {
                long d = (long)((dividend / (Math.Pow(10, i))) % 10);
                long x = remainder * 10 + d;
                var n = (int)(x / divisor);
                remainder = (int)(x - (divisor * n));
                ints.Add(n);
            }
            var s = string.Join("", ints).TrimStart('0');
            if (s == "") s = "0";
            if (isNeg) s = $"-{s}";
            if (remainder == 0) return s;

            var decs = new List<int>();
            var remainderList = new List<long>();
            var loop = false;
            var loopStartIdx = 0;
            for (var i = 0; i < maxDecLen; i++)
            {
                remainderList.Add(remainder);
                long x = remainder * 10;
                var n = (int)(x / divisor);
                decs.Add(n);
                remainder = (int)(x - (divisor * n));
                if (remainder == 0) break;

                var idx = remainderList.IndexOf(remainder);
                if (idx >= 0)
                {
                    loop = true;
                    loopStartIdx = idx;
                    break;
                }
            }
            var s2 = string.Join("", decs);
            if (!loop)
            {
                return $"{s}.{s2}";
            }
            else
            {
                s2 = s2.Insert(loopStartIdx, "(");
                return $"{s}.{s2})";
            }
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
            lines = "0166-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}