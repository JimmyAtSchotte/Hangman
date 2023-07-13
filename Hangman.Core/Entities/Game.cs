using Hangman.Core.Types;

namespace Hangman.Core.Entities;

public class Game
{
    private List<char> _guesses;
    public Guid Guid { get; init; }
    public Word CorrectWord { get; init; }
    public GameStatus GameStatus { get; private set; }
    public int RemainingGuesses { get; private set; }

    public List<char> Guesses
    {
        get => _guesses ??= new List<char>();
        set => _guesses = value;
    }

    public Game()
    {
        RemainingGuesses = Constants.AllowedGuesses;
        GameStatus = GameStatus.KeepPlaying;
    }

    public void Guess(char guess)
    {
        guess = char.ToUpperInvariant(guess);
        
        if(Guesses.All(c => c != guess))
            Guesses.Add(guess);

        RemainingGuesses = Constants.AllowedGuesses - Guesses.Count(c => !CorrectWord.ContainsChar(c));
        GameStatus = GetCurrentGameStatus();
    }

    public char?[] GetWordProgress()
    {
        if (RemainingGuesses <= 0)
            return Array.ConvertAll(CorrectWord.ToCharArray(), c => (char?)c);
        
        var wordProgress = new char?[CorrectWord.Length];
  
        foreach (var guess in Guesses)
        {
            var correctIndexes = CorrectWord.FindIndexes(guess);

            foreach (var correctIndex in correctIndexes)
                wordProgress[correctIndex] = guess;
        }
        
        return wordProgress;
    }
    
    private GameStatus GetCurrentGameStatus()
    {
        if (RemainingGuesses <= 0)
            return GameStatus.GameOver;

        if (GetWordProgress().All(c => c != null))
            return GameStatus.Victory;

        return GameStatus.KeepPlaying;
    }
    
   
}