using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4
{
    public class LineParser
    {
        public IEnumerable<InputLine> Parse(IEnumerable<string> lines)
        {
            var combinedLines = string.Empty;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    yield return new InputLine(combinedLines);

                    combinedLines = string.Empty;
                }

                combinedLines += line + " ";
            }
            
            yield return new InputLine(combinedLines);
        }
    }

    public class InputLine
    {
        public InputLine(string line)
        {
            Console.WriteLine(line);

            Fields = new Dictionary<string, string>();

            var pairs = line.Split(' ');
            foreach (var pair in pairs.Where(p => !string.IsNullOrEmpty(p)))
            {
                var field = pair.Split(':')[0];
                var value = pair.Split(':')[1];
                Fields.Add(field, value);
            }
        }

        public Dictionary<string, string> Fields { get; set; }
    }
}