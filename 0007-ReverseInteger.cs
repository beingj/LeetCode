using System;
using Xunit;
using Util;
using System.Text;

namespace ReverseInteger
{
    public class Solution
    {
        public int Reverse(int x)
        {
            // 32-bit signed integer range: [−2^31,  2^31 − 1]
            // returns 0 when the reversed integer overflows.
            int min = (int)(-Math.Pow(2, 31));
            int max = (int)Math.Pow(2, 31) - 1;
            if ((x <= min) || (x > max))
                return 0;

            // temp result may overflows, so set them to long type
            long x2 = Math.Abs(x);
            int e = (int)Math.Log10(x2);
            long f = 0;
            long res = 0;
            int z = 0;

            for (var i = 0; i <= e; i++)
            {
                f = (int)Math.Pow(10, i);
                z = (int)((x2 / f) % 10);
                res += (long)(z * Math.Pow(10, e - i));
                if (((x < 0) && (-res < min)) ||
                    ((x > 0) && (res > max)))
                    return 0;
            }
            if (x < 0)
                res = -res;
            return (int)res;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("ReverseInteger");

            int input;
            int exp, res;

            // Console.WriteLine((int)Math.Log10(123));
            // Console.WriteLine(Math.Log10(-999));

            using (new Timeit())
            {
                input = 123;
                exp = 321;
                res = new Solution().Reverse(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = -123;
                exp = -321;
                res = new Solution().Reverse(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = 120;
                exp = 21;
                res = new Solution().Reverse(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = -2147483648;
                exp = 0;
                res = new Solution().Reverse(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = 1534236469;
                exp = 0;
                res = new Solution().Reverse(input);
                Assert.Equal(exp, res);
            }
        }
    }

}