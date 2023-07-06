namespace Hangman.Core.Entities;

public class Game
{
    private List<GuessResult> _previousGuesses;
    public Guid Guid { get; set; }
    public string CorrectWord { get; set; }

    public List<GuessResult> Guesses
    {
        get => _previousGuesses ??= new List<GuessResult>();
        set => _previousGuesses = value;
    }
}