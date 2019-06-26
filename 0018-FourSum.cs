using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace FourSum
{
    public class Solution
    {
        public IList<IList<int>> FourSum0(int[] nums, int target)
        {
            List<IList<int>> res = new List<IList<int>>();
            if (nums.Length < 4)
                return res;

            List<int> sortedNums = new List<int>();
            for (var i = 0; i < nums.Length; i++)
            {
                sortedNums.Add(nums[i]);
            }
            sortedNums.Sort();
            int len = sortedNums.Count;
            int maxIdx = len - 1;
            for (var i = 0; i < len; i++)
            {
                for (var j = i + 1; j < len; j++)
                {
                    for (var k = j + 1; k < len; k++)
                    {
                        for (var m = k + 1; m < len; m++)
                        {
                            var sum = sortedNums[i] + sortedNums[j] + sortedNums[k] + sortedNums[m];
                            if (sum == target)
                            {
                                res.Add(new List<int> { sortedNums[i], sortedNums[j], sortedNums[k], sortedNums[m] });
                                break;
                            }
                        }
                    }
                }
            }
            List<IList<int>> res2 = new List<IList<int>>();
            HashSet<string> x = new HashSet<string>();
            for (var i = 0; i < res.Count; i++)
            {
                if (x.Add(string.Format("{0},{1},{2}",
                    res[i][0], res[i][1], res[i][2])))
                {
                    res2.Add(res[i]);
                }
            }
            return res2;
        }

        public IList<IList<int>> FourSum1(int[] nums, int target)
        {
            List<IList<int>> res = new List<IList<int>>();
            if (nums.Length < 4)
                return res;

            List<int> sortedNums = new List<int>();
            for (var i = 0; i < nums.Length; i++)
            {
                sortedNums.Add(nums[i]);
            }
            sortedNums.Sort();
            int len = sortedNums.Count;
            int maxIdx = len - 1;
            for (var i = 0; i < len; i++)
            {
                for (var j = i + 1; j < len; j++)
                {
                    for (var k = j + 1; k < len; k++)
                    {
                        if (k == maxIdx)
                            break;
                        int ijk = sortedNums[i] + sortedNums[j] + sortedNums[k];
                        int mi = k + 1, mj = maxIdx, m;
                        while (true)
                        {
                            m = mi + (int)((mj - mi) / 2);
                            var sum = ijk + sortedNums[m];
                            if (sum == target)
                            {
                                res.Add(new List<int>() { sortedNums[i], sortedNums[j], sortedNums[k], sortedNums[m] });
                                break;
                            }
                            if (mi == mj)
                                break;
                            if (sum < target)
                            {
                                if (mi == m)
                                    mi = m + 1;
                                else
                                    mi = m;
                            }
                            else
                            {
                                if (mj == m)
                                    mj = m - 1;
                                else
                                    mj = m;
                            }
                        }
                    }
                }
            }
            List<IList<int>> res2 = new List<IList<int>>();
            HashSet<string> x = new HashSet<string>();
            for (var i = 0; i < res.Count; i++)
            {
                if (x.Add(string.Format("{0},{1},{2}",
                    res[i][0], res[i][1], res[i][2])))
                {
                    res2.Add(res[i]);
                }
            }
            return res2;
        }
        public IList<IList<int>> FourSum(int[] nums, int target)
        {
            List<IList<int>> res = new List<IList<int>>();
            if (nums.Length < 4)
                return res;

            List<int> sortedNums = new List<int>();
            for (var i = 0; i < nums.Length; i++)
            {
                sortedNums.Add(nums[i]);
            }
            sortedNums.Sort();
            int len = sortedNums.Count;
            int maxIdx = len - 1;

            int min = sortedNums[0] - 1;
            int iii = min, jjj = min, kkk = min;
            for (var i = 0; i < len; i++)
            {
                if (iii == sortedNums[i])
                {
                    continue;
                }
                else
                {
                    iii = sortedNums[i];
                }
                jjj = min;
                for (var j = i + 1; j < len; j++)
                {
                    if (jjj == sortedNums[j])
                    {
                        continue;
                    }
                    else
                    {
                        jjj = sortedNums[j];
                    }
                    kkk = min;
                    for (var k = j + 1; k < len; k++)
                    {
                        if (kkk == sortedNums[k])
                        {
                            continue;
                        }
                        else
                        {
                            kkk = sortedNums[k];
                        }
                        int ijk = sortedNums[i] + sortedNums[j] + sortedNums[k];
                        if ((k < maxIdx) && (ijk + sortedNums[k + 1] > target))
                        {
                            break;
                        }
                        if (ijk + sortedNums[maxIdx] < target)
                        {
                            continue;
                        }
                        if (k == maxIdx)
                            break;

                        int mi = k + 1, mj = maxIdx, m;
                        while (true)
                        {
                            m = mi + (int)((mj - mi) / 2);
                            var sum = ijk + sortedNums[m];
                            if (sum == target)
                            {
                                res.Add(new List<int>() { sortedNums[i], sortedNums[j], sortedNums[k], sortedNums[m] });
                                break;
                            }
                            if (mi == mj)
                                break;
                            if (sum < target)
                            {
                                if (mi == m)
                                    mi = m + 1;
                                else
                                    mi = m;
                            }
                            else
                            {
                                if (mj == m)
                                    mj = m - 1;
                                else
                                    mj = m;
                            }
                        }
                    }
                }
            }
            List<IList<int>> res2 = new List<IList<int>>();
            HashSet<string> x = new HashSet<string>();
            for (var i = 0; i < res.Count; i++)
            {
                if (x.Add(string.Format("{0},{1},{2}",
                    res[i][0], res[i][1], res[i][2])))
                {
                    res2.Add(res[i]);
                }
            }
            return res2;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("FourSum");

            int[] input;
            int target;
            IList<IList<int>> exp, res;

            input = new int[] { 1, 0, -1, 0, -2, 2 };
            target = 0;
            exp = new List<IList<int>>{
                    new List<int>{-2, -1, 1, 2},
                    new List<int>{-2,  0, 0, 2},
                    new List<int>{-1, 0, 0, 1},
                };
            using (new Timeit())
            {
                res = new Solution().FourSum0(input, target);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                res = new Solution().FourSum0(input, target);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                res = new Solution().FourSum(input, target);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                res = new Solution().FourSum(input, target);
                Assert.Equal(exp, res);
            }

            using (new Timeit())
            {
                input = new int[] { 0, 0, 0, 0 };
                target = 1;
                exp = new List<IList<int>>
                {
                };
                res = new Solution().FourSum(input, target);
                Assert.Equal(exp, res);
            }
            // [0,1,5,0,1,5,5,-4]
            // 11
            using (new Timeit())
            {
                input = new int[] { 0, 1, 5, 0, 1, 5, 5, -4 };
                target = 11;
                exp = new List<IList<int>>
                {
                    // [[-4,5,5,5],[0,1,5,5]]
                    new List<int>{-4,5,5,5},
                    new List<int>{0,1,5,5}
                };
                res = new Solution().FourSum(input, target);
                Assert.Equal(exp, res);
            }
        }
    }
}