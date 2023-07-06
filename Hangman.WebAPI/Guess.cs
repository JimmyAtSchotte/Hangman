using Hangman.Core;
using Hangman.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;


namespace Hangman.WebAPI;


[Route("/guess")]
public class Guess : Ardalis.ApiEndpoints.EndpointBaseAsync.WithRequest<GuessCommand>.WithResult<ActionResult<HangmanResponse>>
{
    private readonly GameRepository _gameRepository;

    public Guess(GameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    [HttpPost]
    public override async Task<ActionResult<HangmanResponse>> HandleAsync(GuessCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var game = await _gameRepository.FindAsync(request.GameId, cancellationToken);

        if (game == null)
            return NotFound();

        var gameEngine = new HangmanEngine(game.CorrectWord, 8);
        var guessResult = gameEngine.Guess(request.Character);

        game.PreviousGuesses = guessResult.PreviousGuesses.ToList();
        
        await _gameRepository.SaveAsync(game, cancellationToken);

        return Ok(new HangmanResponse()
        {
            Guid = game.Guid
        });
    }
}

public class GuessCommand
{
    public Guid GameId { get; set; }
    public char Character { get; set; }
}