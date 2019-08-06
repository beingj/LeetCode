using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ConvertSortedArrayToBinarySearchTree
{
    public class Solution
    {
        public TreeNode SortedArrayToBST(int[] nums)
        {
            return MyAtoB(nums.ToList(), null, true);
        }
        TreeNode MyAtoB(List<int> nums, TreeNode root, bool isLeft)
        {
            if (nums.Count == 0)
            {
                return root;
            }
            var parentIdx = nums.Count / 2;
            TreeNode parent = new TreeNode(nums[parentIdx]);
            if (root == null)
            {
                root = parent;
            }
            else
            {
                if (isLeft)
                    root.left = parent;
                else
                    root.right = parent;
            }
            var numsLeft = nums.Take(parentIdx).ToList();
            var numsRight = nums.Skip(parentIdx + 1).ToList();
            MyAtoB(numsLeft, parent, true);
            MyAtoB(numsRight, parent, false);
            return root;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[-10,-3,0,5,9]
[0,-3,9,-10,null,5]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}