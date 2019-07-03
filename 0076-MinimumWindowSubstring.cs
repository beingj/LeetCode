using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MinimumWindowSubstring
{
    public class Solution
    {
        public string MinWindow(string s, string t)
        {
            // give up
            // copy from: https://leetcode.com/problems/minimum-window-substring/discuss/26808/Here-is-a-10-line-template-that-can-solve-most-'substring'-problems/25848
            // explain: https://leetcode.com/problems/minimum-window-substring/discuss/26808/Here-is-a-10-line-template-that-can-solve-most-'substring'-problems/25861
            var map = new Dictionary<char, int>();
            foreach (char c in s)
                map[c] = 0;
            foreach (char c in t)
            {
                if (map.ContainsKey(c))
                    map[c]++;
                else
                    return ""; // c in t but not exist in s, no match
            }

            int start = 0, end = 0, minStart = 0, minLen = int.MaxValue, counter = t.Length;
            while (end < s.Length)
            {
                char c1 = s[end];
                if (map[c1] > 0)
                    counter--;

                map[c1]--;

                end++;

                while (counter == 0)
                {
                    if (minLen > end - start)
                    {
                        minLen = end - start;
                        minStart = start;
                    }

                    char c2 = s[start];
                    map[c2]++;

                    if (map[c2] > 0)
                        counter++;

                    start++;
                }
            }
            return minLen == int.MaxValue ? "" : s.Substring(minStart, minLen);
        }
        // public string MinWindow4(string s, string t)
        // {
        //     if (t.Length > s.Length)
        //     {
        //         return "";
        //     }
        //     // if (t == s)
        //     // {
        //     //     return s;
        //     // }
        //     var tCntDict = new Dictionary<char, int>();
        //     var mIdxDict = new Dictionary<char, List<int>>();
        //     foreach (var i in t)
        //     {
        //         if (!tCntDict.ContainsKey(i))
        //         {
        //             tCntDict[i] = 0;
        //             mIdxDict[i] = new List<int>();
        //         }
        //         tCntDict[i]++;
        //     }

        //     var subs = new List<Tuple<int, int>>();
        //     var idxList = new List<char>();
        //     for (var idx = 0; idx < s.Length; idx++)
        //     {
        //         var c = s[idx];
        //         if (tCntDict.ContainsKey(c))
        //         {
        //             mIdxDict[c].Add(idx);
        //             idxList.Add(c);
        //             if (idxList.All(n => n == c))
        //             {
        //                 var x = idxList.Count - tCntDict[c];
        //                 if (x > 0)
        //                 {
        //                     mIdxDict[c].RemoveRange(0, x);
        //                     idxList.RemoveRange(0, x);
        //                 }
        //             }
        //             if (idxList.Count >= t.Length)
        //             {
        //                 var ok = true;
        //                 foreach (var i in idxList)
        //                 {
        //                     if (mIdxDict[i].Count < tCntDict[i])
        //                     {
        //                         ok = false;
        //                     }
        //                 }
        //                 if (ok)
        //                 {
        //                     while (idxList.Count > t.Length)
        //                     {
        //                         var c2 = idxList.First();
        //                         if (mIdxDict[c2].Count > tCntDict[c2])
        //                         {
        //                             idxList.RemoveAt(0);
        //                             mIdxDict[c2].RemoveAt(0);
        //                         }
        //                         else
        //                         {
        //                             break;
        //                         }
        //                     }
        //                     subs.Add(new Tuple<int, int>(mIdxDict[idxList.First()].First(), idx));
        //                 }
        //             }
        //         }
        //     }
        //     if (subs.Count == 0)
        //     {
        //         return "";
        //     }
        //     var range = subs.OrderBy(i => i.Item2 - i.Item1).First();
        //     string buf = s.Substring(range.Item1, range.Item2 - range.Item1 + 1);
        //     return buf;
        // }
        // public string MinWindow3(string s, string t)
        // {
        //     var tCount = new Dictionary<char, int>();
        //     var idxList = new Dictionary<char, List<int>>();
        //     foreach (var i in t)
        //     {
        //         if (!tCount.ContainsKey(i))
        //         {
        //             tCount[i] = 0;
        //             idxList[i] = new List<int>();
        //         }
        //         tCount[i]++;
        //     }
        //     var tTotal = t.Length;
        //     var matchTotal = 0;

        //     var subs = new List<Tuple<int, int>>();
        //     // int startIdx = 0;
        //     var idxAll = new List<char>();
        //     for (var idx = 0; idx < s.Length; idx++)
        //     {
        //         var c = s[idx];
        //         if (tCount.ContainsKey(c))
        //         {
        //             // if ((idxAll.Count == tCount[c]) && (idxList[c].Count == tCount[c]))
        //             // {
        //             //     idxAll.RemoveAt(0);
        //             //     idxList[c].RemoveAt(0);
        //             //     matchTotal--;
        //             //     // if (matchTotal == tTotal)
        //             //     // {
        //             //     //     subs.Add(new Tuple<int, int>(idxList[idxAll.First()].First(), idx));
        //             //     // }
        //             // }
        //             // if (matchTotal == 0)
        //             // {
        //             //     startIdx = idx;
        //             // }
        //             idxList[c].Add(idx);
        //             idxAll.Add(c);
        //             if (idxList[c].Count <= tCount[c])
        //             {
        //                 matchTotal++;
        //             }
        //             else
        //             {
        //                 // if (idxAll.First() == c)
        //                 // {
        //                 //     idxAll.RemoveAt(0);
        //                 //     idxList[c].RemoveAt(0);
        //                 //     matchTotal--;
        //                 //     // if (matchTotal == tTotal)
        //                 //     // {
        //                 //     subs.Add(new Tuple<int, int>(idxList[idxAll.First()].First(), idx));
        //                 //     // }
        //                 // }
        //                 if (idxAll.All(n => n == c))
        //                 {
        //                     // startIdx = idx;
        //                     idxAll.Clear();
        //                     idxAll.Add(c);
        //                     matchTotal = 1;
        //                     idxList[c].Clear();
        //                     idxList[c].Add(idx);
        //                 }
        //                 var i = idxAll.IndexOf(c);
        //                 if (idxAll.Count - (i + 1) == tTotal)
        //                 {
        //                     // remove till i and save
        //                     foreach (var j in idxAll.Take(i + 1))
        //                     {
        //                         idxList[j].RemoveAt(0);
        //                         matchTotal--;
        //                     }
        //                     idxAll.RemoveRange(0, i + 1);
        //                     subs.Add(new Tuple<int, int>(idxList[idxAll.First()].First(), idx));
        //                 }
        //             }
        //             if (matchTotal == tTotal)
        //             {
        //                 // if((idx-startIdx+1)<minLen)
        //                 // subs.Add(new Tuple<int, int>(startIdx, idx));
        //                 subs.Add(new Tuple<int, int>(idxList[idxAll.First()].First(), idx));
        //             }
        //         }
        //     }
        //     if (subs.Count == 0)
        //     {
        //         return "";
        //     }
        //     var range = subs.OrderBy(i => i.Item2 - i.Item1).First();
        //     string buf = s.Substring(range.Item1, range.Item2 - range.Item1 + 1);
        //     return buf;
        // }
        // public string MinWindow2(string s, string t)
        // {
        //     if (t.Length > s.Length)
        //     {
        //         return "";
        //     }
        //     // Console.WriteLine($"s.len {s.Length} vs t.len {t.Length}");
        //     if (t.Length == s.Length)
        //     {
        //         if (t == s)
        //         {
        //             return s;
        //         }
        //         var x = new Dictionary<char, int>();
        //         foreach (var i in s)
        //         {
        //             if (!x.ContainsKey(i))
        //             {
        //                 x[i] = 0;
        //             }
        //             x[i]++;
        //         }
        //         var y = new Dictionary<char, int>();
        //         foreach (var i in t)
        //         {
        //             if (!y.ContainsKey(i))
        //             {
        //                 y[i] = 0;
        //             }
        //             y[i]++;
        //         }
        //         foreach (var i in y.Keys)
        //         {
        //             if (!x.ContainsKey(i))
        //             {
        //                 return "";
        //             }
        //             if (x[i] != y[i])
        //             {
        //                 return "";
        //             }
        //         }
        //         // Console.WriteLine("count match");
        //         return s;
        //     }
        //     var count0 = new Dictionary<char, int>();
        //     foreach (var c in t)
        //     {
        //         if (!count0.ContainsKey(c))
        //         {
        //             count0[c] = 0;
        //         }
        //         count0[c]++;
        //     }
        //     var count = new Dictionary<char, int>();
        //     foreach (var k in count0.Keys)
        //     {
        //         count[k] = count0[k];
        //     }
        //     var countTotal = t.Length;

        //     var res = new List<string>();
        //     var buf = new List<char>();
        //     // var tt = t.ToList();
        //     foreach (var c in s)
        //     {
        //         // var i = tt.IndexOf(c);
        //         // if (i >= 0)
        //         // {
        //         //     tt.RemoveAt(i);
        //         // }
        //         if (count.ContainsKey(c))
        //         {
        //             // tt.Remove(c);
        //             if (count[c] > 0)
        //             {
        //                 count[c]--;
        //                 countTotal--;
        //             }
        //         }

        //         // if (tt.Count < t.Length)
        //         if (countTotal < t.Length)
        //         {
        //             buf.Add(c);
        //         }

        //         // if (tt.Count == 0)
        //         if (countTotal == 0)
        //         {
        //             res.Add(new string(buf.ToArray()));
        //             var cnt = new Dictionary<char, int>();
        //             foreach (var c3 in buf)
        //             {
        //                 if (count0.ContainsKey(c3))
        //                 {
        //                     if (!cnt.ContainsKey(c3))
        //                     {
        //                         cnt[c3] = 0;
        //                     }
        //                     cnt[c3]++;
        //                 }
        //             }

        //             while (true)
        //             {
        //                 var c2 = buf[0];
        //                 // if (count.ContainsKey(c2) && (cnt[c2] > count[c2]))
        //                 if (count0.ContainsKey(c2) && (cnt[c2] > count0[c2]))
        //                 {
        //                     cnt[c2]--;
        //                     buf.RemoveAt(0);
        //                     while (true)
        //                     {
        //                         if (t.Contains(buf[0]))
        //                         {
        //                             res.Add(new string(buf.ToArray()));
        //                             break;
        //                         }
        //                         buf.RemoveAt(0);
        //                     }
        //                 }
        //                 else
        //                 {
        //                     break;
        //                 }
        //             }
        //             // tt.Add(buf[0]);
        //             count[buf[0]]++;
        //             countTotal++;

        //             while (true)
        //             {
        //                 buf.RemoveAt(0);
        //                 if (buf.Count == 0)
        //                 {
        //                     break;
        //                 }
        //                 if (t.Contains(buf[0]))
        //                 {
        //                     break;
        //                 }
        //             }
        //         }
        //     }
        //     int minSub = s.Length;
        //     string sub = "";
        //     foreach (var i in res)
        //     {
        //         if (i.Length <= minSub)
        //         {
        //             sub = i;
        //             minSub = i.Length;
        //         }
        //     }
        //     return sub;
        // }
        // public string MinWindow1(string s, string t)
        // {
        //     if (t.Length > s.Length)
        //     {
        //         return "";
        //     }
        //     var idxList = new Dictionary<char, List<int>>();
        //     foreach (var c in t)
        //     {
        //         idxList[c] = new List<int>();
        //     }

        //     int idx = 0;
        //     while (idx < s.Length)
        //     {
        //         if (idxList.ContainsKey(s[idx]))
        //         {
        //             idxList[s[idx]].Add(idx);
        //             if (idxList[s[idx]].Count > 1)
        //             {
        //                 foreach (var j in idxList)
        //                 {
        //                     while (j.Value.Count > 1)
        //                     {
        //                         if (j.Value.First() < idx)
        //                         {
        //                             j.Value.RemoveAt(0);
        //                         }
        //                     }
        //                 }
        //             }
        //         }
        //         idx++;
        //     }

        //     if (idxList.Any(i => i.Value.Count == 0))
        //     {
        //         return "";
        //     }
        //     var start = idxList.Select(i => i.Value.First()).Min();
        //     var end = idxList.Select(i => i.Value.Last()).Max();
        //     return s.Substring(start, end - start + 1);
        // }
    }
    public class Test
    {
        static void Verify(string s, string t, string exp)
        {
            Console.WriteLine($"{s.Substring(0, Math.Min(s.Length, 10))}, {t.Substring(0, Math.Min(t.Length, 10))}");
            string res;
            using (new Timeit())
            {
                res = new Solution().MinWindow(s, t);
            }
            Assert.Equal(exp, res);
        }
        static public void Run()
        {
            Console.WriteLine(typeof(Solution).Namespace);

            var input = @"
ADOBECODEBANC
ABC
BANC
a
aa
''
aa
aa
aa
abc
ac
abc
bba
ab
ba
acbbaca
aba
baca
cabwefgewcwaefgcf
cae
cwae
abc
cba
abc
";
            var lines = input.CleanInput();
            // lines = "0076-data.txt".InputFromFile();
            string s, t, exp;
            int idx = 0;
            while (idx < lines.Length)
            {
                s = lines[idx++];
                t = lines[idx++];
                exp = lines[idx++];
                if (exp == "''")
                {
                    exp = "";
                }
                Verify(s, t, exp);
            }
        }
    }
}