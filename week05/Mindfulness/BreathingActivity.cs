// BreathingActivity.cs
// Guides the user through a timed session of deep breathing.
// Alternates "Breathe in..." and "Breathe out..." with a countdown pause after each.

using System;

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. " +
            "Clear your mind and focus on your breathing.")
    {
    }

    public void Run()
    {
        DisplayStartMessage();

        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        bool breatheIn = true;

        while (DateTime.Now < endTime)
        {
            if (breatheIn)
            {
                Console.Write("\nBreathe in... ");
            }
            else
            {
                Console.Write("\nBreathe out... ");
            }

            // Countdown for 4 seconds each breath phase
            int breathSeconds = 4;
            // Don't exceed remaining time
            TimeSpan remaining = endTime - DateTime.Now;
            if (remaining.TotalSeconds < breathSeconds)
                breathSeconds = (int)remaining.TotalSeconds;

            if (breathSeconds > 0)
                ShowCountdown(breathSeconds);

            breatheIn = !breatheIn;
        }

        Console.WriteLine();
        DisplayEndMessage();
    }
}
