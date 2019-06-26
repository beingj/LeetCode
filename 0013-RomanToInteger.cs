using System;
using Xunit;
using Util;
using System.Text;

namespace RomanToInteger
{
    public class Solution
    {
        // Symbol       Value
        // I             1
        // V             5
        // X             10
        // L             50
        // C             100
        // D             500
        // M             1000
        public int RomanToInt(string s)
        {
            int res = 0;
            int idx = 0;
            while (s[idx] == 'M')
            {
                res += 1000;
                idx++;
                if (idx >= s.Length)
                    return res;
            }
            s = s.Substring(idx);
            if (s.StartsWith("CM"))
            {
                res += 900;
                if (s.Length == 2)
                    return res;
                s = s.Substring(2);
            }
            if (s.StartsWith("D"))
            {
                res += 500;
                if (s.Length == 1)
                    return res;
                s = s.Substring(1);
            }
            if (s.StartsWith("CD"))
            {
                res += 400;
                if (s.Length == 2)
                    return res;
                s = s.Substring(2);
            }

            // X             10
            // L             50
            // C             100
            idx = 0;
            while (s[idx] == 'C')
            {
                res += 100;
                idx++;
                if (idx >= s.Length)
                    return res;
            }
            s = s.Substring(idx);
            if (s.StartsWith("XC"))
            {
                res += 90;
                if (s.Length == 2)
                    return res;
                s = s.Substring(2);
            }
            if (s.StartsWith("L"))
            {
                res += 50;
                if (s.Length == 1)
                    return res;
                s = s.Substring(1);
            }
            if (s.StartsWith("XL"))
            {
                res += 40;
                if (s.Length == 2)
                    return res;
                s = s.Substring(2);
            }

            // I             1
            // V             5
            // X             10
            idx = 0;
            while (s[idx] == 'X')
            {
                res += 10;
                idx++;
                if (idx >= s.Length)
                    return res;
            }
            s = s.Substring(idx);
            if (s.StartsWith("IX"))
            {
                res += 9;
                if (s.Length == 2)
                    return res;
                s = s.Substring(2);
            }
            if (s.StartsWith("V"))
            {
                res += 5;
                if (s.Length == 1)
                    return res;
                s = s.Substring(1);
            }
            if (s.StartsWith("IV"))
            {
                res += 4;
                if (s.Length == 2)
                    return res;
                s = s.Substring(2);
            }
            idx = 0;
            while (s[idx] == 'I')
            {
                res += 1;
                idx++;
                if (idx >= s.Length)
                    return res;
            }
            return res;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("RomanToInteger");
            // Console.WriteLine(new Solution().RomanToInt("III"));
            // Console.WriteLine(new Solution().RomanToInt("MM"));
            // Console.WriteLine(new Solution().RomanToInt("MCMXCIV"));
            // Console.WriteLine(new Solution().RomanToInt("MMMXL"));
            // return;

            string input;
            int exp, res;

            using (new Timeit())
            {
                input = "III";
                exp = 3;
                res = new Solution().RomanToInt(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "IV";
                exp = 4;
                res = new Solution().RomanToInt(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "IX";
                exp = 9;
                res = new Solution().RomanToInt(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "LVIII";
                exp = 58;
                res = new Solution().RomanToInt(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "MCMXCIV";
                exp = 1994;
                res = new Solution().RomanToInt(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "MMXIX";
                exp = 2019;
                res = new Solution().RomanToInt(input);
                Assert.Equal(exp, res);
            }
        }
    }
}