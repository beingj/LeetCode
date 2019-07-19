using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ScrambleString
{
    public class Solution
    {
        Dictionary<string, bool> Saved = new Dictionary<string, bool>();
        public bool IsScramble(string s1, string s2)
        {
            // give up.
            // https://leetcode.com/problems/scramble-string/discuss/29392/Share-my-4ms-c%2B%2B-recursive-solution
            var k = string.Format("{0}|{1}", s1, s2);
            if (Saved.ContainsKey(k))
            {
                return Saved[k];
            }
            int len1 = s1.Length, len2 = s2.Length;
            if (len1 != len2)
            {
                Saved[k] = false;
                return false;
            }
            if (len1 == 0)
            {
                Saved[k] = true;
                return true;
            }
            if (s1 == s2)
            {
                Saved[k] = true;
                return true;
            }

            var cnts = new Dictionary<char, int>();
            for (int i = 0; i < len1; i++)
            {
                if (!cnts.ContainsKey(s1[i]))
                {
                    cnts[s1[i]] = 0;
                }
                cnts[s1[i]]++;
                if (!cnts.ContainsKey(s2[i]))
                {
                    cnts[s2[i]] = 0;
                }
                cnts[s2[i]]--;
            }
            foreach (var i in cnts)
            {
                if (i.Value != 0)
                {
                    Saved[k] = false;
                    return false; // fast pruning 
                }
            }
            if (len1 <= 3)
            {
                Saved[k] = true;
                return true; // fast pruning. 
            }

            for (int i = 1; i < len1; i++)
            {
                if (IsScramble(s1.Substring(0, i), s2.Substring(0, i))
                 && IsScramble(s1.Substring(i), s2.Substring(i)))
                {
                    Saved[k] = true;
                    return true;
                }
                if (IsScramble(s1.Substring(0, i), s2.Substring(len1 - i))
                 && IsScramble(s1.Substring(i), s2.Substring(0, len1 - i)))
                {
                    Saved[k] = true;
                    return true;
                }
            }
            Saved[k] = false;
            return false;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
great
rgeat
true
great
rgeate
false
abcde
caebd
false
bcdebcdebcdebcdebcdebcdebcdebcdebcdebcdebcdebcdebcdebcdebcdebcdebcdebcdebcdebcdebcdebcde
cebdcebdcebdcebdcebdcebdcebdcebdcebdcebdcebdcebdcebdcebdcebdcebdcebdcebdcebdcebdcebdcebd
false
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}