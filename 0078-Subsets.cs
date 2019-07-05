using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Subsets
{
    public class Solution
    {
        public IList<IList<int>> Subsets(int[] nums)
        {
            IList<IList<int>> combList = new List<IList<int>>();
            int n = nums.Length;
            for (var k = 1; k <= n; k++)
            {
                foreach (var j in Combine(n, k))
                {
                    combList.Add(j);
                }
            }
            IList<IList<int>> combList2 = new List<IList<int>>();
            combList2.Add(new List<int>());
            foreach (var list in combList)
            {
                combList2.Add(list.Select(i => nums[i - 1]).ToList());
            }
            return combList2;
        }
        public IList<IList<int>> Combine(int n, int k)
        {
            IList<IList<int>> combList = new List<IList<int>>();
            for (var i = 0; i < k; i++)
            {
                combList = comb(n, k, combList);
            }
            return combList;
        }
        IList<IList<int>> comb(int n, int k, IList<IList<int>> combList)
        {
            IList<IList<int>> combList2 = new List<IList<int>>();
            if (combList.Count == 0)
            {
                for (var i = 1; i <= (n - k + 1); i++)
                {
                    combList2.Add(new List<int> { i });
                }
            }
            else
            {
                foreach (var i in combList)
                {
                    var last = i.Last();
                    for (var j = last + 1; j <= n; j++)
                    {
                        var x = new List<int>(i);
                        x.Add(j);
                        combList2.Add(x);
                    }
                }
            }
            return combList2;
        }
    }
    public class Test
    {
        static void Verify(int[] nums, IList<IList<int>> exp)
        {
            Console.WriteLine($"{nums.Int1dToJson()}");
            IList<IList<int>> res;
            using (new Timeit())
            {
                res = new Solution().Subsets(nums);
            }
            res.OrderBy(i => i.OrderBy(j => j));
            // var x = res.Select(i => i.ToArray()).ToArray();
            var y = exp.Select(i => i.ToList()).ToList();
            y.OrderBy(i => i.OrderBy(j => j));
            // Console.WriteLine(x.Int2dToJson());
            // Assert.Equal(exp, x);
            Assert.Equal(y, res);
        }
        static public void Run()
        {
            Console.WriteLine(typeof(Solution).Namespace);

            var input = @"
[1,2,3]
[[], [1], [2], [3], [1,2], [1,3], [2,3], [1,2,3] ]
";
            var lines = input.CleanInput();
            int[] nums;
            IList<IList<int>> exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                nums = lines[idx++].JsonToInt1d();
                exp = lines[idx++].JsonToInt2d();
                Verify(nums, exp);
            }
        }
    }
}