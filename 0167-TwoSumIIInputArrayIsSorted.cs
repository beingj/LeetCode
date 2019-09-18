using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace TwoSumIIInputArrayIsSorted
{
    public class Solution
    {
        public int[] TwoSum(int[] numbers, int target)
        {
            for (var i = 0; i < numbers.Length; i++)
            {
                var x = numbers[i];
                for (var j = i + 1; j < numbers.Length; j++)
                {
                    var y = numbers[j];
                    var z = x + y;
                    if (z == target)
                    {
                        return new int[] { i + 1, j + 1 };
                    }
                    else if (z > target)
                    {
                        break;
                    }
                }
            }
            return new int[] { 0, 0 };
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[2,7,11,15]
9
[1,2]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}