using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Day13
{
    public class One : IAnswerGenerator
    {
        private readonly int _timestamp;
        private readonly List<string> _busses;

        public One(IEnumerable<string> input)
        {
            var parsedInput = new LineParser().Parse(input);
            _timestamp = parsedInput.Item1;
            _busses = parsedInput.Item2;
        }

        public long Generate()
        {
            var departures = new Dictionary<int, int>();
            foreach (var bus in _busses)
            {
                if (bus == "x") continue;

                var busId = int.Parse(bus);
                var lastDeparture = _timestamp % busId;
                departures.Add(busId, busId - lastDeparture);
            }

            var nextBusTime = departures.Min(pair => pair.Value);
            var nextBus = departures.Single(pair => pair.Value == nextBusTime).Key;

            return nextBus * nextBusTime;
        }
    }
}