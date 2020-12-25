using System;
using System.Collections.Generic;
using System.Linq;

namespace Day22
{
    public class Two : IAnswerGenerator
    {
        private readonly List<Player> _players;
        private readonly Dictionary<int, Dictionary<int, List<List<int>>>> _previousCardsScores;
        private readonly Dictionary<int, List<Player>> _games;

        public Two(IEnumerable<string> input)
        {
            _previousCardsScores = new Dictionary<int, Dictionary<int, List<List<int>>>>();
            
            _games = new Dictionary<int, List<Player>>();

            _players = new LineParser().Parse(input).ToList();
        }

        public long Generate()
        {
            _games.Add(1, _players);
            var winner = Play();

            return Calculate(winner);
        }

        private long Calculate(Player winner)
        {
            long result = 0;
            for (var i = winner.Cards.Count; i > 0; i--)
            {
                result += (long) i * winner.GetTopCard();
            }

            return result;
        }

        private Player Play(int game = 1)
        {
            var players = _games[game];

            _previousCardsScores.Add(game, new Dictionary<int, List<List<int>>>());
            _previousCardsScores[game].Add(1, new List<List<int>>());
            _previousCardsScores[game].Add(2, new List<List<int>>());

            var round = 1;
            Player winner = null;
            while (winner == null)
            {
                round++;

                if (PreventInfiniteGame(game, players, out var quickWinner))
                {
                    return quickWinner;
                }

                Round(players.First(), players.Last());
                
                winner = IsWinner(players);
            }

            return winner;
        }

        private bool PreventInfiniteGame(int game, List<Player> players, out Player play)
        {
            play = null;
            foreach (var player in players)
            {
                if (Contains(_previousCardsScores[game][player.Id], player.GetCardScore()))
                {
                    {
                        play = players.First();
                        return true;
                    }
                }

                _previousCardsScores[game][player.Id].Add(player.GetCardScore());
            }

            return false;
        }

        private bool Contains(IEnumerable<List<int>> previousDecks, IReadOnlyList<int> currentDeck)
        {
            var foundMatch = false;
            foreach (var deck in previousDecks)
            {
                if (deck.Count != currentDeck.Count) continue;
                
                for (var i = 0; i < deck.Count; i++)
                {
                    if (deck[i] != currentDeck[i]) break;

                    if (i == deck.Count - 1)
                    {
                        foundMatch = true;
                    }
                }
            }

            return foundMatch;
        }

        private void Round(Player one, Player two)
        {
            var oneCard = one.GetTopCard();
            var twoCard = two.GetTopCard();

            if (one.Cards.Count >= oneCard &&
                two.Cards.Count >= twoCard)
            {
                var oneCopy = new Player(one.Id, one.Cards.ToList().GetRange(0, oneCard));
                var twoCopy = new Player(two.Id, two.Cards.ToList().GetRange(0, twoCard));

                var game = _games.Keys.Max() + 1;
                _games.Add(game, new List<Player> { oneCopy, twoCopy });
                var winner = Play(game);
                if (winner.Id == one.Id)
                {
                    one.AddCard(oneCard);
                    one.AddCard(twoCard);
                }
                else
                {
                    two.AddCard(twoCard);
                    two.AddCard(oneCard);
                }
            }
            else
            {
                if (oneCard > twoCard)
                {
                    one.AddCard(oneCard);
                    one.AddCard(twoCard);
                }
                else
                {
                    two.AddCard(twoCard);
                    two.AddCard(oneCard);
                }
            }
        }

        private Player IsWinner(List<Player> players)
        {
            if (players.Any(p => p.Cards.Count == 0))
            {
                return players.FirstOrDefault(p => p.Cards.Count != 0);
            }

            return null;
        }

        private void Print(List<Player> players, int game, int round)
        {
            Console.WriteLine($"Game {game} round {round}");
            foreach (var player in players)
            {
                Console.WriteLine($"{string.Join(',', player.Cards)}");
            }
        }
    }
}