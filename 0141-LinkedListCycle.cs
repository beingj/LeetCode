using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LinkedListCycle
{
    public class Solution
    {
        public bool WrappedHasCycle(ListNode head, int pos)
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
            return HasCycle(head);
        }
        public bool HasCycle(ListNode head)
        {
            var node = head;
            var nodes = new List<ListNode>();
            while (node != null)
            {
                var pos = nodes.IndexOf(node);
                if (pos > -1)
                    return true;
                nodes.Add(node);
                node = node.next;
            }
            return false;
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
false
[3,2,0,-4]
1
true
[1,2]
0
true
[1]
-1
false
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}