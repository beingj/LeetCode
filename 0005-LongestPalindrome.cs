using System;
using Xunit;
using Util;

namespace LongestPalindrome
{
    public class Solution
    {
        public string LongestPalindrome(string s)
        {
            int max = s.Length;
            int idx = 0, len = 0;
            if (IsSame(s))
                return s;

            for (var i = 0; i < max; i++)
            {
                for (var j = i; j < max; j++)
                {
                    int l = j - i + 1;
                    if (IsPalindrome(s, i, l))
                    {
                        if (l > len)
                        {
                            idx = i;
                            len = l;
                        }
                    }
                }
            }
            return s.Substring(idx, len);
        }
        public static Boolean IsPalindrome(string s, int idx = 0, int len = 0)
        {
            int half = 0;
            if (len == 0)
                len = s.Length;
            int max = idx + len - 1;
            if (len % 2 == 0)
            {
                half = len / 2;
            }
            else
            {
                half = (len - 1) / 2;
            }
            for (var i = 0; i < half; i++)
            {
                if (s[idx + i] != s[max - i])
                    return false;
            }
            return true;
        }
        public static Boolean IsSame(string s)
        {
            if (s.Length == 0)
                return true;
            char c = s[0];
            for (var i = 1; i < s.Length; i++)
            {
                if (s[i] != c)
                    return false;
            }
            return true;
        }
    }
    public class Test
    {
        static public void Run()
        {
            string Input;
            string exp, res;

            Console.WriteLine("LongestPalindrome");

            Assert.True(Solution.IsPalindrome("bab", 0, 3));
            Assert.True(Solution.IsPalindrome("baab", 0, 4));
            Assert.False(Solution.IsPalindrome("bba", 0, 3));
            Assert.False(Solution.IsPalindrome("baba", 0, 4));

            Assert.True(Solution.IsPalindrome("bab"));
            Assert.True(Solution.IsPalindrome("baab"));
            Assert.False(Solution.IsPalindrome("bba"));
            Assert.False(Solution.IsPalindrome("baba"));

            Input = "babad";
            exp = "bab";
            res = new Solution().LongestPalindrome(Input);
            Assert.Equal(exp, res);

            Input = "a";
            exp = "a";
            res = new Solution().LongestPalindrome(Input);
            Assert.Equal(exp, res);

            Input = "bb";
            exp = "bb";
            res = new Solution().LongestPalindrome(Input);
            Assert.Equal(exp, res);

            Input = "ac";
            exp = "a";
            res = new Solution().LongestPalindrome(Input);
            Assert.Equal(exp, res);

            Input = "ccc";
            exp = "ccc";
            res = new Solution().LongestPalindrome(Input);
            Assert.Equal(exp, res);

            using (new Timeit())
            {
                Input = "miycvxmqggnmmcwlmizfojwrurwhwygwfykyefxbgveixykdebenzitqnciigfjgrzzbtgeazyrbiirmejhdwcgjzwqolrturjlqpsgunuqerqjevbheblmbvgxyedxshswsokbhzapfuojgyfhctlaifrisgzqlczageirnukgnmnbwogknyyuynwsuwbumdmoqwxprykmazghcpmkdcjduepjmjdxrhvixxbfvhybjdpvwjbarmbqypsylgtzyuiqkexgvirzylydrhrmuwpmfkvqllqvekyojoacvyrzjevaupypfrdguhukzuqojolvycgpjaendfetkgtojepelhcltorueawwjpltehbbjrvznxhahtuaeuairvuklctuhcyzomwrrznrcqmovanxmiyilefybkbveesrxkmqrqkowyrimuejqtikcjfhizsmumajbqglxrvevexnleflocxoqgoyrzgqflwiknntdcykuvdcpzlakljidclhkllftxpinpvbngtexngdtntunzgahuvfnqjedcafzouopiixw";
                exp = "vqllqv";
                res = new Solution().LongestPalindrome(Input);
                // Console.WriteLine(res);
                Assert.Equal(exp, res);
            }
            using (new Timeit())
            {
                res = new Solution().LongestPalindrome(Input);
                // Console.WriteLine(res);
                Assert.Equal(exp, res);
            }

            using (new Timeit())
            {
                Input = "1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111";
                exp = Input;
                res = new Solution().LongestPalindrome(Input);
                // Console.WriteLine(res);
            }
            using (new Timeit())
            {
                res = new Solution().LongestPalindrome(Input);
                // Console.WriteLine(res);
                Assert.Equal(exp, res);
            }
        }
    }

}