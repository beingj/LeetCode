using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;

namespace SimplifyPath
{
    public class Solution
    {
        public string SimplifyPath(string path)
        {
            if (path.EndsWith('/'))
            {
                path = path.TrimEnd('/');
            }
            if (path == "")
            {
                return "/";
            }
            var ds = path.Split('/').Where(i => i != "");
            var p = new List<string>();
            foreach (var i in ds)
            {
                if (i == "..")
                {
                    if (p.Count > 0)
                    {
                        p.RemoveAt(p.Count - 1);
                    }
                }
                else if (i == ".")
                {

                }
                else
                {
                    p.Add(i);
                }
            }
            return "/" + string.Join('/', p);
        }
    }

    public class Test
    {
        static void Verify(string path, string exp)
        {
            Console.WriteLine($"{path}");
            string res;
            using (new Timeit())
            {
                res = new Solution().SimplifyPath(path);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("SimplifyPath");

            var input = @"
/home/
/home
/../
/
/home//foo/
/home/foo
/a/./b/../../c/
/c
/a/../../b/../c//.//
/c
/a//b////c/d//././/..
/a/b/c
";
            var lines = input.CleanInput();
            string path, exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                path = lines[idx++];
                exp = lines[idx++];
                Verify(path, exp);
            }
        }
    }
}