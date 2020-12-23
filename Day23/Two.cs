using System;
using System.Collections.Generic;
using System.Linq;

namespace Day23
{
    public class Two : IAnswerGenerator
    {
        private readonly Dictionary<int, Cup> _cups;
        private Cup _currentCup;
        private readonly int _size;
        private readonly int _lowestValue;
        private readonly int _highestValue;

        public Two(IEnumerable<string> input)
        {
            var values = new LineParser().Parse(input).ToList();

            values.AddRange(Enumerable.Range(values.Max() + 1, 1000000 - values.Count));
            _lowestValue = values.Min();
            _highestValue = values.Max();
            _size = values.Count;

            var listOfCups = values.Select(v => new Cup(v)).ToList();
            for (var i = 0; i < _size; i++)
            {
                listOfCups[i].Next = listOfCups[(i + 1) % _size];
            }
            
            _cups = listOfCups.ToDictionary(c => c.Value, c => c);
        }

        public long Generate()
        {
            var rounds = 10000000;
            
            //Print(0);

            _currentCup = _cups.First().Value;
            for (var i = 0; i < rounds; i++)
            {
                Round();
                
                //Print(i);
            }

            var cupOne = _cups[1];
            long result = (long) cupOne.Next.Value * (long) cupOne.Next.Next.Value;
            return result;
        }

        private void Round()
        {
            var destination = _currentCup.Value;
            do
            {
                destination--;

                if (destination < _lowestValue)
                {
                    destination = _highestValue;
                }
            } while (destination == _currentCup.Next.Value || 
                     destination == _currentCup.Next.Next.Value ||
                     destination == _currentCup.Next.Next.Next.Value);

            var destinationCup = _cups[destination];

            var lastOfThree = _currentCup.Next.Next.Next;
            var currentNext = _currentCup.Next;
            var destinationNext = destinationCup.Next;

            _currentCup.Next = lastOfThree.Next;
            destinationCup.Next = currentNext;
            lastOfThree.Next = destinationNext;

            _currentCup = _currentCup.Next;
        }
        
        private void Print(int moves)
        {
            Console.WriteLine($"-- move {moves + 1} --");

            var cupsAsString = string.Empty;
            var cup = _cups.First().Value;
            for (var i = 0; i < _cups.Count; i++)
            {
                if (cup.Value == _currentCup.Value)
                {
                    cupsAsString += $"({cup.Value}) ";
                }
                else
                {
                    cupsAsString += cup.Value + " ";
                }
                
                cup = cup.Next;
            }

            Console.WriteLine($"cups: {cupsAsString}");
            Console.WriteLine();
        }
    }

    internal class Cup
    {
        public int Value { get; }
        public Cup Next { get; set; }

        public Cup(int value)
        {
            Value = value;
        }
    }
}