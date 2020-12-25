using System;
using System.Collections.Generic;
using System.Linq;

namespace Day25
{
    public class LineParser
    {
        public IEnumerable<long> Parse(IEnumerable<string> lines)
        {
            return lines.Select(long.Parse);
        }
    }
}