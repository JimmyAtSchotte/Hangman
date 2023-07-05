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
    
    [Test]
    public void MatchedPartOfWord()
    {
        var game = new HangmanEngine("ab");
        var guessResult = game.Guess('b');
        
        Assert.IsFalse(guessResult.Victory);
        Assert.IsNull(guessResult.WordProgress[0]);
        Assert.AreEqual('b', guessResult.WordProgress[1]);
    }
}

public class HangmanEngine
{
    private readonly char[] _correctWord;
    private readonly char?[] _wordProgress;

    public HangmanEngine(string correctWord)
    {
        _correctWord = correctWord.ToCharArray();
        _wordProgress = new char?[_correctWord.Length];
    }

    public GuessResult Guess(char guess)
    {
        for (var i = 0; i < _correctWord.Length; i++)
        {
            if (_correctWord[i].Equals(guess))
                _wordProgress[i] = guess;
        }

        return new GuessResult()
        {
            Victory = _wordProgress.All(c => c != null),
            WordProgress = _wordProgress
        };
    }
}

public class GuessResult
{
    public bool Victory { get; set; }
    public char?[] WordProgress { get; set; }
}