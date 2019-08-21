using System;
using Xunit;
using Util;
using Node = Util.GraphNode;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CloneGraph
{
    public class Solution
    {
        public Node CloneGraph(Node node)
        {
            if (node == null) return null;
            var cloned = new Dictionary<Node, Node>();
            return GetNodeOrCreate(node, cloned);
        }
        Node GetNodeOrCreate(Node node, Dictionary<Node, Node> cloned)
        {
            if (cloned.ContainsKey(node))
            {
                return cloned[node];
            }
            var nodeClone = new Node();
            nodeClone.val = node.val;
            nodeClone.neighbors = new List<Node>();
            cloned[node] = nodeClone;
            foreach (var i in node.neighbors)
            {
                var nb = GetNodeOrCreate(i, cloned);
                nodeClone.neighbors.Add(nb);
            }
            return nodeClone;
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
            lines = "0133-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}