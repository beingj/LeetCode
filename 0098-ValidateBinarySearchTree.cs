using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ValidateBinarySearchTree
{
    public class Solution
    {
        public bool IsValidBST(TreeNode root)
        {
            if (root == null)
                return true;

            return MyIsValidBST(root, "", new Dictionary<string, int>());
        }
        public bool MyIsValidBST(TreeNode root, string path, Dictionary<string, int> vals)
        {
            for (var i = 0; i < path.Length; i++)
            {
                var p = path.Substring(0, i);
                var v = vals[p];
                if (path[i] == 'R')
                {
                    if (root.val <= v) return false;
                }
                else
                {
                    if (root.val >= v) return false;
                }
            }
            vals[path] = root.val;

            if (root.left != null)
            {
                if (root.left.val >= root.val)
                    return false;
                if (MyIsValidBST(root.left, $"{path}L", vals) == false)
                    return false;
            }
            if (root.right != null)
            {
                if (root.right.val <= root.val)
                    return false;
                if (MyIsValidBST(root.right, $"{path}R", vals) == false)
                    return false;
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
[2,1,3]
true
[5,1,4,null,null,3,6]
false
[10,5,15,null,null,6,20]
false
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
            // var s = "[10,5,15,null,null,6,20]";
            // // var s = "[5,1,4,null,null,3,6]";
            // Console.WriteLine(TreeNode.FromStr(s));
        }
    }
}