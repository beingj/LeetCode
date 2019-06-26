using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace RotateList
{
    public class Solution
    {
        public ListNode RotateRight(ListNode head, int k)
        {
            if (head == null)
            {
                return head;
            }
            if (k == 0)
            {
                return head;
            }
            ListNode last = head;
            int n = 1;
            while (last.next != null)
            {
                last = last.next;
                n++;
            }
            last.next = head;
            ListNode tail = head;
            k = k % n;
            var step = n - k - 1;
            for (var i = 0; i < step; i++)
            {
                tail = tail.next;
            }
            ListNode newHead = tail.next;
            tail.next = null;
            return newHead;
        }
    }

    public class Test
    {
        static void Verify(ListNode head, int k, ListNode exp)
        {
            Console.WriteLine($"{head} => {k}");
            ListNode res;
            using (new Timeit())
            {
                res = new Solution().RotateRight(head, k);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("RotateList");
            var input = @"
1->2->3->4->5
2
4->5->1->2->3
0->1->2
4
2->0->1

1

1->2
1
2->1
1->2->3
2000000000
2->3->1
";
            var lines = input.Trim(new char[] { '\n', '\r', ' ' }).Split('\n')
                            .Select(x => x.Trim(new char[] { '\r', ' ' })).Where(y => !y.StartsWith('#')).ToArray();
            ListNode head; int k;
            ListNode exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                head = ListNode.FromList(lines[idx++]);
                k = int.Parse(lines[idx++]);
                exp = ListNode.FromList(lines[idx++]);
                Verify(head, k, exp);
            }
        }
    }
}