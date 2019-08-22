using System;
using Xunit;
using Util;
using Node = Util.GraphNode;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Candy
{
    public class Solution
    {
        public int Candy(int[] ratings)
        {
            if (ratings.Length < 2)
                return 1;

            var values = new int[ratings.Length];
            int startIdx = 0;
            while (true)
            {
                if (startIdx == (ratings.Length - 1))
                    break;

                int endIdx = startIdx + 1;
                if (ratings[endIdx] < ratings[startIdx])
                {
                    // downstairs
                    while ((endIdx < ratings.Length) && (ratings[endIdx] < ratings[endIdx - 1]))
                    {
                        endIdx++;
                    }
                    endIdx--;
                    var v = 1;
                    for (var i = endIdx; i > startIdx; i--)
                    {
                        values[i] = v++;
                    }
                    values[startIdx] = Math.Max(values[startIdx], v);
                }
                else if (ratings[endIdx] > ratings[startIdx])
                {
                    // upstairs
                    while ((endIdx < ratings.Length) && (ratings[endIdx] > ratings[endIdx - 1]))
                    {
                        endIdx++;
                    }
                    endIdx--;
                    var v = 1;
                    for (var i = startIdx; i <= endIdx; i++)
                    {
                        values[i] = v++;
                    }
                }
                else
                {
                    // ratings[endIdx] == ratings[startIdx]
                    while ((endIdx < ratings.Length) && (ratings[endIdx] == ratings[endIdx - 1]))
                    {
                        endIdx++;
                    }
                    endIdx--;
                    var v = 1;
                    values[startIdx] = Math.Max(values[startIdx], v);
                    for (var i = startIdx + 1; i <= endIdx; i++)
                    {
                        values[i] = v;
                    }
                }
                startIdx = endIdx;
            }
            return values.Sum();
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[10,10,10,10,10,10]
6
[1,3,4,5,2]
11
[1,2,2]
4
[1,0,2]
5
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}