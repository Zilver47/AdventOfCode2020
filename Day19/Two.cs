using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day19
{
    public class Two : IAnswerGenerator
    {
        private readonly Dictionary<int, string> _rules;
        private readonly List<string> _messages;

        public Two(IEnumerable<string> input)
        {
            var parsed = new LineParser().Parse(input);
            _rules = parsed.Item1;
            _messages = parsed.Item2;
        }
		
        public long Generate()
        {
            _rules[8] =
                "42 | 42 (42 | 42 (42 | 42 (42 | 42 (42 | 42 (42 | 42 (42 | 42 (42 | 42 (42 | 42 (42 | 42 (42))))))))))";
			
            _rules[11] =
                "42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 31) 31) 31) 31) 31) 31) 31) 31) 31) 31) 31";

			
            var regex = new Regex(@"\d+", RegexOptions.Compiled);
			var mainRule = _rules[0];
			while (true)
			{
				var match = regex.Match(mainRule);
				if (match.Success)
				{
					var rule = _rules[int.Parse(match.Value)];
                    if (rule.Contains("\""))
					{
						rule = rule[1..^1];
					}
					else
					{
						rule = "(" + rule + ")";
					}

					mainRule = regex.Replace(mainRule, rule, 1);
				}
				else
				{
					break;
				}

				//Console.WriteLine(mainRule);
			}

            mainRule = $"^{mainRule.Replace(" ", "")}$";

			//Console.WriteLine(mainRule);

            return _messages.Count(message => Regex.IsMatch(message, mainRule));
        }
    }
}