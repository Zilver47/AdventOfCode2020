using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Day14
{
    public class One : IAnswerGenerator
    {
        private readonly List<Program> _programs;
        private string _mask;
        private readonly Dictionary<int, long> _value;

        public One(IEnumerable<string> input)
        {
            _value = new Dictionary<int, long>();
            _programs = new LineParser().Parse(input).ToList();
        }

        public long Generate()
        {
            foreach (var program in _programs)
            {
                _mask = program.Mask;
                foreach (var transformation in program.Transformations)
                {
                    _value[transformation.Key] = Transform(transformation.Value);
                }
            }

            return _value.Values.Sum(v => v);
        }

        public long Transform(int value)
        {
            var bits = ConvertToBits(value);
            Console.WriteLine(bits);
            Console.WriteLine(_mask);

            var resultAsList = new List<char>();
            for (var i = 0; i < _mask.Length; i++)
            {
                var bit = i >= bits.Length ? '0' : bits[bits.Length - i - 1];
                var bitmask = _mask[_mask.Length - i - 1];
                resultAsList.Add(bitmask == 'X' ? bit : bitmask);
            }
            
            var result = Convert(resultAsList);
            Console.WriteLine(result);

            return result;
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

        public long Convert(List<char> binaryNumber)
        {
            long @base = 1;

            long result = 0;
            foreach (var bit in binaryNumber)
            {
                result += bit * @base;
                @base *= 2;
            }

            return result;
        }
    }
}