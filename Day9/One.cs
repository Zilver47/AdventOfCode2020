using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Extensions;

namespace Day9
{
    public class AnswerGenerator : IAnswerGenerator
    {
        private readonly List<long> _data;

        public AnswerGenerator(IEnumerable<string> input)
        {
            _data = new LineParser().Parse(input);
        }

        public string Generate()
        {
            var preamble = 25;
            var invalidNumber = GetInvalidNumber(preamble);

            var set = FindContiguousSet(invalidNumber);
            return (set.Min() + set.Max()).ToString();
        }

        private long GetInvalidNumber(int preamble)
        {
            long invalidNumber = 0;
            for (var i = preamble; i < _data.Count; i++)
            {
                var previous = _data.GetRange(i - preamble, preamble);
                if (!IsInvalid(previous, _data[i]))
                {
                    invalidNumber = _data[i];
                }
            }

            return invalidNumber;
        }

        private bool IsInvalid(List<long> previous, long value)
        {
            foreach (var numberOne in previous)
            {
                if (previous.Where(n => n != numberOne)
                    .Any(numberTwo => numberOne + numberTwo == value))
                {
                    return true;
                }
            }

            return false;
        }

        private List<long> FindContiguousSet(long value)
        {
            for (var i = 0; i < _data.Count; i++)
            {
                var numberOne = _data[i];
                var setTotal = numberOne;
                var set = new List<long> { numberOne };
                
                for (var k = i + 1; k < _data.Count; k++)
                {
                    var otherNumber = _data[k];
                    setTotal += otherNumber;
                    set.Add(otherNumber);

                    if (setTotal == value)
                    {
                        return set;
                    }

                    if (setTotal > value)
                    {
                        break;
                    }
                }
            }

            return Array.Empty<long>().ToList();
        }
    }
}