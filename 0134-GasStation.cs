using System;
using Xunit;
using Util;
using Node = Util.GraphNode;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace GasStation
{
    public class Solution
    {
        public int CanCompleteCircuit(int[] gas, int[] cost)
        {
            for (var i = 0; i < gas.Length; i++)
            {
                if (TryIndex(gas, cost, i))
                {
                    return i;
                }
            }
            return -1;
        }
        bool TryIndex(int[] gas, int[] cost, int startIdx)
        {
            var tank = 0;
            for (var i = 0; i < gas.Length; i++)
            {
                var gasIdx = startIdx + i;
                if (gasIdx >= gas.Length)
                {
                    gasIdx %= gas.Length;
                }
                tank += gas[gasIdx];
                var costIdx = gasIdx;
                if (tank < cost[costIdx])
                {
                    return false;
                }
                tank -= cost[costIdx];
            }
            return true;
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[1,2,3,4,5]
[3,4,5,1,2]
3
[2,3,4]
[3,4,3]
-1
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}