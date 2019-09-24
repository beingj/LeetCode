using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BinarySearchTreeIterator
{
    public class Solution
    {
        public class BSTIterator
        {
            List<int> values = new List<int>();
            int idx = 0;
            public BSTIterator(TreeNode root)
            {
                if (root != null)
                {
                    LTR(root);
                }
                // Console.WriteLine(string.Join(", ", values));
            }
            void LTR(TreeNode node)
            {
                if (node.left != null)
                {
                    LTR(node.left);
                }
                values.Add(node.val);
                if (node.right != null)
                {
                    LTR(node.right);
                }
            }

            /** @return the next smallest number */
            public int Next()
            {
                return values[idx++];
            }

            /** @return whether we have a next smallest number */
            public bool HasNext()
            {
                return idx < values.Count;
            }
        }
        public int Next(TreeNode root)
        {
            var iter = new BSTIterator(root);
            return iter.Next();
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[7,3,15,null,null,9,20]
3
";
            var lines = input.CleanInput();
            // Verify.Method(new Solution(), lines);
            lines = "0173-data.txt".InputFromFile();
            Verify.NestedClass(typeof(Solution).GetNestedTypes().First(), lines);
        }
    }
}