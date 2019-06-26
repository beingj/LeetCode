using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace MyPow
{
    public class Solution
    {
        public double MyPow(double x, int n)
        {
            // NOTE: give up and use solution in discuss
            // https://leetcode.com/problems/powx-n/discuss/19546/Short-and-easy-to-understand-solution
            if (x == 1 || n == 0)
                return 1;
            // note boundary value
            if (n == int.MinValue)
            {
                if (x == -1)
                    return 1;
                else
                    return 0;
            }

            if (n < 0)
            {
                n *= -1;
                x = 1 / x;
            }
            return (n % 2 == 0) ? MyPow(x * x, n / 2) : x * MyPow(x * x, n / 2);
        }
    }

    public class Solution2
    {
        // https://leetcode.com/problems/powx-n/discuss/19546/Short-and-easy-to-understand-solution
        public double MyPow(double x, int n)
        {
            if (n == 0)
                return 1;
            long m = n;
            if (n < 0)
            {
                m = -n;
                x = 1 / x;
            }
            return (m & 1) == 1 ? x * MyPow(x * x, (int)(m >> 1)) : MyPow(x * x, (int)(m >> 1));
        }
    }
    public class Test
    {
        static void Verify(double x, int n, double exp)
        {
            Console.WriteLine($"{x}^{n}");
            double res;
            using (new Timeit())
            {
                res = new Solution().MyPow(x, n);
                // res = new Solution2().MyPow(x, n);
            }
            int exp2 = (int)(exp * 1000000);
            int res2 = (int)(res * 1000000);
            Assert.Equal(exp2, res2);
        }
        static public void Run()
        {
            Console.WriteLine("MyPow");
            double x;
            int n;
            double exp;

            // x = 2.0;
            // n = 10;
            // exp = 1024;
            // Verify(x, n, exp);

            // x = 2.1;
            // n = 3;
            // exp = 9.261;
            // Verify(x, n, exp);

            // x = 2.0;
            // n = -2;
            // exp = 0.25;
            // Verify(x, n, exp);

            // x = 0.00001;
            // n = 2147483647;
            // exp = 0;
            // Verify(x, n, exp);

            // x = -2;
            // n = 2;
            // exp = 4;
            // Verify(x, n, exp);

            // x = 34.00515;
            // n = -3;
            // exp = 3e-05;
            // Verify(x, n, exp);

            // x = 1.00000;
            // n = 2147483647;
            // exp = 1;
            // Verify(x, n, exp);

            x = 2.00000;
            n = -2147483648;
            exp = 0;
            Verify(x, n, exp);
        }
    }
}