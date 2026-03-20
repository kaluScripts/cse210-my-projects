/// <summary>
/// Manages a library of scriptures loaded from a file.
/// Can return a randomly chosen scripture for the user to practice.
/// </summary>
public class ScriptureLibrary
{
    private List<Scripture> _scriptures;
    private static Random _random = new Random();

    public ScriptureLibrary()
    {
        _scriptures = new List<Scripture>();
    }

    /// <summary>
    /// Loads scriptures from a plain-text file.
    /// Expected format per line:  Book Chapter:Verse[-EndVerse] | Scripture text
    /// Lines beginning with '#' are treated as comments and skipped.
    /// </summary>
    public void LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        foreach (string line in File.ReadLines(filePath))
        {
            string trimmed = line.Trim();
            if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#"))
            {
                continue;
            }

            // Split on the pipe character separating reference from text
            string[] parts = trimmed.Split('|');
            if (parts.Length != 2)
            {
                continue;
            }

            string refPart = parts[0].Trim();
            string textPart = parts[1].Trim();

            Reference reference = ParseReference(refPart);
            if (reference != null)
            {
                _scriptures.Add(new Scripture(reference, textPart));
            }
        }
    }

    /// <summary>
    /// Adds a scripture directly (used for built-in defaults).
    /// </summary>
    public void AddScripture(Scripture scripture)
    {
        _scriptures.Add(scripture);
    }

    /// <summary>
    /// Returns a randomly chosen scripture from the library, or null if empty.
    /// </summary>
    public Scripture GetRandomScripture()
    {
        if (_scriptures.Count == 0)
        {
            return null;
        }

        return _scriptures[_random.Next(_scriptures.Count)];
    }

    /// <summary>
    /// Returns the number of scriptures in the library.
    /// </summary>
    public int Count()
    {
        return _scriptures.Count;
    }

    // ------------------------------------------------------------------
    // Private helpers
    // ------------------------------------------------------------------

    /// <summary>
    /// Parses a reference string such as "John 3:16" or "Proverbs 3:5-6".
    /// Returns null if the format is not recognised.
    /// </summary>
    private Reference ParseReference(string refText)
    {
        try
        {
            // Find the last space to split the book name from "Chapter:Verse"
            int lastSpace = refText.LastIndexOf(' ');
            if (lastSpace < 0)
            {
                return null;
            }

            string book = refText.Substring(0, lastSpace).Trim();
            string chapterVerse = refText.Substring(lastSpace + 1).Trim();

            string[] cvParts = chapterVerse.Split(':');
            if (cvParts.Length != 2)
            {
                return null;
            }

            int chapter = int.Parse(cvParts[0]);

            if (cvParts[1].Contains('-'))
            {
                string[] verseParts = cvParts[1].Split('-');
                int startVerse = int.Parse(verseParts[0]);
                int endVerse = int.Parse(verseParts[1]);
                return new Reference(book, chapter, startVerse, endVerse);
            }
            else
            {
                int verse = int.Parse(cvParts[1]);
                return new Reference(book, chapter, verse);
            }
        }
        catch
        {
            return null;
        }
    }
}
