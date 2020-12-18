using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17
{
    public class MapOne
    {
        private readonly List<List<List<bool>>> _fields;

        public int Layers => _fields.Count;
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public MapOne(IEnumerable<string> lines)
        {
            _fields = new List<List<List<bool>>>();
            var rows = new List<List<bool>>(lines.Count());

            foreach (var line in lines)
            {
                var rowFields = new List<bool>(line.Length);

                foreach (var character in line)
                {
                    rowFields.Add(character == '#');
                }

                rows.Add(rowFields);
            }

            _fields.Add(rows);

            Rows = rows.Count;
            Columns = rows[0].Count;
        }

        public MapOne(int z, int x, int y)
        {
            _fields = new List<List<List<bool>>>();
            for (int i = 0; i < z; i++)
            {
                _fields.Add(new List<List<bool>>());
                for (int j = 0; j < x; j++)
                {
                    _fields[i].Add(new List<bool>());
                    for (int k = 0; k < y; k++)
                    {
                        _fields[i][j].Add(false);
                    }
                }
            }

            Rows = x;
            Columns = y;
        }

        public void Print()
        {
            for (var h = 0; h < _fields.Count; h++)
            {
                Console.WriteLine("z=" + h);

                var layer = _fields[h];
                for (var i = 0; i < layer.Count; i++)
                {
                    var row = layer[i];

                    for (var j = 0; j < row.Count; j++)
                    {
                        Console.Write(row[j] ? '#' : '.');
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }

        public List<List<List<bool>>> GetAllFields()
        {
            return _fields;
        }

        public List<bool> GetNeightbours(in int z, in int x, in int y)
        {
            var result = new List<bool>();
            for (var i = z - 1; i <= z + 1; i++)
            {
                if (i < 0 || i >= _fields.Count)
                {
                    continue;
                }

                var layer = _fields[i];
                for (var j = x - 1; j <= x + 1; j++)
                {
                    if (j < 0 || j >= layer.Count)
                    {
                        continue;
                    }

                    var row = layer[j];
                    for (var k = y - 1; k <= y + 1; k++)
                    {
                        if (k < 0 || k >= row.Count)
                        {
                            continue;
                        }

                        if (i == z && j == x && k == y) // ben je zelf
                        {
                            continue;
                        }

                        result.Add(row[k]);
                    }
                }
            }

            return result;
        }

        public bool GetField(in int z, in int x, in int y)
        {
            return _fields[z][x][y];
        }

        public void SetField(in int z, in int x, in int y, in bool value)
        {
            _fields[z][x][y] = value;
        }

        public void Expand()
        {
            //if (_fields.Count == 1)
            //{
            //    _fields.Insert(0, CreateRows());
            //    _fields.Add(CreateRows());
            //    return;
            //}

            Rows += 2;
            Columns += 2;

            // Enlarge existing fields
            foreach (var rows in _fields)
            {
                foreach (var row in rows)
                {
                    row.Insert(0, false);
                    row.Add(false);
                }
                
                rows.Insert(0, CreateRow());
                rows.Add(CreateRow());
            }

            _fields.Insert(0, CreateRows());
            _fields.Add(CreateRows());
        }

        private List<List<bool>> CreateRows()
        {
            var columns = CreateRow();

            var rows = new List<List<bool>>(Rows);
            for (int i = 0; i < Rows; i++)
            {
                rows.Add(columns);
            }

            return rows;
        }

        private List<bool> CreateRow()
        {
            var columns = new List<bool>();
            for (int i = 0; i < Columns; i++)
            {
                columns.Add(false);
            }

            return columns;
        }

        public long Count()
        {
            var result = 0;
            for (var h = 0; h < _fields.Count; h++)
            {
                var layer = _fields[h];
                for (var i = 0; i < layer.Count; i++)
                {
                    var row = layer[i];

                    for (var j = 0; j < row.Count; j++)
                    {
                        if (row[j])
                        {
                            result++;
                        }
                    }
                }
            }

            return result;
        }
    }
}