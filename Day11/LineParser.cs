using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Day11
{
    public class LineParser
    {
        public Map Parse(IEnumerable<string> lines)
        {
            return new Map(lines.ToArray());
        }
    }

    public class InputLine
    {
        public InputLine()
        {
            Columns = new List<char>(10); 
        }

        public InputLine(string line)
        {
            Columns = new List<char>();
            foreach (var character in line)
            {
                Columns.Add(character);
            }
        }

        public List<char> Columns { get; set; }
    }
}