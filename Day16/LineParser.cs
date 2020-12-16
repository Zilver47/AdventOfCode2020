using System;
using System.Collections.Generic;
using System.Linq;

namespace Day16
{
    public class LineParser
    {
        public Tuple<List<FieldRule>, List<Ticket>> Parse(IEnumerable<string> lines)
        {
            var fieldRules = new List<FieldRule>();
            var tickets = new List<Ticket>();

            var rules = true;
            foreach (var line in lines)
            {
                if (line.StartsWith("your ticket"))
                {
                    rules = false;
                }

                if (string.IsNullOrEmpty(line) || line.StartsWith("your") || line.StartsWith("nearby"))
                {
                    continue;
                }

                if (rules)
                {
                    fieldRules.Add(new FieldRule(line));
                }
                else
                {
                    tickets.Add(new Ticket(line));
                }
            }

            return new Tuple<List<FieldRule>, List<Ticket>>(fieldRules, tickets);
        }
    }

    public class FieldRule
    {
        public FieldRule(string line)
        {
            var parts = line.Split(':');
            Field = parts[0];

            ValidValues = new List<int>();

            var valuesPart = parts[1].Trim();
            var firstDash = valuesPart.IndexOf("-");
            var firstRangeStart = valuesPart.Substring(0, firstDash);
            var firstRangeStop = valuesPart.Substring(firstDash + 1, valuesPart.IndexOf(' ', firstDash) - firstDash);
            ValidValues.AddRange(Enumerable.Range(int.Parse(firstRangeStart), int.Parse(firstRangeStop) - int.Parse(firstRangeStart) + 1));

            var secondDash = valuesPart.IndexOf('-', firstDash + 1);
            var secondRangeStart = valuesPart.Substring(valuesPart.IndexOf(' ') + 4, secondDash - valuesPart.IndexOf(' ') - 4);
            var secondRangeStop = valuesPart.Substring(secondDash + 1, valuesPart.Length - 1 - secondDash);
            ValidValues.AddRange(Enumerable.Range(int.Parse(secondRangeStart), int.Parse(secondRangeStop) - int.Parse(secondRangeStart) + 1));
        }

        public string Field { get; }

        public List<int> ValidValues { get; }
    }

    public class Ticket
    {
        public Ticket(string line)
        {
            Values = new List<int>();
            Values.AddRange(line.Split(',').Select(int.Parse));
        }

        public List<int> Values { get; set; }
    }
}