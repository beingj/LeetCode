using System;
using Xunit;
using Util;
using System.Text;

namespace ContainerWithMostWater
{
    public class Solution
    {
        public int MaxArea(int[] height)
        {
            int area = 0, wxh = 0;
            for (var i = 0; i < height.Length; i++)
            {
                for (var j = i + 1; j < height.Length; j++)
                {
                    wxh = (j - i) * (Math.Min(height[i], height[j]));
                    if (wxh > area)
                    {
                        area = wxh;
                    }
                }
            }
            return area;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("ContainerWithMostWater");

            int[] input;
            int exp, res;

            using (new Timeit())
            {
                input = new int[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 };
                exp = 49;
                res = new Solution().MaxArea(input);
                Assert.Equal(exp, res);
            }
        }
    }
}