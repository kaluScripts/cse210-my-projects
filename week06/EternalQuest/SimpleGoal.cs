using System;

// A goal that can be completed once. Once marked complete, it cannot be recorded again.
public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _isComplete = false;
    }

    public SimpleGoal(string name, string description, int points, bool isComplete)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (_isComplete)
        {
            Console.WriteLine("This goal is already complete!");
            return 0;
        }

        _isComplete = true;
        return GetPoints();
    }

    public override bool IsComplete() => _isComplete;

    public override string GetDisplayString()
    {
        string checkBox = _isComplete ? "[X]" : "[ ]";
        return $"{checkBox} {GetName()} ({GetDescription()})";
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{GetName()},{GetDescription()},{GetPoints()},{_isComplete}";
    }
}
