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
        public bool IsInterleave(string s1, string s2, string s3)
        {
            if (s1.Length + s2.Length != s3.Length) return false;

            var cacheDict = new Dictionary<(string, string, string), bool>();
            if (MyCheckWithCache(s1, s2, s3, cacheDict)) return true;
            if (MyCheckWithCache(s2, s1, s3, cacheDict)) return true;

            return false;
        }
        bool MyCheckWithCache(string s1, string s2, string s3, Dictionary<(string, string, string), bool> cacheDict)
        {
            var k = (s1, s2, s3);
            if (!cacheDict.ContainsKey(k))
            {
                cacheDict[k] = MyCheck(s1, s2, s3, cacheDict);
            }
            return cacheDict[k];
        }

        bool MyCheck(string s1, string s2, string s3, Dictionary<(string, string, string), bool> cacheDict)
        {
            if (s2 == "") return s1 == s3;

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
                            var x = MyCheckWithCache(s2, s1.Substring(idx), s3.Substring(idx), cacheDict);
                            if (x) return true;
                        }
                    }
                }
                else
                {
                    // if (s1[s1idx] != s3[s3idx])
                    if (c2 != s3[idx]) return false;
                    return MyCheckWithCache(s2, s1.Substring(idx), s3.Substring(idx), cacheDict);
                }
            }
            // s1 all matches
            return s2 == s3.Substring(s1.Length);
        }
    }
    public class Test
    {
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
            var lines = input.CleanInput();
            // var lines = "0097-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}