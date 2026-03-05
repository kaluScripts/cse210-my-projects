using System;

class Program
{
    static void Main(string[] args)
    {
        // Ask the user for their grade percentage
        Console.Write("What is your grade percentage? ");
        string input = Console.ReadLine();

        // Convert the input from string to integer
        int percentage = int.Parse(input);

        // Determine the letter grade
        string letter = "";

        if (percentage >= 90)
        {
            letter = "A";
        }
        else if (percentage >= 80)
        {
            letter = "B";
        }
        else if (percentage >= 70)
        {
            letter = "C";
        }
        else if (percentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Determine the grade sign (+ or -)
        int lastDigit = percentage % 10;
        string sign = "";

        if (lastDigit >= 7)
        {
            sign = "+";
        }
        else if (lastDigit < 3)
        {
            sign = "-";
        }

        // Handle special cases
        if (letter == "A" && sign == "+")
        {
            sign = ""; // No A+
        }

        if (letter == "F")
        {
            sign = ""; // No F+ or F-
        }

        // Display the final grade
        Console.WriteLine($"Your grade is {letter}{sign}");

        // Determine if the student passed
        if (percentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Don't give up! Keep working and try again.");
        }
    }
}
