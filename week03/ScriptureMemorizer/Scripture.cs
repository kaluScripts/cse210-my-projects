/// <summary>
/// Encapsulates a complete scripture: its reference and the words of its text.
/// Responsible for displaying itself, hiding random words, and knowing when fully hidden.
/// </summary>
public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private static Random _random = new Random();

    /// <summary>
    /// Creates a Scripture from a Reference and the full text string.
    /// </summary>
    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ')
                     .Select(w => new Word(w))
                     .ToList();
    }

    /// <summary>
    /// Returns the complete display string: reference followed by all words,
    /// each word shown or hidden according to its state.
    /// </summary>
    public string GetDisplayText()
    {
        string wordText = string.Join(" ", _words.Select(w => w.GetDisplayText()));
        return $"{_reference.GetDisplayText()}\n{wordText}";
    }

    /// <summary>
    /// Randomly hides a given number of words that are not yet hidden.
    /// If fewer unhidden words remain than the count requested, hides whatever is left.
    /// </summary>
    public void HideRandomWords(int count)
    {
        List<Word> visibleWords = _words.Where(w => !w.IsHidden()).ToList();

        int toHide = Math.Min(count, visibleWords.Count);

        for (int i = 0; i < toHide; i++)
        {
            int index = _random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index); // avoid hiding the same word twice in one round
        }
    }

    /// <summary>
    /// Returns true when every word in the scripture is hidden.
    /// </summary>
    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden());
    }
}
