using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SortList
{
    public class Solution
    {
        public ListNode SortList(ListNode head)
        {
            if (head == null)
                return head;

            var nodes = new List<ListNode>();
            var node = head;
            while (node != null)
            {
                nodes.Add(node);
                var curr = node;
                node = node.next;
                curr.next = null;
            }
            QuickSort(nodes, 0, nodes.Count - 1);

            for (var i = 0; i < nodes.Count - 1; i++)
            {
                nodes[i].next = nodes[i + 1];
            }

            return nodes[0];
        }
        // copy from: https://baike.baidu.com/item/%E5%BF%AB%E9%80%9F%E6%8E%92%E5%BA%8F%E7%AE%97%E6%B3%95
        static int sortUnit(IList<ListNode> array, int low, int high)
        {
            ListNode key = array[low];
            while (low < high)
            {
                /*从后向前搜索比key小的值*/
                while (array[high].val >= key.val && high > low)
                    --high;
                /*比key小的放左边*/
                array[low] = array[high];
                /*从前向后搜索比key大的值，比key大的放右边*/
                while (array[low].val <= key.val && high > low)
                    ++low;
                /*比key大的放右边*/
                array[high] = array[low];
            }
            /*左边都比key小，右边都比key大。//将key放在游标当前位置。//此时low等于high */
            array[low] = key;
            // foreach (ListNode i in array)
            // {
            //     Console.Write("{0}\t", i);
            // }
            // Console.WriteLine();
            return high;
        }
        static void QuickSort(IList<ListNode> array, int low, int high)
        {
            if (low >= high)
                return;
            /*完成一次单元排序*/
            int index = sortUnit(array, low, high);
            /*对左边单元进行排序*/
            QuickSort(array, low, index - 1);
            /*对右边单元进行排序*/
            QuickSort(array, index + 1, high);
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[4,2,1,3]
[1,2,3,4]
[-1,5,3,4,0]
[-1,0,3,4,5]
[]
[]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}