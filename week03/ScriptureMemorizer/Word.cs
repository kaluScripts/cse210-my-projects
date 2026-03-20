/// <summary>
/// Encapsulates a single word in a scripture, including its hidden/shown state.
/// Responsible for rendering itself as either the real word or underscores.
/// </summary>
public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    /// <summary>
    /// Returns true if this word is currently hidden.
    /// </summary>
    public bool IsHidden()
    {
        return _isHidden;
    }

    /// <summary>
    /// Hides the word so it will display as underscores.
    /// </summary>
    public void Hide()
    {
        _isHidden = true;
    }

    /// <summary>
    /// Reveals the word so it displays normally.
    /// </summary>
    public void Show()
    {
        _isHidden = false;
    }

    /// <summary>
    /// Returns the display text: the word itself if visible, or underscores matching
    /// the number of letters in the word if hidden. Punctuation is preserved after underscores.
    /// </summary>
    public string GetDisplayText()
    {
        if (!_isHidden)
        {
            return _text;
        }

        // Separate trailing punctuation so underscores only replace letters
        string letters = _text.TrimEnd(',', '.', ';', ':', '!', '?', '"', '\'');
        string punctuation = _text.Substring(letters.Length);
        return new string('_', letters.Length) + punctuation;
    }
}
