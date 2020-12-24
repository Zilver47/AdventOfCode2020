using System;
using System.Collections.Generic;

namespace Day24
{
    public class Tile : IEquatable<Tile>
    {
        public int X { get; }
        public int Y { get; }

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IEnumerable<Tile> GetAdjacent()
        {
            return new[]
            {
                new Tile(X + 2, Y),
                new Tile(X + 1, Y + 1),
                new Tile(X - 1, Y + 1),
                new Tile(X - 2, Y),
                new Tile(X - 1, Y - 1),
                new Tile(X + 1, Y - 1),
            };
        }

        public bool Equals(Tile other)
            => other != null && X == other.X && Y == other.Y;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Tile)obj);
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }
    }
}
