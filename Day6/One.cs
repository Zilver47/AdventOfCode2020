using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    public class One : IAnswerGenerator
    {
        private readonly List<Group> _groups;

        public One(IEnumerable<string> input)
        {
            _groups = new LineParser().Parse(input).ToList();
        }

        public long Generate()
        {
            var result = _groups.Aggregate<Group, long>(0, (current, group) => 
                current + group.CountPositiveAnswersByEveryOne());

            return result;
        }
    }
}