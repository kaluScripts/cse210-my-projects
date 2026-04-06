using System;
using System.Collections.Generic;
using System.IO;

// Manages the list of goals, the user's score, and handles saving/loading.
// EXCEEDS REQUIREMENTS: Implements a leveling system with titles based on score.
public class GoalManager
{
    private List<Goal> _goals;
    private int _score;
    private string _playerName;

    // Level thresholds and titles for gamification
    private static readonly int[] _levelThresholds = new int[] { 0, 500, 1500, 3000, 6000, 10000, 20000, 50000 };
    private static readonly string[] _levelTitles = new string[]
    {
        "Novice Adventurer", "Apprentice Seeker", "Determined Pilgrim", "Courageous Wanderer",
        "Valiant Knight", "Eternal Champion", "Legendary Sage", "Ascended Guardian"
    };

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _playerName = "Adventurer";
    }

    public void SetPlayerName(string name) => _playerName = name;
    public string GetPlayerName() => _playerName;
    public int GetScore() => _score;

    public string GetCurrentTitle()
    {
        string title = _levelTitles[0];
        for (int i = 0; i < _levelThresholds.Length; i++)
        {
            if (_score >= _levelThresholds[i])
                title = _levelTitles[i];
        }
        return title;
    }

    public int GetCurrentLevel()
    {
        int level = 1;
        for (int i = 0; i < _levelThresholds.Length; i++)
        {
            if (_score >= _levelThresholds[i])
                level = i + 1;
        }
        return level;
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"\n  Player : {_playerName}");
        Console.WriteLine($"  Score  : {_score} points");
        Console.WriteLine($"  Level  : {GetCurrentLevel()} — {GetCurrentTitle()}");
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void DisplayGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("  No goals yet. Create one!");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {_goals[i].GetDisplayString()}");
        }
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("  No goals to record. Please create a goal first.");
            return;
        }

        DisplayGoals();
        Console.Write("\n  Which goal did you accomplish? (number): ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int index) || index < 1 || index > _goals.Count)
        {
            Console.WriteLine("  Invalid selection.");
            return;
        }

        Goal selected = _goals[index - 1];
        int previousLevel = GetCurrentLevel();
        int earned = selected.RecordEvent();
        _score += earned;

        if (earned > 0)
            Console.WriteLine($"  You earned {earned} points! Total score: {_score}");
        else if (earned < 0)
            Console.WriteLine($"  You lost {Math.Abs(earned)} points for recording a bad habit. Total score: {_score}");

        // Check for level-up
        int newLevel = GetCurrentLevel();
        if (newLevel > previousLevel)
        {
            Console.WriteLine($"\n  *** LEVEL UP! You are now Level {newLevel}: {GetCurrentTitle()} ***");
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("\n  Goal Types:");
        Console.WriteLine("  1. Simple Goal       - Completed once for points");
        Console.WriteLine("  2. Eternal Goal      - Repeating, never finishes");
        Console.WriteLine("  3. Checklist Goal    - Complete N times for a bonus");
        Console.WriteLine("  4. Negative Goal     - Lose points for a bad habit");
        Console.WriteLine("  5. Progress Goal     - Track progress toward a large target");
        Console.Write("  Select goal type: ");

        string choice = Console.ReadLine();

        Console.Write("  Enter goal name: ");
        string name = Console.ReadLine();

        Console.Write("  Enter a short description: ");
        string description = Console.ReadLine();

        Console.Write("  Points (per event): ");
        if (!int.TryParse(Console.ReadLine(), out int points) || points <= 0)
        {
            Console.WriteLine("  Invalid points. Goal not created.");
            return;
        }

        switch (choice)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, description, points));
                Console.WriteLine("  Simple goal created!");
                break;

            case "2":
                _goals.Add(new EternalGoal(name, description, points));
                Console.WriteLine("  Eternal goal created!");
                break;

            case "3":
                Console.Write("  How many times must this be completed? ");
                if (!int.TryParse(Console.ReadLine(), out int required) || required <= 0)
                {
                    Console.WriteLine("  Invalid count. Goal not created.");
                    return;
                }
                Console.Write("  Bonus points upon completion: ");
                if (!int.TryParse(Console.ReadLine(), out int bonus) || bonus < 0)
                {
                    Console.WriteLine("  Invalid bonus. Goal not created.");
                    return;
                }
                _goals.Add(new ChecklistGoal(name, description, points, required, bonus));
                Console.WriteLine("  Checklist goal created!");
                break;

            case "4":
                _goals.Add(new NegativeGoal(name, description, points));
                Console.WriteLine("  Negative goal created! Recording this will cost you points.");
                break;

            case "5":
                Console.Write("  What is the target amount (e.g. 100)? ");
                if (!double.TryParse(Console.ReadLine(), out double target) || target <= 0)
                {
                    Console.WriteLine("  Invalid target. Goal not created.");
                    return;
                }
                Console.Write("  Unit (e.g. miles, pages, hours): ");
                string unit = Console.ReadLine();
                _goals.Add(new ProgressGoal(name, description, points, target, unit));
                Console.WriteLine("  Progress goal created!");
                break;

            default:
                Console.WriteLine("  Unknown goal type. Goal not created.");
                break;
        }
    }

    public void SaveGoals()
    {
        Console.Write("  Enter filename to save (e.g. goals.txt): ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine($"{_playerName},{_score}");
            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine($"  Goals saved to {filename}");
    }

    public void LoadGoals()
    {
        Console.Write("  Enter filename to load (e.g. goals.txt): ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("  File not found.");
            return;
        }

        _goals.Clear();
        string[] lines = File.ReadAllLines(filename);

        // First line: playerName,score
        string[] header = lines[0].Split(",");
        _playerName = header[0];
        _score = int.Parse(header[1]);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            int colonIndex = line.IndexOf(':');
            string type = line.Substring(0, colonIndex);
            string data = line.Substring(colonIndex + 1);
            string[] parts = data.Split(",");

            Goal goal = null;

            switch (type)
            {
                case "SimpleGoal":
                    goal = new SimpleGoal(parts[0], parts[1], int.Parse(parts[2]), bool.Parse(parts[3]));
                    break;

                case "EternalGoal":
                    goal = new EternalGoal(parts[0], parts[1], int.Parse(parts[2]));
                    break;

                case "ChecklistGoal":
                    goal = new ChecklistGoal(parts[0], parts[1], int.Parse(parts[2]),
                                             int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
                    break;

                case "NegativeGoal":
                    goal = new NegativeGoal(parts[0], parts[1], int.Parse(parts[2]), int.Parse(parts[3]));
                    break;

                case "ProgressGoal":
                    goal = new ProgressGoal(parts[0], parts[1], int.Parse(parts[2]),
                                            double.Parse(parts[3]), parts[4], double.Parse(parts[5]));
                    break;

                default:
                    Console.WriteLine($"  Unknown goal type '{type}' skipped.");
                    break;
            }

            if (goal != null)
                _goals.Add(goal);
        }

        Console.WriteLine($"  Loaded {_goals.Count} goals for {_playerName}. Score: {_score}");
    }
}
