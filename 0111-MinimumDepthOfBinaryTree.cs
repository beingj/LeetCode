using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MinimumDepthOfBinaryTree
{
    public class Solution
    {
        public int MinDepth(TreeNode root)
        {
            if (root == null) return 0;
            int minDepth = int.MaxValue;
            ByLevel(root, 1, ref minDepth);
            return minDepth;
        }
        void ByLevel(TreeNode node, int depth, ref int minDepth)
        {
            if (depth >= minDepth) return;
            if ((node.left == null) && (node.right == null))
            {
                if (depth < minDepth) minDepth = depth;
                return;
            }
            depth++;
            if (node.left != null) ByLevel(node.left, depth, ref minDepth);
            if (node.right != null) ByLevel(node.right, depth, ref minDepth);
        }
        void ByLevel1(TreeNode node, string path, ref int minDepth)
        {
            var depth = path.Length + 1;
            if (depth >= minDepth) return;
            if ((node.left == null) && (node.right == null))
            {
                if (depth < minDepth) minDepth = depth;
                return;
            }
            if (node.left != null) ByLevel1(node.left, $"{path}L", ref minDepth);
            if (node.right != null) ByLevel1(node.right, $"{path}R", ref minDepth);
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[3,9,20,null,null,15,7]
2
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}