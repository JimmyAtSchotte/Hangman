namespace Hangman.Core.Entities;

public class GuessResult
{
    public char Character { get; set; }
    public bool WordContainsCharacter { get; set; }
}