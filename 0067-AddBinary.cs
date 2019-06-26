using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;

namespace AddBinary
{
    public class Solution
    {
        public string AddBinary(string a, string b)
        {
            bool carry = false;
            int max = Math.Max(a.Length, b.Length);
            var res = new List<char>();
            for (var i = 0; i < max; i++)
            {
                var ai = a.Length - 1 - i;
                var bi = b.Length - 1 - i;
                char ac = ai < 0 ? '0' : a[ai];
                char bc = bi < 0 ? '0' : b[bi];
                if (carry)
                {
                    if (ac == '1' || bc == '1')
                    {
                        carry = true;
                        if (ac == '1' && bc == '1')
                        {
                            // 1+1  +1
                            res.Add('1');
                        }
                        else
                        {
                            // 1+0  +1
                            res.Add('0');
                        }
                    }
                    else
                    {
                        // 0+0  +1
                        carry = false;
                        res.Add('1');
                    }
                }
                else
                {
                    if (ac == '1' && bc == '1')
                    {
                        carry = true;
                        // 1+1  +0
                        res.Add('0');
                    }
                    else
                    {
                        carry = false;
                        if (ac == '1' || bc == '1')
                        {
                            // 1+0  +0
                            res.Add('1');
                        }
                        else
                        {
                            // 0+0  +0
                            res.Add('0');
                        }
                    }
                }
            }
            if (carry)
            {
                res.Add('1');
            }
            res.Reverse();
            return string.Join("", res);
        }
    }

    public class Test
    {
        static void Verify(string a, string b, string exp)
        {
            Console.WriteLine($"{a} + {b}");
            string res;
            using (new Timeit())
            {
                res = new Solution().AddBinary(a, b);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("AddBinary");

            var input = @"
11
1
100
1010
1011
10101
1111
1111
11110
";
            var lines = input.Trim(new char[] { '\n', '\r', ' ' }).Split('\n')
                            .Select(x => x.Trim(new char[] { '\r', ' ' })).Where(y => !y.StartsWith('#')).ToArray();
            string a, b;
            string exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                a = lines[idx++];
                b = lines[idx++];
                exp = lines[idx++];
                Verify(a, b, exp);
            }
        }
    }
}