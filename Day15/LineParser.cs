using System;
using System.Collections.Generic;
using System.Linq;

namespace Day15
{
    public class LineParser
    {
        public IEnumerable<int> Parse(IEnumerable<string> lines)
        {
            return lines.First().Split(',').Select(int.Parse);
        }
    }
}