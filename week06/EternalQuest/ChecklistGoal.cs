using System;

// A goal that must be accomplished a certain number of times to be complete.
// Each time it is recorded, the user gains points. A bonus is awarded upon completion.
public class ChecklistGoal : Goal
{
    private int _requiredCount;
    private int _currentCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int requiredCount, int bonusPoints)
        : base(name, description, points)
    {
        _requiredCount = requiredCount;
        _currentCount = 0;
        _bonusPoints = bonusPoints;
    }

    public ChecklistGoal(string name, string description, int points, int requiredCount, int bonusPoints, int currentCount)
        : base(name, description, points)
    {
        _requiredCount = requiredCount;
        _currentCount = currentCount;
        _bonusPoints = bonusPoints;
    }

    public override int RecordEvent()
    {
        if (IsComplete())
        {
            Console.WriteLine("This checklist goal is already complete!");
            return 0;
        }

        _currentCount++;

        if (_currentCount >= _requiredCount)
        {
            Console.WriteLine($"  *** Bonus! You completed the checklist goal and earned {_bonusPoints} bonus points! ***");
            return GetPoints() + _bonusPoints;
        }

        return GetPoints();
    }

    public override bool IsComplete() => _currentCount >= _requiredCount;

    public override string GetDisplayString()
    {
        string checkBox = IsComplete() ? "[X]" : "[ ]";
        return $"{checkBox} {GetName()} ({GetDescription()}) -- Currently completed: {_currentCount}/{_requiredCount}";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{GetName()},{GetDescription()},{GetPoints()},{_requiredCount},{_bonusPoints},{_currentCount}";
    }
}
