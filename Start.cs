using System;
using System.IO;

public static class Start
{
    static void Main()
    {
        var lines = File.ReadAllLines("input.txt");
        var generator = new Day10.AnswerGenerator(lines);

        Console.WriteLine("Answer: " + generator.Generate());
        Console.ReadLine();
    }
}