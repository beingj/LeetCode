using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreePreorderTraversal
{
    public class Solution
    {
        public IList<int> PreorderTraversal(TreeNode root)
        {
            if (root == null)
            {
                return new List<int>();
            }
            var values = new List<int> { root.val };
            if (root.left != null)
            {
                values.AddRange(PreorderTraversal(root.left));
            }
            if (root.right != null)
            {
                values.AddRange(PreorderTraversal(root.right));
            }
            return values;
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[1,null,2,3]
[1,2,3]
[]
[]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}