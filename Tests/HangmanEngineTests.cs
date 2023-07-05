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
}

public enum GameStatus
{
    KeepPlaying,
    Victory,
    GameOver,
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
        var isCorrectGuess = false;
        
        for (var i = 0; i < _correctWord.Length; i++)
        {
            if (!_correctWord[i].Equals(guess)) 
                continue;
            
            isCorrectGuess = true;
            _wordProgress[i] = guess;
        }
        
        if(!isCorrectGuess)
            _failedGuesses++;

        return new GuessResult()
        {
            Victory = _wordProgress.All(c => c != null),
            WordProgress = _wordProgress,
            RemainingGuesses = _allowedGuesses - _failedGuesses,
            Status = GetCurrentGameStatus()
        };
    }

    private GameStatus GetCurrentGameStatus()
    {
        if (_allowedGuesses == _failedGuesses)
            return GameStatus.GameOver;
        
        if (_wordProgress.All(c => c != null))
            return GameStatus.Victory;

        return GameStatus.KeepPlaying;
    }
}

public class GuessResult
{
    public bool Victory { get; set; }
    public char?[] WordProgress { get; set; }
    public int RemainingGuesses { get; set; }
    public GameStatus Status { get; set; }
}