using System;

class Program
{
    static void Main(string[] args)
    {
        // Ask for the user's first name
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();

        // Ask for the user's last name
        Console.Write("What is your last name? ");
        string lastName = Console.ReadLine();

        // Display the formatted result
        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}.");
    }
}
