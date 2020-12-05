using System;
using System.Collections.Generic;
using System.Linq;

namespace Day5
{
    public class LineParser
    {
        public IEnumerable<SeatSpecification> Parse(IEnumerable<string> lines)
        {
            return lines.Select(line => new SeatSpecification(line));
        }
    }

    public class SeatSpecification
    {
        public SeatSpecification(string specification)
        {
            Rows = new List<char>();
            Columns = new List<char>();

            Rows.AddRange(specification.Substring(0, 7).ToCharArray());
            Columns.AddRange(specification.Substring(7, 3).ToCharArray());
        }

        public List<char> Rows { get; set; }

        public List<char> Columns { get; set; }
    }
}