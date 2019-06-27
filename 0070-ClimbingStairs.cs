using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace ClimbingStairs
{
    public class Solution
    {
        public int ClimbStairs(int n)
        {
            int a, b;
            int maxB = n / 2;
            int sum = 0;
            for (b = 0; b <= maxB; b++)
            {
                a = n - b * 2;
                sum += Perm(a, b);
            }
            return sum;
        }
        public static int Perm(int a, int b)
        {
            if (a == 0 || b == 0)
            {
                return 1;
            }
            // var res = Fact(a + b) / Fact(a) / Fact(b);

            var up = new List<int>();
            var max = Math.Max(a, b);
            for (var i = a + b; i > max; i--)
            {
                up.Add(i);
            }

            var down = new List<int>();
            var min = Math.Min(a, b);
            for (var i = min; i > 1; i--)
            {
                down.Add(i);
            }

            var toBeRemoved = new List<int>();
            foreach (var x in down)
            {
                for (var i = 0; i < up.Count; i++)
                {
                    if (up[i] % x == 0)
                    {
                        up[i] = up[i] / x;
                        toBeRemoved.Add(x);
                        break;
                    }
                }
            }
            foreach (var i in toBeRemoved)
            {
                down.Remove(i);
            }
            long res = 1;
            foreach (var i in up)
            {
                res *= i;
            }
            foreach (var i in down)
            {
                res /= i;
            }
            // Console.WriteLine($"{a}+{b} => {res}");
            return (int)res;
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
                res = new Solution().ClimbStairs(x);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("ClimbingStairs");

            var input = @"
2
2
3
3
4
5
35
14930352
44
1134903170
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