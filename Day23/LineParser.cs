using System;
using System.Collections.Generic;
using System.Linq;

namespace Day23
{
    public class LineParser
    {
        public IEnumerable<int> Parse(IEnumerable<string> lines)
        {
            foreach (var character in lines.First())
            {
                yield return int.Parse(character.ToString());
            }
        }
    }
}