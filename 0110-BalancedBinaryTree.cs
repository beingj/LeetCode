using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BalancedBinaryTree
{
    public class Solution
    {
        public bool IsBalanced(TreeNode root)
        {
            if (root == null)
                return true;
            var lvls = new Dictionary<string, int>();
            ByLevel(root, "", lvls);
            var maxLevel = lvls.Keys.Select(i => i.Length).Max();
            var path = new List<string> { "" };
            for (var i = 1; i < maxLevel; i++)
            {
                var path2 = new List<string>();
                foreach (var p in path)
                {
                    int left = 0, right = 0;
                    var k = $"{p}L";
                    if (lvls.ContainsKey(k))
                    {
                        left = lvls[k];
                        path2.Add(k);
                    }
                    k = $"{p}R";
                    if (lvls.ContainsKey(k))
                    {
                        right = lvls[k];
                        path2.Add(k);
                    }
                    if (Math.Abs(left - right) > 1)
                        return false;
                }
                path = path2;
            }
            return true;
        }
        void ByLevel(TreeNode node, string path, Dictionary<string, int> lvls)
        {
            lvls[path] = 1;
            if (node.left != null)
                ByLevel(node.left, $"{path}L", lvls);
            if (node.right != null)
                ByLevel(node.right, $"{path}R", lvls);
            if ((node.left == null) && (node.right == null))
            {
                for (var i = 0; i < path.Length; i++)
                {
                    var p = path.Substring(0, i);
                    lvls[p] = Math.Max(lvls[p], path.Length - p.Length + 1);
                }
            }
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[3,9,20,null,null,15,7]
true
[1,2,2,3,3,null,null,4,4]
false
[1,2,3,4,5,null,6,7]
true
[1,2,2,3,null,null,3,4,null,null,4]
false
";
            var lines = input.CleanInput();
            lines = "0110-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}