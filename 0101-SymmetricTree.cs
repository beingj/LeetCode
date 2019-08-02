using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SymmetricTree
{
    public class Solution
    {
        public bool IsSymmetric(TreeNode root)
        {
            if (root == null)
            {
                return true;
            }
            return CompareBT(root.left, root.right);
        }
        bool CompareBT(TreeNode left, TreeNode right)
        {
            if (left == null)
            {
                return right == null;
            }
            if (right == null) return false;
            if (left.val != right.val) return false;

            if (!CompareBT(left.left, right.right)) return false;
            if (!CompareBT(left.right, right.left)) return false;

            return true;
        }
    }
    public class Test
    {
        void GetBTPath(TreeNode root, string path, Dictionary<string, int> vals)
        {
            vals[path] = root.val;
            if (root.left != null)
            {
                GetBTPath(root.left, $"{path}L", vals);
            }
            if (root.right != null)
            {
                GetBTPath(root.right, $"{path}R", vals);
            }
        }
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[1,2,2,3,4,4,3]
true
[1,2,2,null,3,null,3]
false
[1,2,2,2,null,2]
false
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}