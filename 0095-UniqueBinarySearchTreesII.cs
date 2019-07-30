using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UniqueBinarySearchTreesII
{
    public class Solution2
    {
        // give up.
        // copy from: https://leetcode.com/problems/unique-binary-search-trees-ii/discuss/31494/A-simple-recursive-solution
        public List<TreeNode> GenerateTrees(int n)
        {
            if (n == 0)
            {
                return new List<TreeNode>();
            }
            return genTrees(1, n);
        }

        public List<TreeNode> genTrees(int start, int end)
        {

            List<TreeNode> list = new List<TreeNode>();

            if (start > end)
            {
                list.Add(null);
                return list;
            }

            if (start == end)
            {
                list.Add(new TreeNode(start));
                return list;
            }

            List<TreeNode> left, right;
            for (int i = start; i <= end; i++)
            {

                left = genTrees(start, i - 1);
                right = genTrees(i + 1, end);

                foreach (TreeNode lnode in left)
                {
                    foreach (TreeNode rnode in right)
                    {
                        TreeNode root = new TreeNode(i);
                        root.left = lnode;
                        root.right = rnode;
                        list.Add(root);
                    }
                }
            }
            return list;
        }
    }
    public class Solution
    {
        // 每个TreeNode内的数字顺序必须严格和答案一致，各个TreeNode之间的顺序可以和答案不同
        // 这个要求比题目要求的structurally unique要严格
        public IList<TreeNode> GenerateTrees(int n)
        {
            if (n == 0)
            {
                return new List<TreeNode>();
            }
            var vals = new List<int>();
            var i = 1;
            while (i <= n)
            {
                vals.Add(i++);
            }
            return MakeTree(n, vals);
        }
        public static List<TreeNode> MakeTree(int n, List<int> vals)
        {
            if (n == 0)
            {
                return new List<TreeNode>() { null };
            }
            var all = new List<TreeNode>();
            for (var i = 0; i < n; i++)
            {
                var leftChildren = MakeTree(i, vals.Take(i).ToList());
                var rightChildren = MakeTree(n - 1 - i, vals.Skip(i + 1).ToList());
                foreach (var leftChild in leftChildren)
                {
                    foreach (var rightChild in rightChildren)
                    {
                        var root = new TreeNode(vals[i]);
                        root.left = leftChild;
                        root.right = rightChild;
                        all.Add(root);
                    }
                }
            }
            return all;
        }
    }
    public class Test
    {
        public static TreeNode CloneTreeNode(TreeNode srcNode, TreeNode dstNode = null, TreeNode dstRoot = null)
        {
            if (dstRoot == null)
            {
                dstRoot = new TreeNode(srcNode.val);
                dstNode = dstRoot;
            }

            if (srcNode.left != null)
            {
                dstNode.left = new TreeNode(srcNode.left.val);
                CloneTreeNode(srcNode.left, dstNode.left, dstRoot);
            }
            if (srcNode.right != null)
            {
                dstNode.right = new TreeNode(srcNode.right.val);
                CloneTreeNode(srcNode.right, dstNode.right, dstRoot);
            }
            return dstRoot;
        }
        static public void Run()
        {
            var input = @"
1
[[1]]
2
[[1,2],[1,null,2]]
3
[ [1,null,3,2], [3,2,null,1], [3,1,null,null,2], [2,1,3], [1,null,2,null,3] ]
";
            var lines = input.CleanInput();
            // Verify.Method(new Solution(), lines, sortRet: true);
            var exp = "[1,null,2,null,3],[1,null,3,2],[2,1,3],[3,1,null,null,2],[3,2,null,1]";
            Console.WriteLine(exp);
            var s = new Solution().GenerateTrees(3).Select(i => i.ToStrByLevel());
            Console.WriteLine(string.Join(", ", s));
            s = new Solution2().GenerateTrees(3).Select(i => i.ToStrByLevel());
            Console.WriteLine(string.Join(", ", s));
            // var s = "[ [1,null,3,2], [3,2,null,1], [3,1,null,null,2], [2,1,3], [1,null,2,null,3] ]";
            // var sl = s.Split("],").ToList();
            // var j = 0;
            // foreach (var i in s.JsonToIListTreeNode())
            // {
            //     Console.WriteLine(sl[j++]);
            //     Console.WriteLine(i.ToStrByLevel());
            //     Console.WriteLine(i.ToStrByLevel(ignoreVal:true));
            //     Console.WriteLine(i.ToStr(ignoreX: true));
            //     Console.WriteLine();
            // }
            // var s = "[3,2,null,1]";
            // var s = "[3,1,null,null,2]";
            // var n = s.JsonToTreeNode();
            // Console.WriteLine(s);
            // Console.WriteLine(n);
            // Console.WriteLine(n.ToStrByLevel());
            // Console.WriteLine(n.left);
        }
    }
}