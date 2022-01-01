using System.Text.Json.Serialization;

namespace ProsperousUniverse.API;

public sealed record AuthSession(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("token")] string Token,
    [property: JsonPropertyName("created")] DateTime CreatedAt,
    [property: JsonPropertyName("expiry")] DateTime? ExpiresAt,
    [property: JsonPropertyName("lastActivity")] DateTime? LastActivityAt,
    [property: JsonPropertyName("termination")] DateTime? TerminatesAt,
    [property: JsonPropertyName("account")] AuthSessionAccount Account,
    [property: JsonPropertyName("userIds")] Dictionary<string, string> UserIds);