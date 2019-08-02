using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MaximumDepthOfBinaryTree
{
    public class Solution
    {
        public int MaxDepth(TreeNode root)
        {
            if (root == null)
                return 0;
            var valsDict = new Dictionary<string, int>();
            ByLevel(root, "", valsDict);
            var maxLvl = valsDict.Keys.Select(i => i.Length).Max();
            return maxLvl + 1;
        }
        void ByLevel(TreeNode node, string path, Dictionary<string, int> vals)
        {
            if (node == null)
                return;
            vals[path] = node.val;
            if (node.left != null)
                ByLevel(node.left, $"{path}L", vals);
            if (node.right != null)
                ByLevel(node.right, $"{path}R", vals);
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[3,9,20,null,null,15,7]
3
[]
0
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}