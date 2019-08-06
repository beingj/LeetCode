using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ConstructBinaryTreeFromPreorderAndInorderTraversal
{
    public class Solution
    {
        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            return MyBuildTree(preorder.ToList(), inorder.ToList(), null, true);
        }
        public TreeNode MyBuildTree(List<int> preorder, List<int> inorder, TreeNode root, bool isLeft)
        {
            if (preorder.Count == 0)
            {
                return root;
            }
            TreeNode parent = new TreeNode(preorder.First());
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
            var parentIdx = inorder.ToList().IndexOf(parent.val);
            var inLeft = inorder.Take(parentIdx).ToList();
            var inRight = inorder.Skip(parentIdx + 1).ToList();
            var preLeft = preorder.Skip(1).Take(parentIdx).ToList();
            var preRight = preorder.Skip(parentIdx + 1).ToList();
            MyBuildTree(preLeft, inLeft, parent, true);
            MyBuildTree(preRight, inRight, parent, false);
            return root;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[3,9,20,15,7]
[9,3,15,20,7]
[3,9,20,null,null,15,7]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}