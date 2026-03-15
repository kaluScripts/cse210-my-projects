// =============================================================================
// Scripture Memorizer Program
// Author : Darlene Kalu
// Course : CSE 210
//
// EXCEEDING CORE REQUIREMENTS:
//
//  1. SCRIPTURE LIBRARY FROM A FILE
//     A ScriptureLibrary class loads scriptures from "scriptures.txt" at runtime.
//     The file ships with 8 pre-loaded scriptures, and the user can add more by
//     editing the file.
//
//  2. RANDOM SCRIPTURE SELECTION
//     Instead of always showing the same scripture, the program randomly picks
//     one from the library each session, keeping practice varied and interesting.
//
//  3. PUNCTUATION-AWARE HIDING
//     The Word class preserves trailing punctuation (commas, periods, etc.)
//     when replacing a word with underscores, so the rhythm of the verse
//     remains visible even as content is hidden.
//
//  4. ONLY VISIBLE WORDS ARE TARGETED
//     HideRandomWords() builds a list of only the words that are not yet hidden
//     before selecting randomly, so no "wasted" turns occur. Every press of
//     Enter is guaranteed to hide new words.
//
//  5. GRACEFUL FALLBACK
//     If the library file is missing or empty, five hard-coded default scriptures
//     are used so the program always runs correctly out of the box.
// =============================================================================

using System;
using System.IO;

// Suppress nullable reference warnings for clean console output
#pragma warning disable CS8600, CS8602

class Program
{
    // Number of words hidden per Enter keypress
    private const int WordsHiddenPerRound = 3;

    static void Main(string[] args)
    {
        ScriptureLibrary library = BuildLibrary();

        Scripture scripture = library.GetRandomScripture();

        if (scripture == null)
        {
            Console.WriteLine("No scriptures available. Please check scriptures.txt.");
            return;
        }

        RunSession(scripture);
    }

    // ------------------------------------------------------------------
    // Private helpers
    // ------------------------------------------------------------------

    /// <summary>
    /// Loads scriptures from file; falls back to built-in defaults if needed.
    /// </summary>
    private static ScriptureLibrary BuildLibrary()
    {
        ScriptureLibrary library = new ScriptureLibrary();

        string filePath = "scriptures.txt";
        library.LoadFromFile(filePath);

        if (library.Count() == 0)
        {
            // Built-in fallback scriptures
            library.AddScripture(new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world that he gave his one and only Son, " +
                "that whoever believes in him shall not perish but have eternal life."));

            library.AddScripture(new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all your heart and lean not on your own understanding; " +
                "in all your ways submit to him, and he will make your paths straight."));

            library.AddScripture(new Scripture(
                new Reference("Joshua", 1, 9),
                "Have I not commanded you? Be strong and courageous. Do not be afraid; " +
                "do not be discouraged, for the Lord your God will be with you wherever you go."));

            library.AddScripture(new Scripture(
                new Reference("Philippians", 4, 13),
                "I can do all this through him who gives me strength."));

            library.AddScripture(new Scripture(
                new Reference("Romans", 8, 28),
                "And we know that in all things God works for the good of those who love him, " +
                "who have been called according to his purpose."));
        }

        return library;
    }

    /// <summary>
    /// Runs the interactive memorization loop for a single scripture.
    /// </summary>
    private static void RunSession(Scripture scripture)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("All words are hidden. Great work!");
                break;
            }

            Console.Write("Press Enter to continue or type 'quit' to exit: ");
            string input = Console.ReadLine();

            if (input != null && input.Trim().ToLower() == "quit")
            {
                Console.WriteLine("\nGoodbye! Keep practicing!");
                break;
            }

            scripture.HideRandomWords(WordsHiddenPerRound);
        }
    }
}
