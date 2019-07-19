using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RemoveDuplicatesFromSortedArrayII
{
    public class Solution
    {
        public int RemoveDuplicates(int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }
            int last = nums[0];
            int len = nums.Length;
            int i = 1;
            int repeatCnt = 1;
            int repeatMax = 2;
            while (true)
            {
                if (i == len)
                {
                    break;
                }
                if (nums[i] == last)
                {
                    if (repeatCnt < repeatMax)
                    {
                        repeatCnt++;
                    }
                    else
                    {
                        // if (repeatCnt == repeatMax)
                        repeatCnt = 1;
                        int repeat = 1;
                        for (var j = i + 1; j < len; j++)
                        {
                            if (nums[j] != last)
                            {
                                break;
                            }
                            repeat++;
                        }

                        for (var j = i + repeat; j < len; j++)
                        {
                            nums[j - repeat] = nums[j];
                        }
                        len -= repeat;
                        continue;
                    }
                }
                else
                {
                    // if (nums[i] != last)
                    repeatCnt = 1;
                    last = nums[i];
                }
                i++;
            }
            return len;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
[1,1,1,2,2,3]
5
[1, 1, 2, 2, 3]
[0,0,1,1,1,1,2,3,3]
7
[0, 0, 1, 1, 2, 3, 3]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines, 0, true);
        }
    }
}