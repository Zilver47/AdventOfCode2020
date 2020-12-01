using System;

public static class Start
{
    static void Main()
    {
        var generator = new Day1One();

        Console.WriteLine("Answer: " + generator.Generate());
        Console.ReadLine();
    }
}