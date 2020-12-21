using System;
using System.Collections.Generic;
using System.Linq;

namespace Day21
{
    public class LineParser
    {
        public IEnumerable<Food> Parse(IEnumerable<string> lines)
        {
            return lines.Select(line => new Food(line));
        }
    }

    public class Food
    {
        public List<string> Ingredients { get; }
        public HashSet<string> Allergens { get; }

        public Food(string line)
        {
            Ingredients = new List<string>();
            Allergens = new HashSet<string>();

            var parts = line.Split('(');
            var ingredientsPart = parts[0].Trim();
            var ingredientsParts = ingredientsPart.Split(' ');
            Ingredients.AddRange(ingredientsParts);

            var allergensParts = parts[1].Replace("contains ", string.Empty).Replace(")", string.Empty).Split(',');
            foreach (var part in allergensParts)
            {
                Allergens.Add(part.Trim());
            }
        }
    }
}