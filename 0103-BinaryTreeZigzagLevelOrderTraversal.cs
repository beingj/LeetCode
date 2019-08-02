using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreeZigzagLevelOrderTraversal
{
    public class Solution
    {
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            var valsByLvl = new List<IList<int>>();
            var valsDict = new Dictionary<string, int>();
            ByLevel(root, "", valsDict);
            if (valsDict.Keys.Count == 0)
            {
                return valsByLvl;
            }
            var maxLvl = valsDict.Keys.Select(i => i.Length).Max();
            var path = new List<string> { "" };
            for (var i = 0; i <= maxLvl; i++)
            {
                var path2 = new List<string>();
                var valsThisLvl = new List<int>();
                foreach (var p in path)
                {
                    if (valsDict.ContainsKey(p))
                    {
                        valsThisLvl.Add(valsDict[p]);
                        path2.Add($"{p}L");
                        path2.Add($"{p}R");
                    }
                }
                valsByLvl.Add(valsThisLvl);
                path = path2;
            }
            var valsByLvl2 = new List<IList<int>>();
            var isEven = true;
            foreach (var i in valsByLvl)
            {
                if (isEven)
                    valsByLvl2.Add(i);
                else
                    valsByLvl2.Add(i.Reverse().ToList());

                isEven = !isEven;
            }
            return valsByLvl2;
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
[ [3], [20,9], [15,7] ]
[]
 [ ]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}