using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace PermutationSequence
{
    public class Solution
    {
        public string GetPermutation(int n, int k)
        {
            var nums = new List<int>();
            for (var i = 1; i <= n; i++)
            {
                nums.Add(i);
            }

            k--;
            var current = new List<int>();
            int idx;
            for (var i = n; i > 2; i--)
            {
                var x = Factorial(i - 1);
                var y = Factorial(i);
                idx = (k % y) / x;
                current.Add(nums[idx]);
                nums.RemoveAt(idx);
            }

            var z = k % 2;
            if (z == 1)
            {
                nums.Reverse();
            }
            current.AddRange(nums);
            return string.Join("", current);
        }
        static int Factorial(int number)
        {
            int fact = number;
            for (var i = number - 1; i >= 1; i--)
            {
                fact = fact * i;
            }
            return fact;
        }
    }
    public class Solution1
    {
        public string GetPermutation(int n, int k)
        {
            var current = new List<int>();
            for (var i = 1; i <= n; i++)
            {
                current.Add(i);
            }

            for (var i = 1; i < k; i++)
            {
                current = Permutation(current);
            }
            return string.Join("", current);
        }
        List<int> Permutation(List<int> current)
        {
            // "123"
            // "132"
            // "213" 3,3
            // "231"
            // "312"
            // "321"

            // 1234
            // 1243
            // 1324
            // 1342
            // 1423
            // 1432
            // 2134
            // 2143
            // 2314 4,9
            // 2341

            int i;
            for (i = current.Count - 1; i > 0; i--)
            {
                if (current[i] > current[i - 1])
                {
                    break;
                }
            }
            List<int> next = new List<int>();
            var prev = current[i - 1];
            var tail = current.Skip(i).ToList();
            var x = tail.Where(y => y > prev).Min();
            tail.Remove(x);
            tail.Add(prev);
            tail.Sort();
            next.AddRange(current.Take(i - 1).ToList());
            next.Add(x);
            next.AddRange(tail);
            return next;
        }
    }
    public class Test
    {
        static void Verify(int n, int k, string exp)
        {
            Console.WriteLine($"{n},{k}");
            string res;
            using (new Timeit())
            {
                res = new Solution().GetPermutation(n, k);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("PermutationSequence");
            int n, k;
            string exp;
            var input = @"
3
3
213
4
9
2314
9
171669
531679428
9
278621
792861534
2
2
21
3
2
132
";
            var lines = input.Trim(new char[] { '\n', '\r', ' ' }).Split('\n')
                            .Select(x => x.Trim(new char[] { '\r', ' ' })).Where(y => !y.StartsWith('#')).ToArray();
            int idx = 0;
            while (idx < lines.Length)
            {
                n = int.Parse(lines[idx++]);
                k = int.Parse(lines[idx++]);
                exp = lines[idx++];
                Verify(n, k, exp);
            }
        }
    }
}