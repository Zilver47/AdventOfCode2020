using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day20
{
    public class Two : IAnswerGenerator
    {
        private readonly List<Tile> _tiles;
        private int _size;

        public Two(IEnumerable<string> input)
        {
            _tiles = new LineParser().Parse(input);
            _size = (int)Math.Sqrt(_tiles.Count);
        }

        public long Generate()
        {
            var image = new bool[12*9, 12*9];
            var imageRow = 0;
            var imageColumn = 0;

            // Way to much work

            return -1;
        }
    }
}