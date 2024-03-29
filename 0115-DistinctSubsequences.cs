using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DistinctSubsequences
{
    public class Solution
    {
        public int NumDistinct(string s, string t)
        {
            var allChars = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            var notInTChars = allChars.Where(c => !t.Contains(c)).ToArray();
            s = s.Trim(notInTChars);

            return CachedMyNumDistinct(s, t, new Dictionary<(string, string), int>());
        }
        int CachedMyNumDistinct(string s, string t, Dictionary<(string, string), int> cache)
        {
            var k = (s, t);
            if (!cache.ContainsKey(k))
                cache[k] = MyNumDistinct(s, t, cache);
            return cache[k];
        }
        int MyNumDistinct(string s, string t, Dictionary<(string, string), int> cache)
        {
            if (t.Length == 1)
            {
                return s.Where(i => i == t[0]).Count();
            }
            var sum = 0;
            var sIdx = 0;
            while (true)
            {
                sIdx = s.IndexOf(t[0]);
                if (sIdx < 0) break;
                s = s.Substring(sIdx + 1);
                sum += CachedMyNumDistinct(s, t.Substring(1), cache);
            }
            return sum;
        }
    }
    public class Test
    {
        // index version is even slower(4x) than substring version
        // int MyNumDistinct2(string s, string t, int sIdx, int tIdx)
        // {
        //     if ((t.Length - tIdx) == 1)
        //     {
        //         return s.Skip(sIdx).Where(i => i == t[tIdx]).Count();
        //     }
        //     var sum = 0;
        //     while (true)
        //     {
        //         sIdx = s.IndexOf(t[tIdx], sIdx);
        //         if (sIdx < 0) break;
        //         sum += MyNumDistinct2(s, t, sIdx + 1, tIdx + 1);
        //         sIdx++;
        //     }
        //     return sum;
        // }
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
aabb
ab
4
daacaedaceacabbaabdccdaaeaebacddadcaeaacadbceaecddecdeedcebcdacdaebccdeebcbdeaccabcecbeeaadbccbaeccbbdaeadecabbbedceaddcdeabbcdaeadcddedddcececbeeabcbecaeadddeddccbdbcdcbceabcacddbbcedebbcaccac
ceadbaa
8556153
ccc
c
3
b
b
1
rabbbit
rabbit
3
babgbag
bag
5
ddd
dd
3
";
            var lines = input.CleanInput();
            lines = "0115-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}