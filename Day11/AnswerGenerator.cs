using System;
using System.Collections.Generic;

namespace Day11
{
    public class AnswerGenerator2 : IAnswerGenerator
    {
        private Map _map;

        public AnswerGenerator2(IEnumerable<string> input)
        {
            _map = new LineParser().Parse(input);
        }

        public long Generate()
        {
            long previousScore = -1;
            long score = 0;
            while (score != previousScore)
            {
                previousScore = score;

                //_map.Print();

                DetermineNewMap();

                score = _map.CalculateScore();
            }

            return _map.CountPositives();
        }

        private void DetermineNewMap()
        {
            var depth = int.MaxValue;
            var newMap = new Map(_map.Rows, _map.Columns);
            foreach (var field in _map.GetAllFields())
            {
                var value = field.Value switch
                {
                    '#' when _map.Scan(field.Row, field.Column, depth) >= 5 => 'L',
                    'L' when _map.Scan(field.Row, field.Column, depth) == 0 => '#',
                    _ => field.Value
                };

                newMap.SetField(field.Row, field.Column, value);
            }

            _map = newMap;
        }
    }
}