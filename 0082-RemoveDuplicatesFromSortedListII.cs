using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RemoveDuplicatesFromSortedListII
{
    public class Solution
    {
        public ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null)
            {
                return head;
            }
            while ((head.next != null) && (head.next.val == head.val))
            {
                var x = head.val;
                var find = false;
                while (head.next != null)
                {
                    head = head.next;
                    if (head.val != x)
                    {
                        find = true;
                        break;
                    }
                }
                if (!find)
                {
                    return null;
                }
            }
            ListNode lastprev = head;
            ListNode last = head;
            ListNode prev = head;
            ListNode curr = head;
            while (curr.next != null)
            {
                prev = curr;
                curr = curr.next;

                if (curr.val == prev.val)
                {
                    last = lastprev;
                    last.next = null;
                }
                else
                {
                    last.next = curr;
                    lastprev = last;
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
#[1,2,3,3,4,4,5]
#[1,2,5]
#[1,1,1,2,3]
#[2,3]
[1,1,2,2]
[]
#[1,1]
#[]
";
            var lines = input.CleanInput();
            ListNode head;
            ListNode exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                head = lines[idx++].JsonToListNode();
                exp = lines[idx++].JsonToListNode();
                Verify(head, exp);
            }
        }
    }
}