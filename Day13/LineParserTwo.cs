using System;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    public class LineParserTwo
    {
        public List<List<string>> Parse(IEnumerable<string> lines)
        {
            var result = new List<List<string>>();
            foreach (var line in lines)
            {
                var busses = line.Split(',');
                result.Add(busses.ToList());
            }

            return result;
        }
    }
}