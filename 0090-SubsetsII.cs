using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SubsetsII
{
    public class Solution
    {
        public IList<IList<int>> SubsetsWithDup(int[] nums)
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
            IList<string> combList2Str = new List<string>();
            combList2.Add(new List<int>());
            foreach (var list in combList)
            {
                var list2 = list.Select(i => nums[i - 1]).ToList();
                list2.Sort();
                var list2Str = string.Join(',', list2);
                if (!combList2Str.Contains(list2Str))
                {
                    combList2.Add(list2);
                    combList2Str.Add(list2Str);
                }
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
        static public void Run()
        {
            var input = @"
[1,2,2]
[ [2], [1], [1,2,2], [2,2], [1,2], [] ]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines, sortRet: true);
        }
    }
}