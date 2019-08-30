using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreePostorderTraversal
{
    public class Solution
    {
        public IList<int> PostorderTraversal(TreeNode root)
        {
            if (root == null)
            {
                return new List<int>();
            }
            var values = new List<int>();
            if (root.left != null)
            {
                values.AddRange(PostorderTraversal(root.left));
            }
            if (root.right != null)
            {
                values.AddRange(PostorderTraversal(root.right));
            }
            values.Add(root.val);
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
[3,2,1]
[]
[]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}