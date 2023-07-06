using Hangman.Core;
using Hangman.Core.Infrastructure;
using Hangman.Core.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Hangman.WebAPI;


[Route("/create-game")]
public class CreateGame : Ardalis.ApiEndpoints.EndpointBaseAsync.WithoutRequest.WithResult<HangmanResponse>
{
    private readonly GameRepository _gameRepository;

    public CreateGame(GameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    [HttpPost]
    public override async Task<HangmanResponse> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var game = await _gameRepository.AddAsync(new Game()
        {
            Guid = Guid.NewGuid(),
            CorrectWord = "Hello World"
        });

        return new HangmanResponse()
        {
            GameId = game.Guid,
            WordProgress = Enumerable.Repeat<char?>(default, game.CorrectWord.Length).ToArray(),
            RemainingGuesses = HangmanEngine.AllowedGuesses,
            Status = GameStatus.KeepPlaying,
            PreviousGuesses = Enumerable.Empty<GuessResult>()
        };
    }
}