using System;
using Xunit;
using Util;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace SubstringWithConcatenationOfAllWords
{
    public class Solution
    {
        public IList<int> FindSubstring(string s, string[] words)
        {
            if (words.Length == 0)
            {
                return new List<int>();
            }
            int combLen = words[0].Length * words.Length;
            if (s.Length < combLen)
            {
                return new List<int>();
            }
            int wl = words[0].Length;
            Dictionary<string, int> dtotal = new Dictionary<string, int>();
            Dictionary<string, int> dcount = new Dictionary<string, int>();
            List<string> uniWords = new List<string>();
            foreach (var w in words)
            {
                if (!uniWords.Contains(w))
                    uniWords.Add(w);
            }
            foreach (var w in words)
            {
                if (!dtotal.ContainsKey(w))
                {
                    dcount.Add(w, 0);
                    dtotal.Add(w, 1);
                }
                else
                {
                    dtotal[w] = dtotal[w] + 1;
                }
            }

            var res = new List<int>();
            var maxIdx = s.Length - combLen;
            for (var i = 0; i <= maxIdx; i++)
            {
                var sub = s.Substring(i, combLen);
                if (Find(sub, uniWords, dtotal, dcount, combLen, wl))
                {
                    res.Add(i);
                }
            }
            return res;
        }
        public static bool Find(string s, List<string> words, Dictionary<string, int> dtotal, Dictionary<string, int> dcount, int cl, int wl)
        {
            foreach (var w in words)
            {
                dcount[w] = 0;
            }
            for (var i = 0; i < s.Length; i += wl)
            {
                var sub = s.Substring(i, wl);
                if (!dcount.ContainsKey(sub))
                {
                    return false;
                }
                else
                {
                    if (dcount[sub] > dtotal[sub])
                    {
                        return false;
                    }
                    dcount[sub] = dcount[sub] + 1;
                }
            }
            foreach (var w in words)
            {
                if (dcount[w] != dtotal[w])
                    return false;
            }
            return true;
        }
    }
    public class Test
    {
        static public void Run()
        {
            Console.WriteLine("SubstringWithConcatenationOfAllWords");
            // return;
            string s;
            string[] words;
            List<int> exp, res;

            s = "barfoothefoobarman";
            words = new string[] { "foo", "bar" };
            exp = new List<int> { 0, 9 };
            using (new Timeit())
            {
                res = (List<int>)new Solution().FindSubstring(s, words);
            }
            Assert.Equal(exp, res);
            using (new Timeit())
            {
                res = (List<int>)new Solution().FindSubstring(s, words);
            }
            Assert.Equal(exp, res);

            s = "pjzkrkevzztxductzzxmxsvwjkxpvukmfjywwetvfnujhweiybwvvsrfequzkhossmootkmyxgjgfordrpapjuunmqnxxdrqrfgkrsjqbszgiqlcfnrpjlcwdrvbumtotzylshdvccdmsqoadfrpsvnwpizlwszrtyclhgilklydbmfhuywotjmktnwrfvizvnmfvvqfiokkdprznnnjycttprkxpuykhmpchiksyucbmtabiqkisgbhxngmhezrrqvayfsxauampdpxtafniiwfvdufhtwajrbkxtjzqjnfocdhekumttuqwovfjrgulhekcpjszyynadxhnttgmnxkduqmmyhzfnjhducesctufqbumxbamalqudeibljgbspeotkgvddcwgxidaiqcvgwykhbysjzlzfbupkqunuqtraxrlptivshhbihtsigtpipguhbhctcvubnhqipncyxfjebdnjyetnlnvmuxhzsdahkrscewabejifmxombiamxvauuitoltyymsarqcuuoezcbqpdaprxmsrickwpgwpsoplhugbikbkotzrtqkscekkgwjycfnvwfgdzogjzjvpcvixnsqsxacfwndzvrwrycwxrcismdhqapoojegggkocyrdtkzmiekhxoppctytvphjynrhtcvxcobxbcjjivtfjiwmduhzjokkbctweqtigwfhzorjlkpuuliaipbtfldinyetoybvugevwvhhhweejogrghllsouipabfafcxnhukcbtmxzshoyyufjhzadhrelweszbfgwpkzlwxkogyogutscvuhcllphshivnoteztpxsaoaacgxyaztuixhunrowzljqfqrahosheukhahhbiaxqzfmmwcjxountkevsvpbzjnilwpoermxrtlfroqoclexxisrdhvfsindffslyekrzwzqkpeocilatftymodgztjgybtyheqgcpwogdcjlnlesefgvimwbxcbzvaibspdjnrpqtyeilkcspknyylbwndvkffmzuriilxagyerjptbgeqgebiaqnvdubrtxibhvakcyotkfonmseszhczapxdlauexehhaireihxsplgdgmxfvaevrbadbwjbdrkfbbjjkgcztkcbwagtcnrtqryuqixtzhaakjlurnumzyovawrcjiwabuwretmdamfkxrgqgcdgbrdbnugzecbgyxxdqmisaqcyjkqrntxqmdrczxbebemcblftxplafnyoxqimkhcykwamvdsxjezkpgdpvopddptdfbprjustquhlazkjfluxrzopqdstulybnqvyknrchbphcarknnhhovweaqawdyxsqsqahkepluypwrzjegqtdoxfgzdkydeoxvrfhxusrujnmjzqrrlxglcmkiykldbiasnhrjbjekystzilrwkzhontwmehrfsrzfaqrbbxncphbzuuxeteshyrveamjsfiaharkcqxefghgceeixkdgkuboupxnwhnfigpkwnqdvzlydpidcljmflbccarbiegsmweklwngvygbqpescpeichmfidgsjmkvkofvkuehsmkkbocgejoiqcnafvuokelwuqsgkyoekaroptuvekfvmtxtqshcwsztkrzwrpabqrrhnlerxjojemcxel";
            words = new string[] { "dhvf", "sind", "ffsl", "yekr", "zwzq", "kpeo", "cila", "tfty", "modg", "ztjg", "ybty", "heqg", "cpwo", "gdcj", "lnle", "sefg", "vimw", "bxcb" };
            Console.WriteLine(words.Length);
            // return;
            exp = new List<int> { 0, 9 };
            using (new Timeit())
            {
                res = (List<int>)new Solution().FindSubstring(s, words);
            }
            Console.WriteLine(string.Join(",", res));
            // Assert.Equal(exp, res);

            s = "wordgoodgoodgoodbestword";
            words = new string[] { "word", "good", "best", "word" };
            using (new Timeit())
            {
                res = (List<int>)new Solution().FindSubstring(s, words);
            }

            var sb = new StringBuilder();
            // s.Length==10000
            for (int i = 0; i < 5000; i++)
            {
                sb.Append("ab");
            }
            s = sb.ToString();
            words = new string[200];
            // words.Length ==200
            for (int i = 0; i < 100; i++)
            {
                words[2 * i] = "ab";
                words[2 * i + 1] = "ba";
            }
            // Console.WriteLine(s);
            // Console.WriteLine(string.Join(",", words));
            using (new Timeit())
            {
                res = (List<int>)new Solution().FindSubstring(s, words);
            }
            Console.WriteLine(res.Count);
        }
    }
}