using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;

namespace MergeTwoSortedLists
{
    public class Solution
    {
        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null)
                return l2;
            if (l2 == null)
                return l1;
            ListNode res = null;
            ListNode node = null;
            ListNode n1 = l1, n2 = l2;
            while (true)
            {
                if (n1.val <= n2.val)
                {
                    if (res == null)
                    {
                        res = new ListNode(n1.val);
                        node = res;
                    }
                    else
                    {
                        node.next = new ListNode(n1.val);
                        node = node.next;
                    }

                    if (n1.next != null)
                    {
                        n1 = n1.next;
                    }
                    else
                    {
                        while (true)
                        {
                            node.next = new ListNode(n2.val);
                            node = node.next;
                            if (n2.next == null)
                                break;
                            n2 = n2.next;
                        }
                        break;
                    }
                }
                else
                {
                    if (res == null)
                    {
                        res = new ListNode(n2.val);
                        node = res;
                    }
                    else
                    {
                        node.next = new ListNode(n2.val);
                        node = node.next;
                    }

                    if (n2.next != null)
                    {
                        n2 = n2.next;
                    }
                    else
                    {
                        while (true)
                        {
                            node.next = new ListNode(n1.val);
                            node = node.next;
                            if (n1.next == null)
                                break;
                            n1 = n1.next;
                        }
                        break;
                    }
                }
            }
            return res;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("MergeTwoSortedLists");

            ListNode l1, l2;
            ListNode exp, res;

            using (new Timeit())
            {
                // Input: 1->2->4, 1->3->4
                // Output: 1->1->2->3->4->4
                l1 = ListNode.FromList(new int[] { 1, 2, 4 });
                l2 = ListNode.FromList(new int[] { 1, 3, 4 });
                exp = ListNode.FromList(new int[] { 1, 1, 2, 3, 4, 4 });
                res = new Solution().MergeTwoLists(l1, l2);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                // Input: [],[]
                // Output: []
                l1 = null;
                l2 = null;
                exp = null;
                res = new Solution().MergeTwoLists(l1, l2);
                Assert.Equal(exp, res);
            }
        }
    }
}