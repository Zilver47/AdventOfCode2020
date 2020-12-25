using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20
{
    public class LineParser
    {
        public List<Tile> Parse(IEnumerable<string> lines)
        {
            var result = new List<Tile>();

            var combinedLines = new List<string>();
            Tile tile;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    tile = new Tile(combinedLines);
                    result.Add(tile);
                    combinedLines = new List<string>();
                }
                else
                {
                    combinedLines.Add(line);
                }
            }
            tile = new Tile(combinedLines);
            result.Add(tile);

            return result;
        }
    }

    public class Tile
    {
        public int Number { get; set; }

        public Map Map { get; }

        public List<bool> Top { get; set; }

        public List<bool> Right { get; set; }

        public List<bool> Bottom { get; set; }

        public List<bool> Left { get; set; }

        public Tile(List<string> lines)
        {
            Number = int.Parse(lines[0].Replace("Tile ", string.Empty).Replace(":", string.Empty));
            lines.RemoveAt(0);

            Map = new Map(10, 10);
            for (var i = 0; i < 10; i++)
            {
                var line = lines[i];
                for (var j = 0; j < 10; j++)
                {
                    Map.SetField(i, j, line[j] == '#');
                }
            }

            Top = new List<bool>();
            foreach (var line in lines[0])
            {
                Top.Add(line == '#');
            }

            Bottom = new List<bool>();
            foreach (var line in lines[^1])
            {
                Bottom.Add(line == '#');
            }

            Right = new List<bool>();
            foreach (var line in lines)
            {
                Right.Add(line[^1] == '#');
            }

            Left = new List<bool>();
            foreach (var line in lines)
            {
                Left.Add(line[0] == '#');
            }
        }

        public List<bool> GetTop(Change change)
        {
            var result = Top.ToList();
            if (change == Change.FlippedHorizontal) result = Bottom.ToList();
            else if (change == Change.FlippedVertical) result.Reverse();
            else if (change == Change.Rotated180)
            {
                result = Bottom.ToList();
                result.Reverse();
            }
            else if (change == Change.Rotated270) result = Right.ToList();
            else if (change == Change.Rotated90FlippedVertical) result = Left.ToList();
            else if (change == Change.Rotated270FlippedHorizontal)
            {
                result = Left.ToList();
            }
            else if (change == Change.Rotated270FlippedVertical)
            {
                result = Right.ToList();
                result.Reverse();
            }
            else if (change == Change.Rotated90)
            {
                result = Left.ToList();
                result.Reverse();
            }
            else if (change == Change.Rotated90FlippedHorizontal)
            {
                result = Right.ToList();
                result.Reverse();
            }

            return result;
        }

        public List<bool> GetRight(Change change)
        {
            var result = Right.ToList();
            if (change == Change.FlippedVertical) { result = Left.ToList(); }
            else if (change == Change.FlippedHorizontal) result.Reverse();
            else if (change == Change.Rotated180)
            {
                result = Left.ToList();
                result.Reverse();
            }
            else if (change == Change.Rotated270)
            {
                result = Bottom.ToList();
                result.Reverse();
            }
            else if (change == Change.Rotated90FlippedVertical) result = Bottom.ToList();
            else if (change == Change.Rotated270FlippedHorizontal)
            {
                result = Bottom.ToList();
            }
            else if (change == Change.Rotated270FlippedVertical)
            {
                result = Top.ToList();
                result.Reverse();
            }
            else if (change == Change.Rotated90) result = Top.ToList();
            else if (change == Change.Rotated90FlippedHorizontal)
            {
                result = Top.ToList();
                result.Reverse();
            }

            return result;
        }

        public List<bool> GetBottom(Change change)
        {
            var result = Bottom.ToList();
            if (change == Change.FlippedHorizontal) result = Top.ToList();
            else if (change == Change.FlippedVertical) result.Reverse();
            else if (change == Change.Rotated180)
            {
                result = Top.ToList();
                result.Reverse();
            }
            else if (change == Change.Rotated270) result = Left.ToList();
            else if (change == Change.Rotated90FlippedVertical) result = Right.ToList();
            else if (change == Change.Rotated270FlippedHorizontal)
            {
                result = Right.ToList();
            }
            else if (change == Change.Rotated270FlippedVertical)
            {
                result = Left.ToList();
                result.Reverse();
            }
            else if (change == Change.Rotated90)
            {
                result = Right.ToList();
                result.Reverse();
            }
            else if (change == Change.Rotated90FlippedHorizontal)
            {
                result = Left.ToList();
                result.Reverse();
            }

            return result;
        }

        public List<bool> GetLeft(Change change)
        {
            var result = Left.ToList();
            if (change == Change.FlippedVertical) result = Right.ToList();
            else if (change == Change.FlippedHorizontal) result.Reverse();
            else if (change == Change.Rotated180)
            {
                result = Right.ToList();
                result.Reverse();
            }
            else if (change == Change.Rotated270)
            {
                result = Top.ToList();
                result.Reverse();
            }
            else if (change == Change.Rotated90FlippedVertical) result = Top.ToList();
            else if (change == Change.Rotated270FlippedHorizontal)
            {
                result = Top.ToList();
            }
            else if (change == Change.Rotated270FlippedVertical)
            {
                result = Bottom.ToList();
                result.Reverse();
            }
            else if (change == Change.Rotated90) result = Bottom.ToList();
            else if (change == Change.Rotated90FlippedHorizontal)
            {
                result = Bottom.ToList();
                result.Reverse();
            }

            return result;
        }

        public override string ToString()
        {
            return Number.ToString();
        }

        public Map GetFieldsWithoutBorder(Change change)
        {
            var topLeft = (1, 1);
            var bottomRight = (8, 8);

            if (change == Change.FlippedVertical)
            {
                topLeft = (1, 8);
                bottomRight = (8, 1);
            }
            else if (change == Change.FlippedHorizontal)
            {
                topLeft = (8, 1);
                bottomRight = (1, 8);
            }
            else if (change == Change.Rotated90FlippedHorizontal || change == Change.Rotated180)
            {
                topLeft = (8, 8);
                bottomRight = (1, 1);
            }
            else if (change == Change.Rotated270)
            {
                topLeft = (1, 8);
                bottomRight = (8, 1);
            }
            else if (change == Change.Rotated90FlippedVertical)
            {
            }
            else if (change == Change.Rotated270FlippedHorizontal)
            {
            }
            else if (change == Change.Rotated270FlippedVertical)
            {
                topLeft = (8, 8);
                bottomRight = (1, 1);
            }
            else if (change == Change.Rotated90)
            {
                topLeft = (8, 1);
                bottomRight = (1, 8);
            }

            var fields = new bool[8, 8];
            if (change == Change.Rotated90 ||
                change == Change.Rotated270)
            {
                var y = topLeft.Item2;
                for (var i = 0; i < 8; i++)
                {
                    var x = topLeft.Item1;
                    for (var j = 0; j < 8; j++)
                    {
                        if (change == Change.Rotated90FlippedVertical ||
                            change == Change.Rotated270FlippedHorizontal ||
                            change == Change.Rotated90FlippedHorizontal ||
                            change == Change.Rotated270FlippedVertical)
                        {
                            fields[i, j] = Map.GetField(y, x);
                        }
                        else
                        {
                            fields[i, j] = Map.GetField(x, y);
                        }
                        
                        x = x < bottomRight.Item1 ? x + 1 : x - 1;
                    }
                    
                    y = y < bottomRight.Item2 ? y + 1 : y - 1;
                }
            }
            else
            {
                var x = topLeft.Item1;
                for (var i = 0; i < 8; i++)
                {
                    var y = topLeft.Item2;
                    for (var j = 0; j < 8; j++)
                    {
                        if (change == Change.Rotated90FlippedVertical ||
                            change == Change.Rotated270FlippedHorizontal ||
                            change == Change.Rotated90FlippedHorizontal ||
                            change == Change.Rotated270FlippedVertical)
                        {
                            fields[i, j] = Map.GetField(y, x);
                        }
                        else
                        {
                            fields[i, j] = Map.GetField(x, y);
                        }

                        y = y < bottomRight.Item2 ? y + 1 : y - 1;
                    }

                    x = x < bottomRight.Item1 ? x + 1 : x - 1;
                }
            }

            return new Map(fields);
        }

        public void Print(Change change)
        {
            var map = GetFieldsWithoutBorder(change);
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    Console.Write(map.GetField(i, j) ? '#' : '.');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}