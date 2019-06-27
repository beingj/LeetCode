using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Sqrt
{
    public class Solution
    {
        public int MySqrt(int x)
        {
            // signed 32bit int: 2^31 => sqrt(2^31)=2^15*sqrt(2)
            int maxEnd = (int)(Math.Pow(2, 15) * 1.4142135624);
            // 1.4142135623730950488016887242097

            // Console.WriteLine(maxEnd);
            int start = 1, end = Math.Min(x, maxEnd);
            int mid = (int)((start + end) / 2);
            long sq = mid * mid;
            while (true)
            {
                if (sq == x)
                {
                    return mid;
                }
                else if (sq < x)
                {
                    start = mid + 1;
                }
                else
                {
                    end = mid - 1;
                }

                if (end < start)
                {
                    break;
                }

                // (start+end) may > int.MaxValue, so (start+end)/2 may overflow
                mid = (int)(0.5 * start + 0.5 * end);
                sq = mid * mid;
                // Console.WriteLine(mid);
            }
            if (sq > x)
            {
                return mid - 1;
            }
            else
            {
                if ((mid < maxEnd) && ((mid + 1) * (mid + 1) <= x))
                {
                    return mid + 1;
                }
                return mid;
            }
        }
    }

    public class Test
    {
        static void Verify(int x, int exp)
        {
            Console.WriteLine($"{x}");
            int res;
            using (new Timeit())
            {
                res = new Solution().MySqrt(x);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("Sqrt");

            var input = @"
4
2
8
2
100000000
10000
2147395599
46339
2147395600
46340
2147483647
46340
183692038
13553
";
            var lines = input.CleanInput();
            int x, exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                x = int.Parse(lines[idx++]);
                exp = int.Parse(lines[idx++]);
                Verify(x, exp);
            }
        }
    }
}