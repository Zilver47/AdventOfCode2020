using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Day15
{
    public class Two : IAnswerGenerator
    {
        private readonly List<int> _numbers;
        private readonly Dictionary<int, List<int>> _previousNumbers;

        public Two(IEnumerable<string> input)
        {
            _numbers = new LineParser().Parse(input).ToList();
            _previousNumbers = new Dictionary<int, List<int>>();
            for (var i = 0; i < _numbers.Count; i++)
            {
                _previousNumbers.Add(_numbers[i], new List<int> { i }); 
            }
        }

        public long Generate()
        {
            var lastNumber = _numbers.Last();
            for (var i = _numbers.Count; i < 30000000; i++)
            {
                var newNumber = 0;

                if (_previousNumbers.ContainsKey(lastNumber) && _previousNumbers[lastNumber].Count > 1)
                {
                    var previousOfLastNumber = _previousNumbers[lastNumber];
                    var lastIndexes = previousOfLastNumber.GetRange(previousOfLastNumber.Count - 2, 2);
                        
                    newNumber = lastIndexes.Last() - lastIndexes.First();
                }

                StoreNumber(newNumber, i);
                
                lastNumber = newNumber;
            }

            return lastNumber;
        }

        private void StoreNumber(int newNumber, int index)
        {
            if (_previousNumbers.ContainsKey(newNumber))
            {
                _previousNumbers[newNumber].Add(index);
            }
            else
            {
                _previousNumbers.Add(newNumber, new List<int> {index});
            }
        }
    }
}