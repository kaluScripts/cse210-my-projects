using System;

// EXCEEDS REQUIREMENTS: A "negative goal" that deducts points each time a bad habit is recorded.
// This encourages users to avoid bad habits by making them "cost" points.
// Example: Each time you skip a workout, lose 150 points.
public class NegativeGoal : Goal
{
    private int _timesRecorded;

    public NegativeGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _timesRecorded = 0;
    }

    public NegativeGoal(string name, string description, int points, int timesRecorded)
        : base(name, description, points)
    {
        _timesRecorded = timesRecorded;
    }

    // Returns a negative value to subtract from the score
    public override int RecordEvent()
    {
        _timesRecorded++;
        return -GetPoints();
    }

    // Negative goals are never "complete" — they track ongoing bad habits
    public override bool IsComplete() => false;

    public int GetTimesRecorded() => _timesRecorded;

    public override string GetDisplayString()
    {
        return $"[✗] {GetName()} ({GetDescription()}) -- Recorded {_timesRecorded} time(s) [-{GetPoints()} pts each]";
    }

    public override string GetStringRepresentation()
    {
        return $"NegativeGoal:{GetName()},{GetDescription()},{GetPoints()},{_timesRecorded}";
    }
}
