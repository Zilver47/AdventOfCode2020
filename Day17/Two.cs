using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Day17
{
    public class Two : IAnswerGenerator
    {
        private Map _map;

        public Two(IEnumerable<string> input)
        {
            _map = new LineParser().Parse(input);
        }

        public long Generate()
        {
            _map.Print();

            for (int i = 0; i < 6; i++)
            {
                _map.Expand();

                Cycle();

                //Console.WriteLine($"Cycle {i + 1}");
                //_map.Print();
            }

            var fields = _map.Count();

            return fields;
        }

        private void Cycle()
        {
            var newMap = new Map(_map.Dimensions, _map.Layers, _map.Rows, _map.Columns);
            for (int h = 0; h < _map.Dimensions; h++)
            {
                for (int i = 0; i < _map.Layers; i++)
                {
                    for (int j = 0; j < _map.Rows; j++)
                    {
                        for (int k = 0; k < _map.Columns; k++)
                        {
                            var field = _map.GetField(h, i, j, k);
                            var newField = field;

                            var neightbours = _map.GetNeightbours(h, i, j, k)
                                .Count(n => n);
                            if (field && neightbours != 2 && neightbours != 3)
                            {
                                newField = false;
                            }
                            else if (!field && neightbours == 3)
                            {
                                newField = true;
                            }

                            newMap.SetField(h, i, j, k, newField);
                        }
                    }
                }
                
            }

            _map = newMap;
        }
    }
}