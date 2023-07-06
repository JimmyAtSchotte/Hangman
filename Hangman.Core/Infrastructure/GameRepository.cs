using Hangman.Core.Entities;

namespace Hangman.Core.Infrastructure;

public class GameRepository
{
    public Task<Game> AddAsync(Game game)
    {
        return Task.FromResult(game);
    }
}