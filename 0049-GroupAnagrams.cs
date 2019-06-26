using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace GroupAnagrams
{
    public class Solution
    {
        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            var dict = new Dictionary<string, List<string>>();
            foreach (var s in strs)
            {
                var cs = s.ToList();
                cs.Sort();
                var s2 = new string(cs.ToArray());
                if (!dict.ContainsKey(s2))
                {
                    dict[s2] = new List<string>();
                }
                dict[s2].Add(s);
            }
            var res = new List<IList<string>>();
            foreach(var v in dict.Values)
            {
                res.Add(v);
            }
            return res;
        }
    }
    public class Test
    {
        static void Verify(string[] strs, IList<IList<string>> exp)
        {
            Console.WriteLine($"{string.Join(',', strs)}");
            IList<IList<string>> res;
            using (new Timeit())
            {
                res = new Solution().GroupAnagrams(strs);
            }
            var exp2 = exp.Select(x => { var y = x.ToList(); y.Sort(); return string.Join(',', y); }).ToList();
            var res2 = res.Select(x => { var y = x.ToList(); y.Sort(); return string.Join(',', y); }).ToList();
            exp2.Sort();
            res2.Sort();
            // Console.WriteLine($"{string.Join(" | ", res2)}");
            Assert.Equal(exp2, res2);
        }
        static public void Run()
        {
            Console.WriteLine("GroupAnagrams");
            string[] strs;
            IList<IList<string>> exp;

            strs = new string[]{
                "eat", "tea", "tan", "ate", "nat", "bat"
            };
            exp = new List<IList<string>>{
                new List<string>{"ate","eat","tea"},
                new List<string>{"nat","tan"},
                new List<string>{"bat"},
            };
            Verify(strs, exp);

        }
    }
}