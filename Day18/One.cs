using System;
using System.Collections.Generic;

namespace Day18
{
    public class One : IAnswerGenerator
    {
        private readonly List<string> _assignments;

        public One(IEnumerable<string> input)
        {
            _assignments = new LineParser().Parse(input);
        }

        public long Generate()
        {
            long result = 0;

            foreach (var assignment in _assignments)
            {
                var expression = new Expression(assignment);

                var answer = expression.Solve();
                Console.WriteLine(answer);
                result += answer;
            }

            return result;
        }
    }
}