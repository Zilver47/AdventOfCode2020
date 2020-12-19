using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using AdventOfCode;

public static class Start
{
    static void Main()
    {
        var lines = File.ReadAllLines("input.txt");
        

        
        IAnswerGenerator generator = new Day19.One(lines);

        //Console.WriteLine("Answer 1: " + generator.Generate());
        
        generator = new Day19.Two(lines);

        Console.WriteLine("Answer 2: " + generator.Generate());
        Console.ReadLine();
    }
}