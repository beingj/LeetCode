using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PopulatingNextRightPointersInEachNode
{
    public class Solution
    {
        public Node Connect(Node root)
        {
            if (root == null)
                return root;
            var valsDict = new Dictionary<string, Node>();
            ByLevel(root, "", valsDict);
            var maxLvl = valsDict.Keys.Select(i => i.Length).Max();
            for (var i = 1; i <= maxLvl; i++)
            {
                var path = valsDict.Keys.Where(n => n.Length == i).ToList();
                for (var j = 0; j < path.Count - 1; j++)
                {
                    valsDict[path[j]].next = valsDict[path[j + 1]];
                }
            }
            return root;
        }
        void ByLevel(Node node, string path, Dictionary<string, Node> vals)
        {
            vals[path] = node;
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
";
            var lines = input.CleanInput();
            // Verify.Method(new Solution(), lines);
            lines = "0116-data.txt".InputFromFile();
            var sln = new Solution();
            foreach (var l in lines)
            {
                var n = Node.FromStr(l);
                Console.WriteLine(n.ToStr(leafSpaceN: 5, branchSpaceN: 5, withNext: true));
                sln.Connect(n);
                Console.WriteLine();
                Console.WriteLine(n.ToStr(leafSpaceN: 5, branchSpaceN: 5, withNext: true));
            }
        }
    }
}