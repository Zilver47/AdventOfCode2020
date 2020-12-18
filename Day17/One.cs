using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Day17
{
    public class One : IAnswerGenerator
    {
        private MapOne _map;

        public One(IEnumerable<string> input)
        {
            //_map = new LineParser().Parse(input);
        }

        public long Generate()
        {
            _map.Print();

            for (int i = 0; i < 6; i++)
            {
                _map.Expand();

                Cycle();

                Console.WriteLine($"Cycle {i + 1}");
                _map.Print();
            }

            var fields = _map.Count();

            return fields;
        }

        private void Cycle()
        {
            var newMap = new MapOne(_map.Layers, _map.Rows, _map.Columns);
            for (int i = 0; i < _map.Layers; i++)
            {
                for (int j = 0; j < _map.Rows; j++)
                {
                    for (int k = 0; k < _map.Columns; k++)
                    {
                        var field = _map.GetField(i, j, k);
                        var newField = field;

                        var neightbours = _map.GetNeightbours(i, j, k)
                            .Count(n => n);
                        if (field && neightbours != 2 && neightbours != 3)
                        {
                            newField = false;
                        }
                        else if (!field && neightbours == 3)
                        {
                            newField = true;
                        }

                        newMap.SetField(i, j, k, newField);
                    }
                }
            }

            _map = newMap;
        }
    }
}