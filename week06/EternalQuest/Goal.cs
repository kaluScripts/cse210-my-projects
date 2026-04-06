using System;

// Base class for all goal types. Contains shared attributes and behaviors.
public abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public string GetName() => _name;
    public string GetDescription() => _description;
    public int GetPoints() => _points;

    // Returns points to award when this goal is recorded
    public abstract int RecordEvent();

    // Returns true if the goal is fully completed
    public abstract bool IsComplete();

    // Returns a formatted string for display in the goal list
    public abstract string GetDisplayString();

    // Returns a string representation for saving to file
    public abstract string GetStringRepresentation();
}
