using System;

public class Entry
{
    private string _prompt;
    private string _response;
    private string _date;

    public Entry(string prompt, string response, string date)
    {
        _prompt = prompt;
        _response = response;
        _date = date;
    }

    public string GetPrompt()
    {
        return _prompt;
    }

    public string GetResponse()
    {
        return _response;
    }

    public string GetDate()
    {
        return _date;
    }

    public void Display()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_prompt}");
        Console.WriteLine($"Response: {_response}");
        Console.WriteLine();
    }

    public string ToFileString(string separator)
    {
        // Escape any separator characters inside fields to prevent corruption
        string safeDate = _date.Replace(separator, "[SEP]");
        string safePrompt = _prompt.Replace(separator, "[SEP]");
        string safeResponse = _response.Replace(separator, "[SEP]");
        return $"{safeDate}{separator}{safePrompt}{separator}{safeResponse}";
    }

    public static Entry FromFileString(string line, string separator)
    {
        string[] parts = line.Split(separator);
        if (parts.Length < 3)
        {
            throw new FormatException($"Invalid entry line: {line}");
        }

        // Restore any escaped separators
        string date = parts[0].Replace("[SEP]", separator);
        string prompt = parts[1].Replace("[SEP]", separator);

        // Join remaining parts in case response contained the separator sequence
        string response = string.Join(separator, parts[2..]).Replace("[SEP]", separator);

        return new Entry(prompt, response, date);
    }
}
