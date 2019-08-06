using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PathSum
{
    public class Solution
    {
        public bool HasPathSum(TreeNode root, int sum)
        {
            if (root == null) return false;
            var valsDict = new Dictionary<string, int> { { "", root.val } };
            var good = 0;
            ByLevel(root, "", valsDict, sum, ref good);
            return good > 0;
        }
        void ByLevel(TreeNode node, string path, Dictionary<string, int> valsDict, int exp, ref int good)
        {
            if (good > 0) return;
            valsDict[path] = node.val;
            if ((node.left == null) && (node.right == null))
            {
                var sum = 0;
                for (var i = 0; i <= path.Length; i++)
                {
                    sum += valsDict[path.Substring(0, i)];
                }
                if (sum == exp)
                    good += 1;

                return;
            }
            if (node.left != null) ByLevel(node.left, $"{path}L", valsDict, exp, ref good);
            if (node.right != null) ByLevel(node.right, $"{path}R", valsDict, exp, ref good);
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[5,4,8,11,null,13,4,7,2,null,null,null,1]
22
true
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}