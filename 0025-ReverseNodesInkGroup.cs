using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ReverseNodesInkGroup
{
    public class Solution
    {
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            if ((head == null) || (head.next == null))
            {
                return head;
            }
            ListNode nodePre = null, tail = head;
            int dist = 0, gdist = k - 1;

            ListNode node = head;
            while (true)
            {
                if (dist == gdist)
                {
                    var x = SwapNodes(nodePre, node, k);
                    if (nodePre == null)
                    {
                        head = x;
                    }
                    nodePre = x;
                    for (var i = 0; i < k - 1; i++)
                    {
                        nodePre = nodePre.next;
                    }
                    node = nodePre.next;
                    tail = node;
                    dist = 0;
                }
                if (tail == null)
                {
                    break;
                }
                tail = tail.next;
                if (tail != null)
                {
                    dist++;
                }
            }
            return head;
        }
        static ListNode SwapNodes(ListNode nodePre, ListNode node, int k)
        {
            ListNode groupHead = node;
            int half = k / 2;
            int maxIdx = k - 1;
            for (var i = 0; i < half; i++)
            {
                var x = SwapPair(nodePre, node, maxIdx - i - i);
                if (i == 0)
                {
                    groupHead = x;
                    if (nodePre != null)
                    {
                        nodePre.next = x;
                    }
                }
                if (i == half)
                    break;
                nodePre = x;
                node = x.next;
            }
            return groupHead;
        }
        static ListNode SwapPair(ListNode nodePre, ListNode node, int distance)
        {
            ListNode oldHead = node;

            ListNode oldTailPre = node;
            for (var i = 1; i < distance; i++)
            {
                oldTailPre = oldTailPre.next;
            }

            ListNode oldTail = oldTailPre.next;
            ListNode oldTailNex = oldTailPre.next.next;

            node = oldTail;
            if (nodePre != null)
            {
                nodePre.next = node;
            }
            if (distance == 1)
            {
                node.next = oldHead;
                node.next.next = oldTailNex;
            }
            else
            {
                node.next = oldHead.next;
                oldTailPre.next = oldHead;
                oldTailPre.next.next = oldTailNex;
            }
            // Console.WriteLine($"node: {node}");
            return node;
        }
    }
    public class Solution2
    {
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            if ((head == null) || (head.next == null))
            {
                return head;
            }
            ListNode nodePre = null, tail = head;
            int dist = 0, gdist = k - 1;

            ListNode node = head;
            while (true)
            {
                if (dist == gdist)
                {
                    var x = SwapNodes(nodePre, node, k);
                    if (nodePre == null)
                    {
                        head = x[0];
                    }
                    nodePre = x[k - 1];
                    node = nodePre.next;
                    tail = node;
                    dist = 0;
                }
                if (tail == null)
                {
                    break;
                }
                tail = tail.next;
                if (tail != null)
                {
                    dist++;
                }
            }
            return head;
        }
        static ListNode[] SwapNodes(ListNode nodePre, ListNode node, int k)
        {
            ListNode[] group = new ListNode[k];
            for (var i = 0; i < k; i++)
            {
                group[i] = node;
                node = node.next;
            }
            ListNode tail = node;
            int half = k / 2;
            int maxIdx = k - 1;
            for (var i = 0; i < half; i++)
            {
                SwapPair(group, i, maxIdx - i);
            }
            if (nodePre != null)
            {
                nodePre.next = group[0];
            }
            group[maxIdx].next = tail;
            return group;
        }
        static void SwapPair(ListNode[] group, int idx1, int idx2)
        {
            ListNode oldHead = group[idx1];
            ListNode oldTailPre = group[idx2 - 1];
            ListNode oldTail = group[idx2];
            ListNode oldTailNex = oldTail.next;

            group[idx1] = oldTail;
            ListNode node = group[idx1];

            if (idx1 > 0)
            {
                group[idx1 - 1].next = group[idx1];
            }
            if ((idx2 - idx1) == 1)
            {
                node.next = oldHead;
                node.next.next = oldTailNex;
                group[idx2] = oldHead;
            }
            else
            {
                node.next = oldHead.next;
                oldTailPre.next = oldHead;
                oldTailPre.next.next = oldTailNex;
                group[idx2] = oldHead;
            }
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("ReverseNodesInkGroup");
            ListNode input;
            int k;
            ListNode exp, res;
            // Given 1->2->3->4->5
            // For k = 2, you should return: 2->1->4->3->5
            input = ListNode.FromList(new int[] { 1, 2, 3, 4, 5 });
            k = 2;
            exp = ListNode.FromList(new int[] { 2, 1, 4, 3, 5 });
            using (new Timeit())
            {
                res = new Solution().ReverseKGroup(input, k);
                Assert.Equal(exp, res);
            }

            // For k = 3, you should return: 3->2->1->4->5
            input = ListNode.FromList(new int[] { 1, 2, 3, 4, 5 });
            k = 3;
            exp = ListNode.FromList(new int[] { 3, 2, 1, 4, 5 });
            using (new Timeit())
            {
                res = new Solution().ReverseKGroup(input, k);
                Assert.Equal(exp, res);
            }

            input = null;
            k = 1;
            exp = null;
            using (new Timeit())
            {
                res = new Solution().ReverseKGroup(input, k);
                Assert.Equal(exp, res);
            }

            Console.WriteLine("ReverseNodesInkGroup solution2");
            input = ListNode.FromList(new int[] { 1, 2, 3, 4, 5 });
            k = 2;
            exp = ListNode.FromList(new int[] { 2, 1, 4, 3, 5 });
            using (new Timeit())
            {
                res = new Solution2().ReverseKGroup(input, k);
                Assert.Equal(exp, res);
            }

            // For k = 3, you should return: 3->2->1->4->5
            input = ListNode.FromList(new int[] { 1, 2, 3, 4, 5 });
            k = 3;
            exp = ListNode.FromList(new int[] { 3, 2, 1, 4, 5 });
            using (new Timeit())
            {
                res = new Solution2().ReverseKGroup(input, k);
                Assert.Equal(exp, res);
            }

            input = null;
            k = 1;
            exp = null;
            using (new Timeit())
            {
                res = new Solution2().ReverseKGroup(input, k);
                Assert.Equal(exp, res);
            }
        }
    }
}