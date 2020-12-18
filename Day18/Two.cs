using System;
using System.Collections.Generic;

namespace Day18
{
    public class Two : IAnswerGenerator
    {
        private readonly List<string> _assignments;

        public Two(IEnumerable<string> input)
        {
            _assignments = new LineParser().Parse(input);
        }

        public long Generate()
        {
            long result = 0;

            foreach (var assignment in _assignments)
            {
                var expression = new Expression(assignment);

                AddParenthesisAroundAdditions(expression);

                expression = new Expression(expression.ToAssignment());

                var answer = expression.Solve();
                Console.WriteLine(answer);
                result += answer;
            }

            return result;
        }

        private void AddParenthesisAroundAdditions(Expression expression)
        {
            foreach (var child in expression.Childs)
            {
                AddParenthesisAroundAdditions(child);
            }

            expression.Value = AddParenthesisAroundAdditions(expression.Value).Trim();
        }

        private string AddParenthesisAroundAdditions(string expression)
        {
            if (!expression.Contains("+")) return expression;
            
            var result = string.Empty;
            var parts = expression.Split(' ');
            var @operator = string.Empty;
            var parenthesisAdded = 0;
            for (var i = 0; i < parts.Length; i++)
            {
                var part = parts[i];
                var newPart = string.Empty;
                if (int.TryParse(part, out _) || part == "x")
                {
                    if (string.IsNullOrEmpty(@operator))
                    {
                        if (PeekAhead(parts, i) == "+")
                        {
                            newPart += "(";
                            parenthesisAdded++;
                        }

                        newPart += part;
                    }
                    else
                    {
                        if (PeekAhead(parts, i) == "+")
                        {
                            newPart += "(" + part;
                            parenthesisAdded++;
                        }
                        else
                        {
                            newPart += part;

                            while (parenthesisAdded > 0)
                            {
                                newPart += ")";
                                parenthesisAdded--;
                            } 
                        }
                    }
                }
                else if (IsOperator(part))
                {
                    @operator = part;
                    newPart += part;
                }
                else
                {
                    newPart += part;
                }

                result += newPart + " ";
            }

            return result;
        }

        private string PeekAhead(IReadOnlyList<string> parts, int i)
        {
            i++;
            return i >= parts.Count ? string.Empty : parts[i];
        }

        private bool IsOperator(string value)
        {
            return value == "+" || value == "*" || value == "-" || value == "/";
        }
    }
}