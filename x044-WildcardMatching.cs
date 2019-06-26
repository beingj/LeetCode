
using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace X044
{
    public class Test
    {
        static public void Run()
        {
            string s, p;
            s = "aaaabaabaabbbabaabaabbbbaabaaabaaabbabbbaaabbbbbbabababbaabbabbbbaababaaabbbababbbaabbbaabbaaabbbaabbbbbaaaabaaabaabbabbbaabababbaabbbabababbaabaaababbbbbabaababbbabbabaaaaaababbbbaabbbbaaababbbbaabbbbb";
            // s = "a aaa b aa b aa b b babaabaa bb b baabaaabaaabbabbbaaabbbbbbabababbaabbabbbbaababaaabbbababbbaabbbaabbaaabbbaabbbbbaaaabaaabaabbabbbaabababbaabbbabababbaabaaababbbb babaab ";
            s = "abb ba bb a b aaa aaababbb baa b bbbaaaba bbbb aabbbbb";
            p = "**a*b*b**b*b****bb******b***babaab*ba*a*aaa***baa****b***bbbb*bbaa*a***a*a*****a*b*a*a**ba***aa*a**a*";
            // p = ".*.*a.*b.*b.*.*b.*b.*.*.*.*bb.*.*.*.*.*.*b.*.*.*babaab.*ba.*a.*aaa.*.*.*baa.*.*.*.*b.*.*.*bbbb.*bbaa.*a.*.*.*a.*a.*.*.*.*.*a.*b.*a.*a.*.*ba.*.*.*aa.*a.*.*a.*";
            // p = ".*a.*b.*b.*b.*b.*bb.*b.*babaab.*ba.*a.*aaa.*baa.*b.*bbbb.*bbaa.*a.*a.*a.*a.*b.*a.*a.*.*ba.*aa.*a.*.*a.*";

            // s = "aa";
            // p = "a";
            p = $"^{p}$";
            Regex rx = new Regex(p, RegexOptions.Compiled);
            Console.WriteLine($"=> {rx.Match(s)}");
        }
    }
}