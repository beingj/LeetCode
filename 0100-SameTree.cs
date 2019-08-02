using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SameTree
{
    public class Solution
    {
        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null)
            {
                return q == null;
            }

            if (q == null) return false;
            if (p.val != q.val) return false;

            if (p.left == null)
            {
                if (q.left != null) return false;
            }
            else
            {
                if (!IsSameTree(p.left, q.left)) return false;
            }

            if (p.right == null)
            {
                if (q.right != null) return false;
            }
            else
            {
                if (!IsSameTree(p.right, q.right)) return false;
            }

            return true;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[1,2,3]
[1,2,3]
true
[1,2]
[1,null,2]
false
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}