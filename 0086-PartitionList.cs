using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PartitionList
{
    public class Solution
    {
        public ListNode Partition(ListNode head, int x)
        {
            ListNode appendPoint = null;
            ListNode prev = null;
            ListNode curr = head;
            while (curr != null)
            {
                // skip all nodes < x, and make the last one as appendPoint
                if (curr.val >= x)
                {
                    if (prev != null)
                    {
                        appendPoint = prev;
                    }
                    break;
                }
                prev = curr;
                curr = curr.next;
            }

            while (curr != null)
            {
                if (curr.val < x)
                {
                    ListNode currNext = curr.next;
                    if (appendPoint == null)
                    {
                        // move curr to head
                        ListNode currHead = head;
                        head = curr;
                        head.next = currHead;
                        prev.next = currNext;
                        appendPoint = head;
                    }
                    else
                    {
                        // move curr after appendPoint
                        ListNode lastNext = appendPoint.next;
                        appendPoint.next = curr;
                        appendPoint.next.next = lastNext;
                        prev.next = currNext;
                        appendPoint = appendPoint.next;
                    }
                }
                prev = curr;
                curr = curr.next;
            }
            return head;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
[1,4,3,2,5,2]
3
[1,2,2,4,3,5]
[4,3,2,5,2]
3
[2,2,4,3,5]
[1,1]
2
[1,1]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}