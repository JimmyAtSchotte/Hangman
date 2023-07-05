namespace Tests;

public class GuessResult
{
    public bool Victory { get; set; }
    public char?[] WordProgress { get; set; }
    public int RemainingGuesses { get; set; }
    public GameStatus Status { get; set; }
}