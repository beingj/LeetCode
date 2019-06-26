using System;
using Xunit;
using Util;
using System.Collections.Generic;

namespace RemoveNthFromEnd
{
    public class Solution
    {
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode nth = head, node = head;
            int distance = 0;
            int len = 1;
            while (node.next != null)
            {
                node = node.next;
                len++;
                distance++;
                if (distance > n)
                {
                    nth = nth.next;
                }
            }

            if (len == 1)
            {
                // no node left after removed the only one
                return null;
            }

            if (len == n)
            {
                // remove the head node
                head = head.next;
                return head;
            }
            nth.next = nth.next.next;
            return head;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("RemoveNthFromEnd");

            ListNode input;
            int n;
            ListNode exp, res;

            using (new Timeit())
            {
                // Given linked list: 1->2->3->4->5, and n = 2.
                // After removing the second node from the end,
                // the linked list becomes 1->2->3->5.
                input = ListNode.FromList(new int[] { 1, 2, 3, 4, 5 });
                n = 2;
                exp = ListNode.FromList(new int[] { 1, 2, 3, 5 });
                res = new Solution().RemoveNthFromEnd(input, n);
                Assert.Equal(exp.ToString(), res.ToString());
            }
            using (new Timeit())
            {
                input = ListNode.FromList(new int[] { 1 });
                n = 1;
                exp = null;
                res = new Solution().RemoveNthFromEnd(input, n);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                input = ListNode.FromList(new int[] { 1, 2 });
                n = 2;
                exp = ListNode.FromList(new int[] { 2 });
                res = new Solution().RemoveNthFromEnd(input, n);
                Assert.Equal(exp.ToString(), res.ToString());
            }
        }
    }
}