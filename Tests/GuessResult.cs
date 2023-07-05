namespace Tests;

public class GuessResult
{
    public bool Victory { get; set; }
    public char?[] WordProgress { get; set; }
    public int RemainingGuesses { get; set; }
    public GameStatus Status { get; set; }
    public IEnumerable<Guess> PreviousGuesses { get; set; }
}

public class Guess
{
    public char Character { get; set; }
    public bool WordContainsCharacter { get; set; }
}