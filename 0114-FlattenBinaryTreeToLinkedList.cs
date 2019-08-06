using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FlattenBinaryTreeToLinkedList
{
    public class Solution
    {
        public void Flatten(TreeNode root)
        {
            if (root == null) return;
            var node = root;
            while (true)
            {
                if (node.left == null)
                {
                    if (node.right == null)
                    {
                        break;
                    }
                    node = node.right;
                    continue;
                }
                var left = node.left;
                var right = node.right;
                // move left branch to right
                node.left = null;
                node.right = left;
                if (right != null)
                {
                    // if right branch is not null, append it to the rightmost of left branch
                    var x = FindR(left);
                    x.right = right;
                }
            }
        }
        TreeNode FindR(TreeNode node)
        {
            while (true)
            {
                if (node.right == null) break;
                node = node.right;
            }
            return node;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[1,2,5,3,4,null,6]
[1,null,2,null,3,null,4,null,5,null,6]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines, checkParaIndex: 0);
        }
    }
}