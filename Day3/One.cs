using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
    public class One : IAnswerGenerator
    {
        private List<InputLine> _input;

        public One(IEnumerable<string> input)
        {
            _input = new LineParser().Parse(input).ToList();

            foreach (var row in _input)
            {
                foreach (var tile in row.Tiles)
                {
                    //Console.Write(tile ? "#" : ".");
                }

                //Console.WriteLine("");
            }
        }

        public string Generate()
        {
            var slopeResults = new List<int>();
            var slopes = new List<(int Right, int Down)> {
                new (1, 1),
                new (3, 1),
                new (5, 1),
                new (7, 1),
                new (1, 2)
            };
            foreach (var slope in slopes)
            {
                var numberOfTrees = 0;
                var x = 1;
                var y = 1;
                while (x < _input.Count)
                {
                    x += slope.Down;
                    y += slope.Right;

                    var isTree = IsTree(x, y);
                    //Console.WriteLine(isTree ? " X" : " O");
                    numberOfTrees += isTree ? 1 : 0;
                };

                slopeResults.Add(numberOfTrees);
            }

            double result = 1;
            foreach (var numberOfTrees in slopeResults)
            {
                Console.WriteLine(numberOfTrees);

                result = result * numberOfTrees;
            }

            return result.ToString();
        }

        private bool IsTree(int x, int y)
        {
            var normalizedY = y;
            var repeatInterval = _input[0].Tiles.Count;
            while (normalizedY > repeatInterval)
            {
                normalizedY -= repeatInterval;
            }
            
            if (x == 3) Console.WriteLine($"Find three at: {x},{normalizedY} ({y})");
            if (x == 323) Console.WriteLine($"Find three at: {x},{normalizedY} ({y})");
            return _input[x - 1].Tiles[normalizedY - 1];
        }
    }
}