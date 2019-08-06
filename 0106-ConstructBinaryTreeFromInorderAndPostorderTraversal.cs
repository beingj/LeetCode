using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ConstructBinaryTreeFromInorderAndPostorderTraversal
{
    public class Solution
    {
        public TreeNode BuildTree(int[] inorder, int[] postorder)
        {
            return MyBuildTree(inorder.ToList(), postorder.ToList(), null, true);
        }
        public TreeNode MyBuildTree(List<int> inorder, List<int> postorder, TreeNode root, bool isLeft)
        {
            if (inorder.Count == 0)
            {
                return root;
            }
            TreeNode parent = new TreeNode(postorder.Last());
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
            var postLeft = postorder.Take(inLeft.Count).ToList();
            var postRight = postorder.Skip(inLeft.Count).Take(inRight.Count).ToList();
            MyBuildTree(inLeft, postLeft, parent, true);
            MyBuildTree(inRight, postRight, parent, false);
            return root;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[9,3,15,20,7]
[9,15,7,20,3]
[3,9,20,null,null,15,7]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}