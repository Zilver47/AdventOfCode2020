using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Day16
{
    public class Two : IAnswerGenerator
    {
        private readonly List<FieldRule> _rules;
        private readonly List<Ticket> _tickets;

        public Two(IEnumerable<string> input)
        {
            var parsedInput = new LineParser().Parse(input);
            _rules = parsedInput.Item1;
            _tickets = parsedInput.Item2;
        }

        public long Generate()
        {
            var validTickets = GetValidTickets();

            var fieldPositions = FindFields(validTickets);

            var result = MultiplyDepartureFieldsForMyTicket(fieldPositions);

            return result;
        }

        private List<Ticket> GetValidTickets()
        {
            var validTickets = new List<Ticket>();
            foreach (var ticket in _tickets.GetRange(1, _tickets.Count - 1))
            {
                var hasInvalidValue = false;

                foreach (var value in ticket.Values.Where(value => !IsValid(value)))
                {
                    hasInvalidValue = true;
                }

                if (!hasInvalidValue)
                {
                    validTickets.Add(ticket);
                }
            }

            return validTickets;
        }

        private Dictionary<string, int> FindFields(IReadOnlyCollection<Ticket> validTickets)
        {
            var fieldPositions = _rules.ToDictionary(rule => rule.Field, rule => -1);

            var columns = new List<List<int>>();
            for (var i = 0; i < _rules.Count; i++)
            {
                columns.Add(validTickets.Select(t => t.Values[i]).ToList());
            }

            var remainingRules = _rules;
            while (true)
            {
                for (var i = 0; i < columns.Count; i++)
                {
                    var columnValues = columns[i];
                    var rules = remainingRules.Where(rule => columnValues.All(v => rule.ValidValues.Contains(v)));
                    if (rules.Count() == 1)
                    {
                        fieldPositions[rules.First().Field] = i;

                        remainingRules.Remove(rules.First());
                    }
                }

                if (remainingRules.Count == 0)
                {
                    break;
                }
            }

            return fieldPositions;
        }

        private long MultiplyDepartureFieldsForMyTicket(Dictionary<string, int> fieldPositions)
        {
            var myTicket = _tickets.First();
            long result = 1;
            foreach (var fieldPosition in fieldPositions)
            {
                if (fieldPosition.Key.StartsWith("departure"))
                {
                    result *= myTicket.Values[fieldPosition.Value];
                }
            }

            return result;
        }

        private bool IsValid(int value)
        {
            return _rules.Any(rule => rule.ValidValues.Contains(value));
        }
    }
}