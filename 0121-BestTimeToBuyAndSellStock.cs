using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BestTimeToBuyAndSellStock
{
    public class Solution
    {
        public int MaxProfit(int[] prices)
        {
            var maxProfit = 0;
            var buyIdx = 0;
            while (true)
            {
                if (buyIdx > (prices.Length - 1))
                {
                    break;
                }

                var buyed = false;
                for (var i = buyIdx + 1; i < prices.Length; i++)
                {
                    // 上涨前一天买入。即：一直跌就不要买了
                    if (prices[i] > prices[i - 1])
                    {
                        buyed = true;
                        buyIdx = i - 1;
                        break;
                    }
                }
                if (!buyed)
                {
                    // 一直跌！
                    break;
                }

                // 找到买入那天之后的最高价
                var sellIdx = buyIdx;
                var sellPrice = prices[sellIdx];
                var sold = false;
                for (var i = buyIdx + 1; i < prices.Length; i++)
                {
                    if (prices[i] >= sellPrice)
                    {
                        sellIdx = i;
                        sellPrice = prices[sellIdx];
                        sold = true;
                    }
                }
                if (!sold)
                {
                    // 有上涨才买入，至少有一天有收益
                    throw new Exception("should not go here");
                }

                // 从买入那天开始到最高价那天止，看哪天买入收益最高(买入价最低)
                for (var i = buyIdx; i < sellIdx; i++)
                {
                    var profit = sellPrice - prices[i];
                    // profit = profit > 0 ? profit : 0;
                    maxProfit = Math.Max(maxProfit, profit);
                }

                // 试剩下的天数
                buyIdx = sellIdx;
            }
            return maxProfit;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[1,2]
1
[7,1,5,3,6,4]
5
[7,6,4,3,1]
0
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}