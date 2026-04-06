using System;

// EXCEEDS REQUIREMENTS: A goal where the user makes partial progress toward a large target.
// Points are awarded proportional to the amount of progress recorded.
// Example: Run 100 miles total. Each mile recorded earns (totalPoints / targetAmount) points.
public class ProgressGoal : Goal
{
    private double _targetAmount;
    private double _currentAmount;
    private string _unit;

    public ProgressGoal(string name, string description, int totalPoints, double targetAmount, string unit)
        : base(name, description, totalPoints)
    {
        _targetAmount = targetAmount;
        _currentAmount = 0;
        _unit = unit;
    }

    public ProgressGoal(string name, string description, int totalPoints, double targetAmount, string unit, double currentAmount)
        : base(name, description, totalPoints)
    {
        _targetAmount = targetAmount;
        _currentAmount = currentAmount;
        _unit = unit;
    }

    // Award points per unit of progress
    public override int RecordEvent()
    {
        if (IsComplete())
        {
            Console.WriteLine("This progress goal is already complete!");
            return 0;
        }

        Console.Write($"  How much {_unit} did you complete? ");
        string input = Console.ReadLine();
        if (!double.TryParse(input, out double amount) || amount <= 0)
        {
            Console.WriteLine("  Invalid amount. No progress recorded.");
            return 0;
        }

        double remaining = _targetAmount - _currentAmount;
        if (amount > remaining)
        {
            amount = remaining;
            Console.WriteLine($"  (Capped at {remaining} {_unit} to reach the goal.)");
        }

        _currentAmount += amount;

        double pointsPerUnit = (double)GetPoints() / _targetAmount;
        int earned = (int)(amount * pointsPerUnit);

        if (IsComplete())
        {
            Console.WriteLine($"  *** Goal complete! You finished all {_targetAmount} {_unit}! ***");
        }

        return earned;
    }

    public override bool IsComplete() => _currentAmount >= _targetAmount;

    public override string GetDisplayString()
    {
        string checkBox = IsComplete() ? "[X]" : "[ ]";
        double percent = (_currentAmount / _targetAmount) * 100;
        return $"{checkBox} {GetName()} ({GetDescription()}) -- Progress: {_currentAmount:F1}/{_targetAmount} {_unit} ({percent:F0}%)";
    }

    public override string GetStringRepresentation()
    {
        return $"ProgressGoal:{GetName()},{GetDescription()},{GetPoints()},{_targetAmount},{_unit},{_currentAmount}";
    }
}
