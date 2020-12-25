using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20
{
    public class One : IAnswerGenerator
    {
        private readonly List<Tile> _tiles;
        private readonly int[,] _map;
        private readonly int _size;

        public One(IEnumerable<string> input)
        {
            _tiles = new LineParser().Parse(input);
            _size = (int)Math.Sqrt(_tiles.Count);

            _map = new int[_size,_size];
        }

        public long Generate()
        {
            long result = 1;
            
            foreach (var tile in _tiles.Where(t => t.Number == 2833))
            {
                var possibleTiles = _tiles.Where(t => t.Number != tile.Number).ToList();
                if (FindNext(possibleTiles, tile, Change.None) || 
                    FindNext(possibleTiles, tile, Change.FlippedVertical) || 
                    FindNext(possibleTiles, tile, Change.FlippedHorizontal) || 
                    FindNext(possibleTiles, tile, Change.Rotated180) || 
                    FindNext(possibleTiles, tile, Change.Rotated270) || 
                    FindNext(possibleTiles, tile, Change.Rotated270FlippedHorizontal) || 
                    FindNext(possibleTiles, tile, Change.Rotated270FlippedVertical) || 
                    FindNext(possibleTiles, tile, Change.Rotated90) || 
                    FindNext(possibleTiles, tile, Change.Rotated90FlippedHorizontal) || 
                    FindNext(possibleTiles, tile, Change.Rotated90FlippedVertical)
                    )
                {
                    break;
                }
            }

            result *= _map[0, 0] *
                      (long)_map[0, _size - 1] *
                      _map[_size - 1, 0] *
                      _map[_size - 1, _size - 1];

            return result;
        }

        private bool FindNext(IReadOnlyCollection<Tile> possibleTiles, Tile tile, Change change, int x = 1, int y = 1, bool leftToRight = true)
        {
            _map[y - 1, x - 1] = tile.Number;

            var goDown = false;
            List<Tuple<Tile, Change>> rightOrLeft;
            if (leftToRight)
            {
                rightOrLeft = FindCandidatesRightSide(possibleTiles, tile, change).ToList();
            }
            else
            {
                rightOrLeft = FindCandidatesLeftSide(possibleTiles, tile, change).ToList();
            }
            var bottom = FindCandidatesBottomSide(possibleTiles, tile, change).ToList();
            if (leftToRight)
            {
                if (rightOrLeft.Count == 0 && x < _size || 
                    bottom.Count == 0 && y < _size)
                {
                    return false;
                }
                
                x++;
            }
            else
            {
                if (rightOrLeft.Count == 0 && x > 1 || 
                    bottom.Count == 0 && y < _size)
                {
                    return false;
                }

                x--;
            }
            
            if (x < 1 || x > _size)
            {
                y++;
                x = leftToRight ? --x : ++x;

                goDown = true;
            }

            if (y > _size)
            {
                return true;
            }

            if (goDown)
            {
                foreach (var tuple in bottom)
                {
                    var newPossibleTiles = possibleTiles.Where(t => t.Number != tuple.Item1.Number).ToList();
                    var result = FindNext(newPossibleTiles, tuple.Item1, tuple.Item2, x, y, !leftToRight);
                    if (result) return true;
                }
            }
            else
            {
                foreach (var tuple in rightOrLeft)
                {
                    var newPossibleTiles = possibleTiles.Where(t => t.Number != tuple.Item1.Number).ToList();
                    var result = FindNext(newPossibleTiles, tuple.Item1, tuple.Item2, x, y, leftToRight);
                    if (result) return true;
                }
            }

            return false;
        }

        private IEnumerable<Tuple<Tile, Change>> FindCandidatesRightSide(IEnumerable<Tile> possibleTiles, Tile source,
            Change change)
        {
            foreach (var tile in possibleTiles)
            {
                var sourceList = source.GetRight(change);

                foreach (Change possibleChange in Enum.GetValues(typeof(Change)))
                {
                    if (sourceList.SequenceEqual(tile.GetLeft(possibleChange)))
                    {
                        yield return new Tuple<Tile, Change>(tile, possibleChange);
                    }
                }
            }
        }

        private IEnumerable<Tuple<Tile, Change>> FindCandidatesLeftSide(IEnumerable<Tile> possibleTiles, Tile source,
            Change change)
        {
            foreach (var tile in possibleTiles)
            {
                var sourceList = source.GetLeft(change);

                foreach (Change possibleChange in Enum.GetValues(typeof(Change)))
                {
                    if (sourceList.SequenceEqual(tile.GetRight(possibleChange)))
                    {
                        yield return new Tuple<Tile, Change>(tile, possibleChange);
                    }
                }
            }
        }

        private IEnumerable<Tuple<Tile, Change>> FindCandidatesBottomSide(IEnumerable<Tile> possibleTiles, Tile source,
            Change change)
        {
            foreach (var tile in possibleTiles)
            {
                var sourceList = source.GetBottom(change);

                foreach (Change possibleChange in Enum.GetValues(typeof(Change)))
                {
                    if (sourceList.SequenceEqual(tile.GetTop(possibleChange)))
                    {
                        yield return new Tuple<Tile, Change>(tile, possibleChange);
                    }
                }
            }
        }
    }
}