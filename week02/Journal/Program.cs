using System;

// EXCEEDING REQUIREMENTS
// 1. I Added more prompts than required and encapsulated display logic inside Entry class.
// 2. I also added additional reflective prompts to improve journaling quality.

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        int choice = 0;

        while (choice != 5)
        {
            Console.WriteLine("\nJournal Menu");
            Console.WriteLine("1. Write New Entry");
            Console.WriteLine("2. Display Journal");
            Console.WriteLine("3. Save Journal");
            Console.WriteLine("4. Load Journal");
            Console.WriteLine("5. Quit");

            Console.Write("Select a choice: ");
            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                string prompt = promptGenerator.GetRandomPrompt();

                Console.WriteLine(prompt);
                Console.Write("> ");
                string response = Console.ReadLine();

                string date = DateTime.Now.ToShortDateString();

                Entry entry = new Entry();
                entry._date = date;
                entry._promptText = prompt;
                entry._entryText = response;

                journal.AddEntry(entry);
            }

            else if (choice == 2)
            {
                journal.DisplayAll();
            }

            else if (choice == 3)
            {
                Console.Write("Enter filename: ");
                string fileName = Console.ReadLine();
                journal.SaveToFile(fileName);
            }

            else if (choice == 4)
            {
                Console.Write("Enter filename: ");
                string fileName = Console.ReadLine();
                journal.LoadFromFile(fileName);
            }
        }
    }
}
