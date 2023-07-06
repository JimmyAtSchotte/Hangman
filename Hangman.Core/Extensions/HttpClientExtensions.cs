using System.Text;
using System.Text.Json;

namespace Hangman.Core.Extensions;

public static class HttpClientExtensions
{
    public static async Task<T?> PostWithResponse<T>(this HttpClient client, string url, object command)
    {
        var response = await client.PostAsync(url, new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
        var responseText = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(responseText, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
    }
}