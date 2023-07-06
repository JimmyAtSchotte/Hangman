namespace Hangman.Core.Entities;

public class Game
{
    private List<Guess> _previousGuesses;
    public Guid Guid { get; set; }
    public string CorrectWord { get; set; }

    public List<Guess> Guesses
    {
        get => _previousGuesses ??= new List<Guess>();
        set => _previousGuesses = value;
    }

    public void AddGuess(char guess)
    {
        guess = char.ToUpperInvariant(guess);
        
        if(Guesses.Any(c => c.Character == guess))
            return;

        var correctWord = CorrectWord.Select(char.ToUpperInvariant).ToArray();
        
        var isCorrectGuess = false;
        
        for (var i = 0; i < correctWord.Length; i++)
        {
            if (!correctWord[i].Equals(guess)) 
                continue;
            
            isCorrectGuess = true;
        }

        Guesses.Add(new Guess()
        {
            Character = guess,
            WordContainsCharacter = isCorrectGuess
        });
    }

    public char?[] GetWordProgress()
    {
        var wordProgress = new char?[CorrectWord.Length];
        var correctWord = CorrectWord.Select(char.ToUpperInvariant).ToArray();
        
        foreach (var guess in Guesses.Where(x => x.WordContainsCharacter).Select(x => x.Character))
        {
            for (var i = 0; i < CorrectWord.Length; i++)
            {
                if (!correctWord[i].Equals(guess)) 
                    continue;
            
                wordProgress[i] = guess;
            }
        }

        return wordProgress;
    }
    
    public GameStatus GetCurrentGameStatus()
    {
        if (GetRemainingGuesses() == 0)
            return GameStatus.GameOver;

        if (GetWordProgress().All(c => c != null))
            return GameStatus.Victory;

        return GameStatus.KeepPlaying;
    }
    
    public int GetRemainingGuesses()
    {
        return Constants.AllowedGuesses - Guesses.Count(x => x.WordContainsCharacter == false);
    }
}