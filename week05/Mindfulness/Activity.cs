// Activity.cs
// Base class for all mindfulness activities.
// Contains shared attributes and behaviors to avoid code duplication (DRY principle).

using System;
using System.Threading;

public class Activity
{
    // Protected so derived classes can access duration for their loops
    protected string _name;
    protected string _description;
    protected int _duration;

    // Public read-only property so Program.cs can log the duration after a session
    public int Duration
    {
        get { return _duration; }
    }

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _duration = 0;
    }

    // Shared starting message: shows name, description, asks for duration, then pauses
    public void DisplayStartMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name}.\n");
        Console.WriteLine($"{_description}\n");
        Console.Write("How long, in seconds, would you like for your session? ");
        _duration = int.Parse(Console.ReadLine());
        Console.WriteLine("\nGet ready...");
        ShowSpinner(3);
    }

    // Shared ending message: congratulates the user, names activity and duration
    public void DisplayEndMessage()
    {
        Console.WriteLine("\nWell done!!");
        ShowSpinner(3);
        Console.WriteLine($"\nYou have completed another {_duration} seconds of the {_name}.");
        ShowSpinner(3);
    }

    // Spinner animation using backspace trick: -, \, |, /
    public void ShowSpinner(int seconds)
    {
        string[] spinnerFrames = { "-", "\\", "|", "/" };
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int i = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write(spinnerFrames[i % spinnerFrames.Length]);
            Thread.Sleep(250);
            Console.Write("\b \b");
            i++;
        }
    }

    // Countdown animation: counts down from 'seconds' to 1
    public void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            // Erase the digit(s) — handle multi-digit numbers
            int digits = i.ToString().Length;
            for (int d = 0; d < digits; d++)
                Console.Write("\b \b");
        }
    }
}
