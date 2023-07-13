using Hangman.Core;
using Hangman.Core.Entities;
using Hangman.Core.Types;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class GameTests
{
   [Test]
    public void NewGame()
    {
        var game = new Game()
        {
            CorrectWord = (Word)"a"
        };

        Assert.AreEqual(GameStatus.KeepPlaying, game.GameStatus);
        Assert.AreEqual(Constants.AllowedGuesses, game.RemainingGuesses);
    }
    
    
    [TestCase('a')]
    [TestCase('A')]
    public void CorrectWordGuess(char guess)
    {
        var game = new Game()
        {
            CorrectWord = (Word)"a"
        };

        game.Guess(guess);

        Assert.AreEqual(GameStatus.Victory, game.GameStatus);
    }

    [Test]
    public void UnmatchedCharGuess()
    {
        var game = new Game()
        {
            CorrectWord = (Word)"a"
        };

        game.Guess('b');

        Assert.AreEqual(GameStatus.KeepPlaying, game.GameStatus);
    }

    [Test]
    public void MatchedPartOfWord()
    {
        var game = new Game()
        {
            CorrectWord = (Word)"ab"
        };

        game.Guess('b');

        var wordProgress = game.GetWordProgress();

        Assert.IsNull(wordProgress[0]);
        Assert.AreEqual('B', wordProgress[1]);
        Assert.AreEqual(GameStatus.KeepPlaying, game.GameStatus);
    }

    [Test]
    public void CountdownRemainingGuessesUntilGameOver()
    {
        var game = new Game()
        {
            CorrectWord = (Word)"a"
        };

        game.Guess('b');
        game.Guess('c');
        game.Guess('d');
        game.Guess('e');
        game.Guess('f');
        game.Guess('g');
        game.Guess('h');
        game.Guess('i');
        game.Guess('j');
        game.Guess('k');

        Assert.AreEqual(GameStatus.GameOver, game.GameStatus);
        Assert.AreEqual(new char?[] { 'A' }, game.GetWordProgress());
    }

    [Test]
    public void MultipleGuessesOnSameCharacter()
    {
        var game = new Game()
        {
            CorrectWord = (Word)"a"
        };
        
        game.Guess('b');
        game.Guess('b');
        game.Guess('B');

        Assert.AreEqual(Constants.AllowedGuesses - 1, game.RemainingGuesses);
    }

    [Test]
    public void ListPreviousGuesses()
    {
        var game = new Game()
        {
            CorrectWord = (Word)"ac"
        };
        
        game.Guess('b');
        game.Guess('c');
        game.Guess('d');

        Assert.IsTrue(game.Guesses.ElementAt(0).Equals('B'));
        Assert.IsTrue(game.Guesses.ElementAt(1).Equals('C'));
    }
}