using System;
using Xunit;
using Util;
using System.Text;

namespace MyAtoi
{
    public class Solution
    {
        public int MyAtoi(string str)
        {
            string digits = "0123456789";
            string validChars = "+-" + digits;
            int idx = 0;
            while (idx < str.Length)
            {
                if (str[idx] != ' ')
                    break;
                idx++;
            }
            if (idx >= str.Length)
            {
                return 0;
            }
            if (!validChars.Contains(str[idx]))
                return 0;
            bool isNeg = false;
            if (str[idx] == '-')
            {
                isNeg = true;
                idx++;
            }
            else if (str[idx] == '+')
            {
                idx++;
            }
            while (idx < str.Length)
            {
                if (str[idx] == '0')
                {
                    idx++;
                }
                else
                {
                    break;
                }
            }

            int[] num = new int[str.Length];
            int nidx = 0;
            while (idx < str.Length)
            {
                int x = digits.IndexOf(str[idx]);
                if (x < 0)
                    break;
                num[nidx] = x;
                idx++;
                nidx++;
            }
            nidx--;
            long res = 0;
            int min = (int)(-Math.Pow(2, 31));
            int max = (int)Math.Pow(2, 31) - 1;
            int maxDigits = (int)Math.Log10(max) + 2;
            if ((nidx + 1) > maxDigits)
            {
                if (isNeg)
                {
                    return min;
                }
                else
                {
                    return max;
                }
            }
            for (var i = nidx; i >= 0; i--)
            {
                res += (long)(num[i] * Math.Pow(10, nidx - i));
                if (isNeg && (-res < min))
                {
                    return min;
                }
                if ((!isNeg) && (res > max))
                {
                    return max;
                }
            }
            if (isNeg)
                res = -res;
            return (int)res;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("MyAtoi");

            string input;
            int exp, res;

            using (new Timeit())
            {
                input = "42";
                exp = 42;
                res = new Solution().MyAtoi(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "   -42";
                exp = -42;
                res = new Solution().MyAtoi(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "4193 with words";
                exp = 4193;
                res = new Solution().MyAtoi(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "words and 987";
                exp = 0;
                // The first non-whitespace character is 'w', which is not a numerical 
                // digit or a +/- sign. Therefore no valid conversion could be performed.
                res = new Solution().MyAtoi(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "-91283472332";
                exp = -2147483648;
                // The number "-91283472332" is out of the range of a 32-bit signed integer.
                // Thefore INT_MIN (−231) is returned.
                res = new Solution().MyAtoi(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "";
                exp = 0;
                res = new Solution().MyAtoi(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "+1";
                exp = 1;
                res = new Solution().MyAtoi(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "20000000000000000000";
                exp = 2147483647;
                res = new Solution().MyAtoi(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = "  0000000000012345678";
                exp = 12345678;
                res = new Solution().MyAtoi(input);
                Assert.Equal(exp, res);
            }
        }
    }
}