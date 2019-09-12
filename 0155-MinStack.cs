using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MinStack
{
    public class Solution
    {
        public class MinStack
        {

            List<int> Stack = new List<int>();
            /** initialize your data structure here. */
            public MinStack()
            {

            }

            public void Push(int x)
            {
                Stack.Add(x);
            }

            public void Pop()
            {
                Stack.RemoveAt(Stack.Count - 1);
            }

            public int Top()
            {
                return Stack.Count == 0 ? int.MinValue : Stack.Last();
            }

            public int GetMin()
            {
                return Stack.Min();
            }
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
";
            var lines = input.CleanInput();
            lines = "0155-data.txt".InputFromFile();
            var cls = typeof(Solution).GetNestedTypes().First();
            Verify.NestedClass(cls, lines);
        }
    }
}