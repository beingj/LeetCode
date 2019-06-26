using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Util;

namespace AddTwoNumbers
{
    public class Solution
    {
        public ListNode AddTwoNumbers1(ListNode l1, ListNode l2)
        {
            ListNode node = l1;
            Console.WriteLine($"l1:{l1.val}");
            ulong n1 = 0;
            int b = 0;
            while (true)
            {
                n1 += (ulong)(node.val * Math.Pow(10, b));
                Console.WriteLine($"n1: %10 {n1 % 10} => {n1}");
                if (node.next == null) break;
                node = node.next;
                b += 1;
            }
            Console.WriteLine($"n1 done: %10 {n1 % 10} => {n1}");

            node = l2;
            ulong n2 = 0;
            b = 0;
            while (true)
            {
                n2 += (ulong)(node.val * Math.Pow(10, b));
                if (node.next == null) break;
                node = node.next;
                b += 1;
            }

            // Console.WriteLine($"max:{double.MaxValue}");
            Console.WriteLine($"n1:{l1.ToInt()}");
            Console.WriteLine($"n1: %10 {n1 % 10}");
            Console.WriteLine($"n1:{ListNode.FromInt(n1).ToInt()}");
            Console.WriteLine($"n2: 465 {l2.ToInt()}");
            Console.WriteLine($"n2:{ListNode.FromInt(n2).ToInt()}");
            ulong sum = n1 + n2;
            Console.WriteLine($"sum:{sum}");
            Console.WriteLine($"sum:{ListNode.FromInt(sum)}");
            ListNode res = new ListNode((int)(sum % 10));
            node = res;
            while (true)
            {
                if (sum < 10) break;
                sum /= 10;
                node.next = new ListNode((int)(sum % 10));
                node = node.next;
            }
            return res;
        }
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            int a, b, c, s;
            s = l1.val + l2.val;
            ListNode res = new ListNode(s % 10);
            c = s >= 10 ? 1 : 0;
            ListNode node1 = l1.next;
            ListNode node2 = l2.next;
            ListNode node = res;
            while (true)
            {
                if (node1 == null && node2 == null) break;
                a = node1 != null ? node1.val : 0;
                b = node2 != null ? node2.val : 0;
                s = a + b + c;
                node.next = new ListNode(s % 10);
                c = s >= 10 ? 1 : 0;
                node1 = node1 != null ? node1.next : null;
                node2 = node2 != null ? node2.next : null;
                node = node.next;
            }
            if (c > 0)
                node.next = new ListNode(c);
            return res;
        }
    }
    public class Test
    {
        static public void Run()
        {
            // Input: (2-> 4-> 3) +(5-> 6-> 4)
            // Output: 7-> 0-> 8
            // Explanation: 342 + 465 = 807.

            var l1 = ListNode.FromList(new int[] { 2, 4, 3 });
            var l2 = ListNode.FromList(new int[] { 5, 6, 4 });
            var exp = ListNode.FromList(new int[] { 7, 0, 8 });

            // Console.WriteLine($"{l1}");
            // Console.WriteLine($"{l2}");
            // Console.WriteLine($"{exp}");
            var res = new Solution().AddTwoNumbers(l1, l2);
            Console.WriteLine($"AddTwoNumbers: {l1}+{l2}\nExp: {exp}\nAct: {res}\n");

            l1 = ListNode.FromList(new int[] { 0 });
            l2 = ListNode.FromList(new int[] { 0 });
            exp = ListNode.FromList(new int[] { 0 });
            res = new Solution().AddTwoNumbers(l1, l2);
            Console.WriteLine($"AddTwoNumbers: {l1}+{l2}\nExp: {exp}\nAct: {res}\n");

            l1 = ListNode.FromList(new int[] { 9 });
            l2 = ListNode.FromList(new int[] { 1, 9, 9, 9, 9, 9, 9, 9, 9, 9 });
            exp = ListNode.FromList(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 });
            res = new Solution().AddTwoNumbers(l1, l2);
            Console.WriteLine($"AddTwoNumbers: {l1}+{l2}\nExp: {exp}\nAct: {res}\n");

            l1 = ListNode.FromList(new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 });
            // l1 = ListNode.FromList(new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 });
            // l1 = ListNode.FromList(new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 });
            l2 = ListNode.FromList(new int[] { 5, 6, 4 });
            exp = ListNode.FromList(new int[] { 6, 6, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 });
            res = new Solution().AddTwoNumbers(l1, l2);
            Console.WriteLine($"AddTwoNumbers: {l1}+{l2}\nExp: {exp}\nAct: {res}\n");
        }
    }
}