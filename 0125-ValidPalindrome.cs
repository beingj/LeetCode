using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ValidPalindrome
{
    public class Solution
    {
        public bool IsPalindrome(string s)
        {
            var alphanumeric = "abcdefghijklmnopqrstuvwxyz0123456789".ToArray();
            var cs = s.ToLower().Where(c => alphanumeric.Contains(c)).ToList();
            for (var i = 0; i < cs.Count; i++)
            {
                if (cs[i] != cs[cs.Count - 1 - i])
                    return false;
            }
            return true;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
A man, a plan, a canal: Panama
true
race a car
false
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}