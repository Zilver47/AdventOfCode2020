using System;
using System.Collections.Generic;
using System.Linq;

namespace Day21
{
    public class Two : IAnswerGenerator
    {
        private readonly List<Food> _food;

        public Two(IEnumerable<string> input)
        {
            _food = new LineParser().Parse(input).ToList();
        }

        public long Generate()
        {
            var possibilities = new Dictionary<string, List<Food>>();
            foreach (var food in _food)
            {
                foreach (var allergen in food.Allergens)
                {
                    if (!possibilities.ContainsKey(allergen))
                    {
                        possibilities.Add(allergen, new List<Food>());
                    }

                    possibilities[allergen].Add(food);
                }
            }
            
            var allergens = new Dictionary<string, string>();
            while (true)
            {
                var unknownAllergens = possibilities.Keys.Where(p => !allergens.ContainsKey(p));
                foreach (var unknownAllergen in unknownAllergens)
                {
                    var foodWithAllergen = possibilities[unknownAllergen]
                        .Select(f => f.Ingredients)
                        ;
                    var possibleIngredientsWithAllergen = 
                        foodWithAllergen.Aggregate((a, b) => a.Intersect(b).ToList())
                            .Except(allergens.Values);
                    if (possibleIngredientsWithAllergen.Count() == 1)
                    {
                        allergens.Add(unknownAllergen, possibleIngredientsWithAllergen.Single());
                    }
                }

                if (allergens.Count == possibilities.Count)
                {
                    break;
                }
            }

            var orderedAllergens = allergens.OrderBy(kv => kv.Key);
            var result = string.Join(',', orderedAllergens.Select(kv => kv.Value));

            Console.WriteLine(result);

            return -1;
        }
    }
}