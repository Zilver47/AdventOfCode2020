using System;
using System.Collections.Generic;
using System.Linq;

namespace Day5
{
    public class One : IAnswerGenerator
    {
        private readonly List<SeatSpecification> _seatSpecifications;

        public One(IEnumerable<string> input)
        {
            _seatSpecifications = new LineParser().Parse(input).ToList();
        }

        public long Generate()
        {
            var result = new List<int>();

            foreach (var (row, column) in _seatSpecifications.Select(FindSeat))
            {
                result.Add(row * 8 + column);

                Console.WriteLine("");
            }

            result.Sort();
            var searchRange = result.GetRange(8, result.Count - 16);
            return FindMissingSeatId(searchRange);
        }

        private static long FindMissingSeatId(IReadOnlyList<int> searchRange)
        {
            var previousSeatId = searchRange[0] - 1;
            foreach (var seatId in searchRange)
            {
                if (seatId != previousSeatId + 1)
                {
                    return (seatId - 1);
                }

                previousSeatId = seatId;
            }

            return -1;
        }

        private (int Row, int Column) FindSeat(SeatSpecification seat)
        {
            var column = FindInRange(seat.Columns, (0, 7));
            var row = FindInRange(seat.Rows, (0, 127));
            Console.WriteLine($"row: {row} column: {column}");

            return (row, column);
        }

        private static int FindInRange(IEnumerable<char> specification, (int, int) range)
        {
            foreach (var item in specification)
            {
                if (item.Equals('F') || item.Equals('L'))
                {
                    range.Item2 -= ((range.Item2 - range.Item1) / 2) + 1;
                }
                else
                {
                    range.Item1 += ((range.Item2 - range.Item1) / 2) + 1;
                }
            }

            return range.Item1;
        }
    }
}