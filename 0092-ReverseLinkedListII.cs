using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ReverseLinkedListII
{
    public class Solution
    {
        public ListNode ReverseBetween(ListNode head, int m, int n)
        {
            // copy from 0025-ReverseNodesInkGroup.cs
            var k = n - m + 1;
            if ((head == null) || (head.next == null))
            {
                return head;
            }
            ListNode nodePre = null, tail = head;
            int dist = 0, gdist = k - 1;

            ListNode node = head;
            for (var i = 0; i < (m - 1); i++)
            {
                nodePre = node;
                node = node.next;
            }
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
                    break;
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
            var input = @"
[1,2,3,4,5]
2
4
[1,4,3,2,5]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}