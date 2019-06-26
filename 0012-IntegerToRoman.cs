using System;
using Xunit;
using Util;
using System.Text;

namespace IntegerToRoman
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
        public string IntToRoman(int num)
        {
            StringBuilder res = new StringBuilder();
            // C             100
            // D             500
            // M             1000
            if (num >= 1000)
            {
                for (var i = 0; i < (int)(num / 1000); i++)
                {
                    res.Append("M");
                }
            }
            num = num % 1000;
            if (num >= 900)
            {
                res.Append("CM");
                num -= 900;
            }
            if (num >= 500)
            {
                res.Append("D");
                num -= 500;
            }
            if (num >= 400)
            {
                res.Append("CD");
                num -= 400;
            }
            if (num >= 100)
            {
                for (var i = 0; i < (int)(num / 100); i++)
                {
                    res.Append("C");
                }
            }
            num = num % 100;

            // X             10
            // L             50
            // C             100
            if (num >= 90)
            {
                res.Append("XC");
                num -= 90;
            }
            if (num >= 50)
            {
                res.Append("L");
                num -= 50;
            }
            if (num >= 40)
            {
                res.Append("XL");
                num -= 40;
            }
            if (num >= 10)
            {
                for (var i = 0; i < (int)(num / 10); i++)
                {
                    res.Append("X");
                }
            }
            num = num % 10;

            // I             1
            // V             5
            // X             10
            if (num >= 9)
            {
                res.Append("IX");
                num -= 9;
            }
            if (num >= 5)
            {
                res.Append("V");
                num -= 5;
            }
            if (num >= 4)
            {
                res.Append("IV");
                num -= 4;
            }
            if (num >= 1)
            {
                for (var i = 0; i < (int)(num / 1); i++)
                {
                    res.Append("I");
                }
            }

            return res.ToString();
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("IntegerToRoman");
            Console.WriteLine(new Solution().IntToRoman(1024));
            // return;

            int input;
            string exp, res;

            using (new Timeit())
            {
                input = 3;
                exp = "III";
                res = new Solution().IntToRoman(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = 4;
                exp = "IV";
                res = new Solution().IntToRoman(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = 9;
                exp = "IX";
                res = new Solution().IntToRoman(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = 58;
                exp = "LVIII";
                res = new Solution().IntToRoman(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = 1994;
                exp = "MCMXCIV";
                res = new Solution().IntToRoman(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = 2019;
                exp = "MMXIX";
                res = new Solution().IntToRoman(input);
                Assert.Equal(exp, res);
            }
        }
    }
}