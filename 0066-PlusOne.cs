using System;
using Xunit;
using Util;
using System.Linq;

namespace PlusOne
{
    public class Solution
    {
        public int[] PlusOne(int[] digits)
        {
            int idx = digits.Length - 1;
            int toAdd = 1;
            while (idx >= 0)
            {
                if (toAdd == 0)
                {
                    break;
                }
                var n = digits[idx] + toAdd;
                digits[idx] = n % 10;
                toAdd = (int)(n / 10);
                if (n < 10)
                {
                    break;
                }
                idx--;
            }
            if (toAdd == 0)
            {
                return digits;
            }
            var newDigits = new int[digits.Length + 1];
            newDigits[0] = toAdd;
            for (var i = 0; i < digits.Length; i++)
            {
                newDigits[i + 1] = digits[i];
            }
            return newDigits;
        }
    }

    public class Test
    {
        static void Verify(int[] digits, int[] exp)
        {
            Console.WriteLine(digits.Int1dToJson());
            int[] res;
            using (new Timeit())
            {
                res = new Solution().PlusOne(digits);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("PlusOne");

            var input = @"
[1,2,3]
[1,2,4]
[4,3,2,1]
[4,3,2,2]
[9,9,9]
[1,0,0,0]
";
            var lines = input.Trim(new char[] { '\n', '\r', ' ' }).Split('\n')
                            .Select(x => x.Trim(new char[] { '\r', ' ' })).Where(y => !y.StartsWith('#')).ToArray();
            int[] digits;
            int[] exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                digits = lines[idx++].JsonToInt1d();
                exp = lines[idx++].JsonToInt1d();
                Verify(digits, exp);
            }
        }
    }
}