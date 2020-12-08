using System;
using System.Collections.Generic;
using System.Linq;

namespace Day8
{
    public class LineParser
    {
        public List<Instruction> Parse(IEnumerable<string> lines)
        {
            return lines.Select(line => new Instruction(line)).ToList();
        }
    }

    public class Instruction : ICloneable
    {
        public Instruction(string line)
        {
            var parts = line.Split(' ');
            Operation = parts[0];
            Argument = int.Parse(parts[1]);
        }

        public string Operation { get; set; }

        public int Argument { get; set; }

        public object Clone()
        {
            return new Instruction($"{Operation} {Argument}");
        }
    }
}