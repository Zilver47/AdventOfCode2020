using System;
using System.Collections.Generic;

namespace Day20
{
    public class Map
    {
        private bool[,] _fields;

        public int Rows { get; }
        public int Columns { get; }

        public Map(int rows, int columns)
        {
            _fields = new bool[rows, columns];

            Rows = rows;
            Columns = columns;
        }

        public Map(bool[,] fields)
        {
            _fields = fields;

            Rows = fields.GetUpperBound(0) + 1;
            Columns = fields.GetUpperBound(0) + 1;
        }

        public Map(IList<string> lines)
        {
            _fields = new bool[lines.Count, lines[0].Length];

            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                for (var j = 0; j < line.Length; j++)
                {
                    SetField(i, j, line[j] == '#');
                }
            }

            Rows = lines.Count;
            Columns = lines[0].Length;
        }

        public void Print()
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    Console.Write(_fields[i, j] ? '#' : '.');
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public void Print(Change change)
        {
            var map = ApplyChangeInternal(change);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write(map[i, j] ? '#' : '.');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public bool GetField(in int x, in int y)
        {
            return _fields[x, y];
        }

        public void SetField(in int x, in int y, in bool value)
        {
            _fields[x, y] = value;
        }

        public int Count()
        {
            var result = 0;
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    result += _fields[i, j] ? 1 : 0;
                }

            }

            return result;
        }

        public void Append(Map map, int startingX, int startingY)
        {
            for (var i = 0; i < map.Rows; i++)
            {
                var x = startingX + i;
                for (var j = 0; j < map.Columns; j++)
                {
                    var y = startingY + j;
                    SetField(x, y, map.GetField(i, j));
                }
            }
        }

        public void Rotate()
        {
            var fields = ApplyChangeInternal(Change.Rotated90);
            _fields = fields;
        }

        public void Flip()
        {
            var fields = ApplyChangeInternal(Change.FlippedVertical);
            _fields = fields;
        }

        public int FindMonster()
        {
            return FindMonstersInternal(_fields);
        }

        private bool[,] ApplyChangeInternal(Change change)
        {
            var topLeft = (0, 0);
            var bottomRight = (Rows - 1, Rows - 1);

            if (change == Change.FlippedVertical)
            {
                topLeft = (0, Rows - 1);
                bottomRight = (Rows - 1, 0);
            }
            else if (change == Change.FlippedHorizontal)
            {
                topLeft = (Rows - 1, 0);
                bottomRight = (0, Rows - 1);
            }
            else if (change == Change.Rotated90FlippedHorizontal || change == Change.Rotated180)
            {
                topLeft = (Rows - 1, Rows - 1);
                bottomRight = (0, 0);
            }
            else if (change == Change.Rotated270)
            {
                topLeft = (0, Rows - 1);
                bottomRight = (Rows - 1, 0);
            }
            else if (change == Change.Rotated90FlippedVertical)
            {
            }
            else if (change == Change.Rotated270FlippedHorizontal)
            {
            }
            else if (change == Change.Rotated270FlippedVertical)
            {
                topLeft = (Rows - 1, Rows - 1);
                bottomRight = (0, 0);
            }
            else if (change == Change.Rotated90)
            {
                topLeft = (Rows - 1, 0);
                bottomRight = (0, Rows - 1);
            }

            var fields = new bool[Rows, Rows];
            var x = topLeft.Item1;
            for (var i = 0; i < Rows; i++)
            {
                var y = topLeft.Item2;
                for (var j = 0; j < Rows; j++)
                {
                    if (change == Change.Rotated90FlippedVertical ||
                        change == Change.Rotated270FlippedHorizontal ||
                        change == Change.Rotated90FlippedHorizontal ||
                        change == Change.Rotated270FlippedVertical)
                    {
                        fields[i, j] = _fields[y, x];
                    }
                    else
                    {
                        fields[i, j] = _fields[x, y];
                    }

                    y = y < bottomRight.Item2 ? y + 1 : y - 1;
                }

                x = x < bottomRight.Item1 ? x + 1 : x - 1;
            }

            return fields;
        }

        private int FindMonstersInternal(bool[,] fields)
        {
            var monsterLines = new[]
            {
                "                  # ",
                "#    ##    ##    ###",
                " #  #  #  #  #  #   "
            };
            var monster = new Map(monsterLines);

            var result = 0;
            for (var i = 0; i < Rows - 2; i++)
            {
                for (var j = 0; j < Columns - 20; j++)
                {
                    if (IsMonster(i, j, monster, fields))
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        private bool IsMonster(int startX, int startY, Map monster, bool[,] fields)
        {
            for (var i = 0; i < monster.Rows; i++)
            {
                var x = startX + i;
                for (var j = 0; j < monster.Columns; j++)
                {
                    var y = startY + j;
                    var monsterField = monster.GetField(i, j);
                    if (fields[x, y] != monsterField && monsterField)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}