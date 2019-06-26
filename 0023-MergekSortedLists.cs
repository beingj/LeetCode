using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace MergekSortedLists
{
    public class Solution
    {
        public ListNode MergeKLists(ListNode[] lists)
        {
            lists = lists.Where(x => x != null).ToArray();
            if (lists.Length == 0)
            {
                return null;
            }
            int[] idxs = new int[lists.Length];
            ListNode[] nodes = new ListNode[lists.Length];
            for (var i = 0; i < lists.Length; i++)
            {
                idxs[i] = 0;
                nodes[i] = lists[i];
            }

            ListNode res = null;
            ListNode resNode = null;
            int min, minIdx = 0;
            while (true)
            {
                min = int.MaxValue;
                for (var i = 0; i < lists.Length; i++)
                {
                    if ((nodes[i] != null) && (nodes[i].val < min))
                    {
                        min = nodes[i].val;
                        minIdx = i;
                    }
                }

                if (res == null)
                {
                    res = new ListNode(min);
                    resNode = res;
                }
                else
                {
                    resNode.next = new ListNode(min);
                    resNode = resNode.next;
                }

                if (nodes[minIdx].next != null)
                {
                    nodes[minIdx] = nodes[minIdx].next;
                }
                else
                {
                    nodes[minIdx] = null;
                }

                bool allNull = true;
                foreach (var n in nodes)
                {
                    if (n != null)
                    {
                        allNull = false;
                        break;
                    }
                }
                if (allNull)
                    break;
            }
            return res;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("MergekSortedLists");
            ListNode[] input;
            ListNode exp, res;
            // Input:
            // [
            //   1->4->5,
            //   1->3->4,
            //   2->6
            // ]
            // Output: 1->1->2->3->4->4->5->6
            input = new ListNode[]{
                ListNode.FromList(new int[]{1,4,5}),
                ListNode.FromList(new int[]{1,3,4}),
                ListNode.FromList(new int[]{2,6}),
            };
            exp = ListNode.FromList(new int[] { 1, 1, 2, 3, 4, 4, 5, 6 });

            using (new Timeit())
            {
                res = new Solution().MergeKLists(input);
                Assert.Equal(exp, res);
            }

            // input: []
            input = new ListNode[] { };
            using (new Timeit())
            {
                res = new Solution().MergeKLists(input);
                Assert.Null(res);
            }

            // input: [[]]
            input = new ListNode[] { null };
            using (new Timeit())
            {
                res = new Solution().MergeKLists(input);
                Assert.Null(res);
            }
        }
    }
}