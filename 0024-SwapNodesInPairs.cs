using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace SwapNodesInPairs
{
    public class Solution
    {
        public ListNode SwapPairs(ListNode head)
        {
            if ((head == null) || (head.next == null))
            {
                return head;
            }
            ListNode old1 = head;
            ListNode old3 = old1.next.next;
            head = head.next;
            head.next = old1;
            head.next.next = old3;
            ListNode prePair = head.next;

            while (true)
            {
                ListNode node1 = prePair.next;
                if (node1 == null)
                    break;
                if (node1.next == null)
                    break;
                old1 = node1;
                old3 = node1.next.next;
                node1 = node1.next;
                prePair.next = node1;
                node1.next = old1;
                node1.next.next = old3;
                prePair = node1.next;
            }
            return head;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("SwapNodesInPairs");
            ListNode input;
            ListNode exp, res;
            // Given 1->2->3->4, 
            // you should return the list as 2->1->4->3.
            input = ListNode.FromList(new int[] { 1, 2, 3, 4 });
            exp = ListNode.FromList(new int[] { 2, 1, 4, 3 });
            using (new Timeit())
            {
                res = new Solution().SwapPairs(input);
                Assert.Equal(exp, res);
            }
            input = ListNode.FromList(new int[] { 1, 2, 3 });
            exp = ListNode.FromList(new int[] { 2, 1, 3 });
            using (new Timeit())
            {
                res = new Solution().SwapPairs(input);
                Assert.Equal(exp, res);
            }
            input = ListNode.FromList(new int[] { 1, 2 });
            exp = ListNode.FromList(new int[] { 2, 1 });
            using (new Timeit())
            {
                res = new Solution().SwapPairs(input);
                Assert.Equal(exp, res);
            }
            input = ListNode.FromList(new int[] { 1 });
            exp = ListNode.FromList(new int[] { 1 });
            using (new Timeit())
            {
                res = new Solution().SwapPairs(input);
                Assert.Equal(exp, res);
            }
            input = null;
            using (new Timeit())
            {
                res = new Solution().SwapPairs(input);
                Assert.Null(res);
            }
        }
    }
}