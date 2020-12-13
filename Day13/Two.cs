using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    public class Two : IAnswerGenerator
    {
        private readonly List<List<string>> _schedules;

        public Two(IEnumerable<string> input)
        {
            _schedules = new LineParserTwo().Parse(input);
        }

        public long Generate()
        {
            long result = -1;
            foreach (var schedule in _schedules)
            {
                result = DetermineTimestamp(schedule);
            }

            return result;
        }

        private long DetermineTimestamp(IReadOnlyList<string> schedule)
        {
            var offsets = GetBusIdsWithOffset(schedule);

            long timestamp = offsets.Keys.First();
            var indexOfNonFittingBus = 1;
            long step = offsets.Keys.First();
            while (true)
            {
                if (indexOfNonFittingBus < offsets.Count)
                {
                    var offset = offsets[offsets.Keys.ToArray()[indexOfNonFittingBus]];
                    var busId = offsets.Keys.ToArray()[indexOfNonFittingBus];
                    if ((timestamp + offset) % busId == 0)
                    {
                        step *= busId;
                        indexOfNonFittingBus++;
                    }
                }

                if (offsets.All(pair => (timestamp + pair.Value) % pair.Key == 0))
                {
                    return timestamp;
                }

                timestamp += step;
            }
        }

        private static Dictionary<int, int> GetBusIdsWithOffset(IReadOnlyList<string> schedule)
        {
            var offsets = new Dictionary<int, int>();
            for (var i = 0; i < schedule.Count; i++)
            {
                var bus = schedule[i];

                if (bus == "x") continue;

                var busId = int.Parse(bus);
                offsets.Add(busId, i);
            }

            return offsets;
        }
    }
}