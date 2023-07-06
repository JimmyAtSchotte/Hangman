using Hangman.Core;
using Hangman.Core.Entities;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class HangmanEngineTests
{
    [Test]
    public void CorrectWordGuess()
    {
        var game = new HangmanEngine(new Game()
        {
            CorrectWord = "a"
        });
        var guessResult = game.Guess('a');
        
        Assert.AreEqual(GameStatus.Victory, guessResult.Status);
    }
    
    [Test]
    public void UnmatchedCharGuess()
    {
        var game = new HangmanEngine(new Game()
        {
            CorrectWord = "a" 
        });
        var guessResult = game.Guess('b');
        
        Assert.AreEqual(GameStatus.KeepPlaying, guessResult.Status);
    }
    
    [Test]
    public void MatchedPartOfWord()
    {
        var game = new HangmanEngine(new Game()
        {
            CorrectWord = "ab" 
        });
        var guessResult = game.Guess('b');
        
        Assert.IsNull(guessResult.WordProgress[0]);
        Assert.AreEqual('B', guessResult.WordProgress[1]);
        Assert.AreEqual(GameStatus.KeepPlaying, guessResult.Status);
    }
    
    [Test]
    public void CountdownRemainingGuessesUntilGameOver()
    {
        var game = new HangmanEngine(new Game()
        {
            CorrectWord = "a" 
        });

        var guesses = new[]
        {
            game.Guess('b'),
            game.Guess('c'),
            game.Guess('d'),
            game.Guess('e'),
            game.Guess('f'),
            game.Guess('g'),
            game.Guess('h'),
            game.Guess('i'),
            game.Guess('j'),
            game.Guess('k'),
        };

        Assert.AreEqual(9, guesses[0].RemainingGuesses);
        Assert.AreEqual(8, guesses[1].RemainingGuesses);
        Assert.AreEqual(7, guesses[2].RemainingGuesses);
        Assert.AreEqual(6, guesses[3].RemainingGuesses);
        Assert.AreEqual(5, guesses[4].RemainingGuesses);
        Assert.AreEqual(4, guesses[5].RemainingGuesses);
        Assert.AreEqual(3, guesses[6].RemainingGuesses);
        Assert.AreEqual(2, guesses[7].RemainingGuesses);
        Assert.AreEqual(1, guesses[8].RemainingGuesses);
        Assert.AreEqual(0, guesses[9].RemainingGuesses);
        Assert.AreEqual(GameStatus.GameOver, guesses[9].Status);
        Assert.AreEqual(new char?[] {'A'}, guesses[9].WordProgress);
    }
    
    [Test]
    public void MultipleGuessesOnSameCharacter()
    {
        var game = new HangmanEngine(new Game()
        {
            CorrectWord = "a" 
        });
        
        var guesses = new[]
        {
            game.Guess('b'),
            game.Guess('b'),
            game.Guess('B')
        };
        
        Assert.AreEqual(9, guesses[0].RemainingGuesses);
        Assert.AreEqual(9, guesses[1].RemainingGuesses);
        Assert.AreEqual(9, guesses[2].RemainingGuesses);
        Assert.AreEqual(GameStatus.KeepPlaying, guesses[2].Status);
    }
    
    [Test]
    public void IgnoreCase()
    {
        var game = new HangmanEngine(new Game()
        {
            CorrectWord = "a" 
        });
        var guessResult = game.Guess('A');
        
        Assert.AreEqual(GameStatus.Victory, guessResult.Status);
    }
    
    [Test]
    public void ListPreviousGuesses()
    {
        var game = new HangmanEngine(new Game()
        {
            CorrectWord = "ac" 
        });
        game.Guess('b');
        game.Guess('c');
        var guessResult = game.Guess('d');
        
        Assert.AreEqual('B', guessResult.PreviousGuesses.ElementAt(0).Character);
        Assert.IsFalse(guessResult.PreviousGuesses.ElementAt(0).WordContainsCharacter);
        Assert.AreEqual('C', guessResult.PreviousGuesses.ElementAt(1).Character);
        Assert.IsTrue(guessResult.PreviousGuesses.ElementAt(1).WordContainsCharacter);
    }
    
    [Test]
    public void KeepWordProgressStateStateBetweenInstances()
    {
        var game = new Game()
        {
            CorrectWord = "ac"
        };

        var engine1 = new HangmanEngine(game);
        engine1.Guess('a');
        
        var engine2 = new HangmanEngine(game);
        var guessResult = engine2.Guess('b');
        
        Assert.AreEqual('A', guessResult.WordProgress.ElementAt(0).GetValueOrDefault());
    }
}