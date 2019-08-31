using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace InsertionSortList
{
    public class Solution
    {
        public ListNode InsertionSortList(ListNode head)
        {
            var nodes = new List<ListNode>();
            var node = head;
            while (node != null)
            {
                nodes.Add(node);
                var curr = node;
                node = node.next;
                curr.next = null;
            }
            for (var i = 1; i < nodes.Count; i++)
            {
                var j = i - 1;
                for (; j >= 0; j--)
                {
                    if (nodes[i].val > nodes[j].val)
                    {
                        break;
                    }
                }
                nodes.Insert(j + 1, nodes[i]);
                nodes.RemoveAt(i + 1);
            }
            for (var i = 0; i < nodes.Count - 1; i++)
            {
                nodes[i].next = nodes[i + 1];
            }

            if (nodes.Count > 0)
                return nodes[0];
            else
                return null;
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[4,2,1,3]
[1,2,3,4]
[-1,5,3,4,0]
[-1,0,3,4,5]
[]
[]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}