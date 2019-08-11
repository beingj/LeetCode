using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WordLadderII
{
    public class Solution
    {
        public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
            var front_level = new HashSet<string> { beginWord };
            var end_level = new HashSet<string> { endWord };
            var path_dic = new Dictionary<string, List<string>>();
            var wordSet = new HashSet<string>(wordList);
            if (wordSet.Contains(endWord))
                return new List<IList<string>>();
            bfs(front_level, end_level, true, wordSet, path_dic);
            var path = new List<string>();
            var paths = new List<IList<string>>();
            construct_path(beginWord, endWord, path_dic, path, paths);
            return paths;
        }
        bool bfs(HashSet<string> front_level, HashSet<string> end_level, bool is_forward, HashSet<string> word_set, Dictionary<string, List<string>> path_dic)
        {
            if (front_level.Count == 0)
                return false;
            if (front_level.Count > end_level.Count)
                return bfs(end_level, front_level, !is_forward, word_set, path_dic);

            var lvls = front_level.Concat(end_level);
            foreach (var word in lvls)
                word_set.Remove(word);
            var next_level = new HashSet<string>();
            var done = false;
            while (front_level.Count > 0)
            {
                var word = front_level.First();
                front_level.Remove(word);
                foreach (var c in "abcdefghijklmnopqrstuvwxyz")
                {
                    for (var i = 0; i < word.Length; i++)
                    {
                        var new_word = word.Substring(0, i) + c + word.Substring(i + 1);
                        if (end_level.Contains(new_word))
                        {
                            done = true;
                            add_path(word, new_word, is_forward, path_dic);
                        }
                        else
                        {
                            if (word_set.Contains(new_word))
                            {
                                next_level.Add(new_word);
                                add_path(word, new_word, is_forward, path_dic);
                            }
                        }
                    }
                }
            }
            return done || bfs(next_level, end_level, is_forward, word_set, path_dic);
        }
        void add_path(string word, string new_word, bool is_forward, Dictionary<string, List<string>> path_dic)
        {
            if (is_forward)
            {
                path_dic[word] = path_dic.GetValueOrDefault(word, new List<string>()).Concat(new List<string> { new_word }).ToList();
            }
            else
            {
                path_dic[new_word] = path_dic.GetValueOrDefault(new_word, new List<string>()).Concat(new List<string> { word }).ToList();
            }
        }
        void construct_path(string word, string end_word, Dictionary<string, List<string>> path_dic, List<string> path, List<IList<string>> paths)
        {
            if (word == end_word)
            {
                paths.Add(path);
                return;
            }
            if (path_dic.ContainsKey(word))
            {
                foreach (var item in path_dic[word])
                {
                    construct_path(item, end_word, path_dic, path.Concat(new List<string> { item }).ToList(), paths);
                }
            }
        }
        // public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        // {
        //     // if (!wordList.Contains(endWord))
        //     //     return new List<IList<string>>();
        //     // if (beginWord.Length == 1)
        //     // {
        //     //     var res = new List<IList<string>>();
        //     //     res.Add(new List<string> { beginWord, endWord });
        //     //     return res;
        //     // }
        //     var wordSet = new HashSet<string>(wordList.Where(i => i != beginWord));
        //     var cur_level = new List<string> { beginWord };
        //     var next_level = new List<string>();
        //     var depth = 1;
        //     var n = beginWord.Length;
        //     var parentDict = new Dictionary<string, List<string>> { { beginWord, new List<string> { null } } };
        //     var minDepth = wordList.Count + 1;
        //     var leafList = new List<string>();
        //     while (cur_level.Count > 0)
        //     {
        //         foreach (var item in cur_level)
        //         {
        //             for (var i = 0; i < n; i++)
        //             {
        //                 foreach (var c in "abcdefghijklmnopqrstuvwxyz")
        //                 {
        //                     var word = item.Substring(0, i) + c + item.Substring(i + 1);
        //                     if (wordSet.Contains(word))
        //                     {
        //                         if (word == endWord)
        //                         {
        //                             leafList.Add(item);
        //                         }
        //                         else
        //                         {
        //                             if (!parentDict.ContainsKey(word))
        //                                 parentDict[word] = new List<string>();
        //                             parentDict[word].Add(item);
        //                             next_level.Add(word);
        //                         }
        //                     }
        //                 }
        //             }
        //         }
        //         if (leafList.Count > 0)
        //             break;
        //         foreach (var word in next_level)
        //             wordSet.Remove(word);

        //         depth += 1;
        //         cur_level.Clear();
        //         foreach (var i in next_level)
        //         {
        //             cur_level.Add(i);
        //         }
        //         next_level.Clear();
        //         // cur_level = next_level;
        //         // next_level = new List<string>();
        //     }
        //     var pathList = new List<IList<string>>();

        //     void GetPath(string node, List<string> path)
        //     {
        //         path.Add(node);
        //         foreach (var p in parentDict[node])
        //         {
        //             var path2 = new List<string>();
        //             path2.AddRange(path);
        //             if (p == null)
        //             {
        //                 path2.Reverse();
        //                 path2.Add(endWord);
        //                 pathList.Add(path2);
        //             }
        //             else
        //             {
        //                 GetPath(p, path2);
        //             }
        //         }
        //     }
        //     foreach (var leaf in leafList)
        //     {
        //         var path = new List<string>();
        //         GetPath(leaf, path);
        //     }
        //     if (pathList.Count == 0)
        //         return new List<IList<string>>();
        //     var minLen = pathList.Select(i => i.Count).Min();
        //     var x = pathList.Where(i => i.Count == minLen).Select(i => string.Join(',', i)).Distinct();
        //     var y = new List<IList<string>>();
        //     foreach (var i in x)
        //     {
        //         y.Add(i.Split(',').ToList());
        //     }
        //     return y;
        // }
    }
    public class Test
    {
        // public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        // {
        //     // if (!wordList.Contains(endWord))
        //     //     return new List<IList<string>>();
        //     // if (beginWord.Length == 1)
        //     // {
        //     //     var res = new List<IList<string>>();
        //     //     res.Add(new List<string> { beginWord, endWord });
        //     //     return res;
        //     // }
        //     var wordSet = new HashSet<string>(wordList.Where(i => i != beginWord));
        //     var cur_level = new List<string> { beginWord };
        //     var next_level = new List<string>();
        //     var depth = 1;
        //     var n = beginWord.Length;
        //     var parentDict = new Dictionary<string, List<string>> { { beginWord, new List<string> { null } } };
        //     var minDepth = wordList.Count + 1;
        //     var leafList = new List<string>();
        //     while (cur_level.Count > 0)
        //     {
        //         foreach (var item in cur_level)
        //         {
        //             for (var i = 0; i < n; i++)
        //             {
        //                 foreach (var c in "abcdefghijklmnopqrstuvwxyz")
        //                 {
        //                     var word = item.Substring(0, i) + c + item.Substring(i + 1);
        //                     if (wordSet.Contains(word))
        //                     {
        //                         if (word == endWord)
        //                         {
        //                             leafList.Add(item);
        //                         }
        //                         else
        //                         {
        //                             if (!parentDict.ContainsKey(word))
        //                                 parentDict[word] = new List<string>();
        //                             parentDict[word].Add(item);
        //                             next_level.Add(word);
        //                         }
        //                     }
        //                 }
        //             }
        //         }
        //         foreach (var word in next_level)
        //             wordSet.Remove(word);

        //         depth += 1;
        //         cur_level.Clear();
        //         foreach (var i in next_level)
        //         {
        //             cur_level.Add(i);
        //         }
        //         next_level.Clear();
        //         // cur_level = next_level;
        //         // next_level = new List<string>();
        //     }
        //     var pathList = new List<IList<string>>();

        //     void GetPath(string node, List<string> path)
        //     {
        //         path.Add(node);
        //         foreach (var p in parentDict[node])
        //         {
        //             var path2 = new List<string>();
        //             path2.AddRange(path);
        //             if (p == null)
        //             {
        //                 path2.Reverse();
        //                 path2.Add(endWord);
        //                 pathList.Add(path2);
        //             }
        //             else
        //             {
        //                 GetPath(p, path2);
        //             }
        //         }
        //     }
        //     foreach (var leaf in leafList)
        //     {
        //         var path = new List<string>();
        //         GetPath(leaf, path);
        //     }
        //     if (pathList.Count == 0)
        //         return new List<IList<string>>();
        //     var minLen = pathList.Select(i => i.Count).Min();
        //     var x = pathList.Where(i => i.Count == minLen).Select(i => string.Join(',', i)).Distinct();
        //     var y = new List<IList<string>>();
        //     foreach (var i in x)
        //     {
        //         y.Add(i.Split(',').ToList());
        //     }
        //     return y;
        // }
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
";
            var lines = input.CleanInput();
            lines = "0126-data.txt".InputFromFile();
            Verify.Method(new Solution(), lines);
        }
    }
}