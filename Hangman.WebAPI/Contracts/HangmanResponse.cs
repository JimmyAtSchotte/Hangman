using Hangman.Core.Entities;

namespace Hangman.Core;

public class HangmanResponse
{
    public Guid GameId { get; set; }
    public char?[] WordProgress { get; set; }
    public int RemainingGuesses { get; set; }
    public GameStatus Status { get; set; }
    public IEnumerable<Guess> PreviousGuesses { get; set; }
}