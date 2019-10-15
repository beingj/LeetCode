using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RepeatedDnaSequences
{
    public class Solution
    {
        public IList<string> FindRepeatedDnaSequences(string s)
        {
            // copy from: https://leetcode.com/problems/repeated-dna-sequences/discuss/53855/7-lines-simple-Java-O(n)
            var seen = new HashSet<string>();
            var repeated = new HashSet<string>();
            for (int i = 0; i + 9 < s.Length; i++)
            {
                var ten = s.Substring(i, 10);
                if (!seen.Add(ten))
                    repeated.Add(ten);
            }
            return repeated.ToList();
        }
        public IList<string> FindRepeatedDnaSequences1(string s)
        {
            int len = 10;
            var seq = new HashSet<string>();

            for (var i = 0; i < s.Length - len; i++)
            {
                for (var j = i + 1; j <= s.Length - len; j++)
                {
                    bool m = true;
                    for (var k = 0; k < len; k++)
                    {
                        if (s[j + k] != s[i + k])
                        {
                            m = false;
                            break;
                        }
                    }
                    if (m) seq.Add(s.Substring(i, len));
                }
            }
            return seq.ToList();
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
";
            var lines = input.CleanInput();
            lines = "0187-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}