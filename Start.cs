using System;
using System.IO;

public static class Start
{
    static void Main()
    {
        var lines = File.ReadAllLines("input.txt");
        IAnswerGenerator generator = new Day16.One(lines);

        Console.WriteLine("Answer 1: " + generator.Generate());
        
        generator = new Day16.Two(lines);

        Console.WriteLine("Answer 2: " + generator.Generate());
        Console.ReadLine();
    }
}