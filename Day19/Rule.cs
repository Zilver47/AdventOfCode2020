using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Extensions;

namespace Day19
{
    public class Rule
    {
        public int Number { get; set; }
        public string Value { get; set; }

        public bool Simple { get; set; }
        public List<int> SubRules = new List<int>();

        public Rule(string line)
        {
            var parts = line.Split(':');
            Number = int.Parse(parts[0]);
            Value = parts[1].Trim();

            if (Value.Contains("\""))
            {
                Simple = true;
                Value = Value.Replace("\"", string.Empty);
            }

            var numbers = Value.Replace("|", string.Empty).Split(' ');
            foreach (var part in numbers)
            {
                if (int.TryParse(part, out var number))
                {
                    SubRules.Add(number);
                }
            }
        }

        public HashSet<string> Parse(Dictionary<int, HashSet<string>> rules)
        {
            var result = new HashSet<string>();
            var sections = Value.Split('|');
            foreach (var section in sections)
            {
                var parsedSection = new List<string>();
                parsedSection.Add(string.Empty);

                var parts = section.Trim().Split(' ');
                foreach (var part in parts)
                {
                    var number = int.Parse(part);
                    var parsedPart = rules[number];

                    var clone = parsedSection.Clone();
                    for (var i = 1; i < parsedPart.Count; i++)
                    {
                        parsedSection.AddRange(clone.Reverse());
                    }

                    parsedSection = parsedSection.OrderBy(x => x).ToList();

                    var index = 0;
                    while (index < parsedSection.Count)
                    {
                        foreach (var addition in parsedPart)
                        {
                            parsedSection[index] += addition;
                            index++;
                        }
                    }
                }

                foreach (var x in parsedSection.Where(x => !result.Contains(x)))
                {
                    result.Add(x);
                }
            }

            return result;
        }
    }
}