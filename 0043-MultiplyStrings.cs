using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace MultiplyStrings
{
    public class Solution
    {
        public string Multiply(string num1, string num2)
        {
            if (num1 == "0" || num2 == "0")
                return "0";
            var n1 = num1.ToCharArray().Select(i => int.Parse(i.ToString())).ToList();
            n1.Reverse();
            var n2 = num2.ToCharArray().Select(i => int.Parse(i.ToString())).ToList();
            n2.Reverse();

            var resList = new List<int>();
            var tmp = new List<int>();
            var carry = 0;
            for (var digit = 0; digit < n2.Count; digit++)
            {
                tmp.Clear();
                carry = 0;
                for (var i = 0; i < n1.Count; i++)
                {
                    var p = n2[digit] * n1[i] + carry;
                    if (p < 10)
                    {
                        tmp.Add(p);
                        carry = 0;
                    }
                    else
                    {
                        tmp.Add(p % 10);
                        carry = p / 10;
                    }
                }
                if (carry > 0)
                {
                    tmp.Add(carry);
                }

                carry = 0;
                for (var i = 0; i < tmp.Count; i++)
                {
                    var j = i + digit;
                    int sum = carry;
                    if (j < resList.Count)
                    {
                        sum += resList[j];
                    }
                    sum += tmp[i];
                    if (sum < 10)
                    {
                        carry = 0;
                    }
                    else
                    {
                        carry = sum / 10;
                        sum = sum % 10;
                    }
                    if (j < resList.Count)
                    {
                        resList[j] = sum;
                    }
                    else
                    {
                        resList.Add(sum);
                    }
                }
                if (carry > 0)
                {
                    resList.Add(carry);
                }
            }

            // Console.WriteLine(string.Join("", resList));
            resList.Reverse();
            return string.Join("", resList);
        }
    }

    public class Test
    {
        static void Verify(string num1, string num2, string exp)
        {
            Console.WriteLine($"{num1},{num2}");
            string res;
            using (new Timeit())
            {
                res = new Solution().Multiply(num1, num2);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("MultiplyStrings");
            string num1, num2;
            string exp;

            // num1 = "2";
            // num2 = "3";
            // exp = "6";
            // Verify(num1, num2, exp);

            // num1 = "123";
            // num2 = "456";
            // exp = "56088";
            // Verify(num1, num2, exp);

            num1 = "123";
            num2 = "396";
            exp = "48708";
            Verify(num1, num2, exp);

            // "123456789"
            // "987654321"
            // "121932631112635269"
            num1 = "123456789";
            num2 = "987654321";
            exp = "121932631112635269";
            Verify(num1, num2, exp);

        }
    }
}