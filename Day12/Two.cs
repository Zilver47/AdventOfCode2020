using System;
using System.Collections.Generic;

namespace Day12
{
    public class Two : IAnswerGenerator
    {
        private readonly List<Instruction> _instructions;
        private int x;
        private int y;
        private int waypointX = 10;
        private int waypointY = 1;

        public Two(IEnumerable<string> input)
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
                        MoveWaypoint(instruction.Action, instruction.Value);
                        break;
                    case 'L':
                    case 'R':
                        Rotate(instruction);
                        break;
                    case 'F':
                        Move(instruction.Value);
                        break;
                }

                Console.WriteLine($"{instruction.Action}{instruction.Value}: ship: {x},{y} | Waypoint: {waypointX},{waypointY}");
            }

            return Math.Abs(x) + Math.Abs(y);
        }

        private void MoveWaypoint(char wind, int value)
        {
            switch (wind)
            {
                case 'N':
                    waypointY += value;
                    break;
                case 'E':
                    waypointX += value;
                    break;
                case 'S':
                    waypointY -= value;
                    break;
                case 'W':
                    waypointX -= value;
                    break;
            }
        }

        private void Move(int value)
        {
            y += value * waypointY;
            x += value * waypointX;
        }

        private void Rotate(Instruction instruction)
        {
            var steps = instruction.Value / 90;
            
            for (var i = 0; i < steps; i++)
            {
                var currentWaypointX = waypointX;
                var currentWaypointY = waypointY;
                if (instruction.Action == 'L')
                {
                    waypointX = currentWaypointY * -1;
                    waypointY = currentWaypointX;
                }
                else
                {
                    waypointX = currentWaypointY;
                    waypointY = currentWaypointX * -1;
                }
            }
        }
    }
}