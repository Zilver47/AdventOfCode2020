using System;
using System.Collections.Generic;
using System.Linq;

namespace Day24
{
    public class One : IAnswerGenerator
    {
        private readonly List<List<string>> _instructions;
        private readonly HashSet<Tuple<int, int>> _flipped;

        public One(IEnumerable<string> input)
        {
            _flipped = new HashSet<Tuple<int, int>>();

            _instructions = new LineParser().Parse(input).ToList();
        }

        public long Generate()
        {
            foreach (var instruction in _instructions)
            {
                FollowInstruction(instruction);
            }

            return _flipped.Count;
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

            var position = new Tuple<int, int>(x, y);
            if (_flipped.Contains(position))
            {
                _flipped.Remove(position);
            }
            else
            {
                _flipped.Add(position);
            }
        }
    }
}