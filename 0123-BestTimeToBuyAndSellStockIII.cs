using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BestTimeToBuyAndSellStockIII
{
    public class Solution
    {
        public int MaxProfit(int[] prices)
        {
            var maxProfit = 0;
            var buyIdx = 0;
            var cache = new Dictionary<int, int>();
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

                var sellIdxList = new List<int>();
                for (var i = buyIdx + 2; i < prices.Length; i++)
                {
                    // 下跌前一天卖出。即：一直涨就不要卖
                    if (prices[i] < prices[i - 1])
                    {
                        sellIdxList.Add(i - 1);
                    }
                }
                if (sellIdxList.Count == 0)
                {
                    // 一直涨，就最后一天卖出
                    sellIdxList.Add(prices.Length - 1);
                }

                var bestSellIdx = sellIdxList.First();
                var bestProfit = 0;
                foreach (var sellIdx in sellIdxList)
                {
                    var profit1 = prices[sellIdx] - prices[buyIdx];
                    var profit2 = CachedMaxProfitOne(prices, sellIdx + 1, cache);
                    var profit = profit1 + profit2;
                    if (profit > bestProfit)
                    {
                        bestSellIdx = sellIdx;
                        bestProfit = profit;
                    }
                }
                maxProfit = Math.Max(maxProfit, bestProfit);

                // 试下一天
                buyIdx += 1;
            }
            return maxProfit;
        }
        int CachedMaxProfitOne(int[] prices, int startIdx, Dictionary<int, int> cache)
        {
            var k = startIdx;
            if (!cache.ContainsKey(k))
                cache[k] = MaxProfitOne(prices, startIdx);
            return cache[k];
        }
        int MaxProfitOne(int[] prices, int startIdx)
        {
            // copy from 0121-BestTimeToBuyAndSellStock.cs
            var maxProfit = 0;
            var buyIdx = startIdx;
            while (true)
            {
                if (buyIdx > (prices.Length - 1))
                {
                    break;
                }

                var buyed = false;
                for (var i = buyIdx + 1; i < prices.Length; i++)
                {
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
                    throw new Exception("should not go here");
                }

                for (var i = buyIdx; i < sellIdx; i++)
                {
                    var profit = sellPrice - prices[i];
                    maxProfit = Math.Max(maxProfit, profit);
                }

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
[5,2,3,0,3,5,6,8,1,5]
12
[1,2,4,2,5,7,2,4,9,0]
13
[3,3,5,0,0,3,1,4]
6
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