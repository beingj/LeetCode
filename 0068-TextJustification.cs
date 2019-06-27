using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace TextJustification
{
    public class Solution
    {
        public IList<string> FullJustify(string[] words, int maxWidth)
        {
            int idx = 0;
            var res = new List<string>();
            var line = new List<string>();
            var lineLen = 0;
            string JoinLine(List<string> lst, bool lastLine = false)
            {
                if (lastLine)
                {
                    // the last line must be left-justified instead of fully-justified
                    var s = string.Join(" ", lst);
                    return s.PadRight(maxWidth);
                }
                if (lst.Count == 1)
                {
                    return lst[0].PadRight(maxWidth);
                }
                var total = maxWidth - lst.Sum(i => i.Length);
                var spacesGroup = lst.Count - 1;
                // eg: 11 spaces split to 3 groups: 4, 4, 3:
                // 11/3=3, 11%3=2. add these 2 extra spaces to the first 2 groups
                var tailSpaceLen = (int)(total / spacesGroup);
                var spaces = "".PadRight(tailSpaceLen);
                var extra = total % spacesGroup;
                if (extra == 0)
                {
                    return string.Join(spaces, lst);
                }
                var spaces2 = "".PadRight(tailSpaceLen + 1);
                var head = string.Join(spaces2, lst.Take(extra));
                var tail = string.Join(spaces, lst.Skip(extra));
                return head + spaces2 + tail;
            }
            while (idx < words.Length)
            {
                var word = words[idx];
                if (word.Length >= (maxWidth - 1))
                {
                    if (line.Count > 0)
                    {
                        res.Add(JoinLine(line));
                        line.Clear();
                        lineLen = 0;
                    }
                    if (word.Length == (maxWidth - 1))
                    {
                        res.Add($"{word} ");
                    }
                    else
                    {
                        res.Add(word);
                    }
                }
                else
                {
                    if ((lineLen + word.Length + 1) > maxWidth)
                    {
                        if (line.Count > 0)
                        {
                            res.Add(JoinLine(line));
                            line.Clear();
                            lineLen = 0;
                        }
                    }
                    line.Add(word);
                    if (line.Count == 1)
                    {
                        lineLen += word.Length;
                    }
                    else
                    {
                        lineLen += word.Length + 1;
                    }
                }
                idx++;
            }
            if (line.Count > 0)
            {
                res.Add(JoinLine(line, lastLine: true));
            }
            return res.ToArray();
        }
    }

    public class Test
    {
        static void Verify(string[] words, int maxWidth, IList<string> exp)
        {
            Console.WriteLine($"'{string.Join(", ", words)}' | {maxWidth}");
            IList<string> res;
            using (new Timeit())
            {
                res = new Solution().FullJustify(words, maxWidth);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine("TextJustification");

            var lines = "0068-data.txt".InputFromFile();
            string[] words;
            int maxWidth;
            IList<string> exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                words = lines[idx++].JsonToStr1d();
                maxWidth = int.Parse(lines[idx++]);
                exp = lines[idx++].JsonToStr1d();
                Verify(words, maxWidth, exp);
            }
        }
    }
}