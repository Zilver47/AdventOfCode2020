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
            Policy = line.Split(':')[0];
            Password = line.Split(':')[1];
        }

        public string Policy { get; set; }
        public string Password { get; set; }
    }
}