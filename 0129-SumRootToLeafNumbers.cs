using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SumRootToLeafNumbers
{
    public class Solution
    {
        public int SumNumbers(TreeNode root)
        {
            if (root == null) return 0;
            var sumList = new List<int>();
            ByLevel(root, 0, sumList);
            return sumList.Sum();
        }
        void ByLevel(TreeNode node, int sum, List<int> sumList)
        {
            sum = sum * 10 + node.val;
            if ((node.left == null) && (node.right == null))
            {
                sumList.Add(sum);
                return;
            }

            if (node.left != null)
            {
                ByLevel(node.left, sum, sumList);
            }
            if (node.right != null)
            {
                ByLevel(node.right, sum, sumList);
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
25
[4,9,0,5,1]
1026
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}