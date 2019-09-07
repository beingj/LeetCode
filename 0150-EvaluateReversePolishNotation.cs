using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace EvaluateReversePolishNotation
{
    public class Solution
    {
        public int EvalRPN(string[] tokens)
        {
            var OPs = new List<string> { "+", "-", "*", "/" };
            var nums = new Stack<int>();
            foreach (var s in tokens)
            {
                if (OPs.Contains(s))
                {
                    var b = nums.Pop();
                    var a = nums.Pop();
                    var x = 0;
                    if (s == "+")
                    {
                        x = a + b;
                    }
                    else if (s == "-")
                    {
                        x = a - b;
                    }
                    else if (s == "*")
                    {
                        x = a * b;
                    }
                    else if (s == "/")
                    {
                        x = a / b;
                    }
                    nums.Push(x);
                }
                else
                {
                    nums.Push(int.Parse(s));
                }
            }
            return nums.Pop();
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
            lines = "0150-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}