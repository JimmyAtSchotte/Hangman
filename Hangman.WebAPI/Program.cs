using Hangman.Core.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<GameRepository>();

builder.Services.AddHttpClient("api-ninjas", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.api-ninjas.com/");
    httpClient.DefaultRequestHeaders.Add("X-Api-Key", "lW3ouVXvR91ChLmxJfhFWg==jYGqgPjNohKSvfWq");
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace Hangman.WebAPI
{
    public partial class Program { }
}