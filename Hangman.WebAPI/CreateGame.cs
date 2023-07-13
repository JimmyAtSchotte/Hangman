using System.Text.Json;
using Hangman.Core;
using Hangman.Core.Infrastructure;
using Hangman.Core.Entities;
using Hangman.Core.Types;
using Microsoft.AspNetCore.Mvc;


namespace Hangman.WebAPI;


[Route("/create-game")]
public class CreateGame : Ardalis.ApiEndpoints.EndpointBaseAsync.WithoutRequest.WithResult<HangmanResponse>
{
    private readonly GameRepository _gameRepository;
    private readonly IHttpClientFactory _httpClientFactory;

    public CreateGame(GameRepository gameRepository, IHttpClientFactory httpClientFactory)
    {
        _gameRepository = gameRepository;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public override async Task<HangmanResponse> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var correctWord = await GetRandomWord();
        
        var game = await _gameRepository.AddAsync(new Game()
        {
            Guid = Guid.NewGuid(),
            CorrectWord = new Word(correctWord)
        });

        return HangmanResponse.Create(game);
    }

    private async Task<string> GetRandomWord()
    {
        using var httpClient = _httpClientFactory.CreateClient("api-ninjas");
        var response = await httpClient.GetAsync("v1/randomword");

        response.EnsureSuccessStatusCode();
        
        var responseText = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<WordResponse>(responseText, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        })?.Word;
    }
    
    public class WordResponse
    { 
        public string Word { get; set; }
    }
}

