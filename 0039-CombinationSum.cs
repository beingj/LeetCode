using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace CombinationSum
{
    public class Solution
    {
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            List<IList<int>> combs = new List<IList<int>>();
            for (var i = 0; i < candidates.Length; i++)
            {
                TryComb(candidates, target, i, new List<int>(), combs);
            }
            // remove duplicated ones
            List<IList<int>> combs2 = new List<IList<int>>();
            var combs3 = new List<string>();
            foreach (var i in combs)
            {
                var j = string.Join(',', i);
                if (combs3.Contains(j))
                {
                    continue;
                }
                else
                {
                    combs2.Add(i);
                    combs3.Add(j);
                }
            }
            return combs2;
        }
        static void TryComb(int[] candidates, int target, int idx, List<int> sofar, List<IList<int>> combs)
        {
            if (idx >= candidates.Length)
            {
                return;
            }
            var x = candidates[idx];
            var n = target / x;
            for (var i = 0; i <= n; i++)
            {
                var y = target - x * i;
                var comb = new List<int>();
                comb.AddRange(sofar);
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
    }

    public class Test
    {
        static void Verify(int[] input, int target, IList<IList<int>> exp)
        {
            Console.WriteLine($"{string.Join(',', input)} => {target}");
            IList<IList<int>> res;
            using (new Timeit())
            {
                res = new Solution().CombinationSum(input, target);
            }
            var exp2 = exp.Select(x => string.Join(',', x)).ToList();
            var res2 = res.Select(x => string.Join(',', x)).ToList();
            exp2.Sort();
            res2.Sort();
            Assert.Equal(exp2, res2);
        }
        static public void Run()
        {
            Console.WriteLine("CombinationSum");
            int[] input;
            int target;
            IList<IList<int>> exp;

            input = new int[] { 2, 3, 6, 7 };
            target = 7;
            exp = new List<IList<int>>{
                new List<int>{7},
                new List<int>{2,2,3}
            };
            Verify(input, target, exp);

            input = new int[] { 2, 3, 5 };
            target = 8;
            exp = new List<IList<int>>{
                new List<int>{2,2,2,2},
                new List<int>{2,3,3},
                new List<int>{3,5}
            };
            Verify(input, target, exp);

            input = new int[] { 2, 3, 5 };
            target = 7;
            exp = new List<IList<int>>{
                new List<int>{2,2,3},
                new List<int>{2,5}
            };
            Verify(input, target, exp);
        }
    }
}