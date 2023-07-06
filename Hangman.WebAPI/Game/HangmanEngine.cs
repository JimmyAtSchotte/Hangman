﻿using Hangman.Core.Entities;

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
        
        if(_game.Guesses.Any(c => c.Character == guess))
            return CurrentHangmanStatus();

        foreach (var previousGuess in _game.Guesses.Where(x => x.WordContainsCharacter))
        {
            for (var i = 0; i < _correctWord.Length; i++)
            {
                if (!_correctWord[i].Equals(previousGuess.Character)) 
                    continue;
            
                _wordProgress[i] = previousGuess.Character;
            }
        }

        var isCorrectGuess = false;
        
        for (var i = 0; i < _correctWord.Length; i++)
        {
            if (!_correctWord[i].Equals(guess)) 
                continue;
            
            isCorrectGuess = true;
            _wordProgress[i] = guess;
        }
        
        _game.Guesses.Add(new GuessResult()
        {
            Character = guess,
            WordContainsCharacter = isCorrectGuess
        });

        return CurrentHangmanStatus();
    }

    private HangmanResponse CurrentHangmanStatus()
    {
        var gameStatus = GetCurrentGameStatus();
        
        return new HangmanResponse()
        {
            GameId = _game.Guid,
            WordProgress = gameStatus == GameStatus.GameOver ?  Array.ConvertAll(_correctWord, c => (char?)c) : _wordProgress,
            RemainingGuesses = AllowedGuesses - _game.Guesses.Count(x => x.WordContainsCharacter == false),
            Status = gameStatus,
            PreviousGuesses = _game.Guesses
        };
    }

    private GameStatus GetCurrentGameStatus()
    {
        if (AllowedGuesses == _game.Guesses.Count(x => x.WordContainsCharacter == false))
            return GameStatus.GameOver;
        
        if (_wordProgress.All(c => c != null))
            return GameStatus.Victory;

        return GameStatus.KeepPlaying;
    }
}