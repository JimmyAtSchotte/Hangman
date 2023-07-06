using Hangman.Core.Infrastructure;
using Hangman.Core.Entities;


namespace Hangman.WebAPI;

public class CreateGame : Ardalis.ApiEndpoints.EndpointBaseAsync.WithoutRequest.WithResult<HangmanResponse>
{
    private readonly GameRepository _gameRepository;

    public CreateGame(GameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public override async Task<HangmanResponse> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var game = await _gameRepository.AddAsync(new Game()
        {
            CorrectWord = "Hello World"
        });

        return new HangmanResponse()
        {
            Guid = game.Guid
        };
    }
}