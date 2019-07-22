using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace GrayCode
{
    public class Solution
    {
        public IList<int> GrayCode(int n)
        {
            var seq = new List<IList<int>>();
            var soFar = new List<int>();
            var zero = new List<int>();
            for (var i = 0; i < n; i++)
            {
                zero.Add(0);
            }
            soFar.Add(0);
            var total = (int)Math.Pow(2, n);
            SeqNext(soFar, zero, total, seq);
            // Console.WriteLine(string.Join("\n", seq.Select(x => string.Join(", ", x))));
            return seq.First();
        }
        void SeqNext(List<int> soFar, List<int> last, int total, List<IList<int>> seq)
        {
            if (seq.Count > 0)
            {
                return;
            }
            if (soFar.Count == total)
            {
                seq.Add(soFar);
                return;
            }
            for (var i = 0; i < last.Count; i++)
            {
                var newOne = new List<int>();
                newOne.AddRange(last);
                newOne[i] = 1 - last[i];
                var j = Convert.ToInt32(string.Join("", newOne), 2);
                if (soFar.Contains(j))
                {
                    continue;
                }
                var newSoFar = new List<int>();
                newSoFar.AddRange(soFar);
                newSoFar.Add(j);
                SeqNext(newSoFar, newOne, total, seq);
            }
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
2
[0,2,3,1]
0
[0]
5
[0, 16, 24, 8, 12, 28, 20, 4, 6, 22, 30, 14, 10, 26, 18, 2, 3, 19, 27, 11, 15, 31, 23, 7, 5, 21, 29, 13, 9, 25, 17, 1]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}