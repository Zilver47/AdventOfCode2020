using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Extensions;

namespace Day8
{
    public class AnswerGenerator : IAnswerGenerator
    {
        private readonly List<Instruction> _instructions;

        public AnswerGenerator(IEnumerable<string> input)
        {
            _instructions = new LineParser().Parse(input);
        }

        public string Generate()
        {
            var positionToSwap = -1;
            var instructions = new List<Instruction>(_instructions.ToArray());
            while (true)
            {
                var executionResult = Execute(instructions);
                if (executionResult.Item1)
                {
                    return executionResult.Item2.ToString();
                }

                instructions = _instructions.ToArray().ToList();
                do
                {
                    positionToSwap++;

                    instructions[positionToSwap].Operation = 
                        instructions[positionToSwap].Operation switch
                        {
                            "nop" => "jmp",
                            "jmp" => "nop",
                            _ => instructions[positionToSwap].Operation
                        };
                } 
                while (instructions[positionToSwap].Operation == "acc");
            }
        }

        private Tuple<bool, int> Execute(IList<Instruction> instructions)
        {
            var result = 0;
            var executedPositions = new HashSet<int>();
            for (var i = 0; i < instructions.Count; i++)
            {
                if (executedPositions.Contains(i))
                {
                    return new Tuple<bool, int>(false, 0);
                }

                executedPositions.Add(i);

                switch (instructions[i].Operation)
                {
                    case "acc":
                        result += instructions[i].Argument;
                        break;
                    case "jmp":
                        i += instructions[i].Argument - 1;
                        break;
                }
            }

            return new Tuple<bool, int>(true, result);
        }
    }
}