using System;

public static class Start
{
    static void Main()
    {
        var generator = new Day1Two();

        Console.WriteLine("Answer: " + generator.Generate());
        Console.ReadLine();
    }
}