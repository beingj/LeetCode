using System;
using Xunit;
using Util;
using Node = Util.NodeRandom;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CopyListWithRandomPointer
{
    public class Solution
    {
        public Node CopyRandomList(Node head)
        {
            if (head == null) return null;
            var cloned = new Dictionary<Node, Node>();
            return GetOrCreate(head, cloned);
        }
        Node GetOrCreate(Node node, Dictionary<Node, Node> cloned)
        {
            if (cloned.ContainsKey(node))
            {
                return cloned[node];
            }
            var nodeClone = new Node();
            nodeClone.val = node.val;
            cloned[node] = nodeClone;
            if (node.next != null)
            {
                nodeClone.next = GetOrCreate(node.next, cloned);
            }
            if (node.random != null)
            {
                nodeClone.random = GetOrCreate(node.random, cloned);
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
            lines = "0138-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
            // var sln = new Solution();
            // foreach (var i in lines)
            // {
            //     var n = Node.FromJson(i);
            //     Console.WriteLine(n);
            //     var nn = sln.CopyRandomList(n);
            //     Console.WriteLine(nn);
            // }
        }
    }
}