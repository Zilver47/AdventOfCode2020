using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    public class LineParser
    {
        public List<Instruction> Parse(IEnumerable<string> lines)
        {
            return lines.Select(line => new Instruction(line)).ToList();
        }
    }

    public class Instruction
    {
        public Instruction(string line)
        {
            Action = line[0];
            Value = int.Parse(line.Substring(1));
        }

        public char Action { get; set; }
        public int Value { get; set; }
    }
}