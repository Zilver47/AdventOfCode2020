using System.Collections.Generic;

namespace Day3
{
    public class LineParser
    {
        public IEnumerable<InputLine> Parse(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                yield return new InputLine(line);
            }
        }
    }

    public class InputLine
    {
        public InputLine(string line)
        {
            Columns = new List<bool>();
            foreach (var character in line)
            {
                Columns.Add(character == '#');
            }
        }

        public List<bool> Columns { get; set; }
    }
}