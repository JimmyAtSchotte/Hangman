using Hangman.Core.Entities;

namespace Hangman.Core;

public class HangmanEngine
{
    private readonly Game _game;
    public const int AllowedGuesses = 10;

    private readonly char[] _correctWord;

    public HangmanEngine(Game game)
    {
        _game = game;
        _correctWord = game.CorrectWord.Select(char.ToUpperInvariant).ToArray();
    }

    public HangmanResponse Guess(char guess)
    {
        _game.AddGuess(guess);
        var wordProgress = _game.GetWordProgress();
        
        return CurrentHangmanStatus(wordProgress);
    }

    private HangmanResponse CurrentHangmanStatus(char?[] wordProgress)
    {
        var gameStatus = _game.GetCurrentGameStatus();
        
        return new HangmanResponse()
        {
            GameId = _game.Guid,
            WordProgress = gameStatus == GameStatus.GameOver ?  Array.ConvertAll(_correctWord, c => (char?)c) : wordProgress,
            RemainingGuesses = _game.GetRemainingGuesses(),
            Status = gameStatus,
            PreviousGuesses = _game.Guesses
        };
    }
}