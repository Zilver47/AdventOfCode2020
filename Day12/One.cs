using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace Day12
{
    public class One : IAnswerGenerator
    {
        private readonly List<Instruction> _instructions;
        private int x;
        private int y;
        private char _direction = 'E';
        private List<char> _directions = new List<char> { 'N', 'E', 'S', 'W'};

        public One(IEnumerable<string> input)
        {
            _instructions = new LineParser().Parse(input);
        }

        public long Generate()
        {
            foreach (var instruction in _instructions)
            {
                switch (instruction.Action)
                {
                    case 'N':
                    case 'E':
                    case 'S':
                    case 'W':
                        Move(instruction.Action, instruction.Value);
                        break;
                    case 'L':
                    case 'R':
                        Rotate(instruction);
                        break;
                    case 'F':
                        Move(_direction, instruction.Value);
                        break;
                }

                Console.WriteLine($"{instruction.Action}{instruction.Value}: ship: {x},{y}");
            }

            return Math.Abs(x) + Math.Abs(y);
        }

        private void Move(char wind, int value)
        {
            switch (wind)
            {
                case 'N':
                    y += value;
                    break;
                case 'E':
                    x += value;
                    break;
                case 'S':
                    y -= value;
                    break;
                case 'W':
                    x -= value;
                    break;
            }
        }

        private void Rotate(Instruction instruction)
        {
            var steps = instruction.Value / 90;
            var index = _directions.FindIndex(d => d == _direction);
            if (instruction.Action == 'L')
            {
                index -= steps;
            }
            else
            {
                index += steps;
            }

            if (index < 0) index += 4;
            if (index > 3) index -= 4;

            _direction = _directions[index];
        }
    }
}