using System;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    public class LineParser
    {
        public Tuple<int, List<string>> Parse(IEnumerable<string> lines)
        {
            var timestamp = int.Parse(lines.ToList()[0]);
            var busses = lines.ToList()[1].Split(',');

            return new Tuple<int, List<string>>(timestamp, busses.ToList());
        }
    }
}