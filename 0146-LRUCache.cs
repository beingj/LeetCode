using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LRUCache
{

    /**
     * Your LRUCache object will be instantiated and called as such:
     * LRUCache obj = new LRUCache(capacity);
     * int param_1 = obj.Get(key);
     * obj.Put(key,value);
     */
    public class Solution
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
            var cls = typeof(Solution).GetNestedTypes().First();
            Verify.NestedClass(cls, lines);
        }
    }
}