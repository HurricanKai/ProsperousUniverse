using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace ProsperousUniverse.API;

public sealed class AuthService
{
    private readonly ILogger _logger;
    private readonly IOptions<AuthConfig> _options;
    private readonly HttpClient _client;

    public AuthService(ILogger<AuthService> logger, IOptions<AuthConfig> options, HttpClient client)
    {
        _logger = logger;
        _options = options;
        _client = client;
    }
    
    public async Task<AuthSession> Authenticate()
    {
        var username = _options.Value.Username;
        var password = _options.Value.Password;
        _logger.LogTrace("Beginning Auth");
        
        using var responseMessage = await _client.PostAsJsonAsync("https://sar.simulogics.games/api/sessions", new SessionRequest(
            username, password, true, "password", "pu", null));
        responseMessage.EnsureSuccessStatusCode();
        var session = await responseMessage.Content.ReadFromJsonAsync<AuthSession>() ?? throw new InvalidOperationException("Could not Authenticate");
        _logger.LogInformation("Obtained Auth Session. Logged in as {Name}", session.Account.DisplayName);
        return session;
    }

    private sealed record SessionRequest(
        [property: JsonPropertyName("login")] string Login,
        [property: JsonPropertyName("password")] string Password,
        [property: JsonPropertyName("persistent")] bool Persistent,
        [property: JsonPropertyName("method")] string Method,
        [property: JsonPropertyName("brand")] string Brand,
        [property: JsonPropertyName("metadata")] object? Metadata);
}