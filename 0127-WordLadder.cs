using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WordLadder
{
    public class Solution
    {
        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            // give up.
            // copy from: https://cloud.tencent.com/developer/article/1407026
            var wordSet = new HashSet<string>(wordList);
            var cur_level = new List<string> { beginWord };
            var next_level = new List<string>();
            var depth = 1;
            var n = beginWord.Length;
            while (cur_level.Count > 0)
            {
                foreach (var item in cur_level)
                {
                    if (item == endWord)
                        return depth;
                    for (var i = 0; i < n; i++)
                    {
                        foreach (var c in "abcdefghijklmnopqrstuvwxyz")
                        {
                            var word = item.Substring(0, i) + c + item.Substring(i + 1);
                            if (wordSet.Contains(word))
                            {
                                wordSet.Remove(word);
                                next_level.Add(word);
                            }
                        }
                    }
                }
                depth += 1;
                cur_level.Clear();
                foreach (var i in next_level)
                {
                    cur_level.Add(i);
                }
                next_level.Clear();
                // cur_level = next_level;
                // next_level = new List<string>();
            }
            return 0;
        }
    }
    // public class Solution1
    // {
    //     int minLen;
    //     int wordListCount;
    //     // Dictionary<(string, int), int> cache = new Dictionary<(string, int), int>();
    //     Dictionary<string, int> cache = new Dictionary<string, int>();
    //     public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    //     {
    //         if (beginWord == endWord)
    //             return 2;
    //         if (!wordList.Contains(endWord)) return 0;

    //         minLen = wordList.Count;
    //         wordListCount = wordList.Count;
    //         var x = CachedMyLadderLength(beginWord, endWord, wordList);
    //         return x;
    //     }
    //     int CachedMyLadderLength(string beginWord, string endWord, IList<string> wordList)
    //     {
    //         var k = $"{beginWord}|{wordList.Count}";
    //         // var k = (beginWord, wordList.Count);
    //         // var k = string.Join('|', wordList);
    //         if (!cache.ContainsKey(k))
    //         {
    //             var len = wordListCount - wordList.Count + 1;
    //             if (len >= minLen)
    //             {
    //                 // Console.WriteLine($"{len}>{minLen}");
    //                 cache[k] = 0;
    //                 return 0;
    //             }
    //             cache[k] = MyLadderLength(beginWord, endWord, wordList);
    //             if (cache[k] > 0)
    //             {
    //                 len = wordListCount - wordList.Count + 1;
    //                 len += cache[k];
    //                 if (len < minLen)
    //                     minLen = len;
    //             }
    //         }
    //         return cache[k];
    //     }
    //     int MyLadderLength(string beginWord, string endWord, IList<string> wordList)
    //     {

    //         int Diff(string w1, string w2)
    //         {
    //             var d = 0;
    //             for (var i = 0; i < w1.Length; i++)
    //             {
    //                 if (w1[i] != w2[i]) d++;
    //             }
    //             return d;
    //         }

    //         var positive = new List<int>();
    //         foreach (var w in wordList)
    //         // for (var i = 0; i < wordList.Count; i++)
    //         {
    //             // var w = wordList[i];
    //             // if (w == "-") continue;
    //             var diff = Diff(beginWord, w);
    //             if (diff == 1)
    //             {
    //                 if (w == endWord) return 2;

    //                 var wordList2 = wordList.Where(i => i != w).ToList();
    //                 var x = CachedMyLadderLength(w, endWord, wordList2);
    //                 // var x = CachedMyLadderLength(w, endWord, wordList);
    //                 if (x > 0) positive.Add(x);
    //             }
    //         }
    //         if (positive.Count == 0) return 0;
    //         return 1 + positive.Min();
    //     }
    // }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
";
            var lines = input.CleanInput();
            lines = "0127-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}