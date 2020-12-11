using System;
using System.Collections.Generic;
using System.Linq;

namespace Day11
{
    public class Map
    {
        private const char Positive = '#';
        private const char Negative = 'L';
        private readonly List<(int RowDelta, int ColumnDelta)> _directions = new List<(int RowDelta, int ColumnDelta)>
        {
            ( -1, -1 ),
            ( -1,  0 ),
            ( -1,  1 ),
            (  0,  1 ),
            (  1,  1 ),
            (  1,  0 ),
            (  1, -1 ),
            (  0, -1 )
        };

        public int Rows { get; }
        public int Columns { get; }
        private readonly long[][] _values;

        public Map(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _values = new long[Rows][];
            for (var i = 0; i < Rows; i++)
            {
                _values[i] = new long[columns];
            }
        }

        public Map(IReadOnlyList<string> lines)
        {
            Rows = lines.Count;
            _values = new long[Rows][];
            for (var i = 0; i < Rows; ++i)
            {
                var line = lines[i];
                _values[i] = new long[line.Length];
                for (var j = 0; j < line.Length; j++)
                {
                    _values[i][j] = line[j];
                }
            }

            Columns = _values[0].Length;
        }

        public long GetField(int row, int column)
        {
            return _values[row - 1][column - 1];
        }

        public IEnumerable<Field> GetAllFields()
        {
            for (var i = 0; i < Rows; ++i)
            {
                for (var j = 0; j < Columns; ++j)
                {
                    yield return new Field(_values[i][j], i + 1, j + 1);
                }
            }
        }

        public void SetField(in int row, in int column, long value)
        {
            _values[row - 1][column - 1] = value;
        }

        public void Print()
        {
            foreach (var row in _values)
            {
                foreach (var field in row)
                {
                    Console.Write((char)field);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public int CountPositives()
        {
            return _values.Sum(row => row.Sum(field => field == Positive ? 1 : 0));
        }

        public long CalculateScore()
        {
            long result = 0;
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    if (_values[i][j] == Positive)
                    {
                        result += i + 1 * j + 1;
                    }
                }
            }

            return result;
        }

        public int Scan(int row, int column, int depth)
        {
            return _directions.Sum(direction => 
                Convert.ToInt32(FindInDirection(row, column, direction.RowDelta, direction.ColumnDelta, depth)));
        }

        private bool FindInDirection(int row, int column, int rowDelta, int columnDelta, int depth)
        {
            var currentDepth = 0;

            while (true)
            {
                row += rowDelta;
                column += columnDelta;

                if (row < 1 || column < 1 || row > Rows || column > Columns || currentDepth >= depth)
                {
                    return false;
                }

                switch (_values[row - 1][column - 1])
                {
                    case Positive:
                        return true;
                    case Negative:
                        return false;
                    default:
                        currentDepth++;
                        break;
                }
            }
        }
    }

    public class Field
    {
        public long Value { get; }
        public int Row { get; }
        public int Column { get; }

        public Field(long value, int row, int column)
        {
            Value = value;
            Row = row;
            Column = column;
        }
    }
}
