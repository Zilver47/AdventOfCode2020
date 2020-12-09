using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
    public class One : IAnswerGenerator
    {
        private readonly List<InputLine> _rows;
        private readonly List<(int Right, int Down)> _slopes;

        public One(IEnumerable<string> input)
        {
            _rows = new LineParser().Parse(input).ToList();
            _slopes = new List<(int Right, int Down)> {
                new (1, 1),
                new (3, 1),
                new (5, 1),
                new (7, 1),
                new (1, 2)
            };
        }

        public long Generate()
        {
            var slopeResults = _slopes.Select(DetermineNumberOfTrees);

            var result = MultiplyNumberOfTreesPerScope(slopeResults);
            return result;
        }

        private static long MultiplyNumberOfTreesPerScope(IEnumerable<int> results)
        {
            return results.Aggregate<int, long>(1, (current, numberOfTrees) => current * numberOfTrees);
        }

        private int DetermineNumberOfTrees((int Right, int Down) slope)
        {
            var numberOfTrees = 0;
            var x = 1;
            var y = 1;
            while (x < _rows.Count)
            {
                x += slope.Down;
                y += slope.Right;

                numberOfTrees += IsTree(x, y) ? 1 : 0;
            };
            return numberOfTrees;
        }

        private bool IsTree(int x, int y)
        {
            var normalizedY = y;
            var repeatInterval = _rows[0].Columns.Count;
            while (normalizedY > repeatInterval)
            {
                normalizedY -= repeatInterval;
            }
            
            return _rows[x - 1].Columns[normalizedY - 1];
        }
    }
}