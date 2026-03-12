using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> _entries;
    private static readonly string Separator = "~|~";

    public Journal()
    {
        _entries = new List<Entry>();
    }

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries in the journal yet.");
            return;
        }

        Console.WriteLine("===== Journal Entries =====");
        Console.WriteLine();
        foreach (Entry entry in _entries)
        {
            entry.Display();
            Console.WriteLine("---------------------------");
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine(entry.ToFileString(Separator));
            }
        }

        Console.WriteLine($"Journal saved to '{filename}' ({_entries.Count} entries).");
    }

    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine($"Error: File '{filename}' not found.");
            return;
        }

        _entries.Clear();

        string[] lines = File.ReadAllLines(filename);
        int loaded = 0;
        int skipped = 0;

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            try
            {
                Entry entry = Entry.FromFileString(line, Separator);
                _entries.Add(entry);
                loaded++;
            }
            catch (FormatException)
            {
                skipped++;
            }
        }

        Console.WriteLine($"Journal loaded from '{filename}' ({loaded} entries loaded{(skipped > 0 ? $", {skipped} skipped due to errors" : "")}).");
    }

    public int EntryCount()
    {
        return _entries.Count;
    }
}
