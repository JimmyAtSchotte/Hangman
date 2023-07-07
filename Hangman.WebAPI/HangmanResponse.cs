using Hangman.Core.Entities;

namespace Hangman.WebAPI;

public class HangmanResponse
{
    public Guid GameId { get; set; }
    public char?[] WordProgress { get; set; }
    public int RemainingGuesses { get; set; }
    public GameStatus Status { get; set; }
    public IEnumerable<Core.Entities.Guess> Guesses { get; set; }

    public static HangmanResponse Create(Game game)
    {
        return new HangmanResponse()
        {
            GameId = game.Guid,
            WordProgress = game.GetWordProgress(),
            RemainingGuesses = game.GetRemainingGuesses(),
            Status = game.GetCurrentGameStatus(),
            Guesses = game.Guesses
        };
    }
}