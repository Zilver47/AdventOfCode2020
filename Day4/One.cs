using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
    public class One : IAnswerGenerator
    {
        private readonly List<InputLine> _passwords;
        private readonly HashSet<string> _requiredFields;

        public One(IEnumerable<string> input)
        {
            _passwords = new LineParser().Parse(input).ToList();
            _requiredFields = new HashSet<string>
            {
                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid",
            };
        }

        public string Generate()
        {
            var result = _passwords.Sum(password => IsValid(password) && HasValidValues(password) ? 1 : 0);

            return result.ToString();
        }

        private bool IsValid(InputLine password)
        {
            return _requiredFields.All(requiredField => password.Fields.ContainsKey(requiredField));
        }

        private bool HasValidValues(InputLine password)
        {
            return IsValidRange(password.Fields["byr"], 1920, 2002) &&
                   IsValidRange(password.Fields["iyr"], 2010, 2020) &&
                   IsValidRange(password.Fields["eyr"], 2020, 2030) &&
                   IsValidHeight(password.Fields["hgt"]) &&
                   IsValidHairColor(password.Fields["hcl"]) &&
                   IsValidEyeColor(password.Fields["ecl"]) &&
                   IsValidPasswordId(password.Fields["pid"]);
        }

        private bool IsValidEyeColor(string value)
        {
            var validValues = new List<string> {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
            return validValues.Any(validValue => value == validValue);
        }


        private bool IsValidHairColor(string value)
        {
            var isValidHex = Regex.IsMatch(value, "^#(?:[0-9a-fA-F]{3}){1,2}$");
            return isValidHex && value.Length == 7;
        }

        private bool IsValidHeight(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            if (value.Contains("cm"))
            {
                var number = value.Replace("cm", string.Empty);
                if (int.TryParse(number, out var parsedNumber))
                {
                    return parsedNumber >= 150 && parsedNumber <= 193;
                }
            }
            else if (value.Contains("in"))
            {
                var number = value.Replace("in", string.Empty);
                if (int.TryParse(number, out var parsedNumber))
                {
                    return parsedNumber >= 59 && parsedNumber <= 76;
                }
            }

            return false;
        }
        
        private bool IsValidRange(string value, int min, int max)
        {
            if (string.IsNullOrEmpty(value)) return false;

            if (int.TryParse(value, out var parsedValue))
            {
                return parsedValue >= min && parsedValue <= max;
            }

            return false;
        }
        
        private bool IsValidPasswordId(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            if (int.TryParse(value, out var parsedValue))
            {
                return value.Length == 9;
            }

            return false;
        }
    }
}