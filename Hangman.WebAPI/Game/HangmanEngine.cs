using Hangman.Core.Entities;

namespace Hangman.Core;

public class HangmanEngine
{
    private readonly Game _game;
    public const int AllowedGuesses = 10;

    private readonly char[] _correctWord;
    private readonly char?[] _wordProgress;
    
    public HangmanEngine(Game game)
    {
        _game = game;
        _correctWord = game.CorrectWord.Select(char.ToUpperInvariant).ToArray();
        _wordProgress = new char?[_correctWord.Length];
    }

    public HangmanResponse Guess(char guessingChar)
    {
        var guess = char.ToUpperInvariant(guessingChar);
        
        if(_game.PreviousGuesses.Any(c => c.Character == guess))
            return CurrentHangmanStatus();
        
        var isCorrectGuess = false;
        
        for (var i = 0; i < _correctWord.Length; i++)
        {
            if (!_correctWord[i].Equals(guess)) 
                continue;
            
            isCorrectGuess = true;
            _wordProgress[i] = guess;
        }
        
        _game.PreviousGuesses.Add(new GuessResult()
        {
            Character = guess,
            WordContainsCharacter = isCorrectGuess
        });

        return CurrentHangmanStatus();
    }

    private HangmanResponse CurrentHangmanStatus()
    {
        return new HangmanResponse()
        {
            GameId = _game.Guid,
            WordProgress = _wordProgress,
            RemainingGuesses = AllowedGuesses - _game.PreviousGuesses.Count(x => x.WordContainsCharacter == false),
            Status = GetCurrentGameStatus(),
            PreviousGuesses = _game.PreviousGuesses
        };
    }

    private GameStatus GetCurrentGameStatus()
    {
        if (AllowedGuesses == _game.PreviousGuesses.Count(x => x.WordContainsCharacter == false))
            return GameStatus.GameOver;
        
        if (_wordProgress.All(c => c != null))
            return GameStatus.Victory;

        return GameStatus.KeepPlaying;
    }
}