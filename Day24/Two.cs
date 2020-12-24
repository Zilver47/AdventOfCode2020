using System;
using System.Collections.Generic;
using System.Linq;

namespace Day24
{
    public class Two : IAnswerGenerator
    {
        private readonly List<List<string>> _instructions;
        private TileMap _map;
        private TileMap _newMap;

        public Two(IEnumerable<string> input)
        {
            _map = new TileMap();

            _instructions = new LineParser().Parse(input).ToList();
        }

        public long Generate()
        {
            InitializeTileMap();

            var result = 0;
            const int days = 100;
            for (var i = 0; i < days; i++)
            {
                _newMap = new TileMap();
                foreach (var tile in _map.Keys)
                {
                    foreach (var adjacentTile in tile.GetAdjacent())
                    {
                        CheckTile(adjacentTile);
                    }

                    CheckTile(tile);
                }

                _map = _newMap;

                result = _map.CountFlipped();
                Console.WriteLine($"Day {i + 1}: {result}");
            }

            return result;
        }

        private void InitializeTileMap()
        {
            foreach (var instruction in _instructions)
            {
                FollowInstruction(instruction);
            }

            Console.WriteLine($"Day 0: {_map.CountFlipped()}");
        }

        private void FollowInstruction(IEnumerable<string> instruction)
        {
            var x = 0;
            var y = 0;

            foreach (var steps in instruction)
            {
                switch (steps)
                {
                    case "e":
                        x += 2;
                        break;
                    case "se":
                        x += 1;
                        y += 1;
                        break;
                    case "sw":
                        x -= 1;
                        y += 1;
                        break;
                    case "w":
                        x -= 2;
                        break;
                    case "nw":
                        x -= 1;
                        y -= 1;
                        break;
                    case "ne":
                        x += 1;
                        y -= 1;
                        break;
                }
            }

            var tile = new Tile(x, y);
            _map.Flip(tile);
        }

        private void CheckTile(Tile tile)
        {
            var adjacentTiles = tile.GetAdjacent();
            var numberOfAdjacentFlippedTiles = adjacentTiles.Count(_map.IsFlipped);

            if (_map.IsFlipped(tile))
            {
                if (numberOfAdjacentFlippedTiles == 0 || numberOfAdjacentFlippedTiles > 2)
                {
                    _newMap.AddOrUpdate(tile, false);
                }
                else
                {
                    _newMap.TryAdd(tile, true);
                }
            }
            else if (numberOfAdjacentFlippedTiles == 2)
            {
                _newMap.TryAdd(tile, true);
            }
        }
    }
}