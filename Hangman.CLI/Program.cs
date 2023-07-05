// See https://aka.ms/new-console-template for more information

using Hangman.Core;

var guessesRemaining = 8;
var hangmanEngine = new HangmanEngine("Hello World", guessesRemaining);
var gameStatus = GameStatus.KeepPlaying;

while (gameStatus == GameStatus.KeepPlaying)
{
    Console.WriteLine($"Make a guess (failed guesses remaining: {guessesRemaining})");
    var guess = Console.ReadLine().FirstOrDefault();

    var hangmanStatus = hangmanEngine.Guess(guess);
    gameStatus = hangmanStatus.Status;
    guessesRemaining = hangmanStatus.RemainingGuesses;
    
    switch (hangmanStatus.Status)
    {
        case GameStatus.KeepPlaying:
            Console.WriteLine(string.Join(" ", hangmanStatus.WordProgress.Select(c => c ?? '_')));
            Console.WriteLine(string.Join(", ", hangmanStatus.PreviousGuesses.Select(x => x.Character)));
            break;
        case GameStatus.Victory:
            Console.WriteLine("You won!");
            break;
        case GameStatus.GameOver:
            Console.WriteLine("You lost!");
            Console.WriteLine("Game over!");
            break;
        default:
            throw new ArgumentOutOfRangeException();
    }
}

Console.ReadLine();
