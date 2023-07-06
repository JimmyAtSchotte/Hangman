using Hangman.Core;
using Hangman.Core.Entities;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class HangmanEngineTests
{
    [TestCase('a')]
    [TestCase('A')]
    public void CorrectWordGuess(char guess)
    {
        var game = new Game()
        {
            CorrectWord = "a"
        };

        game.Guess(guess);

        Assert.AreEqual(GameStatus.Victory, game.GetCurrentGameStatus());
    }

    [Test]
    public void UnmatchedCharGuess()
    {
        var game = new Game()
        {
            CorrectWord = "a"
        };

        game.Guess('b');

        Assert.AreEqual(GameStatus.KeepPlaying, game.GetCurrentGameStatus());
    }

    [Test]
    public void MatchedPartOfWord()
    {
        var game = new Game()
        {
            CorrectWord = "ab"
        };

        game.Guess('b');

        var wordProgress = game.GetWordProgress();
        var status = game.GetCurrentGameStatus();

        Assert.IsNull(wordProgress[0]);
        Assert.AreEqual('B', wordProgress[1]);
        Assert.AreEqual(GameStatus.KeepPlaying, status);
    }

    [Test]
    public void CountdownRemainingGuessesUntilGameOver()
    {
        var game = new Game()
        {
            CorrectWord = "a"
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

        Assert.AreEqual(GameStatus.GameOver, game.GetCurrentGameStatus());
        Assert.AreEqual(new char?[] { 'A' }, game.GetWordProgress());
    }

    [Test]
    public void MultipleGuessesOnSameCharacter()
    {
        var game = new Game()
        {
            CorrectWord = "a"
        };
        
        game.Guess('b');
        game.Guess('b');
        game.Guess('B');

        Assert.AreEqual(Constants.AllowedGuesses - 1, game.GetRemainingGuesses());
    }

    [Test]
    public void ListPreviousGuesses()
    {
        var game = new Game()
        {
            CorrectWord = "ac"
        };
        
        game.Guess('b');
        game.Guess('c');
        game.Guess('d');

        Assert.AreEqual('B', game.Guesses.ElementAt(0).Character);
        Assert.IsFalse(game.Guesses.ElementAt(0).WordContainsCharacter);
        Assert.AreEqual('C', game.Guesses.ElementAt(1).Character);
        Assert.IsTrue(game.Guesses.ElementAt(1).WordContainsCharacter);
    }
}