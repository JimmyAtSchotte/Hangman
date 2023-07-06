using System.Net;
using System.Text;
using System.Text.Json;
using Hangman.WebAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Tests;

[TestFixture]
public class APITests : IDisposable
{
    private HttpClient _client;
    private WebApplicationFactory<Program> _application;

    [SetUp]
    public void Setup()
    {
        _application = new WebApplicationFactory<Program>();
        _client = _application.CreateClient();
    }
    
    [Test]
    public async Task CreateGame()
    {
        var response = await _client.PostAsync("/create-game", new StringContent("{}", Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
    }

    [Test]
    public async Task GuessToGame()
    {
        var gameResponse = await PostWithResponse<HangmanResponse>("/create-game", "{}");
        
        var command = new GuessCommand()
        {
            GameId = gameResponse.Guid,
            Character = 'u'
        };
        
        var guessResponse = await _client.PostAsync("/guess", new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json"));
        var responseText = await guessResponse.Content.ReadAsStringAsync();
        guessResponse.EnsureSuccessStatusCode();
    }
    
    [Test]
    public async Task InvalidGameGuess()
    {
        var command = new GuessCommand()
        {
            GameId = Guid.NewGuid(),
            Character = 'u'
        };
        
        var guessResponse = await _client.PostAsync("/guess", new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json"));
        
        Assert.AreEqual(HttpStatusCode.NotFound, guessResponse.StatusCode);
    }
    

    private async Task<T?> PostWithResponse<T>(string path, string json)
    {
        var response = await _client.PostAsync(path, new StringContent(json, Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
        var responseText = await response.Content.ReadAsStringAsync();

       return JsonSerializer.Deserialize<T>(responseText, new JsonSerializerOptions()
       {
           PropertyNameCaseInsensitive = true
       });
    }

    public void Dispose()
    {
        _client.Dispose();
        _application.Dispose();
    }
}