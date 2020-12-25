using System;
using System.Collections.Generic;
using System.Linq;

namespace Day22
{
    public class One : IAnswerGenerator
    {
        private readonly List<Player> _players;
        private readonly int _totalNumberOfCards;

        public One(IEnumerable<string> input)
        {
            _players = new LineParser().Parse(input).ToList();
            _totalNumberOfCards = _players.Count * _players[0].Cards.Count;
        }

        public long Generate()
        {
            var round = 1;
            Player winner = null;
            while (winner == null)
            {
                round++;
                
                Round();

                //Print(round);

                winner = IsWinner();
            }

            return Calculate(winner);
        }

        private long Calculate(Player winner)
        {
            long result = 0;
            for (var i = _totalNumberOfCards; i > 0; i--)
            {
                result += (long) i * winner.GetTopCard();
            }

            return result;
        }

        private void Round()
        {
            var cards = _players.ToDictionary(player => player, player => player.GetTopCard());

            if (cards.First().Value > cards.Last().Value)
            {
                cards.First().Key.AddCards(cards.Values);
            }
            else
            {
                cards.Last().Key.AddCards(cards.Values);
            }
        }

        private Player IsWinner()
        {
            return _players.FirstOrDefault(p => p.Cards.Count == _totalNumberOfCards);
        }

        private void Print(int round)
        {
            Console.WriteLine($"-- Round {round} --");
            foreach (var player in _players)
            {
                Console.WriteLine($"Player {player.Id} desk: {string.Join(',', player.Cards)}");
            }
            Console.WriteLine();
        }
    }
}