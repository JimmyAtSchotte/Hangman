using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class APITests
{
    [Test]
    public async Task CreateGame()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
 
        var response = await client.PostAsync("/create-game", new StringContent("{}", Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
    }
}