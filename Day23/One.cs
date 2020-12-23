using System;
using System.Collections.Generic;
using System.Linq;

namespace Day23
{
    public class One : IAnswerGenerator
    {
        private readonly List<int> _cups;
        private int _currentCupIndex;
        private int _size;
        private int _lowestValue;
        private int _highestValue;
        private int _moves;

        public One(IEnumerable<string> input)
        {
            _cups = new LineParser().Parse(input).ToList();
            _currentCupIndex = 0;
            _size = _cups.Count;
            _lowestValue = _cups.Min();
            _highestValue = _cups.Max();
            _moves = 0;
        }

        public long Generate()
        {
            Play();

            var indexOfOne = _cups.IndexOf(1);
            
            var numberOfCupsAfterCurrent = _size - indexOfOne - 1;
            var cupsOnARow = _cups.GetRange(indexOfOne + 1, numberOfCupsAfterCurrent);
            cupsOnARow.AddRange(_cups.GetRange(0, indexOfOne));

            var result = string.Join(',', cupsOnARow).Replace(",", string.Empty);
            Console.WriteLine(result);

            return -1;
        }

        private void Play()
        {
            var rounds = 100;
            for (int i = 0; i < rounds; i++)
            {
                _moves++;

                //Print();

                Round();
            }
            
            //Print();
        }

        private void Round()
        {
            var current = _cups[_currentCupIndex];
            var three = RemoveThree();

            var newCup = current;
            do
            {
                newCup--;

                if (newCup < _lowestValue)
                {
                    newCup = _highestValue;
                }
            } while (three.Contains(newCup));

            InsertThree(newCup, three);

            _currentCupIndex = _cups.IndexOf(current);
            _currentCupIndex++;
            if (_currentCupIndex == _size)
            {
                _currentCupIndex = 0;
            }
        }

        private void InsertThree(int value, List<int> three)
        {
            var newIndex = _cups.IndexOf(value) + 1;
            foreach (var cup in three)
            {
                if (newIndex >= _size) newIndex = 0;
                _cups.Insert(newIndex, cup);

                newIndex++;
            }
        }

        private List<int> RemoveThree()
        {
            var numberOfCupsAfterCurrent = _size - _currentCupIndex - 1;
            var setSize = numberOfCupsAfterCurrent > 3 ? 3 : numberOfCupsAfterCurrent;
            var three = _cups.GetRange(_currentCupIndex + 1, setSize);
            _cups.RemoveRange(_currentCupIndex + 1, setSize);
            if (three.Count < 3)
            {
                var remaining = 3 - three.Count;
                three.AddRange(_cups.GetRange(0, remaining));
                _cups.RemoveRange(0, remaining);
            }

            return three;
        }

        private void Print()
        {
            Console.WriteLine($"-- move {_moves} --");
            Console.WriteLine($"cups: {string.Join(',', _cups)}");
            Console.WriteLine($"(): {_cups[_currentCupIndex]}");
        }
    }
}