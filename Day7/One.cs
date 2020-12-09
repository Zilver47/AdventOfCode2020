using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    public class AnswerGenerator : IAnswerGenerator
    {
        private readonly Dictionary<string, Rule> _rules;

        public AnswerGenerator(IEnumerable<string> input)
        {
            _rules = new LineParser().Parse(input);
        }

        public long Generate()
        {
            // Part 1
            var result = CountNumberOfBags("shiny gold");

            // Part 2
            var rule = _rules["shiny gold"];
            result = DeepCount(rule) - 1;

            return result;
        }

        private int CountNumberOfBags(string value)
        {
            return _rules.Sum(rule => CanContainBag(rule.Value.ContainingBags, value) ? 1 : 0);
        }

        private bool CanContainBag(Dictionary<string, int> source, string value)
        {
            return source.Keys.Any(bag => bag == value || CanContainBag(_rules[bag].ContainingBags, value));
        }

        private int DeepCount(Rule source)
        {
            var result = 1;
            foreach (var bag in source.ContainingBags)
            {
                var bagRule = _rules[bag.Key];
                var numberOfContainingBags = DeepCount(bagRule);
                result += numberOfContainingBags * bag.Value;
            }

            return result;
        }
    }
}