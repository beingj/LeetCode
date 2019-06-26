using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace UniquePaths
{
    public class Solution
    {
        // given m cols and n rows, all paths are: (m-1) Right and (n-1) Down
        // so question become: permutation of (m-1) R and (n-1) D
        // answer is: factorial(m-1+n-1)/(factorial(m-1)*factorial(n-1))
        public int UniquePaths(int m, int n)
        {
            // return (int)(Fact(m + n - 2) / Fact(m - 1) / Fact(n - 1));
            m--; n--;
            var max = Math.Max(m, n);
            var min = Math.Min(m, n);
            long res = 1;
            for (var i = m + n; i > max; i--)
            {
                res *= i;
            }
            res = res / Fact(min);
            return (int)res;
        }
        public static long Fact(int n)
        {
            long res = 1;
            for (var i = 1; i <= n; i++)
            {
                res *= i;
            }
            return res;
        }
        // public static void Move(int m, int n, int cell, List<char> res)
        // {
        //     if (cell == (m * n - 1))
        //     {
        //         res.Add('a');
        //     }
        //     int row = cell / m;
        //     int col = cell % m;
        //     if ((col + 1) < m)
        //     {
        //         Move(m, n, cell + 1, res);
        //     }
        //     if ((row + 1) < n)
        //     {
        //         // Console.WriteLine(string.Format("{0}=>{1}",string.Join("->", soFar),"D"));
        //         Move(m, n, cell + m, res);
        //     }
        // }
        // public static void Move1(int m, int n, int cell, List<string> soFar = null, List<char> res = null)
        // {
        //     if (cell == (m * n - 1))
        //     {
        //         // Console.WriteLine($"cell {cell}");
        //         if (res == null)
        //         {
        //             res = new List<char>();
        //         }
        //         res.Add('a');
        //         // Console.WriteLine($"{res.Count}: {string.Join("->", soFar)}");
        //         Console.WriteLine($"{string.Join("\t", soFar)}");
        //     }
        //     int row = cell / m;
        //     int col = cell % m;
        //     if (soFar == null)
        //     {
        //         soFar = new List<string>();
        //     }
        //     var newSoFar = new List<string>();
        //     newSoFar.AddRange(soFar);
        //     if ((col + 1) < m)
        //     {
        //         // Console.WriteLine(string.Format("{0}=>{1}",string.Join("->", soFar),"R"));
        //         newSoFar.Add("R");
        //         Move1(m, n, cell + 1, newSoFar, res);
        //     }
        //     newSoFar.Clear();
        //     newSoFar.AddRange(soFar);
        //     if ((row + 1) < n)
        //     {
        //         // Console.WriteLine(string.Format("{0}=>{1}",string.Join("->", soFar),"D"));
        //         newSoFar.Add("D");
        //         Move1(m, n, cell + m, newSoFar, res);
        //     }
        // }
    }

    public class Test
    {
        static void Verify(int m, int n, int exp)
        {
            Console.WriteLine($"{m}x{n}");
            int res;
            using (new Timeit())
            {
                res = new Solution().UniquePaths(m, n);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("UniquePaths");
            // var res = new List<char>();
            // Solution.Move(99, 9, 0, res);
            // Console.WriteLine($"{res.Count}");
            // Solution.Move1(5, 4, 0, res: res);

            // return;
            var input = @"
3
2
3
3
3
6
4
3
10
7
3
28
7
4
84
9
5
495
23
12
193536720
";
            var lines = input.Trim(new char[] { '\n', '\r', ' ' }).Split('\n')
                            .Select(x => x.Trim(new char[] { '\r', ' ' })).Where(y => !y.StartsWith('#')).ToArray();
            int m, n;
            int exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                m = int.Parse(lines[idx++]);
                n = int.Parse(lines[idx++]);
                exp = int.Parse(lines[idx++]);
                Verify(m, n, exp);
            }
        }
    }
}