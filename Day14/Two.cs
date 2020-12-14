using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AdventOfCode.Extensions;

namespace Day14
{
    public class Two : IAnswerGenerator
    {
        private readonly List<Program> _programs;
        private string _mask;
        private readonly Dictionary<long, long> _value;

        public Two(IEnumerable<string> input)
        {
            _value = new Dictionary<long, long>();
            _programs = new LineParser().Parse(input).ToList();
        }

        public long Generate()
        {
            foreach (var program in _programs)
            {
                _mask = program.Mask;
                foreach (var transformation in program.Transformations)
                {
                    var addresses = Transform(transformation.Key);
                    foreach (var address in addresses)
                    {
                        _value[address] = transformation.Value;
                    }
                }
            }

            return _value.Values.Sum(v => v);
        }

        public List<long> Transform(int value)
        {
            var bits = ConvertToBits(value);

            var result = new List<char>();
            for (var i = 0; i < _mask.Length; i++)
            {
                var bit = i >= bits.Length ? '0' : bits[bits.Length - i - 1];
                var bitmask = _mask[_mask.Length - i - 1];
                result.Add(bitmask == '0' ? bit : bitmask);
            }

            return Permutate(result);
        }

        private List<long> Permutate(IEnumerable<char> value)
        {
            var result = new List<List<string>>();
            result.Add(new List<string>());

            foreach (var character in value)
            {
                if (character == 'X')
                {
                    var newMutations = new List<List<string>>();
                    foreach (var mutation in result)
                    {
                        var newMutation = mutation.Clone();
                        newMutation.Add("1");
                        newMutations.Add(newMutation.ToList());

                        mutation.Add("0");
                    }

                    result.AddRange(newMutations);
                }
                else
                {
                    foreach (var mutation in result)
                    {
                        mutation.Add(character.ToString());
                    }
                }
            }

            return result.Select(x => ConvertToLong(x.ToList())).ToList();
        }

        public string ConvertToBits(int value)
        {
            var result = new List<string>();
            while (value >= 0)
            {
                if (value == 0)
                {
                    result.Add("0");
                    break;
                }

                result.Add((value % 2).ToString(CultureInfo.InvariantCulture));
                value /= 2;
            }

            result.Reverse();
            return string.Concat(result);
        }

        public long ConvertToLong(List<string> binaryNumber)
        {
            long base1 = 1;

            long result = 0;
            foreach (var bit in binaryNumber)
            {
                result += int.Parse(bit) * base1;
                base1 *= 2;
            }

            return result;
        }
    }
}