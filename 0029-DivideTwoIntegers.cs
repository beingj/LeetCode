using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace DivideTwoIntegers
{
    public class Solution
    {
        public int Divide(int dividend, int divisor)
        {
            long res = 0;
            long sum = 0;
            bool isDividendNeg = false;
            bool isDivisorNeg = false;
            long ldividend = dividend;
            long ldivisor = divisor;
            if (ldividend < 0)
            {
                isDividendNeg = true;
                ldividend = -ldividend;
            }
            if (ldivisor < 0)
            {
                isDivisorNeg = true;
                ldivisor = -ldivisor;
            }

            if (ldivisor == 1)
            {
                res = ldividend;
            }
            else
            {
                var i = NarrowDown(ldividend, ldivisor);
                if (i > 0)
                {
                    res = 2 << (i - 1);
                    sum = ldivisor << i;
                }
                while (true)
                {
                    if (sum > ldividend)
                    {
                        res--;
                        break;
                    }
                    sum += ldivisor;
                    res++;
                }
            }

            if (isDividendNeg && isDivisorNeg)
            {
            }
            else if (isDividendNeg || isDivisorNeg)
            {
                res = -res;
            }
            if (res > int.MaxValue)
            {
                res = int.MaxValue;
            }
            return (int)res;
        }
        public static int NarrowDown(long dividend, long divisor)
        {
            // TODO: i from 30,20,10 to 10-0
            int i = 0;
            while (true)
            {
                var x = divisor << i;
                if (x > dividend)
                {
                    i--;
                    break;
                }
                i++;
            }
            return i;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("DivideTwoIntegers");
            int dividend, divisor;
            int exp, res;

            dividend = 10;
            divisor = 3;
            exp = 3;
            using (new Timeit())
            {
                res = new Solution().Divide(dividend, divisor);
            }
            Assert.Equal(exp, res);

            dividend = 3;
            divisor = 3;
            exp = 1;
            using (new Timeit())
            {
                res = new Solution().Divide(dividend, divisor);
            }
            Assert.Equal(exp, res);

            dividend = 7;
            divisor = -3;
            exp = -2;
            using (new Timeit())
            {
                res = new Solution().Divide(dividend, divisor);
            }
            Assert.Equal(exp, res);

            // -2147483648 -1
            // 2147483647
            dividend = -2147483648;
            divisor = -1;
            exp = 2147483647;
            using (new Timeit())
            {
                res = new Solution().Divide(dividend, divisor);
            }
            Assert.Equal(exp, res);

            // -2147483648
            // 2
            dividend = -2147483648;
            divisor = 2;
            exp = -1073741824;
            using (new Timeit())
            {
                res = new Solution().Divide(dividend, divisor);
            }
            Assert.Equal(exp, res);
        }
    }
}