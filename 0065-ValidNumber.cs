using System;
using Xunit;
using Util;
using System.Linq;

namespace ValidNumber
{
    // here is a list of characters that can be in a valid decimal number:
    // Numbers 0-9
    // Exponent - "e"
    // Positive/negative sign - "+"/"-"
    // Decimal point - "."
    public class Solution
    {
        public bool IsNumber(string s)
        {
            s = s.Trim(new char[] { ' ', '\t' });
            if (s.Length == 0)
            {
                return false;
            }
            var validNum = "0123456789";
            if (s.Length == 1)
            {
                if (!validNum.Contains(s[0]))
                {
                    return false;
                }
            }
            var validStart = "+-" + validNum + '.';
            if (!validStart.Contains(s[0]))
            {
                return false;
            }
            var validChar = validStart + "e";

            char[] signs = new char[] { '+', '-' };
            int num = 0;
            bool exp = false;
            int expidx = 0;
            int expnum = 0;
            bool point = false;
            int i = 0;
            while (true)
            {
                if (i >= s.Length)
                {
                    break;
                }
                var c = s[i];
                if (!validChar.Contains(c))
                {
                    return false;
                }
                if (signs.Contains(c))
                {
                    if (i > 0 && !exp)
                    {
                        return false;
                    }
                    if (i > 0 && exp && (i != (expidx + 1)))
                    {
                        return false;
                    }
                }
                else if (validNum.Contains(c))
                {
                    if (!exp)
                    {
                        num++;
                    }
                    else
                    {
                        expnum++;
                    }
                }
                else if (c == '.')
                {
                    if (point)
                    {
                        return false;
                    }
                    if (i > 0)
                    {
                        var c2 = s[i - 1];
                        if (!validNum.Contains(c2) && !signs.Contains(c2))
                        {
                            return false;
                        }
                    }
                    if (exp)
                    {
                        return false;
                    }
                    point = true;
                }
                else if (c == 'e')
                {
                    if (i == s.Length - 1)
                    {
                        return false;
                    }
                    if (exp)
                    {
                        return false;
                    }
                    var c2 = s[i - 1];
                    if (!validNum.Contains(c2) && c2 != '.')
                    {
                        return false;
                    }
                    if ((i + 1) < s.Length)
                    {
                        c2 = s[i + 1];
                        if (!validNum.Contains(c2) && !signs.Contains(c2))
                        {
                            return false;
                        }
                    }
                    exp = true;
                    expidx = i;
                }
                i++;
            }
            if (exp && expnum == 0)
            {
                return false;
            }
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class Test
    {
        static void Verify(string s, bool exp)
        {
            Console.WriteLine(s);
            bool res;
            using (new Timeit())
            {
                res = new Solution().IsNumber(s);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("ValidNumber");

            var input = @"
0
true
 0.1 
true
abc
false
1 a
false
2e10
true
 -90e3   
true
 1e
false
e3
false
 6e-1
true
 99e2.5 
false
53.5e93
true
 --6 
false
-+3
false
95a54e53
false
   
false
.1
true
3.
true
.
false
+.8
true
-.
false
 +0e-
false
46.e3
true
 005047e+6
true
";
            var lines = input.Trim(new char[] { '\n', '\r', ' ' }).Split('\n')
                            .Select(x => x.Trim(new char[] { '\r', ' ' })).Where(y => !y.StartsWith('#')).ToArray();
            string s;
            bool exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                s = lines[idx++];
                exp = bool.Parse(lines[idx++]);
                Verify(s, exp);
            }
        }
    }
}