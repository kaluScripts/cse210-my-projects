// GratitudeActivity.cs
// EXCEEDS REQUIREMENTS: An additional activity type.
// Guides the user through writing specific gratitude entries with reflection prompts,
// helping them focus on WHY they are grateful rather than just listing items.

using System;
using System.Collections.Generic;

public class GratitudeActivity : Activity
{
    private List<string> _focusAreas;
    private List<string> _deepenQuestions;
    private List<string> _unusedQuestions;

    public GratitudeActivity()
        : base(
            "Gratitude Activity",
            "This activity will help you cultivate genuine gratitude by guiding you to think deeply " +
            "about specific things you are thankful for and why they matter to you.")
    {
        _focusAreas = new List<string>
        {
            "a person who has positively influenced your life",
            "an experience that taught you something valuable",
            "a personal talent or ability you have developed",
            "something in nature that brings you peace",
            "a challenge that helped you grow stronger"
        };

        _deepenQuestions = new List<string>
        {
            "How has this made your life better?",
            "What would your life look like without it?",
            "Who else benefits because of this?",
            "How did you come to appreciate this?",
            "What do you want to remember about this feeling?",
            "How can you express this gratitude today?",
            "What does this reveal about what truly matters to you?"
        };

        _unusedQuestions = new List<string>(_deepenQuestions);
    }

    private string GetRandomFocus()
    {
        Random random = new Random();
        return _focusAreas[random.Next(_focusAreas.Count)];
    }

    private string GetRandomQuestion()
    {
        if (_unusedQuestions.Count == 0)
            _unusedQuestions = new List<string>(_deepenQuestions);

        Random random = new Random();
        int index = random.Next(_unusedQuestions.Count);
        string question = _unusedQuestions[index];
        _unusedQuestions.RemoveAt(index);
        return question;
    }

    public void Run()
    {
        DisplayStartMessage();

        string focus = GetRandomFocus();
        Console.WriteLine($"\nFor this session, think of {focus}.");
        Console.WriteLine("Take a moment to bring a specific example to mind, then press Enter.");
        Console.ReadLine();

        Console.Write("Now reflect on the following questions about it.");
        ShowSpinner(2);

        DateTime endTime = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            string question = GetRandomQuestion();
            Console.Write($"\n> {question} ");
            ShowSpinner(6);
        }

        Console.WriteLine();
        DisplayEndMessage();
    }
}
