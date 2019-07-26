using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreeInorderTraversal
{
    public class Solution
    {
        public IList<int> InorderTraversal(TreeNode root)
        {
            return LDR(root);
        }
        static IList<int> LDR(TreeNode tn, IList<int> res = null)
        {
            if (res == null)
            {
                res = new List<int>();
            }
            if (tn != null)
            {
                if ((tn.left != null) ||
                    ((tn.left == null) && (tn.right != null)))
                    LDR(tn.left, res);
                res.Add(tn.val);
                if (tn.right != null) LDR(tn.right, res);
            }
            return res;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
[1,null,2,3]
[1,3,2]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}