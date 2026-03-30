// ActivityLog.cs
// EXCEEDS REQUIREMENTS: Tracks and persists a log of completed activity sessions.
// Saves to and loads from a text file (log.txt) in the working directory.

using System;
using System.Collections.Generic;
using System.IO;

public class ActivityLog
{
    private string _logFilePath;
    private List<string> _sessionEntries;

    public ActivityLog(string logFilePath = "log.txt")
    {
        _logFilePath = logFilePath;
        _sessionEntries = new List<string>();
        LoadLog();
    }

    // Add a new log entry for a completed activity
    public void RecordActivity(string activityName, int durationSeconds)
    {
        string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {activityName} | {durationSeconds}s";
        _sessionEntries.Add(entry);
        SaveLog();
    }

    // Display a summary of all recorded sessions
    public void DisplayLog()
    {
        Console.Clear();
        Console.WriteLine("=== Activity Log ===\n");

        if (_sessionEntries.Count == 0)
        {
            Console.WriteLine("No activities recorded yet.");
        }
        else
        {
            foreach (string entry in _sessionEntries)
            {
                Console.WriteLine(entry);
            }
            Console.WriteLine($"\nTotal sessions logged: {_sessionEntries.Count}");
        }

        Console.WriteLine("\nPress Enter to return to the menu.");
        Console.ReadLine();
    }

    private void SaveLog()
    {
        try
        {
            File.WriteAllLines(_logFilePath, _sessionEntries);
        }
        catch (Exception)
        {
            // Silently fail if we can't write — log is a bonus feature
        }
    }

    private void LoadLog()
    {
        try
        {
            if (File.Exists(_logFilePath))
            {
                string[] lines = File.ReadAllLines(_logFilePath);
                _sessionEntries = new List<string>(lines);
            }
        }
        catch (Exception)
        {
            _sessionEntries = new List<string>();
        }
    }
}
