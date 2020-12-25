using System;
using System.Collections.Generic;
using System.Linq;

namespace Day25
{
    public class Two : IAnswerGenerator
    {
        private readonly List<long> _instructions;

        public Two(IEnumerable<string> input)
        {
            _instructions = new LineParser().Parse(input).ToList();
        }

        public long Generate()
        {
            return -1;
        }
    }
}