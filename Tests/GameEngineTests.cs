using NUnit.Framework;

namespace Tests;

[TestFixture]
public class GameEngineTests
{
    [Test]
    public void CorrectWordGuess()
    {
        var game = new GameEngine("a");
        var guessResult = game.Guess('a');
        
        Assert.IsTrue(guessResult);
    }
    
    [Test]
    public void UnmatchedCharGuess()
    {
        var game = new GameEngine("a");
        var guessResult = game.Guess('b');
        
        Assert.IsFalse(guessResult);
    }
}

public class GameEngine
{
    private readonly char[] _correctWord;

    public GameEngine(string correctWord)
    {
        _correctWord = correctWord.ToCharArray();
    }

    public bool Guess(char guess)
    {
        return _correctWord.Any(c => c == guess);
    }
}