using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MaxPointsOnALine
{
    public class Solution
    {
        public int MaxPoints(int[][] points)
        {
            var pointsCnt = new Dictionary<(int x, int y), int>();
            foreach (var p in points)
            {
                var k = (p[0], p[1]);
                if (!pointsCnt.ContainsKey(k))
                {
                    pointsCnt[k] = 1;
                }
                else
                {
                    pointsCnt[k]++;
                }
            }
            var pointsDistinct = pointsCnt.Keys.OrderBy(k => k.x).ToList();
            if (pointsDistinct.Count == 1)
                return pointsCnt[pointsDistinct.First()];

            var lines = new Dictionary<(decimal a, decimal b), HashSet<(int x, int y)>>();
            for (var i = 0; i < pointsDistinct.Count - 1; i++)
            {
                for (var j = i + 1; j < pointsDistinct.Count; j++)
                {
                    if ((pointsDistinct[i].x == pointsDistinct[j].x)
                    && (pointsDistinct[i].y == pointsDistinct[j].y))
                        continue;

                    var ab = ABofPoints(pointsDistinct[i], pointsDistinct[j]);
                    if (lines.ContainsKey(ab))
                    {
                        // lines[ab].Add(i);
                        // lines[ab].Add(j);
                        lines[ab].Add(pointsDistinct[i]);
                        lines[ab].Add(pointsDistinct[j]);
                    }
                    else
                    {
                        // lines[ab] = new HashSet<int> { i, j };
                        lines[ab] = new HashSet<(int x, int y)> { pointsDistinct[i], pointsDistinct[j] };
                    }
                }
            }
            var max = 0;
            foreach (var i in lines.Keys)
            {
                var ps = 0;
                foreach (var j in lines[i])
                {
                    // var v = pointsDistinct[j];
                    // ps += pointsCnt[v];
                    ps += pointsCnt[j];
                }
                max = Math.Max(max, ps);
            }
            return max;
        }
        (decimal a, decimal b) ABofPoints((int x, int y) p1, (int x, int y) p2)
        {
            // y=ax+b
            var dx = p2.x - p1.x;
            if (dx != 0)
            {
                var dy = p2.y - p1.y;
                decimal a = (decimal)dy / dx;
                decimal b = p1.y - a * p1.x;
                return (a, b);
            }
            else
            {
                return (int.MaxValue, p1.x);
            }
        }
    }

    public class Test
    {
        static public void Run()
        {
            var input = @"
#start line, to avoid removed by CleanInput
[[0,0],[94911151,94911150],[94911152,94911151]]
2
#[[-230,-691], [-184,-551], [-161,-481], [-115,-341], [0,9], [23,79], [60,336], [92,289], [115,359], [135,701], [138,429], [150,774], [207,639], [230,709]]
#12
#[[60,336], [92,289], [115,359], [135,701], [138,429], [150,774]]
#3
[[0,9],[138,429],[115,359],[115,359],[-30,-102],[230,709],[-150,-686],[-135,-613],[-60,-248],[-161,-481],[207,639],[23,79],[-230,-691],[-115,-341],[92,289],[60,336],[-105,-467],[135,701],[-90,-394],[-184,-551],[150,774]]
12
[[1,1],[1,1],[1,1]]
3
[[84,250],[0,0],[1,0],[0,-70],[0,-70],[1,-1],[21,10],[42,90],[-42,-230]]
6
[[3,10],[0,2],[0,2],[3,10]]
4
[[0,0],[1,1],[0,0]]
3
[[1,1],[3,2],[5,3],[4,1],[2,3],[1,4]]
4
[[1,1],[2,2],[3,3]]
3
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines);
        }
    }
}