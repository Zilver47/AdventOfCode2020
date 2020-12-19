using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Day19
{
    public class One : IAnswerGenerator
    {
        private readonly List<Rule> _rules;
        private readonly List<string> _messages;
        private readonly Dictionary<int, HashSet<string>> _parsedRules;

        public One(IEnumerable<string> input)
        {
            _parsedRules = new Dictionary<int, HashSet<string>>();

            //var parsed = new LineParser().Parse(input);
            //_rules = parsed.Item1;
            //_messages = parsed.Item2;
        }

        public long Generate()
        {
            var simpleRules = _rules.Where(r => r.Simple);
            foreach (var simpleRule in simpleRules)
            {
                _parsedRules.Add(simpleRule.Number, new HashSet<string> { simpleRule.Value });
            }

            var remainingRules = _rules.Where(r => !r.Simple).OrderBy(r => r.SubRules.Count);
            while (_parsedRules.Count < _rules.Count)
            {
                foreach (var rule in remainingRules)
                {
                    if (_parsedRules.ContainsKey(rule.Number)) continue;

                    var keys = _parsedRules.Keys.ToList();
                    if (rule.SubRules.All(ruleNumber => keys.Contains(ruleNumber)))
                    {
                        _parsedRules.Add(rule.Number, rule.Parse(_parsedRules));
                    }
                }
            }

            var rule0 = _parsedRules[0];

            var result = 0;
            foreach (var message in _messages)
            {
                if (rule0.Contains(message))
                {
                    result++;
                }
            }
            
            return result;
        }
    }
}