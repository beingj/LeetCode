using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ReorderList
{
    public class Solution
    {
        public void ReorderList(ListNode head)
        {
            var node = head;
            var nodes = new List<ListNode>();
            while (node != null)
            {
                nodes.Add(node);
                var curr = node;
                node = node.next;
                curr.next = null;
            }
            var maxIdx = nodes.Count - 1;
            var half = nodes.Count / 2;
            var isEven = (nodes.Count % 2) == 0;
            for (var i = 0; i < half; i++)
            {
                nodes[i].next = nodes[maxIdx - i];
                if ((i >= half - 1) && isEven) continue;
                nodes[maxIdx - i].next = nodes[i + 1];
            }
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[1,2,3,4]
[1,4,2,3]
[1,2,3,4,5]
[1,5,2,4,3]
[]
[]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines, checkParaIndex: 0);
        }
    }
}