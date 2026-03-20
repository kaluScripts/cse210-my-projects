/// <summary>
/// Encapsulates a scripture reference, such as "John 3:16" or "Proverbs 3:5-6".
/// Handles both single verse and verse range references.
/// </summary>
public class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;

    /// <summary>
    /// Constructor for a single-verse reference (e.g., "John 3:16").
    /// </summary>
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
        _endVerse = verse;
    }

    /// <summary>
    /// Constructor for a verse-range reference (e.g., "Proverbs 3:5-6").
    /// </summary>
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    /// <summary>
    /// Returns the formatted reference string.
    /// </summary>
    public string GetDisplayText()
    {
        if (_startVerse == _endVerse)
        {
            return $"{_book} {_chapter}:{_startVerse}";
        }
        else
        {
            return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }
}
