namespace Tests;

public class HangmanEngine
{
    private readonly int _allowedGuesses;
    private readonly char[] _correctWord;
    private readonly char?[] _wordProgress;
    private readonly List<GuessResult> _previousGuesses;

    private int _failedGuesses;

    public HangmanEngine(string correctWord, int allowedGuesses)
    {
        _allowedGuesses = allowedGuesses;
        _correctWord = correctWord.Select(char.ToUpperInvariant).ToArray();
        _wordProgress = new char?[_correctWord.Length];
        _failedGuesses = 0;
        _previousGuesses = new List<GuessResult>();
    }

    public HangmanStatus Guess(char guessingChar)
    {
        var guess = char.ToUpperInvariant(guessingChar);
        
        if(_previousGuesses.Any(c => c.Character == guess))
            return new HangmanStatus()
            {
                Victory = _wordProgress.All(c => c != null),
                WordProgress = _wordProgress,
                RemainingGuesses = _allowedGuesses - _failedGuesses,
                Status = GetCurrentGameStatus()
            };
        
        var isCorrectGuess = false;
        
        for (var i = 0; i < _correctWord.Length; i++)
        {
            if (!_correctWord[i].Equals(char.ToUpperInvariant(guess))) 
                continue;
            
            isCorrectGuess = true;
            _wordProgress[i] = guess;
        }
        
        if(!isCorrectGuess)
            _failedGuesses++;
        
        
        _previousGuesses.Add(new GuessResult()
        {
            Character = guess,
            WordContainsCharacter = isCorrectGuess
        });

        return new HangmanStatus()
        {
            Victory = _wordProgress.All(c => c != null),
            WordProgress = _wordProgress,
            RemainingGuesses = _allowedGuesses - _failedGuesses,
            Status = GetCurrentGameStatus(),
            PreviousGuesses = _previousGuesses
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