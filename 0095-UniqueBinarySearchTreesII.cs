using System;
using Xunit;
using Util;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UniqueBinarySearchTreesII
{
    public class Solution
    {
        // TODO: 95
    }
    public class Test
    {
        static public void Run()
        {
            var input = @"
[1,null,2,3]
[1,3,2]
";
            var lines = input.CleanInput();
            Verify.Method(new Solution(), lines, sortRet: true);
        }
    }
}