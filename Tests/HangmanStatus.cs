namespace Tests;

public class HangmanStatus
{
    public bool Victory { get; set; }
    public char?[] WordProgress { get; set; }
    public int RemainingGuesses { get; set; }
    public GameStatus Status { get; set; }
    public IEnumerable<GuessResult> PreviousGuesses { get; set; }
}