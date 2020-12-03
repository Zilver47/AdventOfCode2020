using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
    public class Two : IAnswerGenerator
    {
        private IEnumerable<InputLine> _input;

        public Two(IEnumerable<string> input)
        {
            _input = new LineParser().Parse(input);
        }

        public string Generate()
        {

            return string.Empty;
        }
    }
}