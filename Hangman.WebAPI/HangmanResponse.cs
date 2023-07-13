using Hangman.Core.Entities;

namespace Hangman.WebAPI;

public class HangmanResponse
{
    public Guid GameId { get; set; }
    public char?[] WordProgress { get; set; }
    public int RemainingGuesses { get; set; }
    public GameStatus Status { get; set; }
    public IEnumerable<GuessResponse> Guesses { get; set; }

    public static HangmanResponse Create(Game game)
    {
        return new HangmanResponse()
        {
            GameId = game.Guid,
            WordProgress = game.GetWordProgress(),
            RemainingGuesses = game.RemainingGuesses,
            Status = game.GameStatus,
            Guesses = game.Guesses.Select(guess => new GuessResponse()
            {
                Character = guess,
                WordContainsCharacter = game.CorrectWord.ContainsChar(guess)
            })
        };
    }
}