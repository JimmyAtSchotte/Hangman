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
        
        Assert.IsTrue(guessResult.Victory);
    }
    
    [Test]
    public void UnmatchedCharGuess()
    {
        var game = new HangmanEngine("a");
        var guessResult = game.Guess('b');
        
        Assert.IsFalse(guessResult.Victory);
    }
}

public class HangmanEngine
{
    private readonly char[] _correctWord;

    public HangmanEngine(string correctWord)
    {
        _correctWord = correctWord.ToCharArray();
    }

    public GuessResult Guess(char guess)
    {
        return new GuessResult()
        {
            Victory = _correctWord.Any(c => c == guess)
        };
    }
}

public class GuessResult
{
    public bool Victory { get; set; }
}