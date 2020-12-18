using System.Collections.Generic;

namespace Day17
{
    public class LineParser
    {
        public Map Parse(IEnumerable<string> lines)
        {
            return new Map(lines);
        }
    }
}