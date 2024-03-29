using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Util
{
    // Definition for singly-linked list.
    public class ListNode
    {
        static Random rnd = new Random();
        int id = rnd.Next(int.MinValue, int.MaxValue);
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
            var node = this;
            var ids = new List<int>();
            var pos = -1;
            while (node != null)
            {
                pos = ids.IndexOf(node.id);
                if (pos > -1)
                    break;
                s.Add(node.val);
                ids.Add(node.id);
                node = node.next;
            }
            if (pos < 0)
                return string.Join(',', s);
            else
                return string.Format("{0}->{1}", string.Join(',', s), pos);
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
            return id;
        }
    }
    public class TreeNode
    {
        static Random rnd = new Random();
        int id = rnd.Next(int.MinValue, int.MaxValue);
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
        public static TreeNode FromDLRSeq(IEnumerable<string> nodesSeq, TreeNode parent = null, List<TreeNode> ancestors = null)
        {
            if (nodesSeq.Count() == 0)
            {
                return null;
            }
            if (parent == null)
            {
                var x = nodesSeq.First();
                nodesSeq = nodesSeq.Skip(1);
                parent = new TreeNode(int.Parse(x));
            }
            if (ancestors == null)
            {
                ancestors = new List<TreeNode>();
            }

            var child = nodesSeq.First();
            nodesSeq = nodesSeq.Skip(1);
            if (child != "null")
            {
                var n = new TreeNode(int.Parse(child));
                if (parent.left == null)
                {
                    parent.left = n;
                }
                else
                {
                    parent.right = n;
                }
                // parent.left = n;
                var ancestors2 = new List<TreeNode>();
                ancestors2.AddRange(ancestors);
                ancestors2.Add(parent);
                FromDLRSeq(nodesSeq, n, ancestors2);
            }
            else
            {
                // left is null
                child = nodesSeq.First();
                nodesSeq = nodesSeq.Skip(1);
                if (child != "null")
                {
                    var n = new TreeNode(int.Parse(child));
                    if (parent.right == null)
                    {
                        parent.right = n;
                        var ancestors2 = new List<TreeNode>();
                        ancestors2.AddRange(ancestors);
                        ancestors2.Add(parent);
                        FromDLRSeq(nodesSeq, n, ancestors2);
                    }
                    else
                    {
                        // var ancestor = ancestors.Last();
                        // ancestors.RemoveAt(ancestors.Count - 1);
                        // FromDLRSeq(nodesSeq, ancestor, ancestors);
                    }
                }
                else
                {
                    // right is null
                    var ancestor = ancestors.Last();
                    while (ancestors.Count > 0)
                    {
                        if (ancestor.right == null)
                        {
                            break;
                        }
                        ancestor = ancestors.Last();
                        ancestors.RemoveAt(ancestors.Count - 1);
                    }
                    FromDLRSeq(nodesSeq, ancestor, ancestors);
                }
            }

            return parent;
        }
        public List<string> DLR(List<string> res = null)
        {
            return DLR(this);
        }
        // https://zhidao.baidu.com/question/682276251728268972.html
        /*先根递归遍历*/
        public static List<string> DLR(TreeNode tn, List<string> res = null)
        {
            if (res == null)
            {
                res = new List<string>();
            }
            if (tn != null)
            {
                res.Add($"{tn.val}");
                if ((tn.left != null) ||
                    ((tn.left == null) && (tn.right != null)))
                    DLR(tn.left, res);
                if (tn.right != null) DLR(tn.right, res);
            }
            else
            {
                res.Add("null");
            }
            return res;
        }
        /*中根递归遍历*/
        public static IList<int> LDR(TreeNode tn, IList<int> res = null)
        {
            if (res == null)
            {
                res = new List<int>();
            }
            if (tn != null)
            {
                if ((tn.left != null) ||
                    ((tn.left == null) && (tn.right != null)))
                    LDR(tn.left, res);
                res.Add(tn.val);
                if (tn.right != null) LDR(tn.right, res);
            }
            return res;
        }
        /*后根递归遍历*/
        public static List<string> LRD(TreeNode tn, List<string> res = null)
        {
            if (res == null)
            {
                res = new List<string>();
            }
            if (tn != null)
            {
                if ((tn.left != null) ||
                    ((tn.left == null) && (tn.right != null)))
                    LRD(tn.left, res);
                if ((tn.right != null) || (tn.left != null))
                    LRD(tn.right, res);
                res.Add($"{tn.val}");
            }
            else
            {
                res.Add("null");
            }
            return res;
        }
        static TreeNode FromStrByLevel(List<string> seq)
        {
            if (seq.Count == 0)
            {
                return null;
            }
            if (seq.First().Trim() == "")
                return null;

            var root = new TreeNode(int.Parse(seq.First()));
            var pathDict = new Dictionary<string, TreeNode>();
            pathDict[""] = root;
            var pathOfLvl = new List<string> { "" };
            int idx = 1;
            while (idx < seq.Count)
            {
                var pathOfLvl2 = new List<string>();
                string childVal = "null";
                string childPath;
                TreeNode childNode;
                foreach (var parentPath in pathOfLvl)
                {
                    if (idx >= seq.Count)
                    {
                        break;
                    }
                    childVal = seq[idx++];
                    if (childVal != "null")
                    {
                        childPath = string.Format("{0}L", parentPath);
                        pathOfLvl2.Add(childPath);
                        childNode = new TreeNode(int.Parse(childVal));
                        pathDict[parentPath].left = childNode;
                        pathDict[childPath] = childNode;
                    }
                    if (idx >= seq.Count)
                    {
                        break;
                    }
                    childVal = seq[idx++];
                    if (childVal != "null")
                    {
                        childPath = string.Format("{0}R", parentPath);
                        pathOfLvl2.Add(childPath);
                        childNode = new TreeNode(int.Parse(childVal));
                        pathDict[parentPath].right = childNode;
                        pathDict[childPath] = childNode;
                    }
                }
                pathOfLvl = pathOfLvl2;
            }
            return root;
        }
        public static TreeNode FromStr(string s)
        {
            var seq = s.Trim().TrimStart('[').TrimEnd(']')
                        .Split(',')
                        .Select(i => i.Trim()).ToList();
            return FromStrByLevel(seq);
        }
        public override string ToString()
        {
            // return string.Format("[{0}]", string.Join(',', DLR()));
            // return string.Format("[{0}]", string.Join(',', DLRWithLevel(this)));
            // var n = "[1,null,2,3]".JsonToTreeNode();
            // Console.WriteLine(n);
            // Console.WriteLine(n);
            // Console.WriteLine(n.ToStr(5, 3));
            // Console.WriteLine(n.ToStr(5, 3, true));
            // n = "[1,2,3,4,5]".JsonToTreeNode();
            // Console.WriteLine(n);
            return ToStr();
        }
        static Dictionary<string, TreeNode> PathByLevel(TreeNode root)
        {
            var pathDict = new Dictionary<string, TreeNode>();
            if (root == null)
            {
                return pathDict;
            }
            var node = root;
            pathDict[""] = root;
            var pathOfLvl = new List<string> { "" };
            // var pathOfAll = new List<List<string>>();
            // pathOfAll.Add(pathOfLvl);

            while (true)
            {
                var pathOfLvl2 = new List<string>();
                int nodeOflvl = 0;
                foreach (var parentPath in pathOfLvl)
                {
                    node = pathDict[parentPath];
                    if (node.left != null)
                    {
                        var p = $"{parentPath}L";
                        pathDict[p] = node.left;
                        pathOfLvl2.Add(p);
                        nodeOflvl++;
                    }
                    if (node.right != null)
                    {
                        var p = $"{parentPath}R";
                        pathDict[p] = node.right;
                        pathOfLvl2.Add(p);
                        nodeOflvl++;
                    }
                }
                if (nodeOflvl == 0)
                    break;
                pathOfLvl = pathOfLvl2;
                // pathOfAll.Add(pathOfLvl);
            }

            // var lst = new List<string>();
            // foreach (var lvl in pathOfAll)
            // {
            //     // Console.WriteLine(string.Join(" - ", lvl.Select(i => pathDict[i].val)));
            //     lst.Add(string.Join(" - ", lvl.Select(i => string.Format("{0}{1}", i.EndsWith("L") ? "/" : "\\", pathDict[i].val))));
            // }
            // return string.Join('\n', lst);
            return pathDict;
        }
        public string ToStrByLevel(bool ignoreVal = false)
        {
            var res = new List<string>();
            var pathDict = PathByLevel(this);

            var placeHolder = "1";
            void AddVal(TreeNode n)
            {
                var val = ignoreVal ? placeHolder : $"{n.val}";
                res.Add(val);
            }
            AddVal(pathDict[""]);

            var pathOfLvl = new List<string> { "" };
            while (true)
            {
                var pathOfLvl2 = new List<string>();
                int nodeOflvl = 0;
                foreach (var parentPath in pathOfLvl)
                {
                    var node = pathDict[parentPath];
                    if (node.left != null)
                    {
                        var p = $"{parentPath}L";
                        pathDict[p] = node.left;
                        pathOfLvl2.Add(p);
                        AddVal(node.left);
                        nodeOflvl++;
                    }
                    else
                    {
                        res.Add("null");
                    }

                    if (node.right != null)
                    {
                        var p = $"{parentPath}R";
                        pathDict[p] = node.right;
                        pathOfLvl2.Add(p);
                        AddVal(node.right);
                        nodeOflvl++;
                    }
                    else
                    {
                        res.Add("null");
                    }
                }
                if (nodeOflvl == 0)
                    break;
                pathOfLvl = pathOfLvl2;
            }
            // remove extra nulls in tail
            while (res.Last() == "null")
            {
                res.RemoveAt(res.Count - 1);
            }
            return string.Format("[{0}]", string.Join(',', res));
        }
        public string ToStr(int leafSpaceN = 3, int branchSpaceN = 1, bool ignoreX = false)
        {
            string X = "x";
            var path = PathByLevel(this);
            var level = path.Keys.Select(i => i.Length).Max();

            var leafSpace = " ".Repeat(leafSpaceN);
            var branchSpace = " ".Repeat(branchSpaceN);
            var leafN = (int)Math.Pow(2, level);
            var maxLen = leafN / 2 * (leafSpaceN + 2) + (leafN / 2 - 1) * branchSpaceN;

            var pathListEachLvl = new List<List<string>>();
            pathListEachLvl.Add(new List<string> { "" });
            for (var i = 1; i <= level; i++)
            {
                var lst = new List<string>();
                foreach (var p in pathListEachLvl.Last())
                {
                    lst.Add($"{p}L");
                    lst.Add($"{p}R");
                }
                pathListEachLvl.Add(lst);
            }
            IEnumerable<string> GetVal(List<string> pathList)
            {
                foreach (var p in pathList)
                {
                    string v = X;
                    if (path.ContainsKey(p))
                    {
                        v = $"{path[p].val}";
                    }
                    yield return v;
                }
            }

            var lastLvl = pathListEachLvl.Last();
            var colIdxList = new List<(string val, int idx)>();
            var colIdx = 0;
            var isEven = true;
            foreach (var v in GetVal(lastLvl))
            {
                colIdxList.Add((v, colIdx));
                colIdx++;
                colIdx += isEven ? leafSpaceN : branchSpaceN;
                isEven = !isEven;
            }

            var colIdxListEachLvl = new List<List<(string val, int idx)>>();
            colIdxListEachLvl.Add(colIdxList);

            for (var lvl = level - 1; lvl >= 0; lvl--)
            {
                var lastColIdx = colIdxListEachLvl.Last();
                var valIdx = 0;
                colIdxList = new List<(string val, int idx)>();
                foreach (var v in GetVal(pathListEachLvl[lvl]))
                {
                    colIdx = (lastColIdx[valIdx * 2].idx + lastColIdx[valIdx * 2 + 1].idx) / 2;
                    colIdxList.Add((v, colIdx));
                    valIdx++;
                }
                colIdxListEachLvl.Add(colIdxList);
            }

            if (ignoreX)
            {
                var extraSpaceN = colIdxListEachLvl.Select(i => i.Where(j => j.val != X).First().idx).Min();
                // colIdxListEachLvl.ForEach(i => Console.WriteLine(string.Join("|", i.Select(j => string.Format("{1},{0}", j.val, j.idx)))));
                // Console.WriteLine($"extraSpaceN {extraSpaceN}");
                colIdxListEachLvl = colIdxListEachLvl.Select(i => i.Select(j => (j.val, j.idx - extraSpaceN)).ToList()).ToList();
                // colIdxListEachLvl.ForEach(i => Console.WriteLine(string.Join("|", i.Select(j => string.Format("{1},{0}", j.val, j.idx)))));
            }
            var colIdxListEachLine = new List<List<(string val, int idx)>>();
            foreach (var lvl in colIdxListEachLvl.SkipLast(1))
            {
                colIdxListEachLine.Add(lvl);
                var x = new List<(string val, int idx)>();
                isEven = true;
                foreach (var v in lvl)
                {
                    if (v.val == X && ignoreX) { }
                    else x.Add(isEven ? ("/", v.idx + 1) : ("\\", v.idx - 1));
                    isEven = !isEven;
                }
                colIdxListEachLine.Add(x);
            }
            colIdxListEachLine.Add(colIdxListEachLvl.Last());
            colIdxListEachLine.Reverse();

            var ss = new List<string>();
            foreach (var lvl in colIdxListEachLine)
            {
                // val may more than 1 digit, preserve double spaces
                var s = new char[(lvl.Last().idx + 1) * 2];
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] = ' ';
                }
                foreach (var v in lvl)
                {
                    if (v.val == X && ignoreX) { }
                    else
                    {
                        var i = v.idx;
                        foreach (var j in v.val)
                        {
                            s[i++] = j;
                        }
                    }
                }
                ss.Add(new string(s));
            }

            return string.Join("\n", ss);
        }
        public static Dictionary<string, string> DLRPath(TreeNode tn, Dictionary<string, string> pathDict = null, string path = "")
        {
            if (pathDict == null)
            {
                pathDict = new Dictionary<string, string>();
            }
            if (tn != null)
            {
                pathDict[path] = $"{tn.val}";
                // if ((tn.left != null) ||
                //     ((tn.left == null) && (tn.right != null)))
                //     DLRPath(tn.left, res, $"{path}L");
                // if (tn.right != null) DLRPath(tn.right, res, path + $"{path}R");
                if (tn.left != null) DLRPath(tn.left, pathDict, $"{path}L");
                if (tn.right != null) DLRPath(tn.right, pathDict, $"{path}R");
            }
            else
            {
                pathDict[path] = "null";
            }
            return pathDict;
        }
        public static List<string> DLRWithLevel(TreeNode tn, List<string> res = null, int level = 0)
        {
            if (res == null)
            {
                res = new List<string>();
            }
            if (tn != null)
            {
                res.Add($"{tn.val}|{level}");
                if ((tn.left != null) ||
                    ((tn.left == null) && (tn.right != null)))
                    DLRWithLevel(tn.left, res, level + 1);
                if (tn.right != null) DLRWithLevel(tn.right, res, level + 1);
            }
            else
            {
                res.Add($"null|{level}");
            }
            return res;
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
                return this.ToString() == ((TreeNode)obj).ToString();
            }
        }
        public override int GetHashCode()
        {
            // warning CS0659: '“ListNode”重写 Object.Equals(
            // object o) 但不重写 Object.GetHashCode()

            // https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=netframework-4.8
            // return this.ToString().GetHashCode();
            return id;
        }
        public static void TestTreeNode()
        {
            // var root = new TreeNode(1);
            // root.left = new TreeNode(2);
            // root.right = new TreeNode(3);

            // root.left.left = new TreeNode(4);
            // root.left.right = new TreeNode(5);

            // root.right.left = new TreeNode(6);
            // root.right.right = new TreeNode(7);

            // [1,null,2,3]
            // var root = new TreeNode(1);
            // root.right = new TreeNode(2);
            // root.right.left = new TreeNode(3);

            // var root = FromDLR("1,2,4,5,3,6,7".Split(",").ToList());
            // var root = FromDLR("1,null,2,3".Split(",").ToList());
            // var root = TreeNode.FromDLRSeq("1,null,2,3".Split(",").ToList());
            var root = "1,null,2,3".JsonToTreeNode();

            Console.WriteLine("/*先根递归遍历*/");
            var res = TreeNode.DLR(root);
            Console.WriteLine(string.Join(", ", res));

            Console.WriteLine();
            Console.WriteLine("/*中根递归遍历*/");
            var res2 = TreeNode.LDR(root);
            Console.WriteLine(string.Join(", ", res2));

            Console.WriteLine();
            Console.WriteLine("/*后根递归遍历*/");
            var res3 = TreeNode.LRD(root);
            Console.WriteLine(string.Join(", ", res3));
        }
    }
    public class Node
    {
        static Random rnd = new Random();
        int id = rnd.Next(int.MinValue, int.MaxValue);
        public int val;
        public Node left;
        public Node right;
        public Node next;

        public Node() { }
        public Node(int _val) { val = _val; }
        public Node(int _val, Node _left, Node _right, Node _next)
        {
            val = _val;
            left = _left;
            right = _right;
            next = _next;
        }

        public static Node FromStr(string s)
        {
            var o = JObject.Parse(s);
            var n = BuildNode(o);
            return n;
        }
        static Node BuildNode(JToken jo)
        {
            if (jo.Type != JTokenType.Object)
                return null;
            var o = jo as JObject;
            if (!o.ContainsKey("val"))
                return null;
            var n = new Node(o["val"].Value<int>());
            if (o.ContainsKey("left"))
            {
                n.left = BuildNode(o["left"]);
            }
            if (o.ContainsKey("right"))
            {
                n.right = BuildNode(jo["right"]);
            }
            return n;
        }
        static Dictionary<string, Node> PathByLevel(Node root)
        {
            var pathDict = new Dictionary<string, Node>();
            if (root == null)
            {
                return pathDict;
            }
            var node = root;
            pathDict[""] = root;
            var pathOfLvl = new List<string> { "" };
            // var pathOfAll = new List<List<string>>();
            // pathOfAll.Add(pathOfLvl);

            while (true)
            {
                var pathOfLvl2 = new List<string>();
                int nodeOflvl = 0;
                foreach (var parentPath in pathOfLvl)
                {
                    node = pathDict[parentPath];
                    if (node.left != null)
                    {
                        var p = $"{parentPath}L";
                        pathDict[p] = node.left;
                        pathOfLvl2.Add(p);
                        nodeOflvl++;
                    }
                    if (node.right != null)
                    {
                        var p = $"{parentPath}R";
                        pathDict[p] = node.right;
                        pathOfLvl2.Add(p);
                        nodeOflvl++;
                    }
                }
                if (nodeOflvl == 0)
                    break;
                pathOfLvl = pathOfLvl2;
                // pathOfAll.Add(pathOfLvl);
            }

            // var lst = new List<string>();
            // foreach (var lvl in pathOfAll)
            // {
            //     // Console.WriteLine(string.Join(" - ", lvl.Select(i => pathDict[i].val)));
            //     lst.Add(string.Join(" - ", lvl.Select(i => string.Format("{0}{1}", i.EndsWith("L") ? "/" : "\\", pathDict[i].val))));
            // }
            // return string.Join('\n', lst);
            return pathDict;
        }
        public string ToStr(int leafSpaceN = 8, int branchSpaceN = 6, bool ignoreX = false, bool withNext = false)
        {
            string X = "x";
            var path = PathByLevel(this);
            var level = path.Keys.Select(i => i.Length).Max();

            var leafSpace = " ".Repeat(leafSpaceN);
            var branchSpace = " ".Repeat(branchSpaceN);
            var leafN = (int)Math.Pow(2, level);
            var maxLen = leafN / 2 * (leafSpaceN + 2) + (leafN / 2 - 1) * branchSpaceN;

            var pathListEachLvl = new List<List<string>>();
            pathListEachLvl.Add(new List<string> { "" });
            for (var i = 1; i <= level; i++)
            {
                var lst = new List<string>();
                foreach (var p in pathListEachLvl.Last())
                {
                    lst.Add($"{p}L");
                    lst.Add($"{p}R");
                }
                pathListEachLvl.Add(lst);
            }
            IEnumerable<string> GetVal(List<string> pathList)
            {
                foreach (var p in pathList)
                {
                    string v = X;
                    if (path.ContainsKey(p))
                    {
                        if (withNext)
                        {
                            var next = path[p].next == null ? X : $"{path[p].next.val}";
                            v = $"{path[p].val}({next})";
                        }
                        else
                        {
                            v = $"{path[p].val}";
                        }
                    }
                    yield return v;
                }
            }

            var lastLvl = pathListEachLvl.Last();
            var colIdxList = new List<(string val, int idx)>();
            var colIdx = 0;
            var isEven = true;
            foreach (var v in GetVal(lastLvl))
            {
                colIdxList.Add((v, colIdx));
                colIdx++;
                colIdx += isEven ? leafSpaceN : branchSpaceN;
                isEven = !isEven;
            }

            var colIdxListEachLvl = new List<List<(string val, int idx)>>();
            colIdxListEachLvl.Add(colIdxList);

            for (var lvl = level - 1; lvl >= 0; lvl--)
            {
                var lastColIdx = colIdxListEachLvl.Last();
                var valIdx = 0;
                colIdxList = new List<(string val, int idx)>();
                foreach (var v in GetVal(pathListEachLvl[lvl]))
                {
                    colIdx = (lastColIdx[valIdx * 2].idx + lastColIdx[valIdx * 2 + 1].idx) / 2;
                    colIdxList.Add((v, colIdx));
                    valIdx++;
                }
                colIdxListEachLvl.Add(colIdxList);
            }

            if (ignoreX)
            {
                var extraSpaceN = colIdxListEachLvl.Select(i => i.Where(j => j.val != X).First().idx).Min();
                // colIdxListEachLvl.ForEach(i => Console.WriteLine(string.Join("|", i.Select(j => string.Format("{1},{0}", j.val, j.idx)))));
                // Console.WriteLine($"extraSpaceN {extraSpaceN}");
                colIdxListEachLvl = colIdxListEachLvl.Select(i => i.Select(j => (j.val, j.idx - extraSpaceN)).ToList()).ToList();
                // colIdxListEachLvl.ForEach(i => Console.WriteLine(string.Join("|", i.Select(j => string.Format("{1},{0}", j.val, j.idx)))));
            }
            var colIdxListEachLine = new List<List<(string val, int idx)>>();
            foreach (var lvl in colIdxListEachLvl.SkipLast(1))
            {
                colIdxListEachLine.Add(lvl);
                var x = new List<(string val, int idx)>();
                isEven = true;
                foreach (var v in lvl)
                {
                    if (v.val == X && ignoreX) { }
                    else x.Add(isEven ? ("/", v.idx + 1) : ("\\", v.idx - 1));
                    isEven = !isEven;
                }
                colIdxListEachLine.Add(x);
            }
            colIdxListEachLine.Add(colIdxListEachLvl.Last());
            colIdxListEachLine.Reverse();

            var ss = new List<string>();
            foreach (var lvl in colIdxListEachLine)
            {
                // val may more than 1 digit, preserve double spaces
                var s = new char[(lvl.Last().idx + 1) * 2];
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] = ' ';
                }
                foreach (var v in lvl)
                {
                    if (v.val == X && ignoreX) { }
                    else
                    {
                        var i = v.idx;
                        var x = v.val;
                        foreach (var j in v.val)
                        {
                            s[i++] = j;
                        }
                    }
                }
                ss.Add(new string(s));
            }

            return string.Join("\n", ss);
        }
        public override string ToString()
        {
            return ToStr();
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
                return this.ToString() == ((Node)obj).ToString();
            }
        }
        public override int GetHashCode()
        {
            // warning CS0659: '“ListNode”重写 Object.Equals(
            // object o) 但不重写 Object.GetHashCode()

            // https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=netframework-4.8
            // return this.ToString().GetHashCode();
            return id;
        }
    }
    public class GraphNode
    {
        static Random rnd = new Random();
        int id = rnd.Next(int.MinValue, int.MaxValue);
        public int val;
        public IList<GraphNode> neighbors;

        public GraphNode() { }
        public GraphNode(int _val, IList<GraphNode> _neighbors)
        {
            val = _val;
            neighbors = _neighbors;
        }
        public static GraphNode FromJson(string s)
        {
            var o = JObject.Parse(s);
            var dict = new Dictionary<int, GraphNode>();
            ParseNode(o, dict);
            var min = dict.Select(i => i.Key).Min();
            var n = dict.Where(i => i.Key == min).First().Value;
            return n;
        }
        static GraphNode GetGraphNodeOrCreate(JObject nObj, Dictionary<int, GraphNode> idDict)
        {
            int id = int.MinValue;
            if (nObj.ContainsKey("$id"))
            {
                id = nObj["$id"].Value<int>();
                if (idDict.ContainsKey(id))
                {
                    // Console.WriteLine($"already node for id: {id}");
                }
                else
                {
                    var x = new GraphNode();
                    x.val = nObj["val"].Value<int>();
                    x.neighbors = new List<GraphNode>();
                    idDict[id] = x;
                }
            }
            else
            {
                // there must be a $ref if no $id
                id = nObj["$ref"].Value<int>();
                if (!idDict.ContainsKey(id))
                {
                    // Console.WriteLine($"no node for ref: {id}");
                    throw new ArgumentOutOfRangeException($"no node for ref: {id}");
                }
            }
            return idDict[id];
        }
        public static void ParseNode(JObject o, Dictionary<int, GraphNode> idDict)
        {
            var n = GetGraphNodeOrCreate(o, idDict);
            if (!o.ContainsKey("neighbors"))
                return;
            var nbs = o["neighbors"] as JArray;
            if (nbs.Count == 0)
                return;
            foreach (var i in nbs)
            {
                var nb = i as JObject;
                var t = GetGraphNodeOrCreate(nb, idDict);
                n.neighbors.Add(t);
                ParseNode(nb, idDict);
            }
        }
        static string GraphNodeToJson(GraphNode node, Dictionary<int, int> refDict = null)
        {
            if (refDict == null)
            {
                refDict = new Dictionary<int, int>();
            }
            var k = node.id;
            if (refDict.ContainsKey(k))
            {
                return $"{{\"$ref\":\"{refDict[k]}\"}}";
            }
            var refId = refDict.Count + 1;
            refDict[k] = refId;
            var nbs = new List<string>();
            foreach (var i in node.neighbors)
            {
                nbs.Add(GraphNodeToJson(i, refDict));
            }
            // {"$id":"1","neighbors":[],"val":10}
            var neighbors = string.Join(",", nbs);
            var s = $"{{\"$id\":\"{refId}\",\"neighbors\":[{neighbors}],\"val\":{node.val}}}";
            return s;
        }
        public override string ToString()
        {
            // return $"{val} [{string.Join(", ", neighbors.Select(i => i.val))}]";
            return GraphNodeToJson(this);
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
                return this.ToString() == ((GraphNode)obj).ToString();
            }
        }
        public override int GetHashCode()
        {
            // warning CS0659: '“ListNode”重写 Object.Equals(
            // object o) 但不重写 Object.GetHashCode()

            // https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=netframework-4.8
            // return this.ToString().GetHashCode();
            return id;
        }
    }
    public class NodeRandom
    {
        static Random rnd = new Random();
        int id = rnd.Next(int.MinValue, int.MaxValue);
        public int val;
        public NodeRandom next;
        public NodeRandom random;

        public NodeRandom() { }
        public NodeRandom(int _val, NodeRandom _next, NodeRandom _random)
        {
            val = _val;
            next = _next;
            random = _random;
        }
        public static NodeRandom FromJson(string s)
        {
            var idDict = new Dictionary<int, NodeRandom>();
            return GetOrCreate(JObject.Parse(s), idDict);
        }
        static NodeRandom GetOrCreate(JObject nObj, Dictionary<int, NodeRandom> idDict)
        {
            int id = int.MinValue;
            if (nObj.ContainsKey("$id"))
            {
                id = nObj["$id"].Value<int>();
                if (idDict.ContainsKey(id))
                {
                    // Console.WriteLine($"already node for id: {id}");
                }
                else
                {
                    var x = new NodeRandom();
                    x.val = nObj["val"].Value<int>();
                    idDict[id] = x;
                    if (nObj.ContainsKey("next"))
                    {
                        if (nObj["next"].Type == JTokenType.Object)
                        {
                            var next = GetOrCreate(nObj["next"] as JObject, idDict);
                            x.next = next;
                        }
                    }
                    if (nObj.ContainsKey("random"))
                    {
                        if (nObj["random"].Type == JTokenType.Object)
                        {
                            var random = GetOrCreate(nObj["random"] as JObject, idDict);
                            x.random = random;
                        }
                    }
                }
            }
            else
            {
                // there must be a $ref if no $id
                id = nObj["$ref"].Value<int>();
                if (!idDict.ContainsKey(id))
                {
                    // Console.WriteLine($"no node for ref: {id}");
                    throw new ArgumentOutOfRangeException($"no node for ref: {id}");
                }
            }
            return idDict[id];
        }
        string ToJson(Dictionary<int, int> idDict = null)
        {
            if (idDict == null)
            {
                idDict = new Dictionary<int, int>();
            }
            var k = id;
            if (idDict.ContainsKey(k))
            {
                return $"{{\"$ref\":\"{idDict[k]}\"}}";
            }
            var refId = idDict.Count + 1;
            idDict[k] = refId;
            var n = next == null ? "null" : next.ToJson(idDict);
            var r = random == null ? "null" : random.ToJson(idDict);
            var s = $"{{\"$id\":\"{refId}\",\"next\":{n},\"random\":{r},\"val\":{val}}}";
            return s;
        }
        public override string ToString()
        {
            return ToJson();
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
                return this.ToString() == ((NodeRandom)obj).ToString();
            }
        }
        public override int GetHashCode()
        {
            // warning CS0659: '“ListNode”重写 Object.Equals(
            // object o) 但不重写 Object.GetHashCode()

            // https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=netframework-4.8
            // return this.ToString().GetHashCode();
            return id;
        }
    }
    static class Ext
    {
        public static string Repeat(this string s, int n)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < n; i++)
            {
                sb.Append(s);
            }
            return sb.ToString();
        }
        public static string ToCap(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return char.ToUpper(str[0]) + str.Substring(1);
        }
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
        public static IList<string> JsonToIListStr(this string s)
        {
            if (s.Trim() == "[]")
            {
                return new List<string>();
            }
            return s.TrimStart('[').TrimEnd(']')
                    .Split(',')
                    .Select(x => x.Trim().Trim('"'))
                    .ToList();
        }
        public static string IListStrToJson(this IList<string> a)
        {
            return string.Format("[{0}]", string.Join(',', a.Select(i => $"\"{i}\"")));
        }
        public static IList<IList<string>> JsonToIListIListStr(this string s)
        {
            if (s.Trim().Replace(" ", "") == "[]")
                return new List<IList<string>>();

            var lst = s.TrimStart(new char[] { '[', ' ' }).TrimEnd(new char[] { ']', ' ' })
                    .Split("],")
                    .Select(x =>
                                x.TrimStart(new char[] { '[', ' ' }).TrimEnd(new char[] { ']', ' ' })
                                .Split(',')
                                .Select(y => y.TrimStart(new char[] { '"' }).TrimEnd(new char[] { '"' }))
                                // .Where(z => z.Length > 0)
                                .ToList())
                    .ToList();
            var res = new List<IList<string>>();
            res.AddRange(lst);
            return res;
        }
        public static string IListIListStrToJson(this IList<IList<string>> a)
        {
            return string.Format("[{0}]", string.Join(",", a.Select(x =>
                        string.Format("[{0}]", string.Join(',', x.Select(i => $"\"{i}\""))))));
        }
        public static T[] JsonToArray1d<T>(this string s, Func<string, T> converter)
        {
            s = s.Trim();
            if ((s[0] != '[') || (s[s.Length - 1] != ']'))
            {
                throw new ArgumentException("string should be started with '[' and end with ']'", "s");
            }
            s = s.Substring(1, s.Length - 2);
            return s.Split(',')
                    .Select(y => converter(y))
                    .ToArray();
        }
        public static string Array1dToJson<T>(this T[] a)
        {
            return string.Format("[{0}]", string.Join(',', a));
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
        public static IList<int> JsonToIListInt(this string s)
        {
            if (!s.Contains(',')) return new List<int>();
            return s.TrimStart('[').TrimEnd(']')
                    .Split(',')
                    .Select(y => int.Parse(y))
                    .ToList();
        }
        public static string IListIntToJson(this IList<int> a)
        {
            return string.Format("[{0}]", string.Join(',', a));
        }
        public static IList<int?> JsonToIListNullableInt(this string s)
        {
            if (!s.Contains(',')) return new List<int?>();
            return s.TrimStart('[').TrimEnd(']')
                    .Split(',')
                    .Select(y =>
                    {
                        int? x = null;
                        if (y != "null") x = int.Parse(y);
                        return x;
                    })
                    .ToList();
        }
        public static string IListNullableIntToJson(this IList<int?> a)
        {
            return string.Format("[{0}]", string.Join(',', a.Select(i => i == null ? "null" : i.ToString())));
        }
        public static T[][] JsonToArray2d<T>(this string s, Func<string, T> converter)
        {
            s = s.Trim();
            if ((s[0] != '[') || (s[s.Length - 1] != ']'))
            {
                throw new ArgumentException("string should be started with '[' and end with ']'", "s");
            }
            s = s.Substring(1, s.Length - 2);
            s = s.TrimStart(new char[] { '[', ' ' }).TrimEnd(new char[] { ']', ' ' });
            // Console.WriteLine($"remove []: {s}");
            return s.Split("],")
                    .Select(x =>
                        x.TrimStart(new char[] { '[', ' ' })
                        .Split(',')
                        // .Where(z => z.Length > 0)
                        .Select(y => converter(y))
                        .ToArray())
                    .ToArray();
        }
        public static string Array2dToJson<T>(this T[][] a)
        {
            return string.Format("[{0}]", string.Join(",", a.Select(x =>
                        string.Format("[{0}]", string.Join(',', x)))));
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
        public static IList<IList<int>> JsonToIListIListInt(this string s)
        {
            if (s.Trim().Replace(" ", "") == "[]")
                return new List<IList<int>>();

            var lst = s.TrimStart(new char[] { '[', ' ' }).TrimEnd(new char[] { ']', ' ' })
                    .Split("],")
                    .Select(x =>
                                x.TrimStart(new char[] { '[', ' ' }).TrimEnd(new char[] { ']', ' ' })
                                .Split(',')
                                .Where(z => z.Length > 0)
                                .Select(y => int.Parse(y))
                                .ToList())
                    .ToList();
            var res = new List<IList<int>>();
            res.AddRange(lst);
            return res;
        }
        public static string IListIListIntToJson(this IList<IList<int>> a)
        {
            return string.Format("[{0}]", string.Join(",", a.Select(x =>
                        string.Format("[{0}]", string.Join(',', x)))));
        }
        public static int[][] IListIListIntToInt2d(this IList<IList<int>> a)
        {
            return a.Select(i => i.ToArray()).ToArray();
        }
        public static IList<IList<int>> Int2dToIListIListInt(this int[][] a)
        {
            var lst = a.Select(i => i.ToList()).ToList();
            var res = new List<IList<int>>();
            res.AddRange(lst);
            return res;
        }
        public static List<string> UnpackPara1(this string s)
        {
            if (s.Trim() == "[]")
            {
                return new List<string>();
            }
            s = s.TrimStart().TrimEnd();
            return s.Substring(2, s.Length - 4)
                    .Split("],[")
                    .Select(x => string.Format("[{0}]", x))
                    .ToList();
        }
        public static List<string> UnpackPara2(this string s)
        {
            if (s.Trim() == "[]")
            {
                return new List<string>();
            }
            s = s.TrimStart().TrimEnd();
            s = s.Substring(1, s.Length - 2);
            if (s.StartsWith('['))
            {
                var s2 = new string(s.Reverse().ToArray());
                var start = 0;
                var lst = new List<string>();
                while (start < s.Length)
                {
                    var end = s.Length - 1 - s2.IndexOf(']');
                    lst.Add(s.Substring(start, end - start + 1));
                    start = end + 1;
                }
                return lst;
            }
            return s.Split(",").ToList();
        }
        public static IList<int> Sorted(this IList<int> a)
        {
            return a.OrderBy(j => j).ToList();
        }
        public static IList<IList<int>> Sorted(this IList<IList<int>> a)
        {
            var lst = a.Select(i => i.OrderBy(j => j).ToList()).OrderBy(k => string.Join(',', k)).ToList();
            var res = new List<IList<int>>();
            res.AddRange(lst);
            return res;
        }
        public static int[] Sorted(this int[] a)
        {
            return a.OrderBy(j => j).ToArray();
        }
        public static int[][] Sorted(this int[][] a)
        {
            return a.Select(i => i.OrderBy(j => j).ToArray()).OrderBy(k => string.Join(',', k)).ToArray();
        }
        public static IList<string> Sorted(this IList<string> a)
        {
            return a.OrderBy(j => j).ToList();
        }
        public static IList<IList<string>> Sorted(this IList<IList<string>> a)
        {
            var lst = a.Select(i => i.OrderBy(j => j).ToList()).OrderBy(k => string.Join(',', k)).ToList();
            var res = new List<IList<string>>();
            res.AddRange(lst);
            return res;
        }
        public static string[] Sorted(this string[] a)
        {
            return a.OrderBy(j => j).ToArray();
        }
        public static string[][] Sorted(this string[][] a)
        {
            return a.Select(i => i.OrderBy(j => j).ToArray()).OrderBy(k => string.Join(',', k)).ToArray();
        }
        public static IList<TreeNode> Sorted(this IList<TreeNode> a)
        {
            return a.OrderBy(k => k.ToStrByLevel(ignoreVal: true))
                    .Select(i => i.ToStrByLevel(ignoreVal: true).JsonToTreeNode())
                    .ToArray();
        }
        public static char[][] JsonToChar2d(this string s, char quote = '\"')
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
        public static TreeNode JsonToTreeNode(this string s)
        {
            return TreeNode.FromStr(s);
        }
        public static string TreeNodeToJson(this TreeNode tn)
        {
            return tn.ToString();
        }
        public static IList<TreeNode> JsonToIListTreeNode(this string s)
        {
            var ss = s.JsonToArray2d(i => i);
            // foreach (var i in ss)
            //     Console.WriteLine(i.Array1dToJson());
            return ss.Select(i => TreeNode.FromStr(i.Array1dToJson())).ToList();
        }
        public static string IListTreeNodeToJson(this IList<TreeNode> lst)
        {
            var ss = lst.Select(i => i.ToString());
            return string.Format("[ {0} ]", string.Join(", ", ss));
        }
        public static GraphNode JsonToGraphNode(this string s)
        {
            return GraphNode.FromJson(s);
        }
        public static string GraphNodeToJson(this GraphNode gn)
        {
            return gn.ToString();
        }
        public static NodeRandom JsonToNodeRandom(this string s)
        {
            return NodeRandom.FromJson(s);
        }
        public static string NodeRandomToJson(this NodeRandom n)
        {
            return n.ToString();
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
            string ExecutionTimeTaken = string.Format("{0:00} s {1:0.00000} ms", sw.Elapsed.Seconds, sw.Elapsed.TotalMilliseconds);
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
            if (t == typeof(string))
            {
                f = x => x.Trim().Trim('"');
            }
            else if (t == typeof(string[]))
            {
                f = x => x.JsonToStr1d();
            }
            else if (t == typeof(IList<string>))
            {
                f = x => x.JsonToIListStr();
            }
            else if (t == typeof(IList<IList<string>>))
            {
                f = x => x.JsonToIListIListStr();
            }
            else if (t == typeof(ListNode))
            {
                f = x => x.JsonToListNode();
            }
            else if (t == typeof(TreeNode))
            {
                f = x => x.JsonToTreeNode();
            }
            else if (t == typeof(IList<TreeNode>))
            {
                f = x => x.JsonToIListTreeNode();
            }
            else if (t == typeof(GraphNode))
            {
                f = x => x.JsonToGraphNode();
            }
            else if (t == typeof(NodeRandom))
            {
                f = x => x.JsonToNodeRandom();
            }
            else if (t == typeof(bool))
            {
                f = x => bool.Parse(x);
            }
            else if (t == typeof(int))
            {
                f = x => int.Parse(x);
            }
            else if (t == typeof(int[]))
            {
                f = x => x.JsonToInt1d();
            }
            else if (t == typeof(int[][]))
            {
                f = x => x.JsonToInt2d();
            }
            else if (t == typeof(IList<int>))
            {
                f = x => x.JsonToIListInt();
            }
            else if (t == typeof(IList<int?>))
            {
                f = x => x.JsonToIListNullableInt();
            }
            else if (t == typeof(IList<IList<int>>))
            {
                f = x => x.JsonToIListIListInt();
            }
            else if (t == typeof(char[][]))
            {
                f = x => x.JsonToChar2d();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Type t", $"converter for {t} not implemented");
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
        public static void Method(dynamic obj, string[] lines, int checkParaIndex = -1, bool truncate = false, bool sortRet = false, bool sortPara = false)
        {
            MethodInfo method = Util.Method.GetMethodsFromType(obj.GetType())[0];
            var inputTypes = Util.Method.GetParametersAndReturnTypes(method);
            var signature = string.Format("{0}.{1}({2}) => {3}",
                            obj.GetType().Namespace,
                            method.Name,
                            string.Join(", ", inputTypes.SkipLast(1)),
                            inputTypes.Last());
            Console.WriteLine(signature);

            Type retExpType = inputTypes.Last();
            bool retVoid = false;
            if (retExpType == typeof(void))
            {
                if (checkParaIndex < 0)
                {
                    throw new ArgumentException("checkParaIndex should be set when return type is void", "checkParaIndex");
                }
                retVoid = true;
                inputTypes.RemoveAt(inputTypes.Count - 1);
            }

            dynamic checkParaExpT = null;
            if (checkParaIndex >= 0)
            {
                checkParaExpT = inputTypes[checkParaIndex];
                inputTypes.Add(checkParaExpT);
                Console.WriteLine($"And in-place change parameter {checkParaIndex}: {checkParaExpT}");
            }

            var inputParser = new InputConverterList(inputTypes);
            var maxChars = 200;
            var maxCharsEach = maxChars / inputTypes.Count;

            dynamic checkPara = null, checkParaExpV = null;
            int idx = 0;
            while (idx < lines.Length)
            {
                string allParaS = null, retExpS = null, checkParaExpS = null;
                var ss = new List<string>();
                var vs = new List<dynamic>();
                foreach (var p in inputParser)
                {
                    var s = lines[idx++];
                    ss.Add(s.Substring(0, Math.Min(s.Length, maxCharsEach)));
                    dynamic tv = p.converter(s);
                    vs.Add(tv);
                }

                if (checkParaIndex >= 0)
                {
                    checkPara = vs[checkParaIndex];
                    checkParaExpS = ss.Last();
                    ss.RemoveAt(ss.Count - 1);
                    checkParaExpV = vs.Last();
                    vs.RemoveAt(vs.Count - 1);
                }

                if (retVoid)
                {
                    retExpS = "void";
                    allParaS = string.Join(" | ", ss);
                }
                else
                {
                    retExpS = ss.Last();
                    allParaS = string.Join(" | ", ss.SkipLast(1));
                }

                if (checkParaExpS != null)
                {
                    Console.WriteLine(string.Format("{0,-50} => {1} | {2}", allParaS, retExpS, checkParaExpS));
                }
                else
                {
                    Console.WriteLine(string.Format("{0,-50} => {1}", allParaS, retExpS));
                }

                dynamic ret = null;
                if (retVoid)
                {
                    using (new Timeit())
                    {
                        method.Invoke(obj, vs.ToArray());
                    }
                }
                else
                {
                    using (new Timeit())
                    {
                        ret = method.Invoke(obj, vs.SkipLast(1).ToArray());
                    }
                    var exp = vs.Last();
                    if (sortRet)
                    {
                        // https://stackoverflow.com/questions/28701867/checking-if-type-or-instance-implements-ienumerable-regardless-of-type-t/28701974#28701974
                        var isIEnumerable = typeof(IEnumerable).IsAssignableFrom(exp.GetType());
                        if (!isIEnumerable)
                        {
                            throw new ArgumentException("sortRet should not be set to true when return type is not IEnumerable", "sortRet");
                        }
                        ret = Ext.Sorted(ret);
                        exp = Ext.Sorted(exp);
                    }
                    Assert.Equal(exp, ret);
                }

                if (checkParaIndex >= 0)
                {
                    if (truncate)
                    {
                        // https://github.com/dotnet/coreclr/issues/15186
                        // The dynamic binder does not—and should not—consider extension method syntax in deciding which overload to bind to.
                        // You should use the static method call syntax of TaskExtensions.TaskExtension(DoSomething(thing)); instead.
                        checkPara = Enumerable.Take(checkPara, ret);
                    }
                    if (sortPara)
                    {
                        var isIEnumerable = typeof(IEnumerable).IsAssignableFrom(checkParaExpV.GetType());
                        if (!isIEnumerable)
                        {
                            throw new ArgumentException("sortPara should not be set to true when para type is not IEnumerable", "sortPara");
                        }
                        checkParaExpV = Ext.Sorted(checkParaExpV);
                        checkPara = Ext.Sorted(checkPara);
                    }
                    Assert.Equal(checkParaExpV, checkPara);
                }
            }
        }
        public static void Method2(dynamic obj, MethodInfo method, string[] inputStrs, bool sortRet = false)
        {
            var inputTypes = Util.Method.GetParametersAndReturnTypes(method);
            var signature = string.Format("{0}.{1}({2}) => {3}",
                            obj.GetType().Name,
                            method.Name,
                            string.Join(", ", inputTypes.SkipLast(1)),
                            inputTypes.Last());
            Console.WriteLine(signature);

            Type retExpType = inputTypes.Last();
            bool retVoid = false;
            if (retExpType == typeof(void))
            {
                retVoid = true;
                inputTypes.RemoveAt(inputTypes.Count - 1);
            }

            var inputParser = new InputConverterList(inputTypes);
            var maxChars = 100;
            var maxCharsEach = maxChars;
            if (inputTypes.Count > 0)
            {
                maxCharsEach = maxChars / inputTypes.Count;
            }

            string allParaS = null, retExpS = null;
            var ss = new List<string>();
            var vs = new List<dynamic>();
            var idx = 0;
            foreach (var p in inputParser)
            {
                var s = inputStrs[idx++];
                ss.Add(s.Substring(0, Math.Min(s.Length, maxCharsEach)));
                dynamic tv = p.converter(s);
                vs.Add(tv);
            }

            if (retVoid)
            {
                retExpS = "null";
                allParaS = string.Join(" | ", ss);
            }
            else
            {
                retExpS = ss.Last();
                allParaS = string.Join(" | ", ss.SkipLast(1));
            }

            if (allParaS == "")
            {
                allParaS = "null";
            }
            Console.WriteLine(string.Format("{0,-50} => {1}", allParaS, retExpS));

            dynamic ret = null;
            if (retVoid)
            {
                using (new Timeit())
                {
                    method.Invoke(obj, vs.ToArray());
                }
            }
            else
            {
                using (new Timeit())
                {
                    ret = method.Invoke(obj, vs.SkipLast(1).ToArray());
                }
                var exp = vs.Last();
                if (sortRet)
                {
                    // https://stackoverflow.com/questions/28701867/checking-if-type-or-instance-implements-ienumerable-regardless-of-type-t/28701974#28701974
                    var isIEnumerable = typeof(IEnumerable).IsAssignableFrom(exp.GetType());
                    if (!isIEnumerable)
                    {
                        throw new ArgumentException("sortRet should not be set to true when return type is not IEnumerable", "sortRet");
                    }
                    ret = Ext.Sorted(ret);
                    exp = Ext.Sorted(exp);
                }
                Assert.Equal(exp, ret);
            }
        }
        public static void NestedClass(Type t, string[] lines, bool sortRet = false)
        {
            for (var i = 0; i < lines.Length; i += 3)
            {
                var op = lines[i].JsonToIListStr();
                var para = lines[i + 1].UnpackPara1();
                var exp = lines[i + 2].UnpackPara2();
                dynamic obj = null;
                for (var idx = 0; idx < para.Count; idx++)
                {
                    if (op[idx] == t.Name)
                    {
                        var argStrs = para[idx].UnpackPara2();
                        var ctor = t.GetConstructors().Where(i => i.GetParameters().Length == argStrs.Count).First();
                        var inputTypes = ctor.GetParameters().Select(i => i.ParameterType).ToList();
                        var inputParser = new InputConverterList(inputTypes);
                        var vs = new List<dynamic>();
                        for (var j = 0; j < inputParser.Count; j++)
                        {
                            vs.Add(inputParser[j].converter(argStrs[j]));
                        }
                        // obj = Activator.CreateInstance(t, args: vs.ToArray(), activationAttributes: null);
                        obj = ctor.Invoke(vs.ToArray());
                        Console.WriteLine($"new {t.Name}({para[idx]})");
                    }
                    else
                    {
                        var m = t.GetMethod(op[idx].ToCap());
                        var x = new List<string>();
                        if (para[idx] != "[null]")
                        {
                            x = para[idx].UnpackPara2();
                        }
                        if (exp[idx] != "null")
                        {
                            x.Add(exp[idx]);
                        }
                        Method2(obj, m, x.ToArray(), sortRet);
                    }
                }
                Console.WriteLine("----");
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