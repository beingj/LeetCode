using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RecoverBinarySearchTree
{
    public class Solution
    {
        public void RecoverTree(TreeNode root)
        {
            var all = new Dictionary<string, TreeNode>();
            var bad = new List<(string, string)>();
            MyCheck(root, "", all, bad);
            foreach (var pair in bad)
            {
                Swap(all, pair.Item1, pair.Item2);
                if (MyIsValidBST(root, "", new Dictionary<string, int>()))
                {
                    return;
                }
                Swap(all, pair.Item1, pair.Item2);
            }

            // [2,3,1] => swap root.left and root.right
            //   2
            //  / \
            // 3   1 => swap 3 and 1
            var aList = bad.Select(i => i.Item1).Distinct().ToList();
            foreach (var a in aList)
            {
                var bList = bad.Where(i => i.Item1 == a).Select(j => j.Item2).ToList();
                if (bList.Count > 1)
                {
                    // there are 2(or more?) children need swap with same parent, try swap these 2 children
                    for (var i = 0; i < bList.Count; i += 2)
                    {
                        Swap(all, bList[i], bList[i + 1]);
                        if (MyIsValidBST(root, "", new Dictionary<string, int>()))
                        {
                            return;
                        }
                        Swap(all, bList[i], bList[i + 1]);
                    }
                }
            }
        }
        void Swap(Dictionary<string, TreeNode> all, string a, string b)
        {
            var x = all[a].val;
            all[a].val = all[b].val;
            all[b].val = x;
        }
        void MyCheck(TreeNode root, string path, Dictionary<string, TreeNode> all, List<(string, string)> bad)
        {
            all[path] = root;

            // var bad2 = new List<string>();
            for (var i = 0; i < path.Length; i++)
            {
                var p = path.Substring(0, i);
                var n = all[p];
                var valid = true;
                if (path[i] == 'R')
                {
                    if (root.val <= n.val) valid = false;
                }
                else
                {
                    if (root.val >= n.val) valid = false;
                }

                if (!valid)
                {
                    // bad2.Add(p);
                    bad.Add((p, path));
                }
            }
            // if (bad2.Count > 0)
            // {
            //     bad.Add((bad2.First(), path));
            // }

            if (root.left != null)
            {
                MyCheck(root.left, $"{path}L", all, bad);
            }
            if (root.right != null)
            {
                MyCheck(root.right, $"{path}R", all, bad);
            }
        }
        bool MyIsValidBST(TreeNode root, string path, Dictionary<string, int> vals)
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
        // var vals = new List<int>();
        // GetVals(root, vals);
        public void GetVals(TreeNode root, List<int> vals)
        {
            vals.Add(root.val);

            if (root.left != null)
            {
                GetVals(root.left, vals);
            }
            if (root.right != null)
            {
                GetVals(root.right, vals);
            }
        }
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
#[3,null,2,null,1]
#[1,null,2,null,3]
[1,3,null,null,2]
[3,1,null,null,2]
[3,1,4,null,null,2]
[2,1,4,null,null,3]
[2,3,1]
[2,1,3]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines, checkParaIndex: 0);
            // var s = new Solution();
            // for (var i = 0; i < lines.Length; i += 2)
            // {
            //     Console.WriteLine(lines[i]);
            //     s.RecoverTree(TreeNode.FromStr(lines[i]));
            // }
            // var s = "[10,5,15,null,null,6,20]";
            // // var s = "[5,1,4,null,null,3,6]";
            // Console.WriteLine(TreeNode.FromStr(s));
        }
    }
}