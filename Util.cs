using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

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
        public static IEnumerable<List<dynamic>> ParseType(this string[] lines, InputConverterList parser)
        {
            int idx = 0;
            while (idx < lines.Length)
            {
                var tvs = new List<dynamic>();
                foreach (var p in parser)
                {
                    var t = p.type;
                    var v = p.converter(lines[idx++]);
                    dynamic tv = Convert.ChangeType(v, t);
                    tvs.Add(tv);
                }
                yield return tvs;
            }
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
        public static char[][] JsonToChar2d(this string s, char quote = '\'')
        {
            return s.TrimStart(new char[] { '[', ' ' }).TrimEnd(new char[] { ']', ' ' })
                    .Split("],")
                    .Select(x =>
                                x.TrimStart(new char[] { '[', ' ' }).TrimEnd(new char[] { ']', ' ' })
                                .Split(',')
                                .Where(z => z.Length > 0)
                                .Select(y => char.Parse(y.Trim(quote)))
                                .ToArray())
                    .ToArray();
        }
        public static string Char2dToJson(this char[][] a, char quote = '\'')
        {
            return string.Format("[{0}]", string.Join(",", a.Select(x =>
                        string.Format("[{1}{0}{1}]", string.Join(string.Format("{0},{0}", quote), x), quote))));
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
    public class InputConverter
    {
        public Type type;
        public Func<string, object> converter;
        public InputConverter() { }
        public InputConverter(Type t)
        {
            type = t;
            converter = ToType(t);
        }
        public InputConverter(Type t, Func<string, object> f)
        {
            type = t;
            converter = f;
        }
        public Func<string, dynamic> ToType(Type t)
        {
            Func<string, dynamic> f = (x) => x;
            if (t == typeof(int))
            {
                f = x => int.Parse(x);
            }
            else if (t == typeof(ListNode))
            {
                f = x => x.JsonToListNode();
            }
            else if (t == typeof(int[]))
            {
                f = x => x.JsonToInt1d();
            }
            else if (t == typeof(int[][]))
            {
                f = x => x.JsonToInt2d();
            }
            else if (t == typeof(char[][]))
            {
                f = x => x.JsonToChar2d();
            }
            else if (t == typeof(string[]))
            {
                f = x => x.JsonToStr1d();
            }
            return f;
        }
    }
    public class InputConverterList : List<InputConverter>
    {
        public InputConverterList(List<Type> ts)
        {
            ts.ForEach(t => Add(t));
        }

        // https://stackoverflow.com/questions/9194363/using-collection-initializer-syntax-on-custom-types
        public void Add(Type t, Func<string, object> f)
        {
            // InputConverter item = new InputConverter(t, f);
            InputConverter item = new InputConverter { type = t, converter = f };
            Add(item);
        }
        public void Add(Type t)
        {
            InputConverter item = new InputConverter(t);
            Add(item);
        }
    }
    public class Method
    {
        public static MethodInfo[] GetMethodsFromType(Type t, BindingFlags flag = BindingFlags.Public | BindingFlags.Instance, bool userDefined = true)
        {
            if (userDefined)
            {
                var defaultMethods = new List<string>{
                    "ToString", "Equals", "GetHashCode", "GetType"
                };
                return t.GetMethods(flag).Where(i => !defaultMethods.Contains(i.Name)).ToArray();
            }
            return t.GetMethods(flag);
        }
        public static MethodInfo GetInfo(string t, string m)
        {
            // var m = Method.GetInfo("PartitionList.Solution", "Partition");
            var typ = Type.GetType(t);
            return typ.GetMethod(m);
        }
        public static List<Type> GetParametersAndReturnTypes(MethodInfo m)
        {
            var tlist = new List<Type>();
            tlist.AddRange(m.GetParameters().Select(i => i.ParameterType));
            tlist.Add(m.ReturnType);
            return tlist;
        }
        public static List<Type> GetReturnAndParametersTypes(MethodInfo m)
        {
            var tlist = new List<Type>();
            tlist.Add(m.ReturnType);
            tlist.AddRange(m.GetParameters().Select(i => i.ParameterType));
            return tlist;
        }
    }
    public class Verify
    {
        public static void Method(dynamic obj, string[] lines, int checkParaIndex = -1, bool truncate = false)
        {
            dynamic para = null, paraExpS = null, paraExpV = null, paraExpT = null;
            MethodInfo method = Util.Method.GetMethodsFromType(obj.GetType())[0];
            var inputTypes = Util.Method.GetParametersAndReturnTypes(method);
            var signature = string.Format("{0}.{1}({2}) => {3}",
                            obj.GetType().Namespace,
                            method.Name,
                            string.Join(", ", inputTypes.SkipLast(1)),
                            inputTypes.Last());
            Console.WriteLine(signature);
            Type expType = inputTypes.Last();
            if (checkParaIndex >= 0)
            {
                paraExpT = inputTypes[checkParaIndex];
                inputTypes.Add(paraExpT);
                Console.WriteLine($"And in-place change parameter {checkParaIndex}: {paraExpT}");
            }

            var inputParser = new InputConverterList(inputTypes);
            var maxChars = 100;
            var maxCharsEach = maxChars / inputTypes.Count;

            int idx = 0;
            while (idx < lines.Length)
            {
                var ss = new List<string>();
                var vs = new List<dynamic>();
                foreach (var p in inputParser)
                {
                    var s = lines[idx++];
                    ss.Add(s.Substring(0, Math.Min(s.Length, maxCharsEach)));
                    dynamic tv = Convert.ChangeType(p.converter(s), p.type);
                    vs.Add(tv);
                }

                if (checkParaIndex >= 0)
                {
                    para = vs[checkParaIndex];
                    paraExpS = ss.Last();
                    ss.RemoveAt(ss.Count - 1);
                    paraExpV = vs.Last();
                    vs.RemoveAt(vs.Count - 1);
                    Console.WriteLine(string.Format("{0,-50} => {1} | {2}", string.Join(" | ", ss.SkipLast(1)), ss.Last(), paraExpS));
                }
                else
                {
                    Console.WriteLine(string.Format("{0,-50} => {1}", string.Join(" | ", ss.SkipLast(1)), ss.Last()));
                }
                dynamic res;
                using (new Timeit())
                {
                    res = method.Invoke(obj, vs.SkipLast(1).ToArray());
                }
                // TODO: how about return void?
                // Assert.Equal(exp, res);
                Assert.Equal(vs.Last(), res);

                if (checkParaIndex >= 0)
                {
                    if (truncate)
                    {
                        // https://github.com/dotnet/coreclr/issues/15186
                        // The dynamic binder does not—and should not—consider extension method syntax in deciding which overload to bind to.
                        // You should use the static method call syntax of TaskExtensions.TaskExtension(DoSomething(thing)); instead.
                        para = Enumerable.Take(para, res);
                    }
                    Assert.Equal(paraExpV, para);
                }
            }
        }
        public static void Function(string inputToString, Func<dynamic> func, dynamic exp)
        {
            Console.WriteLine(inputToString);
            dynamic res;
            using (new Timeit())
            {
                res = func();
            }
            Assert.Equal(exp, res);
        }
        public static void Input(string[] lines,
                                InputConverterList inputParser,
                                Func<List<dynamic>, string> inputFormatter,
                                Func<List<dynamic>, Func<dynamic>> funcConverter
                                )
        {
            foreach (var paras in lines.ParseType(inputParser))
            {
                Verify.Function(inputFormatter(paras), funcConverter(paras), paras.Last());
            }
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