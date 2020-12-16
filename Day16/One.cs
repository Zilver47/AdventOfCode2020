using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Day16
{
    public class One : IAnswerGenerator
    {
        private readonly List<FieldRule> _rules;
        private readonly List<Ticket> _tickets;

        public One(IEnumerable<string> input)
        {
            var parsedInput = new LineParser().Parse(input);
            _rules = parsedInput.Item1;
            _tickets = parsedInput.Item2;
        }

        public long Generate()
        {
            var invalids = new List<int>();
            foreach (var ticket in _tickets.GetRange(1, _tickets.Count - 1))
            {
                invalids.AddRange(ticket.Values.Where(value => !IsValid(value)));
            }

            return invalids.Sum(x => x);
        }

        private bool IsValid(int value)
        {
            return _rules.Any(rule => rule.ValidValues.Contains(value));
        }
    }
}