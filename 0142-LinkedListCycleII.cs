using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LinkedListCycleII
{
    public class Solution
    {
        public string WrappedDetectCycle(ListNode head, int pos)
        {
            var node = head;
            var nodes = new List<ListNode>();
            while (node != null)
            {
                nodes.Add(node);
                node = node.next;
            }
            if (nodes.Count > 0)
                node = nodes.Last();
            if (pos != -1)
                node.next = nodes[pos];

            Console.WriteLine(head);
            var n = DetectCycle(head);
            if (n == null)
                return "no cycle";
            else
                return $"tail connects to node index {nodes.IndexOf(n)}";
        }
        public ListNode DetectCycle(ListNode head)
        {
            var node = head;
            var nodes = new List<ListNode>();
            while (node != null)
            {
                var pos = nodes.IndexOf(node);
                if (pos > -1)
                    return nodes[pos];
                nodes.Add(node);
                node = node.next;
            }
            return null;
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[]
-1
no cycle
[3,2,0,-4]
1
tail connects to node index 1
[1,2]
0
tail connects to node index 0
[1]
-1
no cycle
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}