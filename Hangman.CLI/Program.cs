// See https://aka.ms/new-console-template for more information

using Hangman.Core;
using Hangman.Core.Extensions;
using Hangman.WebAPI;


using var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https:\\\\localhost:7019");

var hangmanGame = await httpClient.PostWithResponse<HangmanResponse>("/create-game", new object());

Console.WriteLine(string.Join(" ", hangmanGame.WordProgress.Select(c => c ?? '_')));

while (hangmanGame.Status == GameStatus.KeepPlaying)
{
    Console.WriteLine($"Make a guess (failed guesses remaining: {hangmanGame.RemainingGuesses})");
    var guess = Console.ReadLine().FirstOrDefault();
    
    hangmanGame = await httpClient.PostWithResponse<HangmanResponse>("/guess", new GuessCommand()
    {
        GameId = hangmanGame.GameId,
        Character = guess
    });

    switch (hangmanGame.Status)
    {
        case GameStatus.KeepPlaying:
            Console.WriteLine(string.Join(" ", hangmanGame.WordProgress.Select(c => c ?? '_')));
            Console.WriteLine(string.Join(", ", hangmanGame.PreviousGuesses.Select(x => x.Character)));
            break;
        case GameStatus.Victory:
            Console.WriteLine("You won!");
            Console.WriteLine("The secret word was");
            Console.WriteLine(string.Join(" ", hangmanGame.WordProgress.Select(c => c ?? '_')));
            break;
        case GameStatus.GameOver:
            Console.WriteLine("You lost!");
            Console.WriteLine("Game over!");
            Console.WriteLine("The secret word was");
            Console.WriteLine(string.Join(" ", hangmanGame.WordProgress.Select(c => c ?? '_')));
            break;
        default:
            throw new ArgumentOutOfRangeException();
    }
}

Console.ReadLine();
