using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace IntersectionOfTwoLinkedLists
{
    public class Solution
    {
        // 8
        // [4,1,8,4,5]
        // [5,0,1,8,4,5]
        // 2
        // 3
        // Intersected at '8'
        public string WrappedGetIntersectionNode(int intersectVal, ListNode listA, ListNode listB, int skipA, int skipB)
        {
            var na = listA;
            for (var i = 0; i < skipA; i++)
            {
                na = na.next;
            }
            var nb = listB;
            for (var i = 1; i < skipB; i++)
            {
                nb = nb.next;
            }
            // Console.WriteLine(nb);
            // Console.WriteLine(na);
            nb.next = na;
            // Console.WriteLine(listA);
            // Console.WriteLine(listB);

            var n = GetIntersectionNode(listA, listB);
            if (n != null)
            {
                var val = n.val;
                return string.Format("Intersected at '{0}'", val);
            }
            else
            {
                return "null";
            }
        }
        public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            var seen = new HashSet<ListNode>();
            var node = headA;
            while (node != null)
            {
                seen.Add(node);
                node = node.next;
            }
            node = headB;
            while (node != null)
            {
                if (seen.Contains(node))
                    return node;
                seen.Add(node);
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
";
            var lines = input.CleanInput();
            lines = "0160-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}