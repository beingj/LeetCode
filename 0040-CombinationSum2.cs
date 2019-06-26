using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace CombinationSum2
{
    public class Solution
    {
        public IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            var cand = new List<int>(candidates);
            cand.Sort();
            cand = cand.Where(x => x <= target).ToList();
            // return FullArray(cand, target);
            return TryCombs(cand, target);
        }
        static IList<IList<int>> FullArray(List<int> candidates, int target)
        {
            // Time Limit Exceeded with long candidates
            // need find a way to reduce unnecessary try
            long n = (long)Math.Pow(2, candidates.Count);
            Console.WriteLine($"len: {candidates.Count}, n: {n}");
            List<IList<int>> combs = new List<IList<int>>();
            for (var i = 1; i < n; i++)
            {
                var comb = new List<int>();
                for (var j = 0; j < candidates.Count; j++)
                {
                    if (((i >> j) & 1) == 1)
                    {
                        comb.Add(candidates[j]);
                    }
                }
                var sum = comb.Sum();
                // Console.WriteLine($"try: {i} {sum} {string.Join(',', comb)}");
                if (sum == target)
                {
                    // Console.WriteLine($"add: {string.Join(',', comb)}");
                    combs.Add(comb);
                }
            }
            return RemoveDup(combs);
        }
        static IList<IList<int>> TryCombs(List<int> candidates, int target)
        {
            List<IList<int>> combs = new List<IList<int>>();
            for (var i = 0; i < candidates.Count; i++)
            {
                TryComb(candidates, target, i, new List<int>(), combs);
            }
            return RemoveDup(combs);
        }
        static void TryComb(List<int> candidates, int target, int idx, List<int> soFar, List<IList<int>> combs)
        {
            if (idx >= candidates.Count)
            {
                return;
            }
            var x = candidates[idx];
            var n = target / x;
            // Each number in candidates may only be used once in the combination.
            n = Math.Min(n, 1);
            for (var i = 0; i <= n; i++)
            {
                var y = target - x * i;
                var comb = new List<int>();
                comb.AddRange(soFar);
                for (var j = 0; j < i; j++)
                {
                    comb.Add(x);
                }

                if (y == 0)
                {
                    // Console.WriteLine($"add: {string.Join(',', comb)}");
                    combs.Add(comb);
                }
                else
                {
                    TryComb(candidates, y, idx + 1, comb, combs);
                }
            }
        }
        static List<IList<int>> RemoveDup(List<IList<int>> combs)
        {
            // remove duplicated ones
            List<IList<int>> combs2 = new List<IList<int>>();
            var combsStr = new List<string>();
            foreach (var i in combs)
            {
                var j = string.Join(',', i);
                if (combsStr.Contains(j))
                {
                    continue;
                }
                else
                {
                    combs2.Add(i);
                    combsStr.Add(j);
                }
            }
            return combs2;
        }
    }

    public class Test
    {
        static void Verify(int[] input, int target, IList<IList<int>> exp)
        {
            Console.WriteLine($"{string.Join(',', input)} => {target}");
            IList<IList<int>> res;
            using (new Timeit())
            {
                res = new Solution().CombinationSum2(input, target);
            }
            var exp2 = exp.Select(x => string.Join(',', x)).ToList();
            var res2 = res.Select(x => string.Join(',', x)).ToList();
            exp2.Sort();
            res2.Sort();
            // Console.WriteLine($"{string.Join(" | ", res2)}");
            Assert.Equal(exp2, res2);
        }
        static public void Run()
        {
            Console.WriteLine("CombinationSum2");
            int[] input;
            int target;
            IList<IList<int>> exp;

            input = new int[] { 10, 1, 2, 7, 6, 1, 5 };
            target = 8;
            exp = new List<IList<int>>{
                new List<int>{1,7},
                new List<int>{1,2,5},
                new List<int>{2,6},
                new List<int>{1,1,6}
            };
            Verify(input, target, exp);

            input = new int[] { 2, 5, 2, 1, 2 };
            target = 5;
            exp = new List<IList<int>>{
                new List<int>{1,2,2},
                new List<int>{5},
            };
            Verify(input, target, exp);

            input = new int[] { 14, 6, 25, 9, 30, 20, 33, 34, 28, 30, 16, 12, 31, 9, 9, 12, 34, 16, 25, 32, 8, 7, 30, 12, 33, 20, 21, 29, 24, 17, 27, 34, 11, 17, 30, 6, 32, 21, 27, 17, 16, 8, 24, 12, 12, 28, 11, 33, 10, 32, 22, 13, 34, 18, 12 };
            target = 27;
            exp = new List<IList<int>>{
                new List<int>{10,17},
                new List<int>{11,16},
                new List<int>{13,14},
                new List<int>{27},
                new List<int>{6,10,11},
                new List<int>{6,21},
                new List<int>{6,6,7,8},
                new List<int>{6,7,14},
                new List<int>{6,8,13},
                new List<int>{6,9,12},
                new List<int>{7,20},
                new List<int>{7,8,12},
                new List<int>{7,9,11},
                new List<int>{8,8,11},
                new List<int>{8,9,10},
                new List<int>{9,18},
                new List<int>{9,9,9},
            };
            Verify(input, target, exp);
        }
    }
}