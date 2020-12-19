using System;
using System.Collections.Generic;
using System.Linq;

namespace Day19
{
    public class LineParser
    {
        public Tuple<Dictionary<int, string>, List<string>> Parse(IEnumerable<string> lines)
        {
            var rules = new Dictionary<int, string>();
            var messages = new List<string>();
            foreach (var line in lines)
            {
                if (line.Contains(":"))
                {
                    var parts = line.Split(':');
                    rules.Add(int.Parse(parts[0]), parts[1].Trim());
                }
                else if (line != "")
                {
                    messages.Add(line);
                }
            }

            return new Tuple<Dictionary<int, string>, List<string>>(rules, messages);
        }
    }
}