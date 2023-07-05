namespace Tests;

public class HangmanEngine
{
    private readonly int _allowedGuesses;
    private readonly char[] _correctWord;
    private readonly char?[] _wordProgress;

    private int _failedGuesses;
    private List<char> _previousGuesses;

    public HangmanEngine(string correctWord, int allowedGuesses)
    {
        _allowedGuesses = allowedGuesses;
        _correctWord = correctWord.ToCharArray();
        _wordProgress = new char?[_correctWord.Length];
        _failedGuesses = 0;
        _previousGuesses = new List<char>();
    }

    public GuessResult Guess(char guess)
    {
        if(_previousGuesses.Any(c => c == guess))
            return new GuessResult()
            {
                Victory = _wordProgress.All(c => c != null),
                WordProgress = _wordProgress,
                RemainingGuesses = _allowedGuesses - _failedGuesses,
                Status = GetCurrentGameStatus()
            };
        
        _previousGuesses.Add(guess);
        
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