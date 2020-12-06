using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    public class LineParser
    {
        public IEnumerable<Group> Parse(IEnumerable<string> lines)
        {
            var groupLines = new List<string>();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    yield return new Group(groupLines);

                    groupLines.Clear();
                }
                else
                {
                    groupLines.Add(line);
                }
            }
            
            yield return new Group(groupLines);
        }
    }

    public class Group
    {
        public Group(IEnumerable<string> groupLines)
        {
            DeclarationForms = new List<DeclarationForm>();
            
            foreach (var line in groupLines)
            {
                DeclarationForms.Add(new DeclarationForm(line));
            }
        }

        public List<DeclarationForm> DeclarationForms { get; set; }

        public int CountPositiveAnswers()
        {
            var all = new List<char>();
            foreach (var df in DeclarationForms)
            {
                all.AddRange(df.PossitiveAnswers);
            }

            return all.Distinct().Count();
        }
        
        public int CountPositiveAnswersByEveryOne()
        {
            var current = new List<char>("abcdefghijklmnopqrstuvwxyz".ToCharArray());

            foreach (var filtered in DeclarationForms.Select(df => df.PossitiveAnswers.Intersect(current).ToList()))
            {
                current = filtered;
            }

            return current.Count;
        }
    }

    public class DeclarationForm
    {
        public DeclarationForm(string line)
        {
            PossitiveAnswers = new List<char>(line.ToCharArray());
        }

        public List<char> PossitiveAnswers { get; set; }
    }
}