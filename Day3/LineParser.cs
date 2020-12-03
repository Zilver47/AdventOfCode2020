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
            Tiles = new List<bool>();
            foreach (var character in line)
            {
                Tiles.Add(character == '#');
            }
        }

        public List<bool> Tiles { get; set; }
    }
}