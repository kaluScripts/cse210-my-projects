// Program.cs
// Mindfulness Program - W05 Project
//
// EXCEEDS REQUIREMENTS:
// 1. Added a fourth activity: GratitudeActivity - guides users to reflect deeply on specific
//    things they are grateful for and why, going beyond simple listing.
// 2. Activity Log: The program tracks every completed session (activity name, duration, timestamp)
//    and saves it to a file (log.txt). Users can view their history from the main menu.
//    The log persists across program runs (saved/loaded from disk).
// 3. No-repeat questions: Both ReflectionActivity and GratitudeActivity use a pool-depletion
//    strategy so questions/prompts are never repeated in a session until every option has been
//    shown at least once.

using System;

class Program
{
    static void Main(string[] args)
    {
        ActivityLog log = new ActivityLog();
        bool keepRunning = true;

        while (keepRunning)
        {
            Console.Clear();
            Console.WriteLine("=== Mindfulness Program ===\n");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start Breathing Activity");
            Console.WriteLine("  2. Start Reflection Activity");
            Console.WriteLine("  3. Start Listing Activity");
            Console.WriteLine("  4. Start Gratitude Activity");
            Console.WriteLine("  5. View Activity Log");
            Console.WriteLine("  6. Quit");
            Console.Write("\nSelect a choice from the menu: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BreathingActivity breathing = new BreathingActivity();
                    breathing.Run();
                    log.RecordActivity("Breathing Activity", GetDurationFromActivity(breathing));
                    break;

                case "2":
                    ReflectionActivity reflection = new ReflectionActivity();
                    reflection.Run();
                    log.RecordActivity("Reflection Activity", GetDurationFromActivity(reflection));
                    break;

                case "3":
                    ListingActivity listing = new ListingActivity();
                    listing.Run();
                    log.RecordActivity("Listing Activity", GetDurationFromActivity(listing));
                    break;

                case "4":
                    GratitudeActivity gratitude = new GratitudeActivity();
                    gratitude.Run();
                    log.RecordActivity("Gratitude Activity", GetDurationFromActivity(gratitude));
                    break;

                case "5":
                    log.DisplayLog();
                    break;

                case "6":
                    Console.WriteLine("\nGoodbye! Keep up the great mindfulness work.");
                    keepRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid option. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    // Helper to retrieve the duration from an activity after it has run.
    // Uses the public accessor; we expose duration via a property on Activity.
    static int GetDurationFromActivity(Activity activity)
    {
        return activity.Duration;
    }
}
