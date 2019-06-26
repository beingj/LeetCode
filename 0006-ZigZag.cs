using System;
using Xunit;
using Util;
using System.Text;

namespace ZigZag
{
    public class Solution
    {
        public string Convert(string s, int numRows)
        {
            // P   A   H   N
            // A P L S I I G
            // Y   I   R

            // P     I    N
            // A   L S  I G
            // Y A   H R
            // P     I
            StringBuilder[] rows = new StringBuilder[numRows];
            for (var i = 0; i < rows.Length; i++)
            {
                rows[i] = new StringBuilder();
            }

            int idx = 0;

            while (true)
            {
                for (var i = 0; i < numRows; i++)
                {
                    if (idx >= s.Length)
                        break;
                    rows[i].Append(s[idx]);
                    idx++;
                }
                if (idx >= s.Length)
                    break;
                for (var i = numRows - 2; i > 0; i--)
                {
                    if (idx >= s.Length)
                        break;
                    rows[i].Append(s[idx]);
                    idx++;
                }
                if (idx >= s.Length)
                    break;
            }

            StringBuilder res = new StringBuilder();
            foreach (var r in rows)
            {
                res.Append(r.ToString());
            }

            return res.ToString();
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("ZigZag");

            string s;
            int numRows;
            string exp, res;

            using (new Timeit())
            {
                s = "PAYPALISHIRING";
                numRows = 3;
                exp = "PAHNAPLSIIGYIR";
                res = new Solution().Convert(s, numRows);
                Console.WriteLine(exp);
                Console.WriteLine(res);
                Assert.Equal(exp, res);
            }

            using (new Timeit())
            {
                s = "PAYPALISHIRING";
                numRows = 4;
                exp = "PINALSIGYAHRPI";
                res = new Solution().Convert(s, numRows);
                Console.WriteLine(exp);
                Console.WriteLine(res);
                Assert.Equal(exp, res);
            }

        }
    }

}