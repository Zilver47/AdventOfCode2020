using System.Collections.Generic;
using System.Linq;

namespace Day24
{
    public class TileMap : Dictionary<Tile, bool>
    {
        public void AddOrUpdate(Tile tile,  bool value)
        {
            if (ContainsKey(tile)) return;
            
            Add(tile, value);
        }
        
        public void Flip(Tile tile)
        {
            if (!TryAdd(tile, true))
            {
                this[tile] = !this[tile];
            }
        }
        
        public bool IsFlipped(Tile tile)
        {
            return ContainsKey(tile) && this[tile];
        }

        public int CountFlipped()
        {
            return Values.Count(v => v);
        }
    }
}
