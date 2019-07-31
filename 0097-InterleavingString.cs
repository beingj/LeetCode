using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace InterleavingString
{
    public class Solution
    {
        Dictionary<string, bool> Saved = new Dictionary<string, bool>();
        public bool IsInterleave(string s1, string s2, string s3)
        {
            if (s1.Length + s2.Length != s3.Length)
                return false;

            if (MyCheck(s1, s2, s3))
                return true;
            if (MyCheck(s2, s1, s3))
                return true;
            return false;
        }

        bool MyCheck(string s1, string s2, string s3)
        {
            var k = $"{s1}|{s2}|{s3}";
            if (Saved.ContainsKey(k))
            {
                return Saved[k];
            }

            if (s2 == "")
            {
                var x = s1 == s3;
                Saved[k] = x;
                return x;
            }

            var c2 = s2[0];
            for (var idx = 0; idx < s1.Length; idx++)
            {
                if (s1[idx] == s3[idx])
                {
                    if (c2 == s3[idx])
                    {
                        if (idx > 0)
                        {
                            // s1 must remove at least 1 char
                            var x = MyCheck(s2, s1.Substring(idx), s3.Substring(idx));
                            if (x)
                            {
                                Saved[k] = true;
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    // if (s1[s1idx] != s3[s3idx])
                    if (c2 != s3[idx])
                    {
                        Saved[k] = false;
                        return false;
                    }
                    else
                    {
                        var x = MyCheck(s2, s1.Substring(idx), s3.Substring(idx));
                        Saved[k] = x;
                        return x;
                    }
                }
            }
            // s1 all matches
            var y = s2 == s3.Substring(s1.Length);
            Saved[k] = y;
            return y;
        }
    }
    public class Test
    {
        bool Check1Char(string s1, string s2, string s3)
        {
            // s1.Length==1
            if ($"{s2}{s1}" == s3)
            {
                return true;
            }
            var c = s1[0];
            for (var i = 0; i < s2.Length; i++)
            {
                var s = s2.ToList();
                s.Insert(i, c);
                if (new string(s.ToArray()) == s3)
                {
                    return true;
                }
            }
            return false;
        }
        bool MyCheck4(string s1, string s2, string s3)
        {
            if ((s1 == "") || (s2 == ""))
            {
                return $"{s1}{s2}" == s3;
            }
            if ((s1.Length == 1) || (s2.Length == 1))
            {
                if (s1.Length == 1)
                    return Check1Char(s1, s2, s3);
                else
                    return Check1Char(s2, s1, s3);
            }

            if (s1.Length + s2.Length != s3.Length)
            {
                return false;
            }
            var s1idx = 0;
            // var s2idx = 0;
            var c2 = s2[0];
            var s3idx = 0;
            while (true)
            {
                if (s3idx >= s3.Length)
                {
                    break;
                }

                while (s1idx < s1.Length)
                {
                    if (s1[s1idx] == s3[s3idx])
                    {
                        if (c2 == s3[s3idx])
                        {
                            if (s1idx > 0)
                            {
                                var x = MyCheck4(s2, s1.Substring(s1idx), s3.Substring(s3idx));
                                if (x)
                                {
                                    return true;
                                }
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        // if (s1[s1idx] != s3[s3idx])
                        if (c2 != s3[s3idx])
                        {
                            return false;
                        }
                        else
                        {
                            return MyCheck4(s2, s1.Substring(s1idx), s3.Substring(s3idx));
                        }
                    }
                    s1idx++;
                    s3idx++;
                }
            }
            return false;
        }
        public bool MyCheck3(string s1, string s2, string s3)
        {
            if (s1 == "")
            {
                return s2 == s3;
            }
            if (s2 == "")
            {
                return s1 == s3;
            }
            if (s1.Length + s2.Length != s3.Length)
            {
                return false;
            }
            var s1idx = 0;
            var s2idx = 0;
            var s3idx = 0;

            while (s1idx < s1.Length)
            {
                if (s3[s3idx] != s1[s1idx])
                    break;
                s1idx++;
                s3idx++;
            }

            if (s3[s3idx] != s2[s2idx])
            {
                return false;
            }
            while (s2idx < s2.Length)
            {
                if (s3[s3idx] != s2[s2idx])
                    break;
                s2idx++;
                s3idx++;
            }

            var x = MyCheck3(s1.Substring(s1idx), s2.Substring(s2idx), s3.Substring(s3idx));
            if (x)
            {
                return true;
            }
            return false;
        }
        public bool MyCheck2(string s1, string s2, string s3)
        {
            if ((s1 == s3) && s2 == "")
            {
                return true;
            }
            if ((s2 == s3) && s1 == "")
            {
                return true;
            }
            if (s1.Length + s2.Length != s3.Length)
            {
                return false;
            }
            var s1idx = 0;
            var s2idx = 0;
            var s3idx = 0;

            while (s3[s3idx] == s1[s1idx])
            {
                s1idx++;
                s3idx++;
                if (s1idx == s1.Length)
                    break;
                continue;
            }
            if (s3[s3idx] != s2[s2idx])
            {
                return false;
            }

            while (s3[s3idx] == s2[s2idx])
            {
                s2idx++;
                s3idx++;
                if (s2idx == s2.Length)
                    break;
                continue;
            }
            var x = MyCheck2(s1.Substring(s1idx), s2.Substring(s2idx), s3.Substring(s3idx));
            if (x)
            {
                return true;
            }
            return false;
        }
        public bool MyCheck1(string s1, string s2, string s3)
        {
            if ((s1 == s3) && s2 == "")
            {
                return true;
            }
            var s1idx = 0;
            var s2idx = 0;
            var s3idx = 0;
            while (true)
            {
                if (s1idx >= s1.Length)
                {
                    break;
                }
                if (s2idx >= s2.Length)
                {
                    break;
                }
                if (s3idx >= s3.Length)
                {
                    break;
                }

                if (s3[s3idx] == s1[s1idx])
                {
                    s1idx++;
                    s3idx++;
                    continue;
                }
                if (s3[s3idx] != s2[s2idx])
                {
                    return false;
                }

                for (; s2idx < s2.Length; s2idx++)
                {
                    if (s3[s3idx] != s2[s2idx])
                    {
                        break;
                    }
                    s3idx++;
                }
                var x = MyCheck1(s1.Substring(s1idx), s2.Substring(s2idx), s3.Substring(s3idx));
                if (x)
                {
                    return true;
                }
            }
            return false;
        }
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
bbca
c
bbcac
true
aabcc
dbbca
aadbcbbcac
true
a

c
false
aa
ab
abaa
true

b
b
true
c

c
true
aabcc
dbbca
aadbbcbcac
true
aabcc
dbbca
aadbbbaccc
false
";
            // var lines = input.CleanInput();
            var lines = "0097-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}