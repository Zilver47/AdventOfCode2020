using System;

public static class Start
{
    static void Main()
    {
        var generator = new Day2Two();

        Console.WriteLine("Answer: " + generator.Generate());
        Console.ReadLine();
    }
}