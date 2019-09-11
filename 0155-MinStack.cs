using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MinStack
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
    public class Solution
    {
        public IList<int?> MinStackTest(IList<string> op, IList<IList<int>> para)
        {
            MinStack stack = new MinStack();
            var rets = new List<int?>();
            for (var idx = 0; idx < para.Count; idx++)
            {
                int? ret = null;
                // ["MinStack","push","push","push","getMin","pop","top","getMin"]
                if (op[idx] == "MinStack")
                {
                    stack = new MinStack();
                }
                else if (op[idx] == "push")
                {
                    stack.Push(para[idx][0]);
                }
                else if (op[idx] == "pop")
                {
                    stack.Pop();
                    ret = null;
                }
                else if (op[idx] == "top")
                {
                    ret = stack.Top();
                }
                else if (op[idx] == "getMin")
                {
                    ret = stack.GetMin();
                }
                rets.Add(ret);
            }
            // Console.WriteLine(rets.IListNullableIntToJson());
            return rets;
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
            Verify.Method(new Solution(), lines);
        }
    }
}