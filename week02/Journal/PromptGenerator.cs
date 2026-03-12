using System;
using System.Collections.Generic;

public class PromptGenerator
{
    private List<string> _prompts;
    private Random _random;

    public PromptGenerator()
    {
        _random = new Random();
        _prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What is something I learned today?",
            "What am I grateful for today?",
            "What challenged me today and how did I handle it?",
            "Describe a moment today where I felt proud of myself.",
            "What is one thing I want to do differently tomorrow?",
            "What made me smile or laugh today?"
        };
    }

    public string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
}
