using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
    public class One : IAnswerGenerator
    {
        private IEnumerable<InputLine> _input;

        public One(IEnumerable<string> input)
        {
            _input = new LineParser().Parse(input);
        }

        public string Generate()
        {
            var result = 0;
            foreach (var set in _input){
                var policy = ParsePolicy(set.Policy);
                var isValid = IsPasswordValid(policy, set.Password);
                if (isValid)
                {
                    result++;            
                }
            }

            return result.ToString();
        }

        private bool IsPasswordValid(PasswordPolicy policy, string password)
        {
            var occurrences = password.Split(policy.Character).Length - 1;
            return occurrences >= policy.MinimalOccurrence && occurrences <= policy.MaximumOccurrence;
        }

        private PasswordPolicy ParsePolicy(string policy)
        {
            var result = new PasswordPolicy();
            var indexOfDash = policy.IndexOf('-');
            var indexOfSpace = policy.IndexOf(' ');
            result.MinimalOccurrence = int.Parse(policy.Substring(0, indexOfDash));
            result.MaximumOccurrence = int.Parse(policy.Substring(indexOfDash + 1, indexOfSpace - indexOfDash - 1));
            result.Character = char.Parse(policy.Substring(indexOfSpace + 1, 1));

            return result;
        }

        private class PasswordPolicy
        {
            public int MinimalOccurrence { get; set; }
            public int MaximumOccurrence { get; set; }
            public char Character { get; set; }
        }
    }
}