using System;
using System.Diagnostics;
using System.IO;

public static class Start
{
    static void Main()
    {
        Console.WriteLine("Start answering...");

        var timer = new Stopwatch();
        timer.Start();
        var lines = File.ReadAllLines("input.txt");
        IAnswerGenerator generator = new Day23.One(lines);
        Console.WriteLine("Answer 1: " + generator.Generate());
        
        timer.Stop();
        Console.WriteLine($"Verstreken tijd: {timer.Elapsed.TotalSeconds}");

        timer.Restart();
        generator = new Day23.Two(lines);

        Console.WriteLine("Answer 2: " + generator.Generate());
        
        timer.Stop();
        Console.WriteLine($"Verstreken tijd: {timer.Elapsed.TotalSeconds}");

        Console.ReadLine();
    }
}