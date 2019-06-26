using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace MaxSubArray
{
    public class Solution
    {
        public int MaxSubArray(int[] nums)
        {
            // 先假设第一个数是最大数
            // 想象一个移动的框子，对框内的数求和。从第一个数开始，框住x1；然后框子的左边不动，右边向右移动一格，框住x1,x2
            // 然后右边依次向后移动，框住更多数
            // 如果发现框内的和比之前保存的最大数还大，将最大数改成这个和；
            // 如果发现框内的数小于零，移动框子的左边到当前位置，放弃之前框住的所有数，重新开始
            int boxLeft = 0, boxRight = 0;
            int max = nums[boxLeft];
            while (boxRight < nums.Length)
            {
                int sum = 0;
                while (boxRight < nums.Length)
                {
                    if (max < nums[boxRight])
                    {
                        max = nums[boxRight];
                    }
                    sum += nums[boxRight];
                    if (sum < 0)
                    {
                        boxRight++;
                        boxLeft = boxRight;
                        break;
                    }
                    if (sum > max)
                    {
                        max = sum;
                    }
                    boxRight++;
                }

            }
            return max;
        }

        public int MaxSubArray1(int[] nums)
        {
            // 先假设第一个数和最后一个数分别是左边和右边的最大数
            // 想象一个移动的框子，对框内的数求和。从第一个数开始，框住x1；然后框子的左边不动，右边向右移动一格，框住x1,x2
            // 然后右边依次向后移动，框住更多数
            // 如果发现框内的和比之前保存的最大数还大，将最大数改成这个和；
            // 如果发现框内的数小于零，移动框子的左边到当前位置，放弃之前框住的所有数，重新开始
            // 想象还有一个框子从最后一个数开始，从右向左移动，做同样的步骤
            // 2个框子最终会在中间相遇，比较左边和右边的最大数，返回大的那个
            int left_left = 0, left_right = 0, right_left = nums.Length - 1, right_right = right_left;
            int left_max = nums[left_left];
            int right_max = nums[right_right];
            while (true)
            {
                if (left_right > right_left)
                {
                    // Console.WriteLine("done");
                    break;
                }

                int sum = 0;
                while (true)
                {
                    if (left_right > right_left)
                    {
                        break;
                    }
                    if (left_max < nums[left_right])
                    {
                        // Console.WriteLine($"left max:{left_max}=>{nums[left_right]}");
                        left_max = nums[left_right];
                    }
                    sum += nums[left_right];
                    if (sum < 0)
                    {
                        left_right++;
                        left_left = left_right;
                        break;
                    }
                    if (sum > left_max)
                    {
                        // Console.WriteLine($"left max:{left_left},{left_right}=>{sum}");
                        left_max = sum;
                    }
                    left_right++;
                }

                sum = 0;
                while (true)
                {
                    if (right_left < left_right)
                    {
                        break;
                    }
                    if (right_max < nums[right_left])
                    {
                        // Console.WriteLine($"right max:{right_max}=>{nums[right_left]}");
                        right_max = nums[right_left];
                    }
                    sum += nums[right_left];
                    if (sum < 0)
                    {
                        right_left--;
                        right_right = right_left;
                        break;
                    }
                    if (sum > right_max)
                    {
                        // Console.WriteLine($"right max:{right_left},{right_right}=>{sum}");
                        right_max = sum;
                    }
                    right_left--;
                }

            }
            return Math.Max(left_max, right_max);
        }
        // public int MaxSubArray(int[] nums)
        // {
        //     int startIdx = 0, endIdx = nums.Length - 1;
        //     for (var i = 0; i < nums.Length; i++)
        //     {
        //         if (nums[i] > 0)
        //         {
        //             startIdx = i;
        //             break;
        //         }
        //     }
        //     for (var i = nums.Length - 1; i >= 0; i--)
        //     {
        //         if (nums[i] > 0)
        //         {
        //             endIdx = i;
        //             break;
        //         }
        //     }
        //     int max = Math.Max(nums[startIdx], nums[endIdx]);
        //     // Console.WriteLine($"{0},{nums.Length - 1}=>{startIdx},{endIdx}");

        //     for (var i = startIdx; i <= endIdx; i++)
        //     {
        //         if (max > 0 && nums[i] <= 0)
        //         {
        //             continue;
        //         }
        //         for (var j = i; j <= endIdx; j++)
        //         {
        //             if (max > 0 && nums[j] <= 0)
        //             {
        //                 continue;
        //             }
        //             var sum = nums.Skip(i).Take(j - i + 1).Sum();
        //             if (sum > max)
        //             {
        //                 max = sum;
        //             }
        //         }
        //     }
        //     return max;
        // }
        // List<int> Comb(int[] nums)
        // {
        //     var res = new List<int>();
        //     bool lastIsPositive = false;
        //     if (nums.Length == 0)
        //     {
        //         return res;
        //     }
        //     lastIsPositive = nums[0] > 0;
        //     int buf = nums[0];
        //     for (var i = 1; i < nums.Length; i++)
        //     {
        //         var thisIsPositive = nums[i] > 0;
        //         if (thisIsPositive && lastIsPositive)
        //         {
        //             buf += nums[i];
        //         }
        //         else
        //         {
        //             res.Add(buf);
        //             buf = nums[i];
        //         }
        //         lastIsPositive = nums[i] > 0;
        //     }
        //     return res;
        // }
    }
    public class Test
    {
        static void Verify(int[] n, int exp)
        {
            Console.WriteLine(string.Join(',', n.Take(10)));
            int res;
            using (new Timeit())
            {
                res = new Solution().MaxSubArray(n);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("MaxSubArray");
            int[] nums;
            int exp;

            nums = new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
            // [4,-1,2,1] has the largest sum = 6.
            exp = 6;
            Verify(nums, exp);
            // return;

            nums = new int[] { 1 };
            exp = 1;
            Verify(nums, exp);

            nums = new int[] { -64, 78, 56, 10, -8, 26, -18, 47, -31, 75, 89, 13, 48, -19, -69, 36, -39, 55, -5, -4, -15, -37, -27, -8, -5, 35, -51, 83, 21, -47, 46, 33, -91, -21, -57, 0, 81, 1, -75, -50, -23, -86, 39, -98, -29, 69, 38, 32, 24, -90, -95, 86, -27, -23, -22, 44, -88, 3, 27, 9, 55, -50, -80, 40, 5, -61, -82, -14, 40, -58, 35, 93, -68, -26, 94, 3, -79, 9, -88, 21, 19, -84, 7, 91, -8, 84, 12, -19, -13, -83, 66, -80, -34, 62, 59, 48, -98, 53, -66, 18, 94, 46, 11, -73, 96, -18, 6, -83, 91, 17, 38, 10, 9, -78, -22, 77, 83, 89, -42, -30, -94, -98, -34, -51, 63, -97, 96, 64, 55, -93, -41, 27, 52, 69, 53, 26, -71, -64, 42, -80, 52, -43, 6, -62, -21, 83, -85, -38, 49, -50, 8, 55, -72, 74, 80, 90, 53, 53, 32, -15, 36, 90, -88, -34, 37, 41, 91, 65, 76, 33, 61, 5, 90, -33, 42, -54, -73, 34, -16, 75, 83, 91, 7, -89, 42, -36, 77, -5, -83, 9, 80, 53, -23, 68, -81, 90, 10, -90, 55, -14, 19, -7, 91, -14, 59, 33, 31, 62, -33, -85, 37, -73, 83, -78, -86, 25, -15, 91, 97, 2, -23, 54, -68, 53, 22, -73, 43, -68, -87, -25, 18, 31, 67, -14, 94, 3, -81, 25, -35, -37, 17, 79, -34, -23, -99, -43, -98, -38, -52, 75, 63, 1, 29, 71, -68, -71, 74, 51, -40, 86, -73, 54, -5, 70, -60, -11, -49, -64, 90, -8, -25, -16, -52, 40, 60, -75, 96, 39, -13, -79, 14, -73, 22, -79, 75, 30, -51, 49, -19, -15, 36, -16, -60, -69, -68, -21, -4, -18, -9, -14, 50, 65, 70, 75, -17, 30, 99, -44, -31, -14, -46, 60, -10, 52, 80, -35, -18, -94, -86, 62, -10, 49, -53, 6, 56, -45, 62, -48, 36, -47, 15, -37, -81, -15, -62, -22, 91, -85, 33, -62, -23, 86, 97, 66, 15, 54, -69, 96, 36, -55, 36, -97, 70, 82, 9, 4, -63, -29, 32, 49, 23, -53, 88, 18, 8, -96, 72, -23, -82, 6, 14, -6, -31, -12, -39, 61, -58, -32, 57, 77, 12, -7, 56, -40, -48, -35, 40, -35, 12, -28, 90, -87, -4, 79, 30, 80, 82, -20, -43, 76, 62, 70, -30, -92, -42, 7, 68, -24, 75, 26, -70, -36, 95, 86, 0, -52, -49, -60, 12, 63, -11, -20, 75, 84, -41, -18, 41, -82, 61, 98, 70, 0, 45, -83, 8, -96, 24, -24, -44, -24, -98, -14, 39, 97, -51, -60, -78, -24, -44, 10, -84, 44, 89, 67, 5, -75, -73, -53, -81, 64, -55, 88, -35, 89, -94, 72, 69, 29, -52, -97, 81, -73, -35, 20, -99, 13, 36, 98, 65, 69, 8, 81, 13, -25, 25, 95, -1, 51, -58, -5, 16, -37, -17, 57, -71, -35, 29, 75, 70, 53, 77, 51, 79, -58, -51, 56, 31, 84, 54, -27, 30, -37, -46, -56, 14, 56, -84, 89, 7, -43, -16, 99, 19, 67, 56, 24, -68, -38, -1, -97, -84, -24, 53, 71, -6, -98, 28, -98, 63, -18, -25, -7, 21, 5, 13, -88, -39, 28, -98, 68, 61, -15, 44, -43, -71, 1, 81, -39, 62, -20, -60, 54, 33, 69, 26, -96, 48, -69, -94, 11, -11, -20, 80, 87, 61, -29, 98, -77, 75, 99, 67, 37, -38, 11, 93, -10, 88, 51, 27, 28, -68, 66, -41, 41, 36, 84, 44, -16, 91, 49, 71, -19, -94, 28, -32, 44, 75, -57, 66, 51, -80, 10, -35, -19, 97, -65, 70, 63, 86, -2, -9, 94, -59, 26, 35, 76, 11, -21, -63, -21, -94, 84, 59, 87, 13, -96, 31, -35, -53, -26, -84, -34, 60, -20, 23, 58, 15, -7, 21, -22, 67, 88, -28, -91, 14, -93, 61, -98, -38, 75, -19, -56, 59, -83, -91, -51, -79, 16, 14, -56, 90, 6, -14, 27, 63, -91, -15, -22, -22, 82, 32, -54, 47, -96, -69, -61, 86, 91, -60, -75, 43, -3, -31, 3, -9, -23, 28, 11, 69, -81, 31, 59, 25, -83, -36, -12, -75, 48, 42, -21, 8, -26, 24, -68, -23, 31, -30, -60, 0, -13, -36, -57, 60, 32, 22, -49, 85, -49, 38, 55, -54, -31, -9, 70, -38, 54, -65, -37, -20, 76, 42, 64, -73, -57, 95, -20, 74, -57, 19, -49, 29, 83, -7, -11, -8, -84, 40, -45, -57, -45, 86, -12, 24, -46, -64, 62, -91, -30, -74, -35, -76, 44, -94, -73, 86, 77, 7, 37, -80, -74, 87, 48, 85, -19, -85, -45, -27, 31, 9, -8, 85, -28, 79, -14, 25, 91, -51, 10, -61, -49, 74, -38, 94, 56, -12, 57, 34, 71, -5, 53, 74, -18, -21, 59, 39, -30, 90, -88, -99, -24, 3, 62, 47, -40, -51, -27, -49, -26, 82, -11, 1, 34, 27, -5, -10, 92, -48, -99, 63, 23, 31, 14, -94, -90, -49, 44, -44, -59, 33, -44, 17, -64, -82, -36, -28, -57, 13, 0, -7, -4, 88, 70, -93, -7, -35, -4, -15, -6, -26, -75, 93, -95, 39, 98, 90, 66, 20, -54, -93, -47, -22, 0, -35, -28, 41, 14, -8, -46, -86, 84, 26, -98, 55, 32, -29, 96, -94, 32, -33, -21, 57, -39, -17, -27, -64, -50, -61, 55, -28, -78, 84, 49, 22, -73, -79, -37, 40, 12, -7, 53, -26, -80, 31, -94, 51, -97, -98, 56, 34, -54, -88, -32, -17, -29, 17, 18, 20, 32, -49, 91, 54, -65, 40, -47, -39, 38, -8, -99, -73, 84, 30, 0, -96, -38, 5, 32, -36, -16, -35, 74, 29, -23, -80, -88, 47, 36, 29, -32, -54, 79, -64, 76, 91, 53, -71, -71, -9, -3, -93, 17, -19, 36, 94, -38, 97, -1, 70, -62, 82, -65, -87, 11, 11, -68, -1, -41, 44, -71, 3, 89 };
            exp = 3452;
            Verify(nums, exp);

            nums = new int[] { 0 };
            exp = 0;
            Verify(nums, exp);

            nums = new int[] { -1, 0, -2 };
            exp = 0;
            Verify(nums, exp);

            var fn = "0053-data.txt";
            using (var fs = File.OpenText(Path.Join(Directory.GetCurrentDirectory(), fn)))
            {
                nums = fs.ReadToEnd().Trim('[', ']').Split(',').Select(x => int.Parse(x)).ToArray();
            }
            Console.WriteLine(nums.Length);
            exp = 11081;
            Verify(nums, exp);
        }
    }
}