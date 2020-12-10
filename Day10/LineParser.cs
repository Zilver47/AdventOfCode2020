using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    public class LineParser
    {
        public List<long> Parse(IEnumerable<string> lines)
        {
            return lines.Select(long.Parse).ToList();
        }
    }
}