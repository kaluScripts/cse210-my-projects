// Exceeding Requirements:
// 1. The PromptGenerator class manages a pool of 10 unique prompts (double the minimum of 5),
//    making the journaling experience more varied and engaging over time.
// 2. The Entry class includes safe serialization/deserialization: any occurrence of the
//    separator sequence inside field content is escaped to "[SEP]" on save and restored on load,
//    preventing file corruption even if the user types the separator characters.
// 3. When loading a file, malformed lines are gracefully skipped and a count of skipped entries
//    is reported so the user is informed without crashing the program.
// 4. The menu displays a live entry count after each action so the user always knows the
//    current state of their journal without having to display all entries.
// 5. Blank / whitespace-only responses are rejected during entry writing, prompting the user
//    to try again, so the journal never contains empty entries.

using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();
        bool running = true;

        Console.WriteLine("Welcome to your Personal Journal!");
        Console.WriteLine();

        while (running)
        {
            DisplayMenu(journal.EntryCount());
            string choice = Console.ReadLine() ?? "";
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry(journal, promptGenerator);
                    break;

                case "2":
                    journal.DisplayAll();
                    break;

                case "3":
                    SaveJournal(journal);
                    break;

                case "4":
                    LoadJournal(journal);
                    break;

                case "5":
                    Console.WriteLine("Goodbye! Keep writing!");
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please enter a number from 1 to 5.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void DisplayMenu(int entryCount)
    {
        Console.WriteLine($"--- Journal Menu ({entryCount} entr{(entryCount == 1 ? "y" : "ies")}) ---");
        Console.WriteLine("1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save the journal to a file");
        Console.WriteLine("4. Load the journal from a file");
        Console.WriteLine("5. Quit");
        Console.Write("What would you like to do? ");
    }

    static void WriteNewEntry(Journal journal, PromptGenerator promptGenerator)
    {
        string prompt = promptGenerator.GetRandomPrompt();
        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("> ");

        string response = "";
        while (string.IsNullOrWhiteSpace(response))
        {
            response = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(response))
            {
                Console.WriteLine("Response cannot be empty. Please write something:");
                Console.Write("> ");
            }
        }

        string date = DateTime.Now.ToShortDateString();
        Entry entry = new Entry(prompt, response, date);
        journal.AddEntry(entry);

        Console.WriteLine("Entry saved!");
    }

    static void SaveJournal(Journal journal)
    {
        Console.Write("Enter filename to save to: ");
        string filename = (Console.ReadLine() ?? "").Trim();

        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("No filename entered. Save cancelled.");
            return;
        }

        journal.SaveToFile(filename);
    }

    static void LoadJournal(Journal journal)
    {
        Console.Write("Enter filename to load from: ");
        string filename = (Console.ReadLine() ?? "").Trim();

        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("No filename entered. Load cancelled.");
            return;
        }

        journal.LoadFromFile(filename);
    }
}
