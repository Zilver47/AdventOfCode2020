using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    public class AnswerGenerator : IAnswerGenerator
    {
        private readonly List<long> _adapters;

        public AnswerGenerator(IEnumerable<string> input)
        {
            _adapters = new LineParser().Parse(input);
        }

        public long Generate()
        {
            var deviceAdapter = _adapters.Max() + 3;
            _adapters.Add(deviceAdapter);

            var numberOfAdapters = _adapters.Count;
            _adapters.Add(0);
            _adapters.Sort();

            // Part 1
            //long previous = 0;
            //for (var i = 0; i < _adapters.Count; i++)
            //{
            //    var current = _adapters[i];
            //    var difference = current - previous;
            //    _differences[(int)difference]++;

            //    previous = current;
            //}

            //Console.WriteLine($"{_differences[1]} differences of 1");
            //Console.WriteLine($"{_differences[2]} differences of 2");
            //Console.WriteLine($"{_differences[3]} differences of 3");
            //return _differences[1] * _differences[3];

            long total = 1;
            var cache = new Dictionary<long, long> {{0, 1}};
            foreach (var adapter in _adapters)
            {
                var nextOptions = CountNextOptions(adapter);
                if (nextOptions.Count == 0)
                {
                    return total;
                }

                var numberOfPathsToAdapter = cache[adapter];
                cache.Remove(adapter);

                foreach (var option in nextOptions)
                {
                    if (cache.ContainsKey(option))
                    {
                        cache[option] += numberOfPathsToAdapter;
                    }
                    else
                    {
                        cache.Add(option, numberOfPathsToAdapter);
                    }
                }

                total = numberOfPathsToAdapter * nextOptions.Count;
            }

            return total;
        }

        private List<long> CountNextOptions(long current)
        {
            return _adapters.Where(adapter => adapter > current && adapter <= current + 3).ToList();
        }
    }
}