using System;

// A goal that never completes. Each time it is recorded, the user gains points.
// Examples: reading scriptures daily, exercising each morning.
public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        return GetPoints();
    }

    // Eternal goals are never complete by definition
    public override bool IsComplete() => false;

    public override string GetDisplayString()
    {
        return $"[∞] {GetName()} ({GetDescription()})";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{GetName()},{GetDescription()},{GetPoints()}";
    }
}
