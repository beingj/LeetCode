using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PathSumII
{
    public class Solution
    {
        public IList<IList<int>> PathSum(TreeNode root, int sum)
        {
            if (root == null) return new List<IList<int>>();
            var valsDict = new Dictionary<string, int> { { "", root.val } };
            var good = new List<IList<int>>();
            ByLevel(root, "", valsDict, sum, good);
            return good;
        }
        void ByLevel(TreeNode node, string path, Dictionary<string, int> valsDict, int exp, List<IList<int>> good)
        {
            valsDict[path] = node.val;
            if ((node.left == null) && (node.right == null))
            {
                var sum = 0;
                var lst = new List<int>();
                for (var i = 0; i <= path.Length; i++)
                {
                    var x = valsDict[path.Substring(0, i)];
                    sum += x;
                    lst.Add(x);
                }
                if (sum == exp) good.Add(lst);
                return;
            }
            if (node.left != null) ByLevel(node.left, $"{path}L", valsDict, exp, good);
            if (node.right != null) ByLevel(node.right, $"{path}R", valsDict, exp, good);
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[5,4,8,11,null,13,4,7,2,null,null,5,1]
22
[ [5,4,11,2], [5,8,4,5] ]
[]
1
[]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}