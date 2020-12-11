using System;
using System.Collections.Generic;
using System.Linq;

namespace Day11
{
    public class AnswerGenerator2 : IAnswerGenerator
    {
        private List<InputLine> _map;

        public AnswerGenerator2(IEnumerable<string> input)
        {
            _map = new LineParser().Parse(input);
        }

        public long Generate()
        {
            long previousScore = -1;
            long score = 0;
            while (score != previousScore)
            {
                previousScore = score;

                PrintMap();

                DetermineNewMap();
                
                score = CalculateScore();
            }

            return CountOccupiedSeats();
        }

        private void PrintMap()
        {
            foreach (var line in _map)
            {
                foreach (var column in line.Columns)
                {
                    Console.Write(column);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private int CountOccupiedSeats()
        {
            return _map.Sum(line => line.Columns.Sum(column => column == '#' ? 1 : 0));
        }

        private void DetermineNewMap()
        {
            var newMap = new List<InputLine>();
            for (var i = 0; i < _map.Count; i++)
            {
                newMap.Add(new InputLine());

                var line = _map[i];
                for (var j = 0; j < line.Columns.Count; j++)
                {
                    switch (line.Columns[j])
                    {
                        case '#' when CountOccupiedAdjacentSeats(i, j) >= 5:
                            newMap[i].Columns.Add('L');
                            break;
                        case 'L' when CountOccupiedAdjacentSeats(i, j) == 0:
                            newMap[i].Columns.Add('#');
                            break;
                        default:
                            newMap[i].Columns.Add(line.Columns[j]); // stays the same
                            break;
                    }
                }
            }

            _map = newMap;
        }

        private int CountOccupiedAdjacentSeats(int rowIndex, int columnIndex)
        {
            var hasOccupiedSeatInDirection = new List<bool>
            {
                BumpsIntoSeat(DetermineSeatsInDirection(rowIndex + 1, columnIndex + 1, -1, -1)),
                BumpsIntoSeat(DetermineSeatsInDirection(rowIndex + 1, columnIndex + 1, -1, 0)),
                BumpsIntoSeat(DetermineSeatsInDirection(rowIndex + 1, columnIndex + 1, -1, 1)),
                BumpsIntoSeat(DetermineSeatsInDirection(rowIndex + 1, columnIndex + 1, 0, 1)),
                BumpsIntoSeat(DetermineSeatsInDirection(rowIndex + 1, columnIndex + 1, 1, 1)),
                BumpsIntoSeat(DetermineSeatsInDirection(rowIndex + 1, columnIndex + 1, 1, 0)),
                BumpsIntoSeat(DetermineSeatsInDirection(rowIndex + 1, columnIndex + 1, 1, -1)),
                BumpsIntoSeat(DetermineSeatsInDirection(rowIndex + 1, columnIndex + 1, 0, -1))
            };

            return hasOccupiedSeatInDirection.Count(x => x);
        }

        private bool BumpsIntoSeat(List<(int Row, int Column)> positions)
        {
            foreach (var seat in positions.Select(position => _map[position.Row - 1].Columns[position.Column - 1]))
            {
                if (seat == '#')
                {
                    return true;
                }

                if (seat == 'L')
                {
                    return false;
                }
            }

            return false;
        }

        private long CalculateScore()
        {
            long result = 0;
            for (var i = 0; i < _map.Count; i++)
            {
                var line = _map[i];
                for (var j = 0; j < line.Columns.Count; j++)
                {
                    if (line.Columns[j] == '#')
                    {
                        result += i + 1 * j + 1;
                    }
                }
            }

            return result;
        }

        private List<(int Row, int Column)> DetermineSeatsInDirection(int row, int column, int rowD, int columnD)
        {
            var result = new List<(int Row, int Column)>();

            var minRow = 1;
            var maxRow = _map.Count;
            var minColumn = 1;
            var maxColumn = _map[0].Columns.Count;

            while (true) 
            {
                row += rowD;
                column += columnD;

                if (row < minRow || column < minColumn || row > maxRow || column > maxColumn)
                {
                    return result;
                }

                result.Add((row, column));
            }
        }

        private List<(int Row, int Column)> DetermineAdjacentSeats(int row, int column)
        {
            var result = new List<(int Row, int Column)>();

            var maxColumn = _map[0].Columns.Count;
            if (row > 1)
            {
                if (column != 1) // not the most left
                {
                    result.Add((row - 1, column - 1));
                }
                
                if (column != maxColumn) // not the most right
                {
                    result.Add((row - 1, column + 1));
                }
                
                result.Add((row - 1, column));
            }

            if (column != 1) // not the most left
            {
                result.Add((row, column - 1));
            }

            if (column != maxColumn)
            {
                result.Add((row, column + 1));
            }

            var maxRow = _map.Count;
            if (row < maxRow)
            {
                if (column != 1) // not the most left
                {
                    result.Add((row + 1, column - 1));
                }
                
                if (column != maxColumn) // not the most right
                {
                    result.Add((row + 1, column + 1));
                }
                
                result.Add((row + 1, column));
            }

            return result;
        }
    }
}