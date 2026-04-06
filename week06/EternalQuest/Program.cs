/*
 * Eternal Quest Program
 * 
 * EXCEEDS REQUIREMENTS — The following features go beyond the core specification:
 *
 * 1. NEGATIVE GOALS (NegativeGoal.cs):
 *    A new goal type that deducts points each time a bad habit is recorded.
 *    Example: "Skipped workout" costs 150 points each time recorded.
 *    This mirrors real life where bad habits set us back, making the gamification
 *    more meaningful and personal.
 *
 * 2. PROGRESS GOALS (ProgressGoal.cs):
 *    A new goal type where users log partial progress toward a large target.
 *    Points are awarded proportionally per unit of progress recorded.
 *    Example: "Run 100 miles total" — record 3 miles today, earn 3% of total points.
 *    The user is prompted for the amount of progress each time they record the event.
 *
 * 3. LEVELING SYSTEM (GoalManager.cs — GetCurrentLevel / GetCurrentTitle):
 *    As the user earns points, they advance through named levels (e.g., "Novice
 *    Adventurer" → "Eternal Champion" → "Ascended Guardian"). When the user levels
 *    up after recording an event, a congratulatory banner is shown. The current
 *    level and title are displayed on the main screen, giving players a clear sense
 *    of progression and motivation to keep going.
 */

using System;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();

        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║        ETERNAL QUEST PROGRAM         ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.Write("\nEnter your name, adventurer: ");
        string name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name))
            manager.SetPlayerName(name);

        bool running = true;

        while (running)
        {
            Console.WriteLine("\n══════════════════════════════════════");
            manager.DisplayPlayerInfo();
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("  Menu:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("\n  Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    manager.CreateGoal();
                    break;

                case "2":
                    Console.WriteLine("\n--- Your Goals ---");
                    manager.DisplayGoals();
                    break;

                case "3":
                    manager.SaveGoals();
                    break;

                case "4":
                    manager.LoadGoals();
                    break;

                case "5":
                    manager.RecordEvent();
                    break;

                case "6":
                    running = false;
                    Console.WriteLine("\nFarewell, " + manager.GetPlayerName() + "! Keep up the eternal quest!");
                    break;

                default:
                    Console.WriteLine("  Invalid option. Please try again.");
                    break;
            }
        }
    }
}
