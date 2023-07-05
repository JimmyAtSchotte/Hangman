using NUnit.Framework;

namespace Tests;

[TestFixture]
public class HangmanEngineTests
{
    [Test]
    public void CorrectWordGuess()
    {
        var game = new HangmanEngine("a");
        var guessResult = game.Guess('a');
        
        Assert.IsTrue(guessResult);
    }
    
    [Test]
    public void UnmatchedCharGuess()
    {
        var game = new HangmanEngine("a");
        var guessResult = game.Guess('b');
        
        Assert.IsFalse(guessResult);
    }
}

public class HangmanEngine
{
    private readonly char[] _correctWord;

    public HangmanEngine(string correctWord)
    {
        _correctWord = correctWord.ToCharArray();
    }

    public bool Guess(char guess)
    {
        return _correctWord.Any(c => c == guess);
    }
}