using Hangman.Core.Entities;

namespace Hangman.Core.Infrastructure;

public class GameRepository
{
    private static List<Game> _games = new List<Game>();

    public Task<Game> AddAsync(Game game)
    {
        _games.Add(game);
        
        return Task.FromResult(game);
    }

    public Task<Game?> FindAsync(Guid gameId, CancellationToken cancellationToken)
    {
        return Task.FromResult(_games.FirstOrDefault(x => x.Guid == gameId));
    }

    public Task SaveAsync(Game game, CancellationToken cancellationToken)
    {
        var existingGame = _games.FirstOrDefault(x => x.Guid == game.Guid);
        
        if(existingGame is null)
            return Task.CompletedTask;

        _games.Remove(existingGame);
        _games.Add(game);

        return Task.CompletedTask;
    }
}