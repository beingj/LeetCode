using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RemoveDuplicatesFromSortedList
{
    public class Solution
    {
        public ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null)
            {
                return head;
            }
            ListNode last = head;
            ListNode prev = head;
            ListNode curr = head;
            while (curr.next != null)
            {
                prev = curr;
                curr = curr.next;

                if (curr.val == prev.val)
                {
                    last.next = null;
                }
                else
                {
                    last.next = curr;
                    last = curr;
                }
            }
            return head;
        }
    }
    public class Test
    {
        static void Verify(ListNode head, ListNode exp)
        {
            Console.WriteLine($"{head}");
            ListNode res;
            using (new Timeit())
            {
                res = new Solution().DeleteDuplicates(head);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine(typeof(Solution).Namespace);

            var input = @"
1->1->2
1->2
1->1->2->3->3
1->2->3
";
            var lines = input.CleanInput();
            ListNode head;
            ListNode exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                head = ListNode.FromList(lines[idx++]);
                exp = ListNode.FromList(lines[idx++]);
                Verify(head, exp);
            }
        }
    }
}