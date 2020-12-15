using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Day15
{
    public class One : IAnswerGenerator
    {
        private readonly List<int> _numbers;

        public One(IEnumerable<string> input)
        {
            _numbers = new LineParser().Parse(input).ToList();
        }

        public long Generate()
        {
            for (var i = _numbers.Count; i < 2020; i++)
            {
                var newNumber = 0;

                var lastNumber = _numbers.Last();
                if (_numbers.Count(n => n == lastNumber) > 1)
                {
                    var firstIndex = _numbers.FindLastIndex(_numbers.Count - 1, x => x == lastNumber);
                    var secondIndex = _numbers.FindLastIndex(firstIndex - 1, x => x == lastNumber);

                    newNumber = firstIndex - secondIndex;
                }

                _numbers.Add(newNumber);
            }


            return _numbers.Last();
        }
    }
}