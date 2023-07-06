namespace Hangman.Core.Entities;

public class Guess
{
    public char Character { get; set; }
    public bool WordContainsCharacter { get; set; }
}