using System.Collections.Generic;

namespace Day18
{
    public class Expression
    {
        public Expression Parent { get; }
        public List<Expression> Childs { get; }

        public string Value { get; set; }
        
        public Expression(string assigment)
        {
            Childs = new List<Expression>();

            Parse(assigment);
        }

        public Expression(Expression parent)
        {
            Parent = parent;
            Childs = new List<Expression>();
        }

        private void Parse(string assigment)
        {
            var currentExpression = this;
            foreach (var character in assigment)
            {
                switch (character)
                {
                    case '(':
                    {
                        currentExpression.Value += "x";

                        var childExpression = new Expression(currentExpression);
                        currentExpression.Childs.Add(childExpression);
                        currentExpression = childExpression;
                        break;
                    }
                    case ')':
                        currentExpression = currentExpression.Parent;
                        break;
                    default:
                        currentExpression.Value += character;
                        break;
                }
            }
        }
        
        public long Solve()
        {
            long result = 0;

            var parts = Value.Split(' ');
            var @operator = string.Empty;
            var childIndex = -1;
            foreach (var part in parts)
            {
                if (int.TryParse(part, out var number))
                {
                    if (string.IsNullOrEmpty(@operator))
                    {
                        result = number;
                    }
                    else
                    {
                        result = Solve(result, @operator, number);
                    }
                }
                else if (IsOperator(part))
                {
                    @operator = part;
                }
                else if (part == "x")
                {
                    var subExpressionResult = Childs[++childIndex].Solve();
                    if (string.IsNullOrEmpty(@operator))
                    {
                        result = subExpressionResult;
                    }
                    else
                    {
                        result = Solve(result, @operator, subExpressionResult);
                    }
                }
            }

            return result;
        }

        private bool IsOperator(string value)
        {
            return value == "+" || value == "*" || value == "-" || value == "/";
        }

        private long Solve(long valueA, string @operator, long valueB)
        {
            return @operator switch
            {
                "+" => valueA + valueB,
                "-" => valueA - valueB,
                "*" => valueA * valueB,
                "/" => valueA / valueB,
                _ => 0
            };
        }

        public string ToAssignment()
        {
            var result = string.Empty;
            var parts = Value.Split(' ');
            var childIndex = -1;
            foreach (var part in parts)
            {
                if (part.Contains("x"))
                {
                    result += part.Replace("x", $"({Childs[++childIndex].ToAssignment()})");
                }
                else
                {
                    result += part;
                }

                result += " ";
            }

            return result;
        }
    }
}