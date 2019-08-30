using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LRUCache
{
    public class LRUCache
    {
        Dictionary<int, int> Dict = new Dictionary<int, int>();
        List<int> Keys = new List<int>();
        int Capacity;
        public LRUCache(int capacity)
        {
            Capacity = capacity;
        }

        public int Get(int key)
        {
            var i = Keys.IndexOf(key);
            if (i >= 0)
            {
                Keys.RemoveAt(i);
                Keys.Add(key);
                return Dict[key];
            }
            return -1;
        }

        public void Put(int key, int value)
        {
            var i = Keys.IndexOf(key);
            if (i >= 0)
            {
                Keys.RemoveAt(i);
            }
            if (Keys.Count >= Capacity)
            {
                var k = Keys.First();
                Dict.Remove(k);
                Keys.RemoveAt(0);
            }
            Keys.Add(key);
            Dict[key] = value;
        }
    }

    /**
     * Your LRUCache object will be instantiated and called as such:
     * LRUCache obj = new LRUCache(capacity);
     * int param_1 = obj.Get(key);
     * obj.Put(key,value);
     */
    public class Solution
    {
        public IList<int?> LRUCacheTest(IList<string> op, IList<IList<int>> para)
        {
            // ["LRUCache","put","put","get","put","get","put","get","get","get"]
            // [[2],[1,1],[2,2],[1],[3,3],[2],[4,4],[1],[3],[4]]
            // [null,null,null,1,null,-1,null,-1,3,4]
            LRUCache cache = new LRUCache(0);
            var rets = new List<int?>();
            for (var idx = 0; idx < para.Count; idx++)
            {
                int? ret = null;
                if (op[idx] == "LRUCache")
                {
                    cache = new LRUCache(para[idx][0]);
                }
                else if (op[idx] == "put")
                {
                    cache.Put(para[idx][0], para[idx][1]);
                }
                else if (op[idx] == "get")
                {
                    ret = cache.Get(para[idx][0]);
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
            lines = "0146-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}