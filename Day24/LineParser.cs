using System;
using System.Collections.Generic;
using System.Linq;

namespace Day24
{
    public class LineParser
    {
        public IEnumerable<List<string>> Parse(IEnumerable<string> lines)
        {
            var result = new List<List<string>>();
            foreach (var line in lines)
            {
                var direction = new List<string>();
                for (int i = 0; i < line.Length; i++)
                {
                    var character = line[i];
                    if (character == 's' || character == 'n')
                    {
                        direction.Add(character.ToString() + line[++i]);
                    }
                    else
                    {
                        direction.Add(character.ToString());
                    }
                }

                result.Add(direction);
            }

            return result;
        }
    }
}