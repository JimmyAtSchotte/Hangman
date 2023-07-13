namespace Hangman.WebAPI;

public class GuessResponse
{
    public char Character { get; set; }
    public bool WordContainsCharacter { get; set; }
}