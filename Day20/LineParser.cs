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

        public List<bool> Top { get; set; }

        public List<bool> Right { get; set; }

        public List<bool> Bottom { get; set; }

        public List<bool> Left { get; set; }

        public Tile(List<string> lines)
        {
            Number = int.Parse(lines[0].Replace("Tile ", string.Empty).Replace(":", string.Empty));
            lines.RemoveAt(0);
            
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
            if (change == Change.FlippedVertical){ result = Left.ToList();}
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
    }
}