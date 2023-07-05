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
        
        Assert.IsTrue(guessResult.Victory);
    }
    
    [Test]
    public void UnmatchedCharGuess()
    {
        var game = new HangmanEngine("a", 2);
        var guessResult = game.Guess('b');
        
        Assert.IsFalse(guessResult.Victory);
    }
    
    [Test]
    public void MatchedPartOfWord()
    {
        var game = new HangmanEngine("ab", 2);
        var guessResult = game.Guess('b');
        
        Assert.IsFalse(guessResult.Victory);
        Assert.IsNull(guessResult.WordProgress[0]);
        Assert.AreEqual('b', guessResult.WordProgress[1]);
    }
    
    [Test]
    public void CountdownRemainingGuessesUntilGameOver()
    {
        var game = new HangmanEngine("a", 3);
        var guessResult1 = game.Guess('b');
        var guessResult2 = game.Guess('c');
        var guessResult3 = game.Guess('d');
        
        Assert.AreEqual(guessResult1.RemainingGuesses, 2);
        Assert.AreEqual(guessResult2.RemainingGuesses, 1);
        Assert.AreEqual(guessResult3.RemainingGuesses, 0);
    }
}

public class HangmanEngine
{
    private readonly int _allowedGuesses;
    private readonly char[] _correctWord;
    private readonly char?[] _wordProgress;

    private int _failedGuesses;

    public HangmanEngine(string correctWord, int allowedGuesses)
    {
        _allowedGuesses = allowedGuesses;
        _correctWord = correctWord.ToCharArray();
        _wordProgress = new char?[_correctWord.Length];
        _failedGuesses = 0;
    }

    public GuessResult Guess(char guess)
    {
        for (var i = 0; i < _correctWord.Length; i++)
        {
            if (_correctWord[i].Equals(guess))
                _wordProgress[i] = guess;
            else
                _failedGuesses++;
        }

        return new GuessResult()
        {
            Victory = _wordProgress.All(c => c != null),
            WordProgress = _wordProgress,
            RemainingGuesses = _allowedGuesses - _failedGuesses
        };
    }
}

public class GuessResult
{
    public bool Victory { get; set; }
    public char?[] WordProgress { get; set; }
    public int RemainingGuesses { get; set; }
}