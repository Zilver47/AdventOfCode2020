using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    public class LineParser
    {
        public IEnumerable<Program> Parse(IEnumerable<string> lines)
        {
            var result = new List<Program>();
            var program = new Program("xxxxxxx");
            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    result.Add(program);
                    program = new Program(line);
                    continue;
                }

                var position = int.Parse(line.Substring(line.IndexOf("[") + 2, line.IndexOf("]") - line.IndexOf("[") - 1));
                var value = int.Parse(line.Substring(line.IndexOf("=") + 2));

                if (program.Transformations.ContainsKey(position))
                {
                    program.Transformations[position] = value;
                }
                else
                {
                    program.Transformations.Add(position, value);
                }
            }

            result.RemoveAt(0);
            result.Add(program);

            return result;
        }
    }

    public class Program
    {
        public Program(string line)
        {
            Transformations = new Dictionary<int, int>();

            Mask = line.Substring(7);
        }

        public string Mask { get; set; }

        public Dictionary<int, int> Transformations { get; set; }
    }
}