using NUnit.Framework;

namespace Tests;

[TestFixture]
public class HangmanEngineTests
{
    [Test]
    public void CorrectWordGuess()
    {
        var game = new HangmanEngine("a", 2);
        var guessResult = game.Guess('a');
        
        Assert.AreEqual(GameStatus.Victory, guessResult.Status);
    }
    
    [Test]
    public void UnmatchedCharGuess()
    {
        var game = new HangmanEngine("a", 2);
        var guessResult = game.Guess('b');
        
        Assert.AreEqual(GameStatus.KeepPlaying, guessResult.Status);
    }
    
    [Test]
    public void MatchedPartOfWord()
    {
        var game = new HangmanEngine("ab", 2);
        var guessResult = game.Guess('b');
        
        Assert.IsNull(guessResult.WordProgress[0]);
        Assert.AreEqual('b', guessResult.WordProgress[1]);
        Assert.AreEqual(GameStatus.KeepPlaying, guessResult.Status);
    }
    
    [Test]
    public void CountdownRemainingGuessesUntilGameOver()
    {
        var game = new HangmanEngine("a", 3);
        var guessResult1 = game.Guess('b');
        var guessResult2 = game.Guess('c');
        var guessResult3 = game.Guess('d');
        
        Assert.AreEqual(2, guessResult1.RemainingGuesses, 2);
        Assert.AreEqual(1, guessResult2.RemainingGuesses, 1);
        Assert.AreEqual(0, guessResult3.RemainingGuesses, 0);
        Assert.AreEqual(GameStatus.GameOver, guessResult3.Status);
    }
    
    [Test]
    public void MultipleGuessesOnSameCharacter()
    {
        var game = new HangmanEngine("a", 3);
        var guessResult1 = game.Guess('b');
        var guessResult2 = game.Guess('b');
        var guessResult3 = game.Guess('b');
        
        Assert.AreEqual(2, guessResult1.RemainingGuesses, 2);
        Assert.AreEqual(1, guessResult2.RemainingGuesses, 2);
        Assert.AreEqual(0, guessResult3.RemainingGuesses, 2);
        Assert.AreEqual(GameStatus.KeepPlaying, guessResult3.Status);
    }
}