using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreeMaximumPathSum
{
    public class Solution
    {
        public int MaxPathSum(TreeNode root)
        {
            if (root == null) return 0;
            var parentDict = new Dictionary<TreeNode, TreeNode>();
            ByLevel(root, parentDict);
            var allNodes = parentDict.Keys.ToList();
            allNodes.Add(root);
            var maxSum = root.val;
            var cache = new Dictionary<(TreeNode, Direction), int>();
            foreach (var n in allNodes)
            {
                var x = CachedMyMaxPathSum(n, Direction.None, parentDict, cache);
                maxSum = Math.Max(maxSum, x);
            }
            return maxSum;
        }
        enum Direction
        {
            None,
            Parent,
            Left,
            Right
        }
        int CachedMyMaxPathSum(TreeNode node, Direction directionFrom, Dictionary<TreeNode, TreeNode> parentDict, Dictionary<(TreeNode, Direction), int> cache)
        {
            var k = (node, directionFrom);
            if (!cache.ContainsKey(k))
                cache[k] = MyMaxPathSum(node, directionFrom, parentDict, cache);
            return cache[k];
        }
        int MyMaxPathSum(TreeNode node, Direction directionFrom, Dictionary<TreeNode, TreeNode> parentDict, Dictionary<(TreeNode, Direction), int> cache)
        {
            var max = 0;
            var x = 0;
            if ((directionFrom != Direction.Parent) && parentDict.ContainsKey(node))
            {
                var p = parentDict[node];
                if (ReferenceEquals(p.left, node))
                {
                    x = CachedMyMaxPathSum(p, Direction.Left, parentDict, cache);
                }
                else
                {
                    x = CachedMyMaxPathSum(p, Direction.Right, parentDict, cache);
                }
            }
            max = Math.Max(max, x);
            if ((directionFrom != Direction.Left) && (node.left != null))
            {
                x = CachedMyMaxPathSum(node.left, Direction.Parent, parentDict, cache);
            }
            max = Math.Max(max, x);
            if ((directionFrom != Direction.Right) && (node.right != null))
            {
                x = CachedMyMaxPathSum(node.right, Direction.Parent, parentDict, cache);
            }
            max = Math.Max(max, x);

            return node.val + max;
        }
        void ByLevel(TreeNode node, Dictionary<TreeNode, TreeNode> parentDict)
        {
            if (node.left != null)
            {
                parentDict[node.left] = node;
                ByLevel(node.left, parentDict);
            }
            if (node.right != null)
            {
                parentDict[node.right] = node;
                ByLevel(node.right, parentDict);
            }
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[1,2,3]
6
[-10,9,20,null,null,15,7]
42
";
            var lines = input.CleanInput();
            // lines = "0124-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}