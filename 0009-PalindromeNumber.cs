using System;
using Xunit;
using Util;
using System.Text;

namespace PalindromeNumber
{
    public class Solution
    {
        public bool IsPalindrome(int x)
        {
            if (x < 0)
                return false;
            int len = (int)Math.Log10(x) + 1;
            int half = 0;
            int max = len - 1;
            if (len % 2 == 0)
            {
                half = len / 2;
            }
            else
            {
                half = (len - 1) / 2;
            }
            for (var i = 0; i < half; i++)
            {
                int a = (int)(x / Math.Pow(10, max - i)) % 10;
                int b = (int)(x / Math.Pow(10, i)) % 10;
                if (a != b)
                    return false;
            }
            return true;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("PalindromeNumber");

            int input;
            bool exp, res;

            using (new Timeit())
            {
                input = 121;
                exp = true;
                res = new Solution().IsPalindrome(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = -121;
                exp = false;
                res = new Solution().IsPalindrome(input);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = 10;
                exp = false;
                res = new Solution().IsPalindrome(input);
                Assert.Equal(exp, res);
            }
        }
    }
}