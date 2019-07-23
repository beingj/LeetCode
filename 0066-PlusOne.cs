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
        static public void Run()
        {
            var input = @"
[1,2,3]
[1,2,4]
[4,3,2,1]
[4,3,2,2]
[9,9,9]
[1,0,0,0]
";
            var lines=input.CleanInput();
            Verify.Method(new Solution(), lines, sortRet: true);
        }
    }
}