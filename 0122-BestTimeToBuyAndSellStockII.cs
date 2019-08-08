using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BestTimeToBuyAndSellStockII
{
    public class Solution
    {
        public int MaxProfit(int[] prices)
        {
            var sumProfit = 0;
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
                    break;
                }

                var sellIdx = buyIdx;
                var sold = false;
                for (var i = buyIdx + 2; i < prices.Length; i++)
                {
                    // 下跌前一天卖出。即：一直涨就不要卖
                    if (prices[i] < prices[i - 1])
                    {
                        sold = true;
                        sellIdx = i - 1;
                        break;
                    }
                }
                if (!sold)
                {
                    // 一直涨，就最后一天卖出
                    sellIdx = prices.Length - 1;
                }

                var profit = prices[sellIdx] - prices[buyIdx];
                // profit = profit > 0 ? profit : 0;
                sumProfit += profit;

                // 试剩下的天数
                buyIdx = sellIdx;
            }
            return sumProfit;
        }
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[7,1,5,3,6,4]
7
[1,2,3,4,5]
4
[7,6,4,3,1]
0
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}