using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Util
{
    // Definition for singly-linked list.
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
        public static ListNode FromList(int[] lst)
        {
            var l1 = new ListNode(lst[0]);
            ListNode current = l1;
            for (var i = 1; i < lst.Length; i++)
            {
                current.next = new ListNode(lst[i]);
                current = current.next;
            }
            return l1;
        }
        public static ListNode FromList(string s)
        {
            // 1->2->3->4->5->NULL
            var lst0 = s.Split("->")
                        .Select(z => z.Trim(new char[] { ' ', '\t' }))
                        .Where(x => x.Length > 0);
            var lst = lst0.Where(x => x != "NULL").Select(y => int.Parse(y)).ToArray();
            if (lst.Length == 0)
            {
                return null;
            }
            return FromList(lst);
        }
        public override string ToString()
        {
            List<int> s = new List<int>();
            ListNode node = this;
            while (true)
            {
                s.Add(node.val);
                if (node.next == null) break;
                node = node.next;
            }
            return string.Join(',', s);
        }
        public string ToInt()
        {
            List<int> lst = new List<int>();
            ListNode node = this;
            while (true)
            {
                lst.Add(node.val);
                if (node.next == null) break;
                node = node.next;
            }
            lst.Reverse();
            return string.Join("", lst);
        }
        public static ListNode FromInt(ulong x)
        {
            var l1 = new ListNode((int)(x % 10));
            ListNode node = l1;
            while (true)
            {
                if (x < 10) break;
                x /= 10;
                node.next = new ListNode((int)(x % 10));
                node = node.next;
            }
            return l1;
        }
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return this.ToString() == ((ListNode)obj).ToString();
            }
        }
        public override int GetHashCode()
        {
            // warning CS0659: '“ListNode”重写 Object.Equals(
            // object o) 但不重写 Object.GetHashCode()

            // https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=netframework-4.8
            // return this.ToString().GetHashCode();
            return this.GetHashCode();
        }
    }
    static class Ext
    {
        public static string P(this int[] nums, string sep = ",")
        {
            return string.Join(sep, nums);
        }

        public static string P(this int[][] nums, string sep1 = ",", string sep2 = "\n")
        {
            if (nums.Length < 10)
            {
                return string.Join(sep2, nums.Select(x => string.Join(sep1, x)));
            }
            return string.Format("{0} ...", string.Join(sep2, nums.Take(10).Select(x => string.Join(sep1, x))));
        }
        public static string[] InputFromFile(this string fn)
        {
            string input;
            using (var fs = File.OpenText(Path.Join(Directory.GetCurrentDirectory(), fn)))
            {
                input = fs.ReadToEnd();
            }
            return input.CleanInput();
        }
        public static string[] CleanInput(this string input)
        {
            return input.Trim(new char[] { '\n', '\r', ' ' })
                        .Split('\n')
                        .Select(x => x.Trim(new char[] { '\r', ' ' }))
                        .Where(y => !y.StartsWith('#'))
                        .ToArray();
        }
        public static string[] JsonToStr1d(this string s)
        {
            return s.TrimStart('[').TrimEnd(']')
                    .Split(',')
                    .Select(x => x.Trim().Trim('"'))
                    .ToArray();
        }
        public static string Str1dToJson(this int[] a)
        {
            return string.Format("[{0}]", string.Join(',', a.Select(i => $"\"{i}\"")));
        }
        public static int[] JsonToInt1d(this string s)
        {
            return s.TrimStart('[').TrimEnd(']')
                    .Split(',')
                    .Select(y => int.Parse(y))
                    .ToArray();
        }
        public static string Int1dToJson(this int[] a)
        {
            return string.Format("[{0}]", string.Join(',', a));
        }
        public static int[][] JsonToInt2d(this string s)
        {
            return s.TrimStart(new char[] { '[', ' ' }).TrimEnd(new char[] { ']', ' ' })
                    .Split("],")
                    .Select(x =>
                                x.TrimStart(new char[] { '[', ' ' }).TrimEnd(new char[] { ']', ' ' })
                                .Split(',')
                                .Where(z => z.Length > 0)
                                .Select(y => int.Parse(y))
                                .ToArray())
                    .ToArray();
        }
        public static string Int2dToJson(this int[][] a)
        {
            return string.Format("[{0}]", string.Join(",", a.Select(x =>
                        string.Format("[{0}]", string.Join(',', x)))));
        }
        public static char[][] JsonToChar2d(this string s)
        {
            return s.TrimStart(new char[] { '[', ' ' }).TrimEnd(new char[] { ']', ' ' })
                    .Split("],")
                    .Select(x =>
                                x.TrimStart(new char[] { '[', ' ' }).TrimEnd(new char[] { ']', ' ' })
                                .Trim('\'')
                                .Split(',')
                                .Where(z => z.Length > 0)
                                .Select(y => char.Parse(y.Trim('\'')))
                                .ToArray())
                    .ToArray();
        }
        public static string Char2dToJson(this char[][] a)
        {
            return string.Format("[{0}]", string.Join(",", a.Select(x =>
                        string.Format("['{0}']", string.Join("\',\'", x)))));
        }
        public static ListNode JsonToListNode(this string s)
        {
            var q = s.TrimStart(new char[] { '[', ' ' }).TrimEnd(new char[] { ']', ' ' })
                    .Split(",")
                    .Select(x => x.Trim());
            return ListNode.FromList(string.Join("->", q));
        }
        public static string ListNodeToJson(this ListNode ln)
        {
            var s = string.Join(',', ln.ToString().Split("->"));
            return string.Format("[{0}]", s);
        }
    }
    public class Timeit : IDisposable
    {
        private Stopwatch sw;
        public Timeit()
        {
            //First Create the instance of Stopwatch Class
            sw = new Stopwatch();
            // Start The StopWatch ...From 000
            sw.Start();
        }

        public void Dispose()
        {
            //Stop the Timer
            sw.Stop();
            string ExecutionTimeTaken = string.Format("{0}s {1}ms", sw.Elapsed.Seconds, sw.Elapsed.TotalMilliseconds);
            Console.WriteLine(ExecutionTimeTaken);
            //    sw.Restart();
        }
    }
    public class Permutation
    {
        // var x = "abc";
        // var x = new List<string> { "a", "b", "c" };
        // var exp = "abc,acb,bac,bca,cab,cba";
        // var y = PermutationWords(x);
        // var z = string.Join(',', y.Select(i=>string.Join("",i)).ToList());
        // Console.WriteLine($"{string.Join(',',x)}=>{exp}");
        // Console.WriteLine($"{string.Join(',',x)}=>{z}");
        public static List<List<string>> PermutationWords(List<string> words)
        {
            var ints = new List<int>();
            for (var i = 0; i < words.Count; i++)
            {
                ints.Add(i);
            }
            var idxsCombList = PermutationInts(ints, 0);
            var res = new List<List<string>>();
            foreach (var idxsComb in idxsCombList)
            {
                res.Add(idxsComb.Select(n => words[n]).ToList());
            }
            return res;
        }
        public static List<List<int>> PermutationInts(List<int> ints, int startIdx)
        {
            if (startIdx == ints.Count - 1)
            {
                return new List<List<int>> { ints };
            }
            var res = new List<List<int>>();
            for (var i = startIdx; i < ints.Count; i++)
            {
                if (IsDuplicated(ints, startIdx, i))
                {
                    continue;
                }
                var ints2 = SwappedInts(ints, startIdx, i);
                res.AddRange(PermutationInts(ints2, startIdx + 1));
            }
            return res;
        }
        public static List<int> SwappedInts(List<int> ints, int a, int b)
        {
            var res = new List<int>(ints);
            int x = res[a];
            res[a] = res[b];
            res[b] = x;
            return res;
        }
        static bool IsDuplicated(List<int> ints, int startIdx, int endIdx)
        {
            int x = ints[endIdx];
            for (var i = startIdx; i < endIdx; i++)
            {
                if (ints[i] == x)
                    return true;
            }
            return false;
        }
    }
}