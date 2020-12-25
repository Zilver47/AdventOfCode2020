using System;
using System.Collections.Generic;
using System.Linq;

namespace Day22
{
    public class LineParser
    {
        public IEnumerable<Player> Parse(IEnumerable<string> lines)
        {
            var result = new List<Player>();
            var combinedLines = new List<string>();
            foreach (var line in lines)
            {
                if (line.StartsWith("Player"))
                {
                    combinedLines = new List<string>();
                }
                else if (string.IsNullOrEmpty(line))
                {
                    result.Add(new Player(combinedLines));
                }

                combinedLines.Add(line);
            }
            
            result.Add(new Player(combinedLines));

            return result;
        }
    }

    public class Player
    {
        public int Id { get; }

        public Queue<int> Cards { get; set; }

        public Player(IList<string> lines)
        {
            Cards = new Queue<int>();

            Id = int.Parse(lines[0].Replace("Player ", string.Empty).Replace(":", string.Empty));
            
            lines.RemoveAt(0);
            foreach (var line in lines)
            {
                Cards.Enqueue(int.Parse(line));
            }
        }

        public Player(in int oneId, IEnumerable<int> cards)
        {
            Cards = new Queue<int>();

            Id = oneId;
            foreach (var card in cards)
            {
                Cards.Enqueue(card);
            }
        }

        public int GetTopCard()
        {
            return Cards.Dequeue();
        }

        public void AddCards(IEnumerable<int> cards)
        {
            foreach (var card in cards.OrderByDescending(x => x))
            {
                Cards.Enqueue(card);
            }
        }

        public void AddCard(int card)
        {
            Cards.Enqueue(card);
        }

        public List<int> GetCardScore()
        {
            var copy = Cards.ToList();
            return copy;
        }

        public string GetCards()
        {
            var copy = Cards.ToList();
            return string.Join(',', copy);
        }
    }
}