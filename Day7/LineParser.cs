using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    public class LineParser
    {
        public Dictionary<string, Rule> Parse(IEnumerable<string> lines)
        {
            var result = new Dictionary<string, Rule>();
            foreach (var line in lines)
            {
                var parts = line.Split(" bags contain ");
                result.Add(parts[0], new Rule(line));
            }

            return result;
        }
    }

    public class Rule
    {
        public Rule(string line)
        {
            ContainingBags = new Dictionary<string, int>();

            var parts = line.Split(" bags contain ");
            Bag = parts[0];
            if (parts[1] == "no other bags.") return;

            var bagDescriptions = parts[1].Split(',');
            foreach (var bagDescription in bagDescriptions)
            {
                var bag = bagDescription
                    .Replace(" bags", string.Empty)
                    .Replace(" bag", string.Empty)
                    .Replace(".", string.Empty)
                    .Trim();

                var numberOf = int.Parse(bag.Split(' ')[0]);
                bag = bag.Substring(2);
                ContainingBags.Add(bag, numberOf);
            }
        }

        public string Bag { get; set; }

        public Dictionary<string, int> ContainingBags { get; set; }
    }
}